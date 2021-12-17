using System.Collections.Generic;
using System.Data.SqlClient;

namespace AddDummyDataDB
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public static void AddRoles(string connectionString)
        {
            List<Role> roles = new();
            Role admin = new Role();
            admin.Name = "PropertyAdmin";
            Role superAdmin = new Role();
            superAdmin.Name = "SuperAdmin";
            Role client = new Role();
            client.Name = "Client";
            roles.Add(admin);
            roles.Add(client);
            roles.Add(superAdmin);
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            foreach(var role in roles)
            {
                var insertCommand = new SqlCommand("INSERT INTO Roles" +"(Name) VALUES (@Name) ", connection);
                insertCommand.Parameters.AddWithValue("@Name", role.Name);
                insertCommand.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
}
