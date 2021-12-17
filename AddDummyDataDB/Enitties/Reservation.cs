using System.Data.SqlClient;
using Bogus;
using System;
namespace AddDummyDataDB
{
    public class Reservation
    {
        public int UserId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal Price { get; set; }
        public static void Add30Reservations(string connectionString)
        {
            var reservationData = new Faker<Reservation>()
               .RuleFor(r => r.UserId, f => f.Random.Number(1,20))
               .RuleFor(r => r.CheckInDate, f => f.Date.Future(2, DateTime.Now))
               .RuleFor(r => r.Price, f => f.Random.Decimal(50, 150))
               .Generate(30);
            using var connection = new SqlConnection(connectionString);        
            connection.Open();
            foreach (var reservation in reservationData)
            {   
                reservation.CheckOutDate = reservation.CheckInDate.AddDays(7);
                var insertCommand = new SqlCommand("INSERT INTO Reservations(UserId,CheckInDate,CheckOutDate,Price) VALUES" +
                    "(@UserId, @CheckInDate, @CheckOutDate, @Price)",connection);
                insertCommand.Parameters.AddWithValue("UserId", reservation.UserId);
                insertCommand.Parameters.AddWithValue("CheckInDate", reservation.CheckInDate);
                insertCommand.Parameters.AddWithValue("CheckOutDate", reservation.CheckOutDate);
                insertCommand.Parameters.AddWithValue("Price", reservation.Price);
                insertCommand.ExecuteNonQuery();
            }
            connection.Close();
        }

    }
}
