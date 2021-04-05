using System.Data.SQLite;


namespace Hackathon
{
    public class PersonDatabase
    {
        private string PersonLocation = @"URI=file:Person.db";
        public void CreatePersonDatabase()
        {
            using var Connections = new SQLiteConnection(PersonLocation); // Connects to the file location and starts a connection
            Connections.Open();
            using var Command = new SQLiteCommand(Connections);
            Command.CommandText = "DROP TABLE IF EXISTS Person"; // Deletes person table if it already exists.
            Command.ExecuteNonQuery();
            Command.CommandText = "CREATE TABLE Person(Name TEXT, Password TEXT, Currency INT, Rank INT)"; // Creates a new person.
            // Executes the command and closes the connection.
            Command.ExecuteNonQuery();
            Connections.Close();
        }
        public void AddToPersonDatabase(string Info)
        {
            string[] PersonDetails = Info.Split(','); // Splits the details of the person when theres a comma in the string.
            using var Connections = new SQLiteConnection(PersonLocation); // New connection to the database file Person.db, connects to it.
            Connections.Open();
            using var Command = new SQLiteCommand(Connections);
            Command.CommandText = "INSERT INTO Person(Name, Password, Currency, Rank) VALUES(@Name, @Password, @Currency, @Rank)"; // Makes a new person with their information
            
            Command.Parameters.AddWithValue("@Name", PersonDetails[0]);
            Command.Parameters.AddWithValue("@Password", PersonDetails[1]);
            Command.Parameters.AddWithValue("@Currency", PersonDetails[2]);
            Command.Parameters.AddWithValue("@Rank", PersonDetails[3]);
            // Executes the command and closes the database.
            Command.ExecuteNonQuery();
            Connections.Close();
        }
    }
}
