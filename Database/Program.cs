using System;

namespace Hackathon
{
    class Program
    {
        static void Main(string[] args)
        {
            TodoToday Todo = new TodoToday();
            Console.WriteLine("Add Items");
            Todo.InsertTodo(Console.ReadLine());
            
        }
    }
}
