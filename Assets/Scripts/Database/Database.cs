using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using System.Linq;

namespace MuseumApp
    {


    public static class Database 
    {
        private static string databasePath = "GameDatabase.db";
        private static readonly SQLiteConnection connection;

        static Database()
        {
            connection = new SQLiteConnection(databasePath);
            connection.CreateTable<User>();
            connection.CreateTable<UserRating>();
            // set flag to use foreign key
            connection.Execute("PRAGMA foreign_keys = ON");
        }

        public static void RegisterPlayer(string userName, string password)
        {
            connection.Insert(new User
            {
                Username = userName,
                Password = password
            });

        }

        public static User GetUser(string username)
        {
            try
            {
                // executed until an exception is thrown or it completes successfully
                return connection.Get<User>(username);
            }
            catch
            {
                // catches exceptions
                return null;
            }
        }

        public static UserRating GetUserAttractionRating(string attractionID)
        {
            if (!User.IsLoggedIn)
            {
                return null;
            }

            var username = User.LoggedInUsername;
            var results = connection.Query<UserRating>(
                $@"SELECT * 
                   FROM {nameof(UserRating)}
                   WHERE {nameof (UserRating.AttractionID)} = '{attractionID}' 
                     AND {nameof(UserRating.RatingUser)} = '{username}' ");

            Debug.Assert(results.Count <= 1, $"{username} has multiple ratings for the same attraction");

            return results.Count == 1 ? results[0] : null;
        }

       public static int GetAttractionTotalRating(string attractionID)
        {
            // var ratings = connection.Table<UserRating>().Where(userRating => userRating.AttractionID == attraction);

            var ratings = (from userRating in connection.Table<UserRating>() where userRating.AttractionID == attractionID select userRating);

            return ratings.Any() ? ratings.Sum(userRating => userRating.Rating) / ratings.Count() : 0;

        }

        public static void Rate(string attractionID, int rating)
        {
            var userRating = GetUserAttractionRating(attractionID);
            if (userRating != null)
            {
                userRating.Rating = rating;
                connection.Update(userRating);
                return;
            }

            connection.Insert(new UserRating
            {
                AttractionID = attractionID,
                RatingUser = User.LoggedInUsername,
                Rating = rating
            });
        }

        public static void DeleteUser()
        {
            if (!User.IsLoggedIn)
            {
                return;
            }

            // UserRating.RatingUser is FK on User.Username, and is set to cascade delete

            connection.Delete(new User { Username = User.LoggedInUsername});

        }

        public static void ClearDatabase()
        {
            connection.DeleteAll<User>();
            connection.DeleteAll<UserRating>();
        }

    }
}
