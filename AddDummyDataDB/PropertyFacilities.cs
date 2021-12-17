using System.Data.SqlClient;
using System.Collections.Generic;
namespace AddDummyDataDB
{
    public class PropertyFacilities
    {
        public int PropertyId { get; set; }
        public int FacilityId { get; set; }
        PropertyFacilities(int proId,int  facId)
        {
            PropertyId = proId;
            FacilityId = facId;
        }
        public static void AddPropertyFacilities(string connectionString)
        {
            var data = new List<PropertyFacilities>();
            for(int i = 0; i < 50; i ++)
            {
                data.Add(new PropertyFacilities(i / 5 + 2, i % 5 + 1));
            }
           using var connection = new SqlConnection(connectionString);
            connection.Open();
            foreach (var propFac in data)
            {
                var inserCommand = new SqlCommand("insert into PropertyFacilities(PropertyId, FacilityId) Values(@prop, @faci)", connection);
                inserCommand.Parameters.AddWithValue("prop", propFac.PropertyId);
                inserCommand.Parameters.AddWithValue("faci", propFac.FacilityId);
                inserCommand.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
}
