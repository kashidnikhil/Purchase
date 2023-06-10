﻿using Abp.IdentityServer4vNext;
using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyTraining1101Demo.Authorization.Delegation;
using MyTraining1101Demo.Authorization.Roles;
using MyTraining1101Demo.Authorization.Users;
using MyTraining1101Demo.Chat;
using MyTraining1101Demo.Customer;
using MyTraining1101Demo.Editions;
using MyTraining1101Demo.Friendships;
using MyTraining1101Demo.MultiTenancy;
using MyTraining1101Demo.MultiTenancy.Accounting;
using MyTraining1101Demo.MultiTenancy.Payments;
using MyTraining1101Demo.PhoneBook;
using MyTraining1101Demo.Purchase.AcceptanceCriterias;
using MyTraining1101Demo.Purchase.Assemblies;
using MyTraining1101Demo.Purchase.Companies.CompanyAddresses;
using MyTraining1101Demo.Purchase.Companies.CompanyContactPersons;
using MyTraining1101Demo.Purchase.Companies.CompanyMaster;
using MyTraining1101Demo.Purchase.DeliveryTerms;
using MyTraining1101Demo.Purchase.Items.CalibrationAgenciesMaster;
using MyTraining1101Demo.Purchase.Items.CalibrationTypeMaster;
using MyTraining1101Demo.Purchase.Items.ItemAccesoriesMaster;
using MyTraining1101Demo.Purchase.Items.ItemAttachmentsMaster;
using MyTraining1101Demo.Purchase.Items.ItemMaster;
using MyTraining1101Demo.Purchase.Items.ItemRateRevisionMaster;
using MyTraining1101Demo.Purchase.Items.ItemStorageConditionMaster;
using MyTraining1101Demo.Purchase.Items.ItemSupplierMaster;
using MyTraining1101Demo.Purchase.Items.ProcurementMaster;
using MyTraining1101Demo.Purchase.Items.RequiredItemSparesMaster;
using MyTraining1101Demo.Purchase.LegalEntities;
using MyTraining1101Demo.Purchase.MaterialGrades;
using MyTraining1101Demo.Purchase.Models;
using MyTraining1101Demo.Purchase.ModelWiseItems.ModelWiseItemMasters;
using MyTraining1101Demo.Purchase.ModelWiseItems.ModelWiseItems;
using MyTraining1101Demo.Purchase.POGeneralTerms;
using MyTraining1101Demo.Purchase.SubAssemblies;
using MyTraining1101Demo.Purchase.SubAssemblyItems;
using MyTraining1101Demo.Purchase.SupplierCategories;
using MyTraining1101Demo.Purchase.Suppliers.MappedSupplierCategories;
using MyTraining1101Demo.Purchase.Suppliers.SupplierAddresses;
using MyTraining1101Demo.Purchase.Suppliers.SupplierBanks;
using MyTraining1101Demo.Purchase.Suppliers.SupplierContactPersons;
using MyTraining1101Demo.Purchase.Suppliers.SupplierMaster;
using MyTraining1101Demo.Purchase.TermsOfPayments;
using MyTraining1101Demo.Purchase.Units;
using MyTraining1101Demo.Storage;

namespace MyTraining1101Demo.EntityFrameworkCore
{
    public class MyTraining1101DemoDbContext : AbpZeroDbContext<Tenant, Role, User, MyTraining1101DemoDbContext>, IAbpPersistedGrantDbContext
    {
        /* Define an IDbSet for each entity of the application */

        public virtual DbSet<Person> Persons { get; set; }

        public virtual DbSet<customer> customers { get; set; }

        public virtual DbSet<Phone> Phones { get; set; }
        public virtual DbSet<BinaryObject> BinaryObjects { get; set; }

        public virtual DbSet<Friendship> Friendships { get; set; }

        public virtual DbSet<ChatMessage> ChatMessages { get; set; }

        public virtual DbSet<SubscribableEdition> SubscribableEditions { get; set; }

        public virtual DbSet<SubscriptionPayment> SubscriptionPayments { get; set; }

        public virtual DbSet<Invoice> Invoices { get; set; }

        public virtual DbSet<PersistedGrantEntity> PersistedGrants { get; set; }

        public virtual DbSet<SubscriptionPaymentExtensionData> SubscriptionPaymentExtensionDatas { get; set; }

        public virtual DbSet<UserDelegation> UserDelegations { get; set; }

        public virtual DbSet<Unit> Units { get; set; }

        public virtual DbSet<LegalEntity> LegalEntities { get; set; }

        public virtual DbSet<AcceptanceCriteria> AcceptanceCriterias { get; set; }

        public virtual DbSet<DeliveryTerm> DeliveryTerms { get; set; }

        public virtual DbSet<MaterialGrade> MaterialGrades { get; set; }

        public virtual DbSet<POGeneralTerm> POGeneralTerms { get; set; }

        public virtual DbSet<SupplierCategory> SupplierCategories { get; set; }

        public virtual DbSet<TermsOfPayment> TermsOfPayments { get; set; }

        public virtual DbSet<Model> Models { get; set; }


        public virtual DbSet<Supplier> Suppliers { get; set; }

        public virtual DbSet<SupplierContactPerson> SupplierContactPersons { get; set; }

        public virtual DbSet<SupplierBank> SupplierBanks { get; set; }

        public virtual DbSet<SupplierAddress> SupplierAddresses { get; set; }

        public virtual DbSet<Company> Companies { get; set; }

        public virtual DbSet<CompanyAddress> CompanyAddresses { get; set; }

        public virtual DbSet<CompanyContactPerson> CompanyContactPersons { get; set; }

        public virtual DbSet<MappedSupplierCategory> MappedSupplierCategories { get; set; }

        public virtual DbSet<Item> Items { get; set; }

        public virtual DbSet<CalibrationType> CalibrationType { get; set; }

        public virtual DbSet<Procurement> Procurements { get; set; }

        public virtual DbSet<CalibrationAgency> CalibrationAgencies { get; set; }

        public virtual DbSet<ItemAttachment> ItemAttachments { get; set; }

        public virtual DbSet<ItemStorageCondition> ItemStorageConditions { get; set; }

        public virtual DbSet<ItemSpare> ItemSpares { get; set; }

        public virtual DbSet<ItemSupplier> ItemSuppliers { get; set; }

        public virtual DbSet<ItemRateRevision> ItemRateRevisions { get; set; }

        public virtual DbSet<ItemAccessory> ItemAccessories { get; set; }

        public virtual DbSet<Assembly> Assemblies { get; set; }

        public virtual DbSet<SubAssembly> SubAssemblies { get; set; }

        public virtual DbSet<SubAssemblyItem> SubAssemblyItems { get; set; }

        public virtual DbSet<ModelWiseItemMaster> ModelWiseItemMasters { get; set; }

        public virtual DbSet<ModelWiseItem> ModelWiseItem { get; set; }

        public MyTraining1101DemoDbContext(DbContextOptions<MyTraining1101DemoDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BinaryObject>(b =>
            {
                b.HasIndex(e => new { e.TenantId });
            });

            modelBuilder.Entity<ChatMessage>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId, e.ReadState });
                b.HasIndex(e => new { e.TenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.UserId, e.ReadState });
            });

            modelBuilder.Entity<Friendship>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId });
                b.HasIndex(e => new { e.TenantId, e.FriendUserId });
                b.HasIndex(e => new { e.FriendTenantId, e.UserId });
                b.HasIndex(e => new { e.FriendTenantId, e.FriendUserId });
            });

            modelBuilder.Entity<Tenant>(b =>
            {
                b.HasIndex(e => new { e.SubscriptionEndDateUtc });
                b.HasIndex(e => new { e.CreationTime });
            });

            modelBuilder.Entity<SubscriptionPayment>(b =>
            {
                b.HasIndex(e => new { e.Status, e.CreationTime });
                b.HasIndex(e => new { PaymentId = e.ExternalPaymentId, e.Gateway });
            });

            modelBuilder.Entity<SubscriptionPaymentExtensionData>(b =>
            {
                b.HasQueryFilter(m => !m.IsDeleted)
                    .HasIndex(e => new { e.SubscriptionPaymentId, e.Key, e.IsDeleted })
                    .IsUnique();
            });

            modelBuilder.Entity<UserDelegation>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.SourceUserId });
                b.HasIndex(e => new { e.TenantId, e.TargetUserId });
            });

            modelBuilder.ConfigurePersistedGrantEntity();
        }
    }
}
