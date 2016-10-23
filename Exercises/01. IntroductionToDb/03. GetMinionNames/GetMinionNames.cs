using System;
using System.Data.SqlClient;

public class GetMinionNames
{
    public static void Main()
    {
        string connectionString = "Server=.; Database=MinionsDB; Trusted_Connection=True";
        SqlConnection connection = new SqlConnection(connectionString);
        int villainId = int.Parse(Console.ReadLine());

        using (connection)
        {
            connection.Open();
            string getVillainString = "SELECT v.Name, m.Name, m.Age FROM MinionsVillains AS mv " +
                                      "INNER JOIN Villains AS v " +
                                      "ON mv.VillianID = v.VillianID " +
                                      "INNER JOIN Minions AS m " +
                                      "ON m.MinionID = mv.MinionID " +
                                      "WHERE v.VillianID = @villianId";

            SqlCommand command = new SqlCommand(getVillainString, connection);
            command.Parameters.AddWithValue("@villianId", villainId);

            SqlDataReader reader = command.ExecuteReader();
            if (!reader.HasRows)
            {
                Console.WriteLine("No villain with ID {0} exists in the database.", villainId);
                connection.Close();
                return;
            }

            int row = 1;
            while (reader.Read())
            {
                string villianName = reader.GetFieldValue<string>(0);
                Console.WriteLine("Villian: {0}", villianName);
                Console.WriteLine("{0}. {1} {2}", row, reader.GetFieldValue<string>(1), reader.GetFieldValue<int>(2));
                row++;
                while (reader.Read())
                {
                    Console.WriteLine("{0}. {1} {2}", row, reader.GetFieldValue<string>(1), reader.GetFieldValue<int>(2));
                    row++;
                }
            }

        }
    }
}

