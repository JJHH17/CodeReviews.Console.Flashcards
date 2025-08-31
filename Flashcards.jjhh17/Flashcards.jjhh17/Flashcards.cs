using System;

namespace Flashcards.jjhh17
{
    public class Flashcards
    {
        private static long idCounter = 0;

        private long id { get; set; }
        public string front { get; set; }
        public string back { get; set; }
        public string stackName { get; set; }

        public Flashcards(string front, string back, string stackName)
        {
            this.id = ++Flashcards.idCounter;
            this.front = front;
            this.back = back;
            this.stackName = stackName;
            DatabaseConnection.AddFlashcard(this.id, front, back, stackName);
        }

        public Flashcards() {}

        public static long GetTotalFlashcardsCreated()
        {
            return Flashcards.idCounter;
        }
    }
}
