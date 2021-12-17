using System.Data.SqlClient;
using Bogus;

namespace AddDummyDataDB
{
    public class RoomCategories
    {
        public string Name { get; set; }
        public int BedsCount { get; set; }
        public string PricePerNight { get; set; }
        public string Description { get; set; }
        public static void Add10RoomCateg(string connectionString)
        {
            var RoomCategData = new Faker<RoomCategories>()
              .RuleFor(u => u.Name, f => f.Lorem.Word())
              .RuleFor(x => x.PricePerNight, f =>  f.Commerce.Price(50,300))
              .RuleFor(x => x.BedsCount, f => f.Random.Number(1, 5))
              .RuleFor(x => x.Description, f => f.Lorem.Paragraph())
              .Generate(10);
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            foreach (var roomCateg in RoomCategData)
            {
                
                var inserCommand = new SqlCommand("insert into RoomCategories(Name, BedsCount,PricePerNight, Description) Values(@name, @beds, @price, @description)", connection);
                inserCommand.Parameters.AddWithValue("name", roomCateg.Name);
                inserCommand.Parameters.AddWithValue("beds", roomCateg.BedsCount);
                inserCommand.Parameters.AddWithValue("price", decimal.Parse(roomCateg.PricePerNight));
                inserCommand.Parameters.AddWithValue("description", roomCateg.Description);
                inserCommand.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
}
