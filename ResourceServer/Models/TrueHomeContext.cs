using Microsoft.Extensions.Options;
using Npgsql;
using ResourceServer.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceServer.Models
{
    public class TrueHomeContext
    {
        private static String query;

        //Get User
        public static User getUser(String login, String password)
        {
            query = "SELECT ID_User, Login, IDRole FROM User" +
                    "WHERE Login = \"" + login +
                    "\" AND Password = \"" + password + "\" LIMIT 1";

            User user = null;
            user = executeSelectUser(query);

            return user;
        }

        public static User getUserById(int id)
        {
            query = "SELECT ID_User, Login, IDRole FROM User" +
                    "WHERE ID_User = \"" + id + "\" LIMIT 1";

            User user = null;
            user = executeSelectUser(query);

            return user;
        }

        private static User executeSelectUser(string query)
        {
            using (var conn = new NpgsqlConnection(AppSettingProvider.connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        return new User()
                        {
                            ID_User = reader.GetInt32(0),
                            Login = reader.GetString(1),
                            IDRole = reader.GetInt32(2)
                        };
                    }

                }
            }
        }


    }
}
