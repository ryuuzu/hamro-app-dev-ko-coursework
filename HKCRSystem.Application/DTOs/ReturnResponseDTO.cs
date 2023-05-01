using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKCRSystem.Application.DTOs
{
    public class ReturnResponseDTO
    {
        public Guid Id { get; set; }
        public Guid RequestId { get; set; }
        public string AcceptedBy { get; set; }
        public DateTime ReturnDate { get; set; }
        public string CarName { get; set; }
        public string UserName { get; set; }
    }
}
