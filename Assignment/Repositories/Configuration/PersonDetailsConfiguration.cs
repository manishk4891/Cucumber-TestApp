namespace Assignment.Repositories.Configuration
{
    using Assignment.Models;
    using System.Data.Entity.ModelConfiguration;

    public class PersonDetailsConfiguration : EntityTypeConfiguration<PersonDetails>
    {
        public PersonDetailsConfiguration()
        {
            ToTable("PersonDetails");
            HasKey(x => x.Id);
            Property(x => x.Id).IsRequired();
            Property(x => x.Name).HasMaxLength(50);
            Property(x => x.Number);
        }
    }
}