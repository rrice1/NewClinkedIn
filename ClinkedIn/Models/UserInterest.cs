using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn.Models
{
    public class UserInterest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int InterestId { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Offense { get; set; }
        public string Name { get; set; }

        public UserInterest(int id, int userid, int interestid)
        {
            Id = id;
            UserId = userid;
            InterestId = interestid;
        }

        public UserInterest(int userId, int serviceId)
        {
            UserId = userId;
            InterestId = serviceId;
        }

        public UserInterest(int userId, int interestId, string username, string displayName, string offense, string name)
        {
            UserId = userId;
            InterestId = interestId;
            Username = username;
            DisplayName = displayName;
            Offense = offense;
            Name = name;
        }
    }
}
