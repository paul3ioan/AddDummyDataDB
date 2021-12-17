using System.Collections.Generic;
using System.Data.SqlClient;

namespace AddDummyDataDB
{
    public class RoomFeatures
    {
        public string Name { get; set; }
        public string IconUrl { get; set; }
        RoomFeatures(string name, string iconUrl)
        {
            Name = name;
            IconUrl = iconUrl;
        }
        public static void AddRoomFeatures(string connectionString)
        {
            List<RoomFeatures> roomFeaturesData = new();
            roomFeaturesData.Add(new RoomFeatures("refrigreator", "#"));
            roomFeaturesData.Add(new RoomFeatures("toilet", "#"));
            roomFeaturesData.Add(new RoomFeatures("bathtub", "#"));
            roomFeaturesData.Add(new RoomFeatures("shower", "#"));
            roomFeaturesData.Add(new RoomFeatures("balcony", "#"));
            roomFeaturesData.Add(new RoomFeatures("spa", "#"));
            

            using var connection = new SqlConnection(connectionString);
            connection.Open();
            foreach (var rf in roomFeaturesData)
            {
                var inserCommand = new SqlCommand("insert into RoomFeatures(Name, IconUrl) Values(@Name, @icon)", connection);
                inserCommand.Parameters.AddWithValue("Name", rf.Name);
                inserCommand.Parameters.AddWithValue("icon", rf.IconUrl);
                inserCommand.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
}
