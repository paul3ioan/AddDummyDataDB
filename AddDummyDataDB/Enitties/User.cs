using System.Collections.Generic;
using System.Data.SqlClient;
using Bogus;
namespace AddDummyDataDB
{
    public class User
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RoleId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public static void Add20RandomUsers(string connectionString)
        {
            List<int> roleValues = new List<int> { 1, 2, 3 };
            var userData = new Faker<User>()
               .RuleFor(u => u.FirstName, f => f.Name.FirstName())
               .RuleFor(u => u.LastName, f => f.Name.LastName())
               .RuleFor(u => u.RoleId, f => f.Random.ListItem(roleValues))
               .RuleFor(u => u.Email, f => f.Internet.Email())
               .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber())
               .RuleFor(u => u.Password, f => f.Internet.Password(20))
               .Generate(20);
            using var connection = new SqlConnection(connectionString);
            /// add protection level for sql injection 
            connection.Open();
            foreach(var user in userData)
            {
                var insertCommand = new SqlCommand( "INSERT INTO Users(FirstName,LastName,RoleId,Email,PhoneNumber,Password) VALUES" +
                    "(@FirstName, @LastName, @RoleId, @Email, @PhoneNumber,@Password )", connection);
                insertCommand.Parameters.AddWithValue("FirstName", user.FirstName);
                insertCommand.Parameters.AddWithValue("LastName", user.LastName);
                insertCommand.Parameters.AddWithValue("RoleId", user.RoleId);
                insertCommand.Parameters.AddWithValue("Email", user.Email);
                insertCommand.Parameters.AddWithValue("PhoneNumber", user.PhoneNumber);
                insertCommand.Parameters.AddWithValue("Password", user.Password);
     
                insertCommand.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
}
