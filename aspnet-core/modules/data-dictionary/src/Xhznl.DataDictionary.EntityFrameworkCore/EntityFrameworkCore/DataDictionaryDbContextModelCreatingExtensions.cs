using System;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Xhznl.DataDictionary.EntityFrameworkCore
{
    public static class DataDictionaryDbContextModelCreatingExtensions
    {
        public static void ConfigureDataDictionary(
            this ModelBuilder builder,
            Action<DataDictionaryModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new DataDictionaryModelBuilderConfigurationOptions(
                DataDictionaryDbProperties.DbTablePrefix,
                DataDictionaryDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

            builder.Entity<DataDictionary>(b =>
            {
                b.ToTable(options.TablePrefix + "DataDictionary", options.Schema);

                b.ConfigureConcurrencyStamp();
                b.ConfigureExtraProperties();
                b.ConfigureAudited();

                b.Property(x => x.Name).IsRequired().HasMaxLength(DataDictionaryConsts.MaxNameLength);
                b.Property(x => x.Description).HasMaxLength(DataDictionaryConsts.MaxNotesLength);
                b.Property(x => x.Short).HasDefaultValue(0);
                b.Property(x => x.IsDeleted).HasDefaultValue(false);

                b.HasIndex(q => q.Name);
            });

            builder.Entity<DataDictionaryDetail>(b =>
            {
                b.ToTable(options.TablePrefix + "DataDictionaryDetail", options.Schema);

                b.ConfigureConcurrencyStamp();
                b.ConfigureExtraProperties();
                b.ConfigureAudited();

                b.Property(x => x.Label).IsRequired().HasMaxLength(DataDictionaryConsts.MaxNameLength);
                b.Property(x => x.Value).IsRequired().HasMaxLength(DataDictionaryConsts.MaxNotesLength);
                b.Property(x => x.Sort).HasDefaultValue(0);
                b.Property(x => x.IsDeleted).HasDefaultValue(false);

                b.HasIndex(q => q.Pid);
            });
        }
    }
}