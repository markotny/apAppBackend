using Dapper;
using Npgsql;
using ResourceServer.JSONModels;
using ResourceServer.Resources;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ResourceServer.Models
{
    public class TrueHomeContext
    {
        private static String query;
        //Get User by Login
        public static User getUser(String login)
        {
            query = @"SELECT * FROM User WHERE Login = @login;";

            User user = null;

            var parameters = new Dictionary<string, object>();
            parameters.Add("login", login);

            DynamicParameters dbParams = new DynamicParameters();
            dbParams.AddDynamicParams(parameters);

            using (var connection = new NpgsqlConnection(AppSettingProvider.connString))
            {
                connection.Open();
                user = connection.Query<User>(query, dbParams).FirstOrDefault();
            }
            return user;
        }
        //Get User by ID
        public static User getUser(int id)
        {
            query = @"SELECT * FROM User WHERE ID_User = @id;";

            User user = null;

            var parameters = new Dictionary<string, object>();
            parameters.Add("id", id);

            DynamicParameters dbParams = new DynamicParameters();
            dbParams.AddDynamicParams(parameters);

            using (var connection = new NpgsqlConnection(AppSettingProvider.connString))
            {
                connection.Open();
                user = connection.Query<User>(query, dbParams).FirstOrDefault();
            }
            return user;
        }
        //Get Apartment by id
        public static Apartment getApartment(int id)
        {
            query = @"SELECT * FROM Apartment WHERE id_ap = @id;";

            Apartment apartment = null;

            var parameters = new Dictionary<string, object>();
            parameters.Add("id", id);

            DynamicParameters dbParams = new DynamicParameters();
            dbParams.AddDynamicParams(parameters);

            using (var connection = new NpgsqlConnection(AppSettingProvider.connString))
            {
                connection.Open();
                apartment = connection.Query<Apartment>(query, dbParams).FirstOrDefault();
            }
            return apartment;
        }
        //Get all Apartments
        public static IList<Apartment> getAllApartments()
        {
            query = @"SELECT * FROM Apartment;";

            IList<Apartment> apartment = null;

            using (var connection = new NpgsqlConnection(AppSettingProvider.connString))
            {
                connection.Open();
                apartment = connection.Query<Apartment>(query).ToList();
            }
            return apartment;
        }
        //Get with limit and offset Apartments
        public static ApartmentJSON getApartments(int limit, int offset)
        {
            query = @"SELECT * FROM Apartment ORDER BY ID_Ap ASC LIMIT @limit OFFSET @offset;";

            IList<Apartment> apartments = null;
            ApartmentJSON apJson = new ApartmentJSON();

            var parameters = new Dictionary<string, object>();
            parameters.Add("limit", limit + 1);
            parameters.Add("offset", offset);

            DynamicParameters dbParams = new DynamicParameters();
            dbParams.AddDynamicParams(parameters);

            using (var connection = new NpgsqlConnection(AppSettingProvider.connString))
            {
                connection.Open();
                apartments = connection.Query<Apartment>(query, dbParams).ToList();
            }

            if(apartments.Count <= limit)
            {
                apJson.hasMore = false;
                apJson.apartmentsList = apartments;
            }
            else
            {
                apartments.RemoveAt(limit);
                apJson.hasMore = true;
                apJson.apartmentsList = apartments;
            }
            
            return apJson;
        }
        //Update Apartment
        public static void updateApartment(Apartment ap)
        {
            query = @"UPDATE Apartment SET "+
                    "Name = @Name,"+
                    "City = @City,"+
                    "Street = @Street,"+
                    "ApartmentNumber = @ApartmentNumber,"+
                    "ImgThumb = @ImgThumb,"+
                    "ImgList = @ImgList,"+
                    "Rate = @Rate,"+
                    "Lat = @Lat,"+
                    "Long = @Long,"+
                    "IDUser = @IDUser"+
                    " WHERE ID_Ap = @ID_Ap;";

            using (var connection = new NpgsqlConnection(AppSettingProvider.connString))
            {
                connection.Open();
                connection.Execute(query, ap);
            }
        }
        //Create Apartment
        public static void createApartment(Apartment ap)
        {
            query = @"INSERT INTO Apartment " +
                    "(Name,City,Street,ApartmentNumber,ImgThumb,ImgList,Rate,Lat,Long,IDUser)" +
                    " VALUES "+
                    "(@Name,@City,@Street,@Address,@ImgThumb,@ImgList,@Rate,@Lat,@Long,@IDUser);";

            using (var connection = new NpgsqlConnection(AppSettingProvider.connString))
            {
                connection.Open();
                connection.Execute(query, ap);
            }
        }
        //Delete Apartment
        public static void deleteApartment(int id)
        {
            query = @"DELETE FROM Apartment" +
                    " WHERE id_ap = @id;";

            var parameters = new Dictionary<string, object>();
            parameters.Add("id", id);

            DynamicParameters dbParams = new DynamicParameters();
            dbParams.AddDynamicParams(parameters);

            using (var connection = new NpgsqlConnection(AppSettingProvider.connString))
            {
                connection.Open();
                connection.Execute(query, dbParams);
            }
        }
    }
}
