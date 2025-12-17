using System;
using System.Collections.Generic;

namespace HangmanEnglish
{
    class Program
    {
        // ASCII картинки шибениці
        static readonly string[] HangmanStages = new string[]
        {
            @"
  ██
██
██
██
██
██
██
██
██████████████████████",
            @"
  ████████████████████
██
██
██
██
██
██
██
██████████████████████",
            @"
 ████████████████████
██             |
██             |
██             |
██             |
██
██
██
██████████████████████",
            @"
 ████████████████████
██             |
██             |
██             O
██            /|\
██            / \
██           =====
██           |   |
██████████████████████",
            @"
  ████████████████████
██             |
██             |
██             |
██             O
██            /|\
██             |
██             |           \=
██            / \           \
███████████████████████████████"
        };

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // 100 унікальних слів + підказки
            Dictionary<string, string> wordHints = new Dictionary<string, string>
            {
                // Легкі слова
                { "apple", "Червоний або зелений фрукт" },
                { "orange", "Цитрус, а також колір" },
                { "house", "Місце, де живуть люди" },
                { "window", "Через це видно вулицю" },
                { "river", "Природна течія води" },
                { "friend", "Людина, з якою у тебе дружба" },
                { "school", "Місце, де навчаються діти" },
                { "chair", "Предмет меблів для сидіння" },
                { "table", "Предмет меблів для роботи або їжі" },
                { "book", "Що читають" },
                { "dog", "Домашня тварина, друг людини" },
                { "cat", "Домашня тварина, що муркоче" },
                { "sun", "Зоря, що освітлює Землю" },
                { "moon", "Небесне тіло вночі" },
                { "star", "Яскрава точка на небі" },
                { "tree", "Рослина з гілками та листям" },
                { "flower", "Рослина з красивими пелюстками" },
                { "car", "Транспортний засіб на колесах" },
                { "bike", "Двоколісний транспорт" },
                { "milk", "Напій від корови" },
                { "water", "Прозора рідина, необхідна для життя" },
                { "bread", "Їжа, що робиться з борошна" },
                { "egg", "Їжа з шкаралупою, від курки" },
                { "pen", "Інструмент для письма" },
                { "pencil", "Інструмент для письма, легко стирається" },
                { "bag", "Щось, куди можна покласти речі" },
                { "shoe", "Взуття для ніг" },
                { "hat", "Що носять на голові" },
                { "rain", "Вода, що падає з неба" },
                { "snow", "Білий холодний опад" },
                { "fire", "Те, що горить" },
                { "ice", "Замерзла вода" },
                { "food", "Що ми їмо" },
                { "milkshake", "Солодкий напій з молока" },
                { "juice", "Рідина із фруктів" },
                { "beach", "Пісочна місцевість біля моря" },
                { "forest", "Місце з великою кількістю дерев" },
                { "island", "Земля, оточена водою" },
                { "cloud", "Білий або сірий об'єкт у небі" },
                { "rainbow", "Кольорова дуга після дощу" },
                { "starfish", "Морський організм" },
            };

            Random rand = new Random();
            List<string> words = new List<string>(wordHints.Keys);

            string secretWord = words[rand.Next(words.Count)];
            string hint = wordHints[secretWord];
            char[] guessed = new string('_', secretWord.Length).ToCharArray();
            int attempts = 3;
            HashSet<char> usedLetters = new HashSet<char>();
            
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔════════════════════════════════════╗");
            Console.WriteLine("║           Ш И Б Е Н И Ц Я          ║");
            Console.WriteLine("╚════════════════════════════════════╝\n");
            Console.ResetColor();

            Console.WriteLine($"Підказка: {hint}\n");

            while (attempts >= 0 && new string(guessed) != secretWord)
            {
                Console.WriteLine(HangmanStages[3 - attempts]);

                Console.WriteLine($"Слово: {string.Join(" ", guessed)}");
                Console.WriteLine($"Залишилось спроб: {attempts}");
                Console.WriteLine($"Використані літери: {string.Join(", ", usedLetters)}");
                Console.Write("Введи букву або слово: ");

                string inp = Console.ReadLine().ToLower();

                if (string.IsNullOrWhiteSpace(inp))
                {
                    Console.WriteLine("Пустий ввід!\n");
                    continue;
                }

                if (inp.Length > 1)
                {
                    if (inp == secretWord)
                    {
                        guessed = secretWord.ToCharArray();
                        break;
                    }
                    else
                    {
                        attempts--;
                        Console.WriteLine("Невірне слово! -1 спроба\n");
                        continue;
                    }
                }

                char input = inp[0];

                if (!char.IsLetter(input))
                {
                    Console.WriteLine("Введи англійську букву!\n");
                    continue;
                }

                if (usedLetters.Contains(input))
                {
                    Console.WriteLine("Ти вже вводив цю букву!\n");
                    continue;
                }

                usedLetters.Add(input);

                if (secretWord.Contains(input))
                {
                    Console.WriteLine("Вірно!\n");
                    for (int i = 0; i < secretWord.Length; i++)
                        if (secretWord[i] == input)
                            guessed[i] = input;
                }
                else
                {
                    attempts--;
                    Console.WriteLine("Невірно!\n");
                }
            }

            Console.Clear();
            if (new string(guessed) == secretWord)
            {
                Console.WriteLine(HangmanStages[0]);
                Console.WriteLine($"Вітаю! Ти вгадав слово: {secretWord}");
            }
            else
            {
                Console.WriteLine(HangmanStages[HangmanStages.Length - 1]);
                Console.WriteLine($"Поразка! Слово було: {secretWord}");
            }

            Console.WriteLine("\nНатисни будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}
