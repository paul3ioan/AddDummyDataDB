using System.Data.SqlClient;
using Bogus;

namespace AddDummyDataDB
{
    public class Rooms
    {
        public int RoomCategoryId { get; set; }
        public int PropertyId { get; set; }
        public static void Add100Rooms(string connectionString)
        {
            var data = new Faker<Rooms>()
                .RuleFor(u => u.RoomCategoryId, f => f.Random.Number(1, 10))
                .RuleFor(u => u.PropertyId, f => f.Random.Number(1, 20))
                .Generate(100);
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            foreach (var propFac in data)
            {
                var inserCommand = new SqlCommand("insert into Rooms(RoomCategoryId, PropertyId) Values(@romcat, @prop)", connection);
                inserCommand.Parameters.AddWithValue("romcat", propFac.RoomCategoryId);
                inserCommand.Parameters.AddWithValue("prop", propFac.PropertyId);
                inserCommand.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
}
