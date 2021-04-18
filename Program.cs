using System;
using System.IO;
using System.Threading;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace QuestionGenerator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var text = @"c:\temp\Questions.txt";
            var text = @"C:\Users\harle\source\repos\QuestionGenerator\QuestionGenerator\Questions.txt";

            if (!File.Exists(text))
            {
                using (FileStream fs = File.Create(text))
                {
                    byte[] initialQuestion = new UTF8Encoding(true).GetBytes("What is the meaning behind your name?");

                    fs.Write(initialQuestion, 0, initialQuestion.Length);
                }
            }

            var isNumber = true;
            int response = 0;

            while (!isNumber || response != 3)
            {
                Console.Clear();
                Console.WriteLine("Do you want to: \n1.) Add question\n2.) Read questions\n3.) Exit");

                int.TryParse(Console.ReadLine(), out response);

                while (response != 1 && response != 2 && response != 3)
                {
                    Console.Clear();
                    Console.WriteLine("Invalid choice.");
                    Thread.Sleep(1000);
                    Console.Clear();
                    Console.WriteLine("Do you want to \n1.)Add question\n2.) Read questions\n3.) Exit");
                    isNumber = int.TryParse(Console.ReadLine(), out response);
                }

                switch (response)
                {
                    case 1:
                        AddQs(text);
                        break;
                    case 2:
                        GenerateQs(text);
                        break;
                    case 3:
                        break;
                    default:
                        break;
                }
            }
            Console.WriteLine("Bye!");
        }
        public static void GenerateQs(string text)
        {
            var task = "reading questions";

            var rand = new Random();
            var lines = File.ReadAllLines(text);
            var count = 0;
            lines = lines.OrderBy(x => rand.Next()).ToArray();

            do
            {
                Console.Clear();
                Console.WriteLine(lines[count]);
                count++;
            } while (Continue(task));
        }

        public static void AddQs(string text)
        {
            var task = "adding questions";

            do
            {
                Console.Clear();
                Console.WriteLine("Add question:");
                var newQuestion = Console.ReadLine();

                using (StreamWriter sw = File.AppendText(text))
                {
                    sw.Write($"\n{newQuestion}");
                }
            } while (Continue(task));
        }
        public static bool Continue(string task)
        {
            char c;

            do
            {
                Console.WriteLine($"Continue {task}? y/n");
                c = Console.ReadKey().KeyChar;
                if (c == 'n' || c == 'N')
                {
                    return false;
                }
            } while (c != 'y' && c != 'Y');
            return true;
        }
    }
}
