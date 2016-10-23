namespace _07.PrintAllMinionNames
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class PrintMinionNames
    {
        public static void Main()
        {
            string connectionString = "Server=.; Database=MinionsDB; Trusted_Connection=True";
            SqlConnection connection = new SqlConnection(connectionString);

            using (connection)
            {
                connection.Open();
                List<string> minions = GetAllMinions(connection);
                int firstIndex = 0;
                int secondIndex = minions.Count - 1;
                for (int i = 0; i < minions.Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        Console.WriteLine(minions[firstIndex]);
                        firstIndex++;
                    }
                    else
                    {
                        Console.WriteLine(minions[secondIndex]);
                        secondIndex--;
                    }
                }
            }
        }

        private static List<string> GetAllMinions(SqlConnection connection)
        {
            List<string> minions = new List<string>();
            string minionsString = "SELECT m.Name FROM Minions AS m";
            SqlCommand command = new SqlCommand(minionsString, connection);
            SqlDataReader reader = command.ExecuteReader();

            using (reader)
            {
                while (reader.Read())
                {
                    minions.Add(reader[0].ToString());
                }
            }

            return minions;
        }
    }
}
