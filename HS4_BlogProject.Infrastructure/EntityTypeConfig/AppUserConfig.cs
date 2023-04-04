using HS4_BlogProject.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS4_BlogProject.Infrastructure.EntityTypeConfig
{

    // Identity AppUSer'ı configure ediyor, biz sadece eklediğimiz property'leri configure edebiliriz.
    class AppUserConfig : BaseEntityConfig<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            // Migration yaptığımızda Identity'nin configuratasyonlarını kontrol etmeyi unutmayalım
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserName).IsRequired(true).HasMaxLength(30);

            builder.Property(x => x.ImagePath).IsRequired(false);

            base.Configure(builder);
        }
    }
}
