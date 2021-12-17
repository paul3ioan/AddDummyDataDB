using System;
using System.Data.SqlClient;
using Bogus;

namespace AddDummyDataDB
{
    public class Reviews
    {
        public int UserId { get; set; }
        public int PropertyId { get; set; }
        public decimal Rating { get; set; }
        public string Description { get; set; }
        public DateTime ReviewDate { get; set; }
        public static void Add10Reviews(string connectionString)
        {
            var reviewData = new Faker<Reviews>()
              .RuleFor(u => u.UserId, f => f.Random.Number(1, 20))
              .RuleFor(x => x.PropertyId, f => f.Random.Number(1,20))
              .RuleFor(x => x.Rating, f => f.Random.Decimal(0, 5))
              .RuleFor(x => x.Description, f=> f.Lorem.Paragraph())
              .Generate(10);
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            foreach (var review in reviewData)
            {
                review.ReviewDate = DateTime.Now;
                var inserCommand = new SqlCommand("insert into Reviews(UserId, PropertyId, Rating, Description, ReviewDate) Values(@userId, @PropertyId, @rating, @description, @reviewDate)", connection);
                inserCommand.Parameters.AddWithValue("userId", review.UserId);
                inserCommand.Parameters.AddWithValue("PropertyId", review.PropertyId);
                inserCommand.Parameters.AddWithValue("rating", review.Rating);
                inserCommand.Parameters.AddWithValue("description", review.Description);
                inserCommand.Parameters.AddWithValue("reviewDate", review.ReviewDate);
                inserCommand.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
}
