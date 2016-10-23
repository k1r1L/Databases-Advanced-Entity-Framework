namespace _04.AddMinions
{
    using System;
    using System.Data.SqlClient;
    public class AddMinions
    {
        public static void Main(string[] args)
        {
            string connectionString = "Server=.; Database=MinionsDB; Trusted_Connection=True";
            SqlConnection connection = new SqlConnection(connectionString);
            string[] minionTokens = Console.ReadLine().Split(new char[] { ':', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string[] villianTokens = Console.ReadLine().Split(new char[] { ':', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            string minionName = minionTokens[1];
            int minionAge = int.Parse(minionTokens[2]);
            string minionTown = minionTokens[3];
            string villianName = villianTokens[1];

            using (connection)
            {
                connection.Open();

                // Checking if town is existent in the database
                SqlCommand command = new SqlCommand($"SELECT t.Name FROM Towns AS t WHERE t.Name = @town", connection);
                command.Parameters.AddWithValue("@town", minionTown);
                if (!CheckExistance(command))
                {
                    command = new SqlCommand("INSERT INTO Towns([Name]) VALUES (@town)", connection);
                    command.Parameters.AddWithValue("@town", minionTown);
                    command.ExecuteNonQuery();
                    Console.WriteLine($"Town {minionTown} was added to the database.");
                }

                // Checking if villian is existent in the database
                command = new SqlCommand("SELECT v.Name FROM Villains AS v WHERE v.Name = @vllianName", connection);
                command.Parameters.AddWithValue("@vllianName", villianName);
                if (!CheckExistance(command))
                {
                    command = new SqlCommand("INSERT INTO Villains([Name], [EvilnessFactor]) VALUES (@villianName, 'evil')", connection);
                    command.Parameters.AddWithValue("@villianName", villianName);
                    command.ExecuteNonQuery();
                    Console.WriteLine($"Villain {villianName} was added to the database.");
                }

                // Checking if minion is existent in the database
                command = new SqlCommand("SELECT m.Name FROM Minions AS m WHERE m.Name = @minionName", connection);
                command.Parameters.AddWithValue("@minionName", minionName);
                if (!CheckExistance(command))
                {
                    command = new SqlCommand("SELECT TOP 1 t.TownID FROM Towns AS t ORDER BY t.TownID DESC", connection);
                    int townId = (int)command.ExecuteScalar();
                    command = new SqlCommand("INSERT INTO Minions(Name,Age,TownID) VALUES (@minionName, @minionAge ,@townId)", connection);
                    command.Parameters.AddWithValue("@minionName", minionName);
                    command.Parameters.AddWithValue("@minionAge", minionAge);
                    command.Parameters.AddWithValue("@townId", townId);
                    command.ExecuteNonQuery();
                }

                // Adding Villian to minion and vice versa
                command = new SqlCommand("SELECT TOP 1 m.MinionID FROM Minions AS m ORDER BY m.MinionID DESC", connection);
                int minionId = (int)command.ExecuteScalar();
                command = new SqlCommand("SELECT v.VillianId FROM Villains AS v WHERE v.Name = @villianName", connection);
                command.Parameters.AddWithValue("@villianName", villianName);
                int villianId = (int) command.ExecuteScalar();
                command = new SqlCommand("INSERT INTO MinionsVillains([MinionID], [VillianID]) VALUES (@minionId, @villianId)", connection);
                command.Parameters.AddWithValue("@minionId", minionId);
                command.Parameters.AddWithValue("@villianId", villianId);
                command.ExecuteNonQuery();
                Console.WriteLine($"Successfully added {minionName} to be minion of {villianName}.");
            }
        }

        private static bool CheckExistance(SqlCommand command)
        {
            SqlDataReader reader = command.ExecuteReader();

            using (reader)
            {
                return reader.HasRows;
            }
        }
    }
}

