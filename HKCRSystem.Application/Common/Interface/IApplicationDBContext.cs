using HKCRSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKCRSystem.Application.Common.Interface
{
    public interface IApplicationDBContext
    {
        DbSet<Attachment> Attachments { get; set; }
        DbSet<Billing> Billings { get; set; }
        DbSet<Car> Cars { get; set; }
        DbSet<Damage> Damages { get; set; }
        DbSet<Offer> Offers { get; set; }
        DbSet<Request> Requests { get; set; }
        DbSet<Return> Returns { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
