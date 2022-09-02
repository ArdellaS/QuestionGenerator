using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace QuestionGenerator
{
    public static class FileManager
    {
        public static string text = @"C:/Temp/Questions.txt";
        public static List<string> SafeQuestions = new List<string>();
        public static List<string> NsfwQuestions = new List<string>();
        public static List<string> AllQuestions = new List<string>();

        public static async void CheckFile()
        {
            if (!File.Exists(text))
            {
                await using FileStream fs = File.Create(text);
                File.WriteAllText(text, "How did you get your name?,safe");
            }            
        }

        private static Dictionary<string,string> GetAllQuestions(string text)
        {
            CheckFile();
            var rand = new Random();

            return File.ReadLines(text)
                .OrderBy(x => rand.Next())
                .Select(x => x.Split(','))
                .ToDictionary(split => split[0],
                    split => split[1]);
        }

        public static void SeperateQuestions()
        {
            var questions = GetAllQuestions(text);
            
            foreach (var question in questions)
            {                
                AllQuestions.Add(question.Key);

                if (question.Value == "safe")
                {
                    SafeQuestions.Add(question.Key);
                }
                else
                {
                    NsfwQuestions.Add(question.Key);
                }
            }
        }

        public static void AddQs()
        {
            Console.Clear();
            Console.WriteLine($"Add question: ");
            var question = Console.ReadLine();

            while (string.IsNullOrEmpty(question))
            {
                Console.Clear();
                Console.WriteLine($"Please add question: ");
                question = Console.ReadLine();
            }

            Console.WriteLine("\nIs this safe? y/n");
            var response = char.ToLower(Console.ReadKey().KeyChar);

            while (!response.Equals('y') && !response.Equals('n'))
            {
                Console.Clear();
                Console.WriteLine($"{question}\nIs this safe? y/n");
                response = char.ToLower(Console.ReadKey().KeyChar);
            }

            switch (response)
            {
                case 'y':
                    question += ",safe";
                    break;
                case 'n':
                    question += ",nsfw";
                    break;
            }

            using StreamWriter sw = File.AppendText(text);
            sw.Write($"\n{question}");
        }
    }
}
