using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        HashSet<string> validWords = LoadWords("words.txt");
        HashSet<string> usedWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        Console.WriteLine("Игра в слова с проверкой на действительность!");
        Console.WriteLine("Введите 'exit' для выхода из игры.");

        string lastWord = string.Empty;
        string currentPlayer = "Игрок 1";
        bool isGameActive = true;

        while (isGameActive)
        {
            char requiredChar = ' ';
            if (!string.IsNullOrEmpty(lastWord))
            {
                char lastChar = lastWord[lastWord.Length - 1];
                // Проверка на мягкий знак
                if (lastChar == 'ь')
                {
                    lastChar = lastWord[lastWord.Length - 2]; // Берем предпоследнюю букву
                }
                requiredChar = char.ToLower(lastChar);
                Console.WriteLine($"Следующее слово должно начинаться с буквы: '{requiredChar}'");
            }

            Console.Write($"{currentPlayer}, введите слово: ");
            string inputWord = Console.ReadLine();

            if (inputWord.ToLower() == "exit")
            {
                Console.WriteLine("Игра завершена.");
                break;
            }

            if (string.IsNullOrWhiteSpace(inputWord))
            {
                Console.WriteLine("Вы ввели пустое слово. Игра окончена.");
                break;
            }

            if (!validWords.Contains(inputWord.ToLower()))
            {
                Console.WriteLine($"Слово '{inputWord}' не существует. Игра окончена.");
                break;
            }

            if (usedWords.Contains(inputWord.ToLower()))
            {
                Console.WriteLine($"Слово '{inputWord}' уже было использовано. Игра окончена.");
                break;
            }

            if (!string.IsNullOrEmpty(lastWord))
            {
                char firstChar = char.ToLower(inputWord[0]);
                if (firstChar != requiredChar)
                {
                    Console.WriteLine($"Ошибка! Слово должно начинаться с буквы '{requiredChar}'. Игра окончена.");
                    break;
                }
            }

            // Добавляем слово в использованные
            usedWords.Add(inputWord.ToLower());
            lastWord = inputWord;
            currentPlayer = currentPlayer == "Игрок 1" ? "Игрок 2" : "Игрок 1";
        }
    }

    static HashSet<string> LoadWords(string filePath)
    {
        HashSet<string> words = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        try
        {
            foreach (var line in File.ReadLines(filePath))
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    words.Add(line.Trim());
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при загрузке слов: {ex.Message}");
        }
        return words;
    }
}