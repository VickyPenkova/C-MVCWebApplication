namespace WebApplication.Models
{
    using System.Data.Entity;

    public partial class EntityDbModel : DbContext
    {
        public EntityDbModel()
            : base("name=EntityDbModel")
        {

            this.Configuration.ProxyCreationEnabled = false;

        }

        public virtual DbSet<customer> customers { get; set; }
        public virtual DbSet<order> orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<customer>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .HasMany(e => e.orders)
                .WithRequired(e => e.customer)
                .HasForeignKey(e => e.customer_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<order>()
                .Property(e => e.name)
                .IsUnicode(false);
        }
    }
}
