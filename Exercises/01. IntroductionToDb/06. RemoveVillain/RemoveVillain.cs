namespace _06.RemoveVillain
{
    using System;
    using System.Data.SqlClient;

    public class RemoveVillain
    {
        public static void Main()
        {
            string connectionString = "Server=.; Database=MinionsDB; Trusted_Connection=True";
            SqlConnection connection = new SqlConnection(connectionString);
            int villainId = int.Parse(Console.ReadLine());

            using (connection)
            {
                connection.Open();
                if (VillainExist(villainId, connection))
                {
                    string villainName = GetVillainName(villainId, connection);
                    int numberOfMinionsReleased = DeleteMinionsVillains(villainId, connection);
                    DeleteVillain(villainId, connection);
                    Console.WriteLine($"{villainName} was deleted");
                    Console.WriteLine($"{numberOfMinionsReleased} minions released");
                }
                else
                {
                    Console.WriteLine("No such villain was found");
                }
            }
        }

        private static string GetVillainName(int villainId, SqlConnection connection)
        {
            string villainName = string.Empty;
            string villainString = "SELECT v.Name FROM Villains AS v WHERE v.VillainID = @villainId";
            SqlCommand command = new SqlCommand(villainString, connection);
            command.Parameters.AddWithValue("@villainId", villainId);

            villainName = command.ExecuteScalar().ToString();

            return villainName;
        }

        private static int DeleteMinionsVillains(int villainId, SqlConnection connection)
        {
            string deleteMinionsVillainsString = "DELETE FROM MinionsVillains WHERE VillainID = @villainId";
            SqlCommand command = new SqlCommand(deleteMinionsVillainsString, connection);
            command.Parameters.AddWithValue("@villainId", villainId);

            return command.ExecuteNonQuery();
        }

        private static void DeleteVillain(int villainId, SqlConnection connection)
        {
            string deleteVillainString = "DELETE FROM Villains WHERE VillainID = @villainId";
            SqlCommand command = new SqlCommand(deleteVillainString, connection);
            command.Parameters.AddWithValue("@villainId", villainId);

            command.ExecuteNonQuery();
        }

        private static bool VillainExist(int villainId, SqlConnection connection)
        {
            string villainString = "SELECT COUNT(*) FROM Villains AS v WHERE v.VillainID = @villainId";
            SqlCommand command = new SqlCommand(villainString, connection);
            command.Parameters.AddWithValue("@villainId", villainId);

            int villainIdFromDatabase = (int)command.ExecuteScalar();

            return villainIdFromDatabase > 0;
        }
    }
}
