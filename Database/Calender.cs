using System.Data.SQLite;
using System;
using System.Collections.Generic;

namespace Hackathon
{
    public class Calender
    {
        
        private string CalenderLocation = "URI=file:Calender.db";
        public void MakeCalender()
        {
            List<string> list = new List<string>(); // Makes a blank list
            DateTime Dates = DateTime.Today; // Gets Date information
        
            for (int i = 0; i < 5; i++) // For loop that gets 4 days after current date
            {
                list.Add((Dates.AddDays(i)).ToString("d")); // Adds every day to the list in Month/Day/Year format.
            }
            String[] DateFormat = list.ToArray(); // Converts list to an array.


            using var Connection = new SQLiteConnection(CalenderLocation); // Makes a new connection to Calender.db
            Connection.Open(); // Starts connection
            using var CommandText = new SQLiteCommand(Connection);
            CommandText.CommandText = @"DROP TABLE IF EXISTS Calender"; // Deletes table Calender if it exists.
            CommandText.ExecuteNonQuery();
            CommandText.CommandText = @"CREATE TABLE Calender(Date TEXT, Items TEXT)"; // Creates table with dates and items.
            CommandText.ExecuteNonQuery();
            CommandText.CommandText = @"INSERT INTO Calender(Date) VALUES(@Monday), (@Tuesday), (@Wednesday), (@Thursday), (@Friday)";
            // Gets values from previously generated array and adds them to the table.
            CommandText.Parameters.AddWithValue("@Monday", DateFormat[0]);
            CommandText.Parameters.AddWithValue("@Tuesday", DateFormat[1]);
            CommandText.Parameters.AddWithValue("@Wednesday", DateFormat[2]);
            CommandText.Parameters.AddWithValue("@Thursday", DateFormat[3]);
            CommandText.Parameters.AddWithValue("@Friday", DateFormat[4]);
            // Executs the command and closes the connection
            CommandText.ExecuteNonQuery();
            Connection.Close();

        }
        public void AddToCalender(String Date, String Item)
        {
            // Opens the Database at Calender.db and makes a new command
            using var Connection = new SQLiteConnection(CalenderLocation);
            Connection.Open();
            using var Command = new SQLiteCommand(Connection);
            // This command updates the values of the item where the Date equals the Date specified by user.
            Command.CommandText = $"UPDATE Calender SET Items = @Item WHERE Date = @Date";
            Command.Parameters.AddWithValue("@Item", Item);
            Command.Parameters.AddWithValue("@Date", Date);
            // Executes command and closes database.
            Command.ExecuteNonQuery();
            Connection.Close();
        }
}
}