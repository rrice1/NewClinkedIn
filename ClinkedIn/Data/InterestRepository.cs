﻿using ClinkedIn.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIn.Data
{
    public class InterestRepository
    {
        UserRepository  _userRepository = new UserRepository();

        static List<Interest> _interests = new List<Interest> {
            new Interest(1, "Movies", 1),
            new Interest(2, "Painting", 1),
            new Interest(3, "Karates", 1),
            new Interest(4, "Movies", 2),
            new Interest(5, "Gardening", 3),
            new Interest(6, "Karates", 4),
            new Interest(7, "Karates", 5),
            new Interest(8, "Gardening", 2),
            new Interest(9, "Gardening", 5)
    };

        public List<Interest> AddInterest(string interestName, int userId)
        {
            var newInterest = new Interest(interestName, userId);

            newInterest.Id = _interests.Count + 1;

            _interests.Add(newInterest);

            return _interests;
        }

        public List<User> GetInterestsList(int userId, string interestName)
        {
            var listOfUsers = _userRepository.GetAllUsers();
            var listOfFriendsWithSameInterest = _interests
                .Where(interest => interest.InterestName.ToLower() == interestName.ToLower())
                .Where(interest => interest.UserId != userId).ToList();
            var FriendsThatUserCanMake = listOfUsers
                .Join(listOfFriendsWithSameInterest,
                user => user.Id,
                interest => interest.UserId,
                (user, interest) => new User(user.Id, user.Username, user.DisplayName))
                .Join(_interests, user => user.Id, interest => interest.UserId, 
                (user,interest)  => {
                    user.Interests.Add(interest.InterestName);
                    return user;
                })
                .ToList();
            return FriendsThatUserCanMake.Distinct().ToList();
        }

        public List<Interest> UpdateInterest(int id, int userId, string interestName)
        {
            //filtering interest based on user id and interest Id.
            var updatedInterest = _interests
                .Where(interest => interest.Id == id)
                .Where(interest => interest.UserId == userId).ToList();

            updatedInterest.First().InterestName = interestName;
            return updatedInterest;
        }

        public List<Interest> DeleteInterest(int id, int userId)
        {
            var FilterInterstToDelete = _interests.Where(interest => interest.Id == id).Where(interest => interest.UserId == userId).ToList();
            var InterestToDelete =_interests.Remove(FilterInterstToDelete.First());
            var remainingInterestsforUser = _interests.Where(x => x.UserId == userId).ToList();
            return remainingInterestsforUser;
        }

        const string ConnectionString = "Server = localhost; Database = NewClinkedIn; Trusted_Connection = True;";

        public Interest AddInterest(string Name)
        {

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var insertInterestCommand = connection.CreateCommand();
                insertInterestCommand.CommandText = $@"insert into Interests (Name) 
                                              output inserted.*
                                              values (@name)";

                insertInterestCommand.Parameters.AddWithValue("name", Name);

                var reader = insertInterestCommand.ExecuteReader();

                if (reader.Read())
                {
                    var insertedName = reader["Name"].ToString();
                    var insertedId = (int)reader["id"];

                    var newInterest = new Interest(Name) { Id = insertedId };

                    //newUser.Id = insertedId; this does the same thing as having it in curlys above

                    connection.Close();

                    return newInterest;
                }
            }


            throw new Exception("No interest found");


        }

        public List<Interest> GetAll()
        {
            var interests = new List<Interest>();
            var connection = new SqlConnection(ConnectionString);
            connection.Open();

            var getAllUsersCommand = connection.CreateCommand();
            getAllUsersCommand.CommandText = @"select * 
                                               from interests";

            var reader = getAllUsersCommand.ExecuteReader();

            while (reader.Read())
            {

                var id = (int)reader["id"];
                var interestName = reader["Name"].ToString();
                var interest = new Interest(interestName) { Id = id };

                interests.Add(interest);
            }

            connection.Close();
            return interests;
        }

    }
}
