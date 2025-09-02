using Spectre.Console;
using System;
using Spectre.Console.Cli;
using Microsoft.Identity.Client.Utils;

namespace Flashcards.jjhh17
{
    public class StudyArea
    {
        private string Date { get; set; }
        public int Score { get; set; }
        private string StackName { get; set; }

        public StudyArea(string stackName)
        {
            Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Score = 0;
            StackName = stackName;
        }

        public StudyArea() { }

        public void IncrementScore()
        {
            Score++;
        }
    }
}

