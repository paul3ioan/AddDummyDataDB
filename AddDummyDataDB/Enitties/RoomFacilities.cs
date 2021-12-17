using System.Data.SqlClient;
using Bogus;

namespace AddDummyDataDB
{
    public class RoomFacilities
    {
        public int RoomId { get; set; }
        public int FacilityId { get; set; }
        public static void AddRoomFacilities(string connectionString)
        {
            var roomFacData = new Faker<RoomFacilities>()
             .RuleFor(u => u.RoomId, f => f.Random.Number(1, 100))
             .RuleFor(c => c.FacilityId, f => f.Random.Number(1, 5))
             .Generate(10);
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            foreach (var roomFac in roomFacData)
            {
                var inserCommand = new SqlCommand("insert into RoomFacilities(RoomId, FacilityId) Values(@room, @fac)", connection);
                inserCommand.Parameters.AddWithValue("room", roomFac.RoomId);
                inserCommand.Parameters.AddWithValue("fac", roomFac.FacilityId);
                inserCommand.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
}
