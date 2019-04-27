using ClinkedIn.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn.Data
{
    public class UserServiceRepository
    {
        public List<UserService> _userServices = new List<UserService>
        {
            new UserService (1,1,1),
            new UserService (2,1,2),
            new UserService (3,2,3),
            new UserService (4,3,4)
        };

        public List<UserService> GetUserServices()
        {
            return _userServices;
        }

        public UserService AddUserService(int id, int userid, int serviceid)
        {
            var newUserService = new UserService(id, userid, serviceid);

            _userServices.Add(newUserService);

            return newUserService;

        }


        public List<UserService> UpdateUserService()
        {
            return _userServices;
        }


        public List<UserService> DeleteUserService(int id)
        {
            var listOfUserServices = _userServices;

            var userServiceToBeRemoved = (from userService in listOfUserServices
                                      where (userService.Id == id)
                                      select userService).ToList();

            _userServices.Remove(userServiceToBeRemoved.First());

            var remainingUserServices = _userServices.ToList();

            return remainingUserServices;

        }

        const string ConnectionString = "Server = localhost; Database = NewClinkedIn; Trusted_Connection = True;";

        public UserService AddUserService(int userId, int serviceId)
        {

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var insertUserServiceCommand = connection.CreateCommand();
                insertUserServiceCommand.CommandText = $@"insert into UserServices (userId, serviceId)
                                              output inserted.*
                                              values (@userId,@serviceId)";

                insertUserServiceCommand.Parameters.AddWithValue("userId", userId);
                insertUserServiceCommand.Parameters.AddWithValue("serviceId", serviceId);


                var reader = insertUserServiceCommand.ExecuteReader();

                if (reader.Read())
                {
                    var insertedUserId = reader["userId"].ToString();
                    var insertedServiceId = reader["serviceId"].ToString();
                    var insertedId = (int)reader["id"];

                    var newUserService = new UserService(userId,serviceId) { Id = insertedId };

                    //newUser.Id = insertedId; this does the same thing as having it in curlys above

                    connection.Close();

                    return newUserService;
                }
            }


            throw new Exception("No service found");


        }

        public List<UserService> GetAll()
        {
            var userServices = new List<UserService>();
            var connection = new SqlConnection(ConnectionString);
            connection.Open();

            var getAllUserServicesCommand = connection.CreateCommand();
            getAllUserServicesCommand.CommandText = @"select * from UserServices us 
                                                  join Users u on u.id = us.userId 
                                                  join Services s on s.id=us.serviceid";

            var reader = getAllUserServicesCommand.ExecuteReader();

            while (reader.Read())
            {

                var id = (int)reader["id"];
                var userId = (int)reader["userId"];
                var serviceId = (int)reader["serviceId"];
                var username = reader["username"].ToString();
                var displayName = reader["displayName"].ToString();
                var offense = reader["offense"].ToString();
                var name = reader["name"].ToString();
                var userService = new UserService(userId, serviceId, username, displayName, offense, name) { Id = id };

                userServices.Add(userService);
            }

            connection.Close();
            return userServices;
        }

    }
}
