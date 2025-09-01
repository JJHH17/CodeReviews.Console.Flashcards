using System;

namespace Flashcards.jjhh17
{
    public class Flashcards
    {
        public string front { get; set; }
        public string back { get; set; }
        public string stackName { get; set; }

        public Flashcards(string front, string back, string stackName)
        {
            front = front;
            back = back;
            stackName = stackName;

            if (front.Length == 0 || back.Length == 0 || front.Length > 50 || back.Length > 50)
            {
                Console.WriteLine("Invalid flashcard input!");
            }
            else
            {
                DatabaseConnection.AddFlashcard(front, back, stackName);
                Console.WriteLine("Flashcard created successfully");
            }
        }

        public Flashcards() {}
    }
}
