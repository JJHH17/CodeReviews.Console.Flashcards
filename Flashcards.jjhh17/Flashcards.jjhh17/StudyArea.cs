using System;

namespace Flashcards.jjhh17
{
    public class StudyArea
    {
        private string Date { get; set; }
        private int Score { get; set; }
        private string StackName { get; set; }

        public StudyArea(string stackName)
        {
            Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Score = 0;
            StackName = stackName;
        }

        public StudyArea() { }
    }
}

