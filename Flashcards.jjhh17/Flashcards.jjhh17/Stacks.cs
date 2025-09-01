using System;

namespace Flashcards.jjhh17
{
    public class Stacks
    {
        public string StackName { get; set; }
        public string Description { get; set; }

        public Stacks(string name, string description)
        {
            StackName = name;
            Description = description;

            if (StackName.Length == 0 || StackName.Length > 50 || Description.Length > 50)
            {
                Console.WriteLine("Invalid stack input!");
            }
            else
            {
                DatabaseConnection.AddStack(name, description);
                Console.WriteLine("Stack created successfully!");
            }
        }

        // For Dapper usage
        public Stacks() { }
    }
}