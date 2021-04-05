using System;
using System.Data.SQLite;

namespace Hackathon
{
    public class TodoToday
    {  
        private string TodoTodayLocation = @"URI=file:TodoList.db"; //Location of the DB file for the day.

        public void CreateTodoToday()
        {
            using var Connections = new SQLiteConnection(TodoTodayLocation); // Sets up a new connection using "TodoList.db"
            Connections.Open(); // Opens the connection
            using var Command = new SQLiteCommand(Connections); // Sets up new command input
            Command.CommandText = @"DROP TABLE IF EXISTS TodoList"; // If a TodoList Table exists before hand, deletes it.
            Command.ExecuteNonQuery(); // Executes the above command ^
            Command.CommandText = @"CREATE TABLE TodoList(ID INTEGER PRIMARY KEY, TODAY TEXT)"; // Makes a todo list that automatically increments by one.
            Command.ExecuteNonQuery();
            Connections.Close(); // Closes the connections
        }
        public void InsertTodo(String Item)
        {
            using var Connections = new SQLiteConnection(TodoTodayLocation); // Starts new Connection
            Connections.Open(); // Opens Connection
            var Insert = "INSERT INTO TodoList(TODAY) VALUES(@Items)"; // Inserts Items into TODAY, which is used to show all the itmes that user has for today.
            using var InsertDB = new SQLiteCommand(Insert, Connections);
            InsertDB.Parameters.AddWithValue("@Items", Item); // Replaces @Items with Item, for security reasons to prevent things like SQL Injection attacks.
            InsertDB.ExecuteNonQuery(); // Runs command.
            Connections.Close(); // Closes the database connection.
        }

        }
    }
