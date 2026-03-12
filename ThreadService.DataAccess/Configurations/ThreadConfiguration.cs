using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThreadService.Persistance.Entities;

namespace ThreadService.Persistance.Configurations
{
    public class ThreadConfiguration : IEntityTypeConfiguration<ThreadEntity>
    {
        public void Configure(EntityTypeBuilder<ThreadEntity> builder)
        {
            builder.HasKey(x => x.ID);

            builder.Property(x => x.CreatedTime).IsRequired();

            builder.Property(x => x.Header).IsRequired();

            builder.Property(x => x.AuthorID).IsRequired();
        }
    }
}
