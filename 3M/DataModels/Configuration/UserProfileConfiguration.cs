using _3M.DataModels.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _3M.DataModels.Configuration
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.HasKey(i => i.UserId);
            builder.Property(i => i.UserId).ValueGeneratedNever();

            builder.ToTable(typeof(UserProfile).Name);
        }
    }
}
