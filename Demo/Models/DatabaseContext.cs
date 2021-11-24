using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Demo.Models
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Agent> Agents { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Claim> Claims { get; set; }
        public virtual DbSet<Credential> Credentials { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerPolicy> CustomerPolicies { get; set; }
        public virtual DbSet<InsuranceType> InsuranceTypes { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Policy> Policies { get; set; }
        public virtual DbSet<PremiumTransaction> PremiumTransactions { get; set; }
        public virtual DbSet<PremiumType> PremiumTypes { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=localhost;Database=ONLINEINSURANCE;Trusted_Connection=False;User ID=sa;Password=Password789");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Agent>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Agent_pk")
                    .IsClustered(false);

                entity.ToTable("Agent");

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.Gender).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Agents)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("Agent_Branch_Id_fk");
            });

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Branch_pk")
                    .IsClustered(false);

                entity.ToTable("Branch");

                entity.Property(e => e.City).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.State).IsUnicode(false);

                entity.Property(e => e.Street).IsUnicode(false);

                entity.Property(e => e.ZipCode)
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Claim>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Claim_pk")
                    .IsClustered(false);

                entity.ToTable("Claim");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.ClaimedDate).HasColumnType("date");

                entity.HasOne(d => d.CustomerPolicy)
                    .WithMany(p => p.Claims)
                    .HasForeignKey(d => d.CustomerPolicyId)
                    .HasConstraintName("Claim_CustomerPolicy_Id_fk");
            });

            modelBuilder.Entity<Credential>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Credential_pk")
                    .IsClustered(false);

                entity.ToTable("Credential");

                entity.HasIndex(e => e.Email, "Credential_Email_uindex")
                    .IsUnique();

                entity.Property(e => e.ActivationCode).IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(125)
                    .IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Credentials)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK_Credential_Branch");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Credentials)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("Credential_Role_Id_fk");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("User_pk")
                    .IsClustered(false);

                entity.ToTable("Customer");

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.CitizenId)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.City).IsUnicode(false);

                entity.Property(e => e.Gender).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.Occupation).IsUnicode(false);

                entity.Property(e => e.State).IsUnicode(false);

                entity.Property(e => e.Street).IsUnicode(false);

                entity.Property(e => e.ZipCode).IsUnicode(false);

                entity.HasOne(d => d.Credential)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.CredentialId)
                    .HasConstraintName("Customer_Credential_Id_fk");
            });

            modelBuilder.Entity<CustomerPolicy>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("CustomerPolicy_pk")
                    .IsClustered(false);

                entity.ToTable("CustomerPolicy");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.PremiumAmount).HasColumnType("money");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.CustomerPolicies)
                    .HasForeignKey(d => d.AgentId)
                    .HasConstraintName("CustomerPolicy_Agent_Id_fk");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerPolicies)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("CustomerPolicy_Customer_Id_fk");

                entity.HasOne(d => d.Policy)
                    .WithMany(p => p.CustomerPolicies)
                    .HasForeignKey(d => d.PolicyId)
                    .HasConstraintName("CustomerPolicy_Policy_Id_fk");

                entity.HasOne(d => d.PremiumType)
                    .WithMany(p => p.CustomerPolicies)
                    .HasForeignKey(d => d.PremiumTypeId)
                    .HasConstraintName("CustomerPolicy_PremiumType_Id_fk");
            });

            modelBuilder.Entity<InsuranceType>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("InsuranceType_pk")
                    .IsClustered(false);

                entity.ToTable("InsuranceType");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Message_pk")
                    .IsClustered(false);

                entity.ToTable("Message");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Message1)
                    .HasColumnType("text")
                    .HasColumnName("Message");
            });

            modelBuilder.Entity<Policy>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("InsurancePlan_pk")
                    .IsClustered(false);

                entity.ToTable("Policy");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Name).IsUnicode(false);

                entity.HasOne(d => d.InsuranceType)
                    .WithMany(p => p.Policies)
                    .HasForeignKey(d => d.InsuranceTypeId)
                    .HasConstraintName("InsurancePlan_InsuranceType_Id_fk");
            });

            modelBuilder.Entity<PremiumTransaction>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Payment_pk")
                    .IsClustered(false);

                entity.ToTable("PremiumTransaction");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.DueDate).HasColumnType("date");

                entity.Property(e => e.PaidDate).HasColumnType("date");

                entity.HasOne(d => d.CustomerPolicy)
                    .WithMany(p => p.PremiumTransactions)
                    .HasForeignKey(d => d.CustomerPolicyId)
                    .HasConstraintName("PremiumTransaction_CustomerPolicy_Id_fk");
            });

            modelBuilder.Entity<PremiumType>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PremiumType_pk")
                    .IsClustered(false);

                entity.ToTable("PremiumType");

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Role_pk")
                    .IsClustered(false);

                entity.ToTable("Role");

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Subscription_pk")
                    .IsClustered(false);

                entity.ToTable("Subscription");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
