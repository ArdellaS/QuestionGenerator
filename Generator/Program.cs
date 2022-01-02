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

        public static string text = @"c:\temp\Questions.txt";


        public static void Main(string[] args)

        {

            if (!File.Exists(text))

            {

                using (FileStream fs = File.Create(text))

                {

                    byte[] initialQuestion = new UTF8Encoding(true).GetBytes("What is the meaning behind your name?");


                    fs.Write(initialQuestion, 0, initialQuestion.Length);

                }

            }


            GenerateQs(text);

            Console.WriteLine("Bye!");

        }

        public static void GenerateQs(string text)

        {


            var rand = new Random();

            var lines = File.ReadAllLines(text);

            lines = lines.OrderBy(x => rand.Next()).ToArray();


            foreach (var line in lines)

            {

                Console.Clear();

                Console.WriteLine(line);

                if (!Continue())

                {

                    break;

                }

            }


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

            Console.WriteLine($"Another Question (y) Add Question (a) y/n");

            var c = Console.ReadKey().KeyChar.ToString().ToLower();


            if (c is "y")

            {

                return true;

            }

            if (c is "a")

            {

                AddQs(text);

                return true;

            }

            return false;

        }

    }

}



