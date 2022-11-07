﻿namespace Domain;

using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Conventions.Instances;
using NHibernate;

public static class SessionFactory
{
    private static ISessionFactory? _factory;

    public static void Init(string connectionString)
    {
        _factory = BuildSessionFactory(connectionString);
    }

    public static ISession OpenSession()
    {
        return _factory.OpenSession();
    }

    private static ISessionFactory BuildSessionFactory(string connectionString)
    {
        return Fluently.Configure()
            .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString))
            .Mappings(m => m.FluentMappings
                .AddFromAssembly(Assembly.GetExecutingAssembly())
                .Conventions.Add(
                    ForeignKey.EndsWith("Id"),
                    DefaultLazy.Never(),
                    ConventionBuilder.Property
                        .When(criteria => criteria.Expect(x => x.Nullable, Is.Not.Set),
                            x => x.Not.Nullable()))
                .Conventions.Add<TableNameConvention>()
                .Conventions.Add<HiLoConvention>())
            .BuildSessionFactory();
    }

    public class HiLoConvention : IIdConvention
    {
        public void Apply(IIdentityInstance instance)
        {
            instance.Column(instance.EntityType.Name + "Id");
            instance.GeneratedBy.HiLo("[dbo].[Ids]", "NextHigh", "9", "EntityName = '" + instance.EntityType.Name + "'");
        }
    }

    public class TableNameConvention : IClassConvention
    {
        public void Apply(IClassInstance instance)
        {
            instance.Table("[dbo].[" + instance.EntityType.Name + "]");
        }
    }
}