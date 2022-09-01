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
            }
            File.WriteAllText(text, "How did you get your name?,safe");
        }

        private static Dictionary<string,string> GetAllQuestions(string text)
        {
            CheckFile();

            return File.ReadLines(text)
                .Select(x => x.Split(','))
                .ToDictionary(split => split[1],
                split => split[0]);
        }

        public static void SeperateQuestions()
        {
            var questions = GetAllQuestions(text);
            
            foreach (var question in questions)
            {                
                AllQuestions.Add(question.Value);

                if (question.Key == "safe")
                {
                    SafeQuestions.Add(question.Value);
                }
                else
                {
                    NsfwQuestions.Add(question.Value);
                }
            }
        }

        public static void AddQs()
        {
            Console.Clear();
            Console.WriteLine($"Add question: ");
            var newQuestion = Console.ReadLine();

            using StreamWriter sw = File.AppendText(text);
            sw.Write($"\n{newQuestion}");

            SeperateQuestions();
        }
    }
}
