namespace _09.IncreaseAgeStoredProcedure
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public class IncreaseAgeStoredProcedure
    {
        public static void Main()
        {
            string connectionString = "Server=.; Database=MinionsDB; Trusted_Connection=True";
            SqlConnection connection = new SqlConnection(connectionString);

            int[] minionIds = Console.ReadLine().Split().Select(int.Parse).ToArray();
            List<int> validMinionIds = new List<int>();
            using (connection)
            {
                connection.Open();
                foreach (var minionId in minionIds)
                {
                    if (UpdateMinionAge(minionId, connection))
                    {
                        validMinionIds.Add(minionId);
                    }
                }

                PrintUpdateMinions(validMinionIds, connection);
            }
        }

        private static void PrintUpdateMinions(List<int> validMinionIds, SqlConnection connection)
        {
            foreach (var validMinionId in validMinionIds)
            {
                string selectMinionSql = "SELECT m.Name, m.Age FROM Minions AS m WHERE m.MinionID = @MinionID";
                SqlCommand selectCommand = new SqlCommand(selectMinionSql, connection);
                selectCommand.Parameters.AddWithValue("@MinionID", validMinionId);

                SqlDataReader reader = selectCommand.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader[0]} - {reader[1]}");
                    }
                }
            }
        }

        private static bool UpdateMinionAge(int minionId, SqlConnection connection)
        {
            SqlCommand command = new SqlCommand("usp_GetOlder", connection) { CommandType = CommandType.StoredProcedure };
            if (ValidMinionId(minionId, connection))
            {
                SqlParameter parameter = new SqlParameter("@MinionID", minionId);
                command.Parameters.Add(parameter);
                command.ExecuteNonQuery();
                return true;
            }

            return false;
        }

        private static bool ValidMinionId(int minionId, SqlConnection connection)
        {
            string checkMinionSql = "SELECT COUNT(*) FROM Minions WHERE MinionID = @MinionID";
            SqlCommand command = new SqlCommand(checkMinionSql, connection);
            command.Parameters.AddWithValue("@MinionID", minionId);
            int idFromDatabase = (int)command.ExecuteScalar();

            return idFromDatabase > 0;
        }
    }
}
