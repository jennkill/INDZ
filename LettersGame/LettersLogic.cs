using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LettersGame
{
    enum Level
    {
        A1 = 1,
        A2,
        B1,
        B2
    }

    public static class LettersGame
    {
        public static void Start()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Word Chain | Typing Race EN";

            DrawHeader();

            Level level = SelectLevel();
            var dictionary = LoadDictionaryByLevel(level);

            HashSet<string> usedWords = new HashSet<string>();
            string lastWord = "";
            int score = 0;

            Random rnd = new Random();
            string firstWord = "";

            while (true)
            {
                string candidate = dictionary.ElementAt(rnd.Next(dictionary.Count));
                char lastChar = candidate[^1];

                if (dictionary.Any(w => w[0] == lastChar && w != candidate))
                {
                    firstWord = candidate;
                    break;
                }
            }

            usedWords.Add(firstWord);
            lastWord = firstWord;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"First word: {firstWord}");
            Console.ResetColor();
            Console.WriteLine($"Your word must start with: '{firstWord[^1]}'");
            Console.WriteLine("-----------------------------------------");

            ShowRules();
            WaitForStart();

            Stopwatch timer = Stopwatch.StartNew();

            while (timer.Elapsed.TotalSeconds < 30)
            {
                double timeLeft = 30 - timer.Elapsed.TotalSeconds;

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"[{timeLeft:00.0}s] ");
                Console.ResetColor();

                Console.Write("Word: ");
                string input = ReadFastWord();

                if (string.IsNullOrWhiteSpace(input))
                {
                    PrintError("Empty input.");
                    continue;
                }

                input = input.ToLower();

                if (!dictionary.Contains(input))
                {
                    PrintError("Word not found in dictionary!");
                    continue;
                }

                if (usedWords.Contains(input))
                {
                    PrintError("This word was already used!");
                    continue;
                }

                if (!IsCorrectChain(lastWord, input))
                {
                    PrintError($"Word must start with '{lastWord[^1]}'!");
                    continue;
                }

                usedWords.Add(input);
                lastWord = input;
                score++;

                PrintSuccess($"Accepted! Score: {score}");
            }

            timer.Stop();
            ShowResult(score, usedWords);
            EndScreen();
        }

        // ===== LEVEL =====
        static Level SelectLevel()
        {
            Console.WriteLine("Select difficulty level:");
            Console.WriteLine("1 - A1 (Easy)");
            Console.WriteLine("2 - A2");
            Console.WriteLine("3 - B1");
            Console.WriteLine("4 - B2 (Hard)");
            Console.Write("Your choice: ");

            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out int choice) &&
                    Enum.IsDefined(typeof(Level), choice))
                {
                    Console.WriteLine("-----------------------------------------\n");
                    return (Level)choice;
                }

                Console.Write("Invalid choice. Try again: ");
            }
        }

        // ===== DICTIONARY =====
        static HashSet<string> LoadDictionaryByLevel(Level level)
        {
            List<string> words = level switch
            {
                Level.A1 => new List<string>
                {
                    "cat","dog","pen","cup","sun","man","hat","bed","book","car",
                    "tree","milk","fish","ball","door","hand","leg","day","box","apple"
                },
                Level.A2 => new List<string>
                {
                    "house","mouse","water","table","chair","bread","green","black","white","phone",
                    "school","teacher","student","window","flower","garden","family","friend","animal","picture"
                },
                Level.B1 => new List<string>
                {
                    "computer","language","holiday","mountain","problem","solution","history","culture","science","weather",
                    "country","project","program","internet","message","example","question","answer","library","building"
                },
                Level.B2 => new List<string>
                {
                    "development","application","environment","information","communication",
                    "performance","architecture","optimization","technology","experience",
                    "responsibility","implementation","configuration","distribution","international",
                    "productivity","organization","maintenance","documentation","collaboration"
                },
                _ => new List<string>()
            };

            Console.WriteLine($"Loaded {words.Count} words ({level}).");
            Console.WriteLine("-----------------------------------------\n");

            return words.ToHashSet();
        }

        // ===== UI & HELPERS =====
        static void DrawHeader()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=========================================");
            Console.WriteLine("      WORD CHAIN • TYPING RACE (EN)");
            Console.WriteLine("=========================================");
            Console.ResetColor();
            Console.WriteLine();
        }

        static void ShowRules()
        {
            Console.WriteLine("Rules:");
            Console.WriteLine("• You have 30 seconds.");
            Console.WriteLine("• Each word must start with the last letter");
            Console.WriteLine("• Words cannot be repeated.");
            Console.WriteLine("-----------------------------------------");
        }

        static void WaitForStart()
        {
            Console.WriteLine("Press ENTER to start...");
            Console.ReadLine();
            Console.WriteLine("\n🔥 GO! TYPE FAST!\n");
        }

        static string ReadFastWord()
        {
            string result = "";
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter) break;

                if (key.Key == ConsoleKey.Backspace && result.Length > 0)
                {
                    result = result[..^1];
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    result += char.ToLower(key.KeyChar);
                    Console.Write(key.KeyChar);
                }
            }
            Console.WriteLine();
            return result.Trim();
        }

        static bool IsCorrectChain(string prev, string cur) =>
            string.IsNullOrEmpty(prev) || cur[0] == prev[^1];

        static void PrintError(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("❌ " + msg);
            Console.ResetColor();
        }

        static void PrintSuccess(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("✅ " + msg);
            Console.ResetColor();
        }

        static void ShowResult(int score, HashSet<string> words)
        {
            Console.WriteLine($"\nScore: {score}");
            Console.WriteLine("Used words:");
            foreach (var w in words)
                Console.WriteLine("• " + w);
        }

        static void EndScreen()
        {
            Console.WriteLine("\nPress ENTER to return...");
            Console.ReadLine();
        }
    }
}
