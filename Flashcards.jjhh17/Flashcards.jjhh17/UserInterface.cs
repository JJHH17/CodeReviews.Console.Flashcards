using Spectre.Console;
using System;
using Spectre.Console.Cli;

namespace Flashcards.jjhh17
{
    public class UserInterface
    {
        enum MenuOptions
        {
            CreateStack,
            PrintAllStacks,
            Exit,
        }

        public static void Menu()
        {
            bool active = true;

            while (active)
            {
                Console.Clear();
                AnsiConsole.MarkupLine("[blue]Welcome to the flashcard application[/]");

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<MenuOptions>()
                        .Title("Select an option:")
                        .AddChoices(Enum.GetValues<MenuOptions>()));

                switch (choice)
                {
                    case MenuOptions.CreateStack:
                        AnsiConsole.MarkupLine("[green]You chose to create a new stack![/]");
                        Console.Write("Enter stack name:");
                        string name = Console.ReadLine();
                        Console.Write("Enter stack description:");
                        string description = Console.ReadLine();
                        Stacks newStack = new Stacks(name, description);
                        AnsiConsole.MarkupLine("[green]Stack created successfully![/]");
                        Console.WriteLine("Press any key to return to the menu...");
                        Console.ReadKey();
                        break;

                    case MenuOptions.PrintAllStacks:
                        AnsiConsole.MarkupLine("[green]You chose to print all stacks![/]");
                        var stacks = DatabaseConnection.ReturnAllStacks();
                        if (stacks.Count == 0)
                        {
                            AnsiConsole.MarkupLine("[red]No stacks found.[/]");
                        }
                        else
                        {
                            var table = new Table();
                            table.AddColumn("Stack Name");
                            table.AddColumn("Description");
                            foreach (var stack in stacks)
                            {
                                table.AddRow(stack.StackName, stack.Description);
                            }
                            AnsiConsole.Write(table);
                        }
                        Console.WriteLine("Press any key to return to the menu...");
                        Console.ReadKey();
                        break;

                    case MenuOptions.Exit:
                        active = false;
                        break;
                }
            }
        }
    }
}