using HKCRSystem.Application.Common.Interface;
using HKCRSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKCRSystem.Infrastructure.Persistence
{
    public class ApplicationDBContext : IdentityDbContext<IdentityUser, IdentityRole, string>, IApplicationDBContext
    {
        public readonly IDateTime _dateTime;

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options, IDateTime dateTime) : base(options)
        {
            _dateTime = dateTime;
        }


        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Billing> Billings { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Damage> Damages { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Return> Returns { get; set; }
        public DbSet<UserAttachment> UserAttachments { get; set; }
    }
}