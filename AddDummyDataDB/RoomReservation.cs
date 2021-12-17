using System.Data.SqlClient;
using Bogus;

namespace AddDummyDataDB
{
    public class RoomReservation
    {
        public int RoomId { get; set; }
        public int ReservationId { get; set; }
        public static void Add30RoomsReservations(string connectionString)
        {
            var data = new Faker<RoomReservation>()
                .RuleFor(u => u.RoomId, f => f.Random.Number(1, 100))
                .RuleFor(u => u.ReservationId, f => f.Random.Number(1, 20))
                .Generate(20);
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            foreach (var roomRes in data)
            {
                var inserCommand = new SqlCommand("insert into RoomReservations(RoomId, ReservationId) Values(@roomId, @reseId)", connection);
                inserCommand.Parameters.AddWithValue("roomId", roomRes.RoomId);
                inserCommand.Parameters.AddWithValue("reseId", roomRes.ReservationId);
                inserCommand.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
}
