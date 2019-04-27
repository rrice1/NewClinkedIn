using ClinkedIn.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn.Data
{
    public class UserInterestRepository
    {




        const string ConnectionString = "Server = localhost; Database = NewClinkedIn; Trusted_Connection = True;";

        public UserInterest AddUserInterest(int userId, int interestId)
        {

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var insertUserInterestCommand = connection.CreateCommand();
                insertUserInterestCommand.CommandText = $@"insert into UserInterests (userId, interestId)
                                              output inserted.*
                                              values (@userId,@interestId)";

                insertUserInterestCommand.Parameters.AddWithValue("userId", userId);
                insertUserInterestCommand.Parameters.AddWithValue("interestId", interestId);


                var reader = insertUserInterestCommand.ExecuteReader();

                if (reader.Read())
                {
                    var insertedUserId = reader["userId"].ToString();
                    var insertedInterestId = reader["interestId"].ToString();
                    var insertedId = (int)reader["id"];

                    var newUserInterest = new UserInterest(userId, interestId) { Id = insertedId };

                    //newUser.Id = insertedId; this does the same thing as having it in curlys above

                    connection.Close();

                    return newUserInterest;
                }
            }


            throw new Exception("No service found");


        }

        public List<UserInterest> GetAll()
        {
            var userInterests = new List<UserInterest>();
            var connection = new SqlConnection(ConnectionString);
            connection.Open();

            var getAllUserInterestsCommand = connection.CreateCommand();
            getAllUserInterestsCommand.CommandText = @"select * from UserInterests ui 
                                                  join Users u on u.id = ui.userId 
                                                  join Interests i on i.id=ui.interestid";

            var reader = getAllUserInterestsCommand.ExecuteReader();

            while (reader.Read())
            {

                var id = (int)reader["id"];
                var userId = (int)reader["userId"];
                var interestId = (int)reader["interestId"];
                var username = reader["username"].ToString();
                var displayName = reader["displayName"].ToString();
                var offense = reader["offense"].ToString();
                var name = reader["name"].ToString();
                var userInterest = new UserInterest(userId, interestId, username, displayName, offense, name) { Id = id };

                userInterests.Add(userInterest);
            }

            connection.Close();
            return userInterests;
        }
    }
}
