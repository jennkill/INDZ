using System;
using System.Collections.Generic;
using System.Linq;

namespace AnagramGame
{
    public static class AnagramLogic
    {
        public static void Start()
        {
            Console.Title = "АНАГРАМА — Мультигра";
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Console.Clear();
                DrawTitle();

                Console.WriteLine("Виберіть рівень:");
                Console.WriteLine("[1] A1 — Легкі слова");
                Console.WriteLine("[2] A2 — Базові слова");
                Console.WriteLine("[3] B1 — Середній рівень");
                Console.WriteLine("[4] B2 — Складні слова");
                Console.WriteLine("[0] Назад в меню");

                Console.Write("\nВаш вибір: ");
                string choice = Console.ReadLine();

                if (choice == "0") break;

                Dictionary<string, List<string>> levels = GetLevels();
                if (!levels.ContainsKey(choice))
                {
                    Console.WriteLine("Невірний вибір. Натисніть Enter...");
                    Console.ReadKey();
                    continue;
                }

                PlayLevel(levels[choice]);
            }
        }

        static void PlayLevel(List<string> baseWords)
        {
            Dictionary<string, List<string>> anagrams = GetAnagramDictionary();
            Random rnd = new Random();

            while (true)
            {
                Console.Clear();
                DrawTitle();

                string rootWord = baseWords[rnd.Next(baseWords.Count)];
                string shuffled = Shuffle(rootWord);

                List<string> allWords = anagrams.ContainsKey(rootWord)
                    ? anagrams[rootWord]
                    : new List<string> { rootWord };

                HashSet<string> found = new HashSet<string>();

                while (found.Count < allWords.Count)
                {
                    Console.Clear();
                    DrawTitle();

                    Console.WriteLine("🔤 Анаграмне слово:");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"   {shuffled.ToUpper()}");
                    Console.ResetColor();

                    Console.WriteLine($"\nЗнайдено ({found.Count}/{allWords.Count}):");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("  " + (found.Count > 0 ? string.Join(", ", found) : "(поки що нічого)"));
                    Console.ResetColor();

                    Console.Write("\nВаш варіант: ");
                    string answer = (Console.ReadLine() ?? "").Trim().ToLower();

                    if (allWords.Contains(answer))
                    {
                        if (!found.Contains(answer))
                        {
                            found.Add(answer);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\n✔ Правильно!");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("\n❗ Це слово вже знайдено");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n✘ Неправильно!");
                    }

                    Console.ResetColor();

                    if (found.Count < allWords.Count)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\nЄ ще слова! Спробуй знайти інші...");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\n🎉 Вітаю! Ти знайшов усі слова!");
                        Console.ResetColor();
                    }

                    Console.WriteLine("\nНатисніть Enter для продовження...");
                    Console.ReadLine();
                }

                Console.WriteLine("\nНатисніть Enter для нової анаграми або введіть '0' для повернення...");
                string next = Console.ReadLine();
                if (next == "0") break;
            }
        }

        static string Shuffle(string input)
        {
            Random rnd = new Random();
            char[] arr = input.ToCharArray();

            for (int i = 0; i < arr.Length; i++)
            {
                int j = rnd.Next(arr.Length);
                (arr[i], arr[j]) = (arr[j], arr[i]);
            }

            if (new string(arr) == input && arr.Length > 1)
                return Shuffle(input);

            return new string(arr);
        }

        static Dictionary<string, List<string>> GetLevels()
        {
            return new Dictionary<string, List<string>>
            {
                ["1"] = new List<string> { "tea", "dog", "rat" },
                ["2"] = new List<string> { "stop", "evil", "least" },
                ["3"] = new List<string> { "listen", "secure", "react" },
                ["4"] = new List<string> { "cinema", "triangle", "conversation" }
            };
        }

        static Dictionary<string, List<string>> GetAnagramDictionary()
        {
            return new Dictionary<string, List<string>>
            {
                ["tea"] = new List<string> { "tea", "eat", "ate" },
                ["dog"] = new List<string> { "dog", "god" },
                ["rat"] = new List<string> { "rat", "tar", "art" },

                ["stop"] = new List<string> { "stop", "pots", "post", "spot", "tops" },
                ["evil"] = new List<string> { "evil", "live", "veil", "vile" },
                ["least"] = new List<string> { "least", "stale", "steal", "slate" },

                ["listen"] = new List<string> { "listen", "silent", "enlist", "tinsel", "inlets" },
                ["secure"] = new List<string> { "secure", "rescue", "recuse" },
                ["react"] = new List<string> { "react", "crate", "cater", "trace", "caret" },

                ["cinema"] = new List<string> { "cinema", "iceman", "anemic" },
                ["triangle"] = new List<string> { "triangle", "altering", "relating" },
                ["conversation"] = new List<string> { "conversation", "conservation" }
            };
        }

        static void DrawTitle()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════╗");
            Console.WriteLine("║    А Н А Г Р А М А — М У Л Ь Т И    ║");
            Console.WriteLine("╚════════════════════════════════════╝\n");
            Console.ResetColor();
        }
    }
}

