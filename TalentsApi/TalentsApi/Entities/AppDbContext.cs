using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading;
using TalentsApi.Entities.Attributes;
using TalentsApi.Entities.BaseEntities;
using TalentsApi.Entities.Events;

namespace TalentsApi.Entities
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        private List<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry> modifiedEntities;
        private List<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry> insertedEntities;
        private List<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry> deletedEntities;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Custom attributes
            DefaultValue.OnModelCreating(modelBuilder);
        }

        protected void initOnCreatedModified()
        {
            var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity && (
                e.State == EntityState.Added
                || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).CreatedDateTime = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).UpdateDateTime = DateTime.Now;
                }
            }
        }

        protected void initSluggable()
        {
            var entriesSluggable = ChangeTracker.Entries().Where(e => e.Entity is SluggableEntity
                                                                      && (e.State == EntityState.Added ||
                                                                          e.State == EntityState.Modified));
            foreach (var entityEntry in entriesSluggable)
            {
                SluggableEntity sluggableEntity = (SluggableEntity)entityEntry.Entity;
                sluggableEntity.Slug = sluggableEntity.GenerateSlug();
            }
        }
        protected void fetchChangesEvents()
        {
            modifiedEntities = ChangeTracker.Entries()
            .Where(p => p.State == EntityState.Modified && typeof(IOnUpdate).IsAssignableFrom(p.Entity.GetType())).ToList();

            insertedEntities = ChangeTracker.Entries()
            .Where(p => p.State == EntityState.Added && typeof(IOnInsert).IsAssignableFrom(p.Entity.GetType())).ToList();

            deletedEntities = ChangeTracker.Entries()
            .Where(p => p.State == EntityState.Deleted && typeof(IOnDelete).IsAssignableFrom(p.Entity.GetType())).ToList();
        }
        protected void initEvents(bool before)
        {
            foreach (var change in modifiedEntities)
            {
                IOnUpdate onUpdateEntity = (IOnUpdate)change.Entity;

                if(before)
                {
                    onUpdateEntity.BeforeUpdate(change.Entity);
                }
                else
                {
                    onUpdateEntity.AfterUpdate(change.Entity);
                }
            }

            foreach (var change in insertedEntities)
            {
                IOnInsert onUpdateEntity = (IOnInsert)change.Entity;

                if (before)
                {
                    onUpdateEntity.BeforeInsert(change.Entity);
                }
                else
                {
                    onUpdateEntity.AfterInsert(change.Entity);
                }
            }

            foreach (var change in deletedEntities)
            {
                IOnDelete onUpdateEntity = (IOnDelete)change.Entity;


                if (before)
                {
                    onUpdateEntity.BeforeDelete(change.Entity);
                }
                else
                {
                    onUpdateEntity.AfterDelete(change.Entity);
                }
            }
        }

        public sealed override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return this.SaveChangesAsync(acceptAllChangesOnSuccess: true, cancellationToken);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            this.initOnCreatedModified();
            this.initSluggable();
            this.fetchChangesEvents();
            this.initEvents(true);
            int saveChangesResult = await base.SaveChangesAsync(true, cancellationToken).ConfigureAwait(false);
            this.initEvents(false);

            return saveChangesResult;
        }
        public override int SaveChanges()
        {
            this.initOnCreatedModified();
            this.initSluggable();
            this.fetchChangesEvents();
            this.initEvents(true);
            int saveChangesResult =  base.SaveChanges();
            this.initEvents(false);

            return saveChangesResult;
        }

    }
}
