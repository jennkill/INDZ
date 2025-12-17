using System;
using HangmanGame;
using AnagramGame;
using LettersGame;

namespace MainApp
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== English Learning Games ===");
                Console.WriteLine("1 - Hangman");
                Console.WriteLine("2 - Anagram");
                Console.WriteLine("3 - Letters");
                Console.WriteLine("0 - Exit");
                Console.Write("\nChoose a game: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        HangmanLogic.Start();
                        break;

                    case "2":
                        AnagramLogic.Start();
                        break;

                    case "3":
                        LettersLogic.Start();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }

                Console.WriteLine("\nPress any key to return to menu...");
                Console.ReadKey();
            }
        }
    }
}

