using System.Data.SqlClient;

class Program
{
    public static void Main(string[] args)
    {
        string connectionString = "Server=.; Database=MinionsDB; Trusted_Connection=True";
        SqlConnection connection = new SqlConnection(connectionString);

        using (connection)
        {
            connection.Open();
            string createTablesString = "CREATE TABLE Countries " +
                                           "( " +
                                           "[CountryID] INT IDENTITY(1, 1) PRIMARY KEY," +
                                           "[Name] VARCHAR(50)" +
                                           ") " +
                                           "CREATE TABLE Towns " +
                                           "( " +
                                           "[TownID] INT IDENTITY(1, 1) PRIMARY KEY," +
                                           "[Name] VARCHAR(50)," +
                                           "[CountryID] INT, " +
                                           "CONSTRAINT FK_Towns_Countries FOREIGN KEY([CountryID])" +
                                           "REFERENCES Countries([CountryID])" +
                                           ") " +
                                           "CREATE TABLE Minions " +
                                              "( " +
                                              "[MinionID] INT IDENTITY(1, 1) PRIMARY KEY," +
                                              "[Name] VARCHAR(50)," +
                                              "[Age] INT," +
                                              "[TownID] INT," +
                                              "CONSTRAINT FK_Minions_Towns FOREIGN KEY([TownID])" +
                                              "REFERENCES Towns([TownID]) " +
                                              ") " + 
                                              "CREATE TABLE Villians " +
                                              "( " +
                                              "[VillianID] INT IDENTITY(1, 1) PRIMARY KEY," +
                                              "[Name] VARCHAR(50)," +
                                              "[EvilnessFactor] VARCHAR(10) CHECK([EvilnessFactor] = 'good' OR [EvilnessFactor] = 'bad' OR [EvilnessFactor] = 'evil' OR [EvilnessFactor] = 'super evil') " +
                                              ") " +
                                              "CREATE TABLE MinionsVillians " +
                                                 "( " +
                                                 "[MinionID] INT," +
                                                 "[VillianID] INT," +
                                                 "CONSTRAINT PK_MinionsVillians PRIMARY KEY(MinionID, VillianID)," +
                                                 "CONSTRAINT FK_MinionsVillians_Minions FOREIGN KEY(MinionID) " +
                                                 "REFERENCES Minions(MinionID)," +
                                                 "CONSTRAINT FK_MinionsVillians_Villians FOREIGN KEY(VillianID) " +
                                                 "REFERENCES Villians(VillianID) " +
                                                 ")";
            string insertString = "INSERT INTO Countries VALUES " +
                                 "('Bulgaria'), ('Bulgaria1'), ('Bulgaria2'), ('Bulgaria3'), ('Bulgaria4')" +
                                 "INSERT INTO Towns VALUES " +
                                 "('Sofia', 1), " +
                                 "('Plovdiv', 2), " +
                                 "('Burgas', 3), " +
                                 "('Varna', 4), " +
                                 "('Mezdra', 5)" + 
                                 "INSERT INTO Minions VALUES " +
                                 "('Minion1', 6, 1), " +
                                 "('Minion2', 4, 2), " +
                                 "('Minion3', 3, 3), " +
                                 "('Minion4', 2, 4), " +
                                 "('Minion5', 1, 5)" +
                                 "INSERT INTO Villians VALUES " +
                                 "('Gru', 'evil'), " +
                                 "('Victor', 'evil'), " +
                                 "('Jilly', 'bad'), " +
                                 "('Milly', 'good'), " +
                                 "('MinionsMaster', 'super evil') " +
                                 "INSERT INTO MinionsVillians VALUES " +
                                 "(1, 2), " +
                                 "(1, 1), " +
                                 "(2, 3), " +
                                 "(2, 4), " +
                                 "(3, 5)";
            SqlCommand createTownsCommand = new SqlCommand(createTablesString, connection);
            createTownsCommand.ExecuteNonQuery();
            SqlCommand insertCommand = new SqlCommand(insertString, connection);
            insertCommand.ExecuteNonQuery();
        }
    }
}

