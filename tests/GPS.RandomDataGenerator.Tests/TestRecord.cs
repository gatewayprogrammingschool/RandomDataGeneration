using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable SuggestBaseTypeForParameter
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

namespace GPS.RandomDataGenerator.Tests
{
    public class TestRecord
    {
        public Guid     UID          { get; set; }
        public string   Name         { get; set; }
        public DateTime DateOfBirth  { get; set; }
        public string   EmailAddress { get; set; }

        public List<TestChildRecord> ChildRecords { get; set; }

        public string InsertStatement =>
            $"INSERT INTO {GetType().Name} ({string.Join(',', Properties.Where(pi => pi.Name != nameof(ChildRecords)).Select(pi => pi.Name))}) VALUES ({string.Join(',', Properties.Where(pi => pi.Name != nameof(ChildRecords)).Select(pi => pi.GetValue(this) is null ? "NULL" : $"'{pi.GetValue(this)}'"))});";

        public string SelectStatement =>
            $"SELECT {string.Join(',', Properties.Where(pi => pi.Name != nameof(ChildRecords)).Select(pi => pi.Name))} FROM {GetType().Name}" +
            (UID != default ? $" WHERE UID = '{UID}';" : ";");

        public string DeleteStatement => $"DELETE FROM {GetType().Name} WHERE UID = '{UID}';";

        public string UpdateStatement =>
            $"UPDATE {GetType().Name} SET {string.Join(',', Properties.Where(pi => pi.Name != nameof(ChildRecords)).Where(pi => pi.Name != nameof(UID)).Select(pi => $"{pi.Name}='{(pi.GetValue(this) is null ? "NULL" : pi.GetValue(this))}'"))} WHERE UID='{UID}';";

        // ReSharper disable once ReturnTypeCanBeEnumerable.Local
        private List<PropertyInfo> Properties =>
            GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                     .Where(pi => !pi.Name.EndsWith("Statement", StringComparison.InvariantCultureIgnoreCase))
                     .ToList();

        public string CreateTableStatement =>
            $"CREATE TABLE {GetType().Name} ({string.Join(',', Properties.Where(pi => pi.Name != nameof(ChildRecords)).Select(pi => $"{pi.Name} {GetSqlType(pi.PropertyType)} NOT NULL"))});";

        private static string GetSqlType(Type type) =>
            type.Name.ToLowerInvariant() switch
            {
                "string" => "nvarchar(max)",
                "guid" => "uniqueidentifier",
                "int" => "int",
                "long" => "bigint",
                "double" => "float",
                "float" => "real",
                "decimal" => "money",
                "bool" => "bit",
                "boolean" => "bit",
                "byte" => "tinyint",
                "datetime" => "datetime2",
                "datetimeoffset" => "datetimeoffset",
                "timespan" => "time",
                _ => "nvarchar(max)"
            };
    }

    public class TestChildRecord
    {
        public Guid     UID       { get; set; }
        public Guid     ParentUID { get; set; }
        public DateTime PlacedOn  { get; set; }
        public int      Number    { get; set; }

        public string InsertStatement =>
            $"INSERT INTO {GetType().Name} ({string.Join(',', Properties.Select(pi => pi.Name))}) VALUES ({string.Join(',', Properties.Select(pi => $"'{pi.GetValue(this)}'"))});";

        public string SelectStatement =>
            $"SELECT {string.Join(',', Properties.Select(pi => pi.Name))} FROM {GetType().Name}" +
            (UID != default ? $" WHERE UID = '{UID}';" : ";");

        public string DeleteStatement => $"DELETE FROM {GetType().Name} WHERE UID = '{UID}';";

        public string UpdateStatement =>
            $"UPDATE {GetType().Name} SET {string.Join(',', Properties.Where(pi => pi.Name != nameof(UID)).Select(pi => $"{pi.Name}='{pi.GetValue(this)}'"))} WHERE UID='{UID}';";

        public string CreateTableStatement =>
            $"CREATE TABLE {GetType().Name} ({string.Join(',', Properties.Where(pi => pi.Name != nameof(TestRecord.ChildRecords)).Select(pi => $"{pi.Name} {GetSqlType(pi.PropertyType)} NOT NULL"))});";

        // ReSharper disable once ReturnTypeCanBeEnumerable.Local
        private List<PropertyInfo> Properties =>
            GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                     .Where(pi => !pi.Name.EndsWith("Statement", StringComparison.InvariantCultureIgnoreCase))
                     .ToList();

        private static string GetSqlType(Type type) =>
            type.Name.ToLowerInvariant() switch
            {
                "string" => "nvarchar(max)",
                "guid" => "uniqueidentifier",
                "int" => "int",
                "long" => "bigint",
                "double" => "float",
                "float" => "real",
                "decimal" => "money",
                "bool" => "bit",
                "boolean" => "bit",
                "byte" => "tinyint",
                "datetime" => "datetime2",
                "datatimeoffset" => "datetimeoffset",
                "timespan" => "time",
                _ => "nvarchar(max)"
            };
    }
}