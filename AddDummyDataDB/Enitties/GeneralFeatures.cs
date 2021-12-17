using System.Data.SqlClient;
using System.Collections.Generic;
namespace AddDummyDataDB
{
    public class GeneralFeatures
    {
        public string Name { get; set; }
        public string IconUrl { get; set; }
        public GeneralFeatures(string name, string iconUrl)
        {
            Name = name;
            IconUrl = iconUrl;
        }
        public static void AddFeatures(string connectionString)
        {
            List<GeneralFeatures> data = new();
            data.Add(new GeneralFeatures("pool", "#"));
            data.Add(new GeneralFeatures("garden", "#"));
            data.Add(new GeneralFeatures("golf", "#"));
            data.Add(new GeneralFeatures("football", "#"));
            data.Add(new GeneralFeatures("ceva", "#"));
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            foreach (var feat in data)
            {
                var inserCommand = new SqlCommand("insert into GeneralFeatures(Name, IconUrl) Values(@name, @url)", connection);
                inserCommand.Parameters.AddWithValue("name", feat.Name);
                inserCommand.Parameters.AddWithValue("url", feat.IconUrl);
                inserCommand.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
}
