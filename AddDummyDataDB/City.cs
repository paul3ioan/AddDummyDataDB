using Bogus;
using System.Data.SqlClient;

namespace AddDummyDataDB
{
    public class City
    {
        public string Name { get; set; }
        public int CountryId { get; set; }

        public static void Add40Cities(string connectionString)
        {
            var cityData = new Faker<City>()
              .RuleFor(u => u.Name, f => f.Address.City())
              .RuleFor(c => c.CountryId, f => f.Random.Number(1, 20))
              .Generate(40);
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            foreach (var city in cityData)
            {
                var inserCommand = new SqlCommand("insert into Cities(Name, CountryId) Values(@Name, @CountryId)", connection);
                inserCommand.Parameters.AddWithValue("Name", city.Name);
                inserCommand.Parameters.AddWithValue("CountryId", city.CountryId);
                inserCommand.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
}
