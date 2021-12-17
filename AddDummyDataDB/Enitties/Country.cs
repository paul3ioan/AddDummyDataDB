using Bogus;
using System.Data.SqlClient;
namespace AddDummyDataDB
{
    public class Country
    {
        public string Name { get; set; }

        public static void Add20Countries(string connectionString)
        {
            var countryData = new Faker<Country>()
                .RuleFor(u => u.Name, f => f.Address.Country())
                .Generate(20);
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            foreach(var country in countryData)
            {
                var inserCommand = new SqlCommand("insert into Countries(Name) Values(@Name)", connection);
                inserCommand.Parameters.AddWithValue("Name", country.Name);
                inserCommand.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
}
