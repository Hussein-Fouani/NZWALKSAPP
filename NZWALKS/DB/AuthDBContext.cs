﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWALKS.DB
{
    public class AuthDBContext : IdentityDbContext
    {
        public AuthDBContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerId= "f1086838-8f02-47a4-ac0b-76fdfffad321";
            var writerId = "9f92d94f-c288-4152-a453-2208ea6d6c91";

            var roles = new List<IdentityRole>
            {

                new IdentityRole
                {
                    Id= readerId,
                    ConcurrencyStamp=writerId,
                    Name="Reader",
                    NormalizedName="Reader".ToUpper(),
                }
            };
        }
    }
}
