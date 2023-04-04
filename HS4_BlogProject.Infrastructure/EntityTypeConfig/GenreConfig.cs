using HS4_BlogProject.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS4_BlogProject.Infrastructure.EntityTypeConfig
{
    class GenreConfig : BaseEntityConfig<Genre>
    {
        public override void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired(true); // 20 karakter olacak
                                                            // Description'ı 50 karakter

            base.Configure(builder);
        }
    }
}
