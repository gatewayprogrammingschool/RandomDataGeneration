using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using GPS.RandomDataGenerator.Abstractions;
using GPS.RandomDataGenerator.Generators;
using Microsoft.Extensions.DependencyInjection;

namespace GPS.RandomDataGenerator.RecordGenerators
{
    public class SimpleRecordGenerator<TRecord, TRecordMap> : IDataGenerator<TRecord>
        where TRecord : class, new()
        where TRecordMap : GeneratorMap
    {
        public SimpleRecordGenerator(IServiceProvider provider)
        {
            Provider = provider;
        }

        private IServiceProvider Provider { get; }

        public IEnumerable<TRecord> Generate(int? seed, int count, params object[] options)
        {
            seed ??= DateTime.Now.Millisecond;
            var map  = Activator.CreateInstance<TRecordMap>();
            var data = new Dictionary<PropertyInfo, object[]>();
            map.Map.ToList().ForEach(pair => data.TryAdd(pair.Key,
                                         GetData(pair.Value, seed.Value, count).Cast<object>().ToArray()));

            for (var i = 0; i < count; ++i)
            {
                var result = Activator.CreateInstance<TRecord>();
                foreach (var propertyInfo in data.Keys)
                {
                    var currentData = data[propertyInfo][i];

                    try
                    {
                        if (currentData is string || !(currentData is IEnumerable children))
                        {
                            propertyInfo.SetValue(result, currentData);
                        }
                        else
                        {
                            var myList = (IList) Activator.CreateInstance(propertyInfo.PropertyType);

                            foreach (var child in children)
                            {
                                var pi = child.GetType().GetProperty("ParentUID");
                                if (pi != null)
                                    pi.SetValue(child, typeof(TRecord).GetProperty("UID")?.GetValue(result));

                                myList.Add(child);
                            }

                            propertyInfo.SetValue(result, myList);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }

                yield return result;
            }
        }

        private Array GetData(
            (GeneratableTypes generatableType, int seed, Type childGeneratorType, int childCount, object[] options)
                type, int baseSeed, int count)
        {
            var (generatableType, seed, childGeneratorType, childCount, options) = type;
            seed += baseSeed;
            var result = generatableType switch
            {
                GeneratableTypes.Date => Provider.GetService<DateGenerator>().Generate(seed, count, options).ToArray(),
                GeneratableTypes.Decimal => Provider.GetService<DecimalGenerator>().Generate(seed, count, options)
                                                    .ToArray(),
                GeneratableTypes.Double => Provider.GetService<DoubleGenerator>().Generate(seed, count, options)
                                                   .ToArray(),
                GeneratableTypes.Email => Provider.GetService<EmailGenerator>().Generate(seed, count, options)
                                                  .ToArray(),
                GeneratableTypes.Guid => Provider.GetService<GuidGenerator>().Generate(seed, count, options).ToArray(),
                GeneratableTypes.Integer => Provider.GetService<IntegerGenerator>().Generate(seed, count, options)
                                                    .ToArray(),
                GeneratableTypes.Name => Provider.GetService<NameGenerator>().Generate(seed, count, options).ToArray(),
                GeneratableTypes.Surname => Provider.GetService<SurNameGenerator>().Generate(seed, count, options)
                                                    .ToArray(),
                GeneratableTypes.GivenName => Provider.GetService<GivenNameGenerator>().Generate(seed, count, options)
                                                      .ToArray(),
                GeneratableTypes.Sequence => Provider.GetService<SequenceGenerator>().Generate(seed, count, options)
                                                     .ToArray(),
                GeneratableTypes.SimpleChildRecord => GetChildren(),
                _ => new object[0]
            };

            return result;

            Array GetChildren()
            {
                var generator = Activator.CreateInstance(childGeneratorType, Provider);

                if (generator == null) return new object[0];

                var mi = childGeneratorType.GetMethod(nameof(Generate));

                if (mi == null) return new object[0];

                var del = CreateDelegate(mi, generator);

                var children = new ArrayList();

                for (var i = 0; i < count; ++i)
                {
                    var child = del.DynamicInvoke(seed, childCount, options);

                    children.Add(child);
                }

                var r = children.ToArray(mi.ReturnType);

                return r;
            }
        }

        private static Delegate CreateDelegate(MethodInfo methodInfo, object target)
        {
            Func<Type[], Type> getType;
            var                isAction = methodInfo.ReturnType == typeof(void);
            var                types    = methodInfo.GetParameters().Select(p => p.ParameterType);

            if (isAction)
            {
                getType = Expression.GetActionType;
            }
            else
            {
                getType = Expression.GetFuncType;
                types = types.Concat(new[] {methodInfo.ReturnType});
            }

            return methodInfo.IsStatic 
                ? Delegate.CreateDelegate(getType(types.ToArray()), methodInfo) 
                : Delegate.CreateDelegate(getType(types.ToArray()), target, methodInfo.Name);
        }
    }
}