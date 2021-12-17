using System.Data.SqlClient;
using Bogus;

namespace AddDummyDataDB
{
    public class Property
    {
        public string Name { get; set; }
        public decimal Rating { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public int AdminId { get; set; }
        public int NumberOfDays { get; set; }
        public int PropertyType { get; set; }
        public bool isDeleted { get; set; }
        public string Phone { get; set; }
        public int TotalRooms { get; set; }
        public static void Add20Property(string connectionString)
        {
             var propertyData = new Faker<Property>()
               .RuleFor(u => u.Name, f => f.Company.CompanyName())
               .RuleFor(u => u.Rating, f => f.Random.Decimal(5))
               .RuleFor(u => u.Description, f => f.Lorem.Paragraph())
               .RuleFor(u => u.Address, f=>f.Address.StreetAddress())
               .RuleFor(u => u.CityId, f => f.Random.Number(1,20))
               .RuleFor(u => u.AdminId, f => f.Random.Number(1,20))
               .RuleFor(u => u.NumberOfDays, f => f.Random.Number(0,20))
               .RuleFor(u => u.PropertyType, f => f.Random.Number(1, 4))
               .RuleFor(u => u.Phone, f => f.Phone.PhoneNumber())
               .RuleFor(u => u.TotalRooms, f=>f.Random.Number(0,6))
               .Generate(20);
            using var connection = new SqlConnection(connectionString);
    
            connection.Open();
            foreach(var prop in propertyData)
            {
                var insertCommand = new SqlCommand("INSERT INTO Properties(Name,Rating,Description,Address,CityId,AdministratorId ,NumberOfDayForRefounds, " +
                   " PropertyTypeId, Phone, TotalRooms) VALUES" + "(@Name, @Rating, @Description, @Address, @CityId,@AdminId,@Days, @PropType,@Phone,@TotalRooms )", connection);
                insertCommand.Parameters.AddWithValue("Name", prop.Name);
                insertCommand.Parameters.AddWithValue("Rating", prop.Rating);
                insertCommand.Parameters.AddWithValue("Description",prop.Description);
                insertCommand.Parameters.AddWithValue("Address", prop.Address);
                insertCommand.Parameters.AddWithValue("CityId", prop.CityId);
                insertCommand.Parameters.AddWithValue("AdminId", prop.AdminId);
                insertCommand.Parameters.AddWithValue("Days", prop.NumberOfDays);
                insertCommand.Parameters.AddWithValue("PropType", prop.PropertyType);
                insertCommand.Parameters.AddWithValue("Phone", prop.Phone);
                insertCommand.Parameters.AddWithValue("TotalRooms", prop.TotalRooms);
                insertCommand.ExecuteNonQuery();
            }
connection.Close();
            }
    }
}
