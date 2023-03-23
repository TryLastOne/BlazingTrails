using BlazingTrails.Api.Persistence.Entitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazingTrails.Api.Persistence.Configurations;

public class RouteInstructionConfig : IEntityTypeConfiguration<RouteInstruction>
{
    public void Configure(EntityTypeBuilder<RouteInstruction> builder)
    {
        builder.Property(x => x.TrailId).IsRequired();
        builder.Property(x => x.Stage).IsRequired();
        builder.Property(x => x.Description).IsRequired();
        
    }
}