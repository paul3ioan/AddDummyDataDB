using System.Data.SqlClient;
using Bogus;
namespace AddDummyDataDB
{
    public class PropertyImages
    {
        public string ImageUrl { get; set; }
        public int PropId { get; set; }
        public static void Add60Images(string connectionString)
        {
            var imageData = new Faker<PropertyImages>()
              .RuleFor(u => u.ImageUrl, f => f.Image.DataUri(0, 50))
              .RuleFor(c => c.PropId, f => f.Random.Number(1, 20))
              .Generate(60);
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            foreach (var img in imageData)
            {
                var inserCommand = new SqlCommand("insert into PropertyImages(ImageUrl, PropertyId) Values(@image, @PropertyId)", connection);
                inserCommand.Parameters.AddWithValue("image", img.ImageUrl);
                inserCommand.Parameters.AddWithValue("PropertyId", img.PropId);
                inserCommand.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
}
