namespace MatheusVSMP.Infra.Data.Migrations
{
    using MatheusVSMP.Infra.Data.Context;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<SqlServerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

    }
}
