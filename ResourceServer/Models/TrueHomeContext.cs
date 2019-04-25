using Dapper;
using Npgsql;
using ResourceServer.JSONModels;
using ResourceServer.Resources;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceServer.Models
{
    public class TrueHomeContext
    {
        private static String query;
        //Get User by Login
        public static User getUserFromLogin(String login)
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
        public static User getUser(string id)
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

        //Add new user
        public static async Task AddUser(User user)
        {
            query = @"INSERT INTO public.user " +
                    "VALUES " +
                    "(@ID_User,@Login,@Email,@IDRole);";
            
            using (var connection = new NpgsqlConnection(AppSettingProvider.connString))
            {
                connection.Open();
                await connection.ExecuteAsync(query, user);
            }
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
                    "Address = @Address,"+
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
        public static async Task<int> createApartment(Apartment ap)
        {
            query = @"INSERT INTO Apartment " +
                    "(Name,City,Street,ApartmentNumber,ImgThumb,ImgList,Rate,Lat,Long,IDUser)" +
                    " VALUES "+
                    "(@Name,@City,@Street,@ApartmentNumber,@ImgThumb,@ImgList,@Rate,@Lat,@Long,@IDUser)" +
                    "RETURNING ID_Ap";

            int id;
            using (var connection = new NpgsqlConnection(AppSettingProvider.connString))
            {
                connection.Open();
                id = await connection.ExecuteScalarAsync<int>(query, ap);
            }

            return id;
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
        //Add picture reference
        public static void AddPictureRef(int id, string fileName)
        {
            var apartment = getApartment(id);
            apartment.ImgList = apartment.ImgList
                                    ?.Concat(new[] {fileName}).ToArray() 
                                    ?? new[] {fileName};
            updateApartment(apartment);

            //TODO: make this work instead of loading whole apartment object
            //query = @"SELECT ImgList FROM Apartment WHERE id_ap = @id;";
            //var updQuery = @"UPDATE Apartment SET ImgList = @imgList WHERE id_ap = @id;";

            //using (var connection = new NpgsqlConnection(AppSettingProvider.connString))
            //{
            //    connection.Open();
            //    var imgList = connection.Query<string[]>(query, new {id}).FirstOrDefault();
            //    imgList.Append(fileName);
            //    connection.Execute(updQuery, new {imgList, id});
            //}
        }
        //Delete picture reference
        public static void DeletePictureRef(int id, string fileName)
        {
            var apartment = getApartment(id);
            apartment.ImgList = apartment.ImgList.Where(file => file != fileName).ToArray();
            updateApartment(apartment);

            //TODO: make this work instead of loading whole apartment object
            //query = @"SELECT ImgList FROM Apartment WHERE id_ap = @id;";
            //var updQuery = @"UPDATE Apartment SET ImgList = @imgList WHERE id_ap = @id;";

            //using (var connection = new NpgsqlConnection(AppSettingProvider.connString))
            //{
            //    connection.Open();
            //    var imgList = connection.Query<string[]>(query, new {id}).FirstOrDefault();
            //    imgList.Append(fileName);
            //    connection.Execute(updQuery, new {imgList, id});
            //}
        }
    }
}
