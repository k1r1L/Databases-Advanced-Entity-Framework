using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

public class ChangeTownNamesCasing
{
    public static void Main(string[] args)
    {
        string connectionString = "Server=.; Database=MinionsDB; Trusted_Connection=True";
        SqlConnection connection = new SqlConnection(connectionString);

        string countryName = Console.ReadLine();
        List<string> allTownsInCountry = new List<string>();
        bool hasUpdated = false;
        using (connection)
        {
            connection.Open();
            allTownsInCountry = GetAllTownsInCountry(countryName, connection);

            if (allTownsInCountry.Any())
            {
                int countryId = GetCountryId(countryName, connection);
                UpdateTowns(countryId, connection);
                hasUpdated = true;
            }
        }

        if (hasUpdated)
        {
            Console.WriteLine($"{allTownsInCountry.Count} town names were affected.");
            Console.WriteLine($"[{string.Join(", ", allTownsInCountry.Select(c => c.ToUpper()))}]");
        }
        else
        {
            Console.WriteLine("No town names were affected.");
        }
    }

    private static int GetCountryId(string countryName, SqlConnection connection)
    {
        string commandString =
            "SELECT TOP 1 c.CountryID FROM Countries AS c WHERE c.Name = @countryName ORDER BY c.CountryID";
        SqlCommand command = new SqlCommand(commandString, connection);
        command.Parameters.AddWithValue("@countryName", countryName);

        int countryId = (int)command.ExecuteScalar();

        return countryId;
    }

    private static void UpdateTowns(int countryId, SqlConnection connection)
    {
        string commandString = "UPDATE Towns " +
                               "SET Name = UPPER(Name) " +
                               "WHERE CountryID = @countryId";
        SqlCommand command = new SqlCommand(commandString, connection);
        command.Parameters.AddWithValue("@countryId", countryId);
        command.ExecuteNonQuery();
    }

    private static List<string> GetAllTownsInCountry(string countryName, SqlConnection connection)
    {
        List<string> towns = new List<string>();
        string commandString = "SELECT t.Name FROM Countries AS c " +
                               "INNER JOIN Towns AS t " +
                               "ON t.CountryID = c.CountryID " +
                               "WHERE c.Name = @countryName";

        SqlCommand command = new SqlCommand(commandString, connection);
        command.Parameters.AddWithValue("@countryName", countryName);
        SqlDataReader reader = command.ExecuteReader();

        using (reader)
        {
            while (reader.Read())
            {
                towns.Add(reader[0].ToString());
            }
        }

        return towns;
    }
}

