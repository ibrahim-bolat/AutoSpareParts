using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Entities.Common;
using AutoSpareParts.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoSpareParts.Persistence.Mappings.Common;

public abstract class BaseEntityMap<TBaseEntity> : IEntityTypeConfiguration<TBaseEntity> where TBaseEntity : BaseEntity, new()
{
    public virtual void Configure(EntityTypeBuilder<TBaseEntity> builder)
    {
        builder.HasKey(entity => entity.Id);
        builder.Property(entity => entity.Id).ValueGeneratedOnAdd();
        builder.Property(entity => entity.Note).HasMaxLength(500);
    }
}
