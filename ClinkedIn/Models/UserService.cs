using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn.Models
{
    public class UserService
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ServiceId { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Offense { get; set; }
        public string Name { get; set; }

        public UserService(int id, int userid, int serviceid)
        {
            Id = id;
            UserId = userid;
            ServiceId = serviceid;
        }

        public UserService(int userId, int serviceId)
        {
            UserId = userId;
            ServiceId = serviceId;
        }

        public UserService(int userId, int serviceId, string username, string displayName, string offense, string name)
        {
            UserId = userId;
            ServiceId = serviceId;
            Username = username;
            DisplayName = displayName;
            Offense = offense;
            Name = name;            
        }

    }
}
