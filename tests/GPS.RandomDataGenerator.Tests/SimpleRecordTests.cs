using System;
using System.Data.SqlClient;
using System.Linq;
using GPS.RandomDataGenerator.RecordGenerators;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;
// ReSharper disable StringLiteralTypo

namespace GPS.RandomDataGenerator.Tests
{
    public class SimpleRecordTests
    {
        private bool WriteData => false;
        private readonly ITestOutputHelper _testOutputHelper;

        public SimpleRecordTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            var service = new ServiceCollection();
            service.AddGenerators();
            Provider = service.BuildServiceProvider(true);
        }

        private IServiceProvider Provider { get; }

        [Theory]
        [InlineData(0x0, 0x1)]
        [InlineData(1, 10)]
        [InlineData(0, 100)]
#if FULL_TESTS
        [InlineData(0, 10000)]
        [InlineData(0, 100000)]
#endif
        public void GenerateTestRecords(int seed, int count)
        {
            var testRecords = new SimpleRecordGenerator<TestRecord, TestRecordGeneratorMap>(Provider)
                              .Generate(seed, count)?
                              .ToList();

            Assert.NotNull(testRecords);
            Assert.NotEmpty(testRecords);
            Assert.DoesNotContain(testRecords, s => s is null);
            Assert.Equal(count, testRecords.Count);

#if DEBUG
            SqlCommand cmd = null;
            SqlTransaction trx = null;

            // Data Access Showdown/Data/TestData.mdf
            if (WriteData)
            {
                var conn = new SqlConnection(
                   "Server=.;AttachDbFilename=C:\\Users\\kingd\\source\\repos\\Data Access Showdown\\Data Access Showdown\\Data\\TestData.mdf;Database=TestData;Trusted_Connection=Yes;");

                conn.Open();

                trx = conn.BeginTransaction();

                cmd = new SqlCommand { Connection = conn, Transaction = trx };

                var first = testRecords.FirstOrDefault();

                if (first != null)
                {
                    cmd.CommandText = first.CreateTableStatement;
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = first.ChildRecords.FirstOrDefault()?.CreateTableStatement;
                    cmd.ExecuteNonQuery();
                }
            }

            _testOutputHelper.WriteLine($"Start Count: {count} ----------------------------");
            //testRecords.ForEach(testRecord => _testOutputHelper.WriteLine($"{testRecord.UID}\t{testRecord.Name}\t{testRecord.Age.Days/365}\t{testRecord.EmailAddress}\t{testRecord.ForeignKey}"));
            testRecords.ForEach(testRecord =>
                                {
                                    _testOutputHelper.WriteLine(testRecord.InsertStatement);
                                    if (WriteData)
                                    {
                                        cmd.CommandText = testRecord.InsertStatement;
                                        cmd.ExecuteNonQuery();
                                    }

                                    testRecord.ChildRecords.ForEach(cr =>
                                                                    {
                                                                        _testOutputHelper.WriteLine(
                                                                            $"\t\t{cr.InsertStatement}");

                                                                        if (WriteData)
                                                                        {
                                                                            cmd.CommandText = cr.InsertStatement;
                                                                            cmd.ExecuteNonQuery();
                                                                        }
                                                                    });
                                    _testOutputHelper.WriteLine($"\t{new string('-', 0x50)}");
                                });

            if (WriteData) trx.Commit();

            _testOutputHelper.WriteLine($"End Count: {count} ----------------------------");

#endif
        }
    }
}