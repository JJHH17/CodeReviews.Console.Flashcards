using Spectre.Console;
using System;
using Spectre.Console.Cli;
using Microsoft.Identity.Client.Utils;

namespace Flashcards.jjhh17
{
    public class StudyArea
    {
        public string Date { get; set; }
        public int Score { get; set; }
        public string StackName { get; set; }
        private int IncrementValue = 1;

        public StudyArea(string stackName)
        {
            Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Score = 0;
            StackName = stackName;
        }

        public StudyArea() { }

        public void IncrementScore()
        {
            Score += IncrementValue;
        }
    }
}

