using AutoSpareParts.Domain.Entities;
using AutoSpareParts.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoSpareParts.Persistence.Mappings;

    public class RequestInfoLogMap:IEntityTypeConfiguration<RequestInfoLog>
    {
        public void Configure(EntityTypeBuilder<RequestInfoLog> builder)
        {
            builder.HasKey(requestInfoLog => requestInfoLog.Id);
            builder.Property(requestInfoLog => requestInfoLog.Id).ValueGeneratedOnAdd();
            builder.Property(requestInfoLog => requestInfoLog.AreaName).HasMaxLength(100);
            builder.Property(requestInfoLog => requestInfoLog.ControllerName).HasMaxLength(100).IsRequired();
            builder.Property(requestInfoLog => requestInfoLog.ActionName).HasMaxLength(100).IsRequired();
            builder.Property(requestInfoLog => requestInfoLog.ActionArguments).HasMaxLength(500);
            builder.Property(requestInfoLog => requestInfoLog.RequestMethodType).HasMaxLength(50);
            builder.Property(requestInfoLog => requestInfoLog.DateTime).HasMaxLength(200);
            builder.Property(requestInfoLog => requestInfoLog.LocalIpAddress).HasMaxLength(200);
            builder.Property(requestInfoLog => requestInfoLog.RemoteIpAddress).HasMaxLength(200);
            builder.Property(requestInfoLog => requestInfoLog.LocalPort).HasMaxLength(7);
            builder.Property(requestInfoLog => requestInfoLog.RemotePort).HasMaxLength(7);
            builder.Property(requestInfoLog => requestInfoLog.Note).HasMaxLength(500);
            builder.HasOne(requestInfoLog => requestInfoLog.AppUser).WithMany(user => user.RequestInfoLogs)
                .HasForeignKey(requestInfoLog => requestInfoLog.UserId).IsRequired(false).OnDelete(DeleteBehavior.Cascade);
        }
    }
