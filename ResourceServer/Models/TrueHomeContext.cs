using Dapper;
using Npgsql;
using ResourceServer.Resources;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace ResourceServer.Models
{
    public class TrueHomeContext
    {
        private static String query;

        //Get User
        public static User getUser(String login)
        {
            query = "SELECT * FROM User WHERE Login = @login;";

            User user = null;

            using (var connection = new NpgsqlConnection(AppSettingProvider.connString))
            {
                connection.Open();
                user = connection.Query<User>(query, login).Single();
            }
            return user;
        }

        public static User getUser(int id)
        {
            query = "SELECT ID_User, Login, IDRole FROM User" +
                    "WHERE ID_User = @id;";

            User user = null;

            using (var connection = new NpgsqlConnection(AppSettingProvider.connString))
            {
                connection.Open();
                user = connection.Query<User>(query, id).Single();
            }
            return user;
        }
    }
}
