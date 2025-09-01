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
            DeleteStack,
            CreateFlashcard,
            PrintFlashcards,
            DeleteFlashcard,
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
                        if (!DatabaseConnection.StackExists(name))
                        {
                            Stacks newStack = new Stacks(name, description);
                            AnsiConsole.MarkupLine("[green]Stack created successfully![/]");
                            Console.WriteLine("Press any key to return to the menu...");
                            Console.ReadKey();
                            break;
                        } else
                        {
                            AnsiConsole.MarkupLine("[red]Stack already exists![/]");
                            AnsiConsole.MarkupLine("Enter any key to continue");
                            Console.ReadKey();
                            break;
                        }

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

                    case MenuOptions.CreateFlashcard:
                        AnsiConsole.MarkupLine("[green]You chose to create a new flashcard![/]");
                        Console.Write("Enter flashcard front:");
                        string front = Console.ReadLine();
                        Console.Write("Enter flashcard back:");
                        string back = Console.ReadLine();
                        Console.Write("Enter stack name for the flashcard:");
                        string stackName = Console.ReadLine();
                        if (DatabaseConnection.StackExists(stackName))
                        {
                            Flashcards newFlashcard = new Flashcards(front, back, stackName);
                            AnsiConsole.MarkupLine("[green]Flashcard created successfully![/]");
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[red]Stack does not exist. Flashcard not created.[/]");
                        }
                        Console.WriteLine("Press any key to return to the menu...");
                        Console.ReadKey();
                        break;

                    case MenuOptions.PrintFlashcards:
                        AnsiConsole.MarkupLine("[green]You chose to view all flashcards[/]");
                        Console.Write("Enter a stack");
                        string StackInput = Console.ReadLine();
                        if (DatabaseConnection.StackExists(StackInput))
                        {
                            var flashcards = DatabaseConnection.ReturnFlashcards(StackInput);
                            var table = new Table();
                            table.AddColumn("Flashcard front");
                            table.AddColumn("Flashcard back");
                            foreach (var card in flashcards)
                            {
                                table.AddRow(card.front, card.back);
                            }
                            AnsiConsole.Write(table);
                        } else
                        {
                            AnsiConsole.MarkupLine("[red]Stack does not exist[/]");
                        }
                        Console.WriteLine("Enter any key to return to the menu...");
                        Console.ReadKey();
                        break;

                    case MenuOptions.DeleteFlashcard:
                        AnsiConsole.MarkupLine("[green]You chose to delete a flashcard[/]");
                        Console.WriteLine("Enter a stack");
                        string stackInput = Console.ReadLine();
                        if (DatabaseConnection.StackExists(stackInput))
                        {
                            var flashcards = DatabaseConnection.ReturnFlashcards(stackInput);
                            var table = new Table();
                            table.AddColumn("Flashcard ID");
                            table.AddColumn("Flashcard front");
                            table.AddColumn("Flashcard back");
                            long i = 1;
                            foreach (var card in flashcards)
                            {
                                string index = i.ToString();
                                table.AddRow(index, card.front, card.back);
                                i++;
                            }
                            AnsiConsole.Write(table);
                            // Completing the deletion process
                            Console.WriteLine("Enter the front of the card you wish to delete");
                            string frontEntry = Console.ReadLine();
                            if (DatabaseConnection.FlashcardExists(frontEntry))
                            {
                                DatabaseConnection.DeleteFlashcard(frontEntry);
                                AnsiConsole.MarkupLine("[green]Flashcard has been deleted[/]");
                            } else
                            {
                                AnsiConsole.MarkupLine("[red]Flashcard does not exist[/]");
                            }
                        } else
                        {
                            AnsiConsole.MarkupLine("[red]Stack does not exist[/]");
                        }
                        Console.WriteLine("Enter any key to continue");
                        Console.ReadKey();
                        break;

                    case MenuOptions.DeleteStack:
                        AnsiConsole.MarkupLine("[green]You chose to delete a stack[/]");
                        // Printing all stacks pre-deletion for ease of use
                        var deletionStacks = DatabaseConnection.ReturnAllStacks();
                        if (deletionStacks.Count == 0)
                        {
                            AnsiConsole.MarkupLine("[red]No stacks found.[/]");
                        }
                        else
                        {
                            var table = new Table();
                            table.AddColumn("Stack Name");
                            table.AddColumn("Description");
                            foreach (var stack in deletionStacks)
                            {
                                table.AddRow(stack.StackName, stack.Description);
                            }
                            AnsiConsole.Write(table);
                        }

                        Console.WriteLine("Enter a stack");
                        string StackInputDeletion = Console.ReadLine();
                        if (DatabaseConnection.StackExists(StackInputDeletion))
                        {
                            DatabaseConnection.DeleteStack(StackInputDeletion);
                            AnsiConsole.MarkupLine("[green]Stack and flashcards have been deleted[/]");
                        } else
                        {
                            AnsiConsole.MarkupLine("[red]Stack does not exist[/]");
                        }
                        Console.WriteLine("Enter any key to continue");
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