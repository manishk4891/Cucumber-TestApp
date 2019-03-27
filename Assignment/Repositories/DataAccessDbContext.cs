namespace Assignment.Repositories
{
    using Assignment.Repositories.Configuration;
    using System.Data.Entity;

    public class DataAccessDbContext : DbContext
    {
        public DataAccessDbContext()
        {
            Database.Connection.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConnection"].ConnectionString;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new PersonDetailsConfiguration());
        }
    }
}