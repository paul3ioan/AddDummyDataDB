using System.Data.SqlClient;
using System.Collections.Generic;

namespace AddDummyDataDB
{
    public class PropertyType
    {
        public string Name { get; set; }
        public static void AddProperties(string connectionString)
        {
            List<string> propertyTypeData = new();
            propertyTypeData.Add("Hotel");
            propertyTypeData.Add("Motel");
            propertyTypeData.Add("Hostel");
            propertyTypeData.Add("Cabin");
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            foreach (var pt in propertyTypeData)
            {
                var inserCommand = new SqlCommand("insert into PropertyType(Type) Values(@Name)", connection);
                inserCommand.Parameters.AddWithValue("Name", pt);
                inserCommand.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
}
