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
            this.front = front;
            this.back = back;
            this.stackName = stackName;
            DatabaseConnection.AddFlashcard(front, back, stackName);
        }

        public Flashcards() {}
    }
}
