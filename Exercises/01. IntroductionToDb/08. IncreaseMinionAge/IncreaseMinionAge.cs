namespace _08.IncreaseMinionAge
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data.SqlClient;
    using System.Text;

    public class IncreaseMinionAge
    {
        public static void Main(string[] args)
        {
            string connectionString = "Server=.; Database=MinionsDB; Trusted_Connection=True";
            SqlConnection connection = new SqlConnection(connectionString);

            int[] minionIds = Console.ReadLine().Split().Select(int.Parse).ToArray();
            using (connection)
            {
                connection.Open();
                UpdateAges(minionIds, connection);
                UpdateNames(minionIds, connection);
                Dictionary<string, int> minions = GetMinionsFromDatabase(minionIds, connection);
                foreach (var pair in minions)
                {
                    Console.WriteLine($"{pair.Key} {pair.Value}");
                }
            }
        }

        private static void UpdateNames(int[] minionIds, SqlConnection connection)
        {
            foreach (var minionId in minionIds)
            {
                if (MinionExists(minionId, connection))
                {
                    string queryString = "UPDATE Minions " +
                                         "SET Name = UPPER(SUBSTRING(Name, 1, 1)) + SUBSTRING(Name, 2, LEN(Name)) " +
                                         "WHERE MinionID = @minionId";
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@minionId", minionId);

                    command.ExecuteNonQuery();
                }
            }
        }

        private static void UpdateAges(int[] minionIds, SqlConnection connection)
        {
            foreach (var minionId in minionIds)
            {
                if (MinionExists(minionId, connection))
                {
                    string queryString = "UPDATE Minions SET Age += 1 WHERE MinionID = @minionId";
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@minionId", minionId);

                    command.ExecuteNonQuery();
                }
            }
        }

        private static bool MinionExists(int minionId, SqlConnection connection)
        {
            string queryString = "SELECT COUNT(*) FROM Minions AS m WHERE m.MinionID = @minionId";
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@minionId", minionId);

            int count = (int) command.ExecuteScalar();

            return count > 0;
        }

        private static Dictionary<string, int> GetMinionsFromDatabase(int[] minionIds, SqlConnection connection)
        {
            Dictionary<string, int> minionsAge = new Dictionary<string, int>();
            StringBuilder minionsString = new StringBuilder();
            minionsString.Append("SELECT m.Name, m.Age FROM Minions AS m WHERE m.MinionID IN (");
            foreach (var minionId in minionIds)
            {
                minionsString.Append($"{minionId},");
            }

            minionsString = minionsString.Remove(minionsString.Length - 1, 1);
            minionsString.Append(")");
            SqlCommand command = new SqlCommand(minionsString.ToString(), connection);
            SqlDataReader reader = command.ExecuteReader();
            using (reader)
            {
                while (reader.Read())
                {
                    minionsAge.Add(reader[0].ToString(), (int)reader[1]);
                }
            }

            return minionsAge;
        }
    }
}
