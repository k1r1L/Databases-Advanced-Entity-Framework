using System;
using System.Data.SqlClient;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

public class GetVilliansNames
{
    public static void Main(string[] args)
    {
        string connectionString = "Server=.; Database=MinionsDB; Trusted_Connection=True";
        SqlConnection connection = new SqlConnection(connectionString);

        using (connection)
        {
            connection.Open();
            string getMinionsString = "SELECT v.Name, mc.MinionsCount FROM " +
                                      "(SELECT v.VillianID, COUNT(*) AS MinionsCount FROM Villains AS v " +
                                      "INNER JOIN MinionsVillains AS mv " +
                                      "ON v.VillianID = mv.VillianID " +
                                      "GROUP BY v.VillianID) AS mc " +
                                      "INNER JOIN Villians AS v " +
                                      "ON mc.VillianID = v.VillianID " +
                                      "WHERE mc.MinionsCount > 3 " +
                                      "ORDER BY mc.MinionsCount DESC";

            SqlCommand command = new SqlCommand(getMinionsString, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write(reader[i] + " ");
                }

                Console.WriteLine();
            }

        }
    }
}
