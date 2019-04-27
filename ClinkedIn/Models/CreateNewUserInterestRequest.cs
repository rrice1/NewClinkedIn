using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn.Models
{
    public class CreateNewUserInterestRequest
    {
        public int UserId { get; set; }
        public int InterestId { get; set; }
    }
}
