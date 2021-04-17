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
            string text = @"c:\temp\Questions.txt";

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
            var rand = new Random();
            var len = File.ReadLines(text).Count();
            var done = new List<int>();
            do
            {
                Console.Clear();
                var qNum = rand.Next(1, len + 1);

                if (done.Count == len)
                {
                    Console.WriteLine("No new questions. Please add more.");
                    Thread.Sleep(2000);
                    break;
                }
                while (done.Contains(qNum))
                {
                    qNum = rand.Next(1, len+1);
                }

                var line = File.ReadLines(text).ElementAt(qNum - 1);
                Console.WriteLine(line);

                done.Add(qNum);
            } while (Continue());
        }
        public static void AddQs(string text)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Add question:");
                var newQuestion = Console.ReadLine();

                using (StreamWriter sw = File.AppendText(text))
                {
                    sw.Write($"\n{newQuestion}");
                }
            } while (Continue());
        }
        public static bool Continue()
        {
            char c;

            do
            {
                Console.WriteLine("Would you like to continue? y/n");
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
