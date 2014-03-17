using System.Configuration;
using System.Data.Entity;
using BusConductor.Domain.Entities;

namespace BusConductor.Data.Core
{
    public class Context : DbContext
    {
        public Context() : base(ConfigurationManager.ConnectionStrings["BusConductor"].ConnectionString)
        {
            Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Bus> Busses { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionRole> PermissionRoles { get; set; }
        public DbSet<PricingPeriod> PricingPeriods { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskType> TaskTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Enquiry> Enquiries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>().ToTable("Booking");
            modelBuilder.Entity<Bus>().ToTable("Bus");
            modelBuilder.Entity<Permission>().ToTable("Permission");
            modelBuilder.Entity<PermissionRole>().ToTable("PermissionRole");
            modelBuilder.Entity<PricingPeriod>().ToTable("PricingPeriod");
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<Task>().ToTable("Task");
            modelBuilder.Entity<TaskType>().ToTable("TaskType");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Voucher>().ToTable("Voucher");
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Enquiry>().ToTable("Enquiry");
            
            modelBuilder.Entity<Bus>()
                .HasRequired<User>(bus => bus.CreatedBy);
            
            modelBuilder.Entity<Bus>()
                .HasMany<Booking>(bus => bus.Bookings)
                .WithRequired(booking => booking.Bus)
                .HasForeignKey(booking => booking.BusId);

            modelBuilder.Entity<Booking>()
                .HasRequired<User>(booking => booking.CreatedBy);

            modelBuilder.Entity<Booking>()
                .HasRequired<Customer>(booking => booking.Customer);

            modelBuilder.Entity<User>()
                .HasRequired<User>(user => user.CreatedBy)
                .WithMany()
                .HasForeignKey(user => user.CreatedById);

            modelBuilder.Entity<Role>()
                .HasRequired<User>(role => role.CreatedBy);

            modelBuilder.Entity<Bus>()
                .HasRequired<User>(bus => bus.CreatedBy);

            modelBuilder.Entity<Enquiry>()
                .HasOptional<Booking>(enquiry => enquiry.ResultingBooking);

            modelBuilder.Entity<Enquiry>()
                .HasRequired<User>(enquiry => enquiry.CreatedBy);

            base.OnModelCreating(modelBuilder);
        }
    }
}
