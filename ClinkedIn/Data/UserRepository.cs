using ClinkedIn.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn.Data
{
    public class UserRepository
    {
        static List<User> _users = new List<User>
        {
            new User(1, "Shane Wilson", "abc", "sdw", "murder"),
            new User(2, "Rob Rice", "abc", "rr", "being too hot"),
            new User(3, "Ripal Patel", "abc", "rp", "evil genius"),
            new User(4, "Wayne Collier", "abc", "wc", "unlawful possession of firearm"),
            new User(5, "Marco Crank", "abc", "sdw", "prostitution")
        };

        //public User AddUser(string username, string password, string displayName, string offense)
        //{
        //    var newUser = new User(username, password, displayName, offense);

        //    newUser.Id = _users.Count + 1;

        //    _users.Add(newUser);

        //    return newUser;
        //}

        public List<User> GetAllUsers()
        {
            return _users;
        }

        public List<User> GetUsersById(int userId)
        {
            return _users;
        }

        const string ConnectionString = "Server = localhost; Database = NewClinkedIn; Trusted_Connection = True;";

        public User AddUser(string username, string password, string displayName, string offense)
        {

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var insertUserCommand = connection.CreateCommand();
                insertUserCommand.CommandText = $@"insert into Users (username, password, displayName, offense) 
                                              output inserted.*
                                              values (@username,@password,@displayName,@offense)";

                insertUserCommand.Parameters.AddWithValue("username", username);
                insertUserCommand.Parameters.AddWithValue("password", password);
                insertUserCommand.Parameters.AddWithValue("displayName", displayName);
                insertUserCommand.Parameters.AddWithValue("offense", offense);


                var reader = insertUserCommand.ExecuteReader();

                if (reader.Read())
                {
                    var insertedUsername = reader["username"].ToString();
                    var insertedPassword = reader["password"].ToString();
                    var insertedDisplayName = reader["displayName"].ToString();
                    var insertedOffense = reader["offense"].ToString();
                    var insertedId = (int)reader["id"];

                    var newUser = new User(username, password, displayName, offense) { Id = insertedId };

                    //newUser.Id = insertedId; this does the same thing as having it in curlys above

                    connection.Close();

                    return newUser;
                }
            }


            throw new Exception("No user found");


        }

        public List<User> GetAll()
        {
            var users = new List<User>();
            var connection = new SqlConnection(ConnectionString);
            connection.Open();

            var getAllUsersCommand = connection.CreateCommand();
            getAllUsersCommand.CommandText = @"select * 
                                               from users";

            var reader = getAllUsersCommand.ExecuteReader();

            while (reader.Read())
            {

                var id = (int)reader["id"];
                var username = reader["username"].ToString();
                var password = reader["password"].ToString();
                var displayName = reader["displayName"].ToString();
                var offense = reader["offense"].ToString();
                var user = new User(username, password, displayName, offense) { Id = id };

                users.Add(user);
            }

            connection.Close();
            return users;
        }

    }
}
