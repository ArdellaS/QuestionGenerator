using System;
using System.Collections.Generic;

namespace QuestionGenerator
{
    public static class Generator
    {
        public static void GetQuestions()
        {
            var questions = GetQuestionList();

            foreach (var line in questions)
            {
                Console.Clear();
                Console.WriteLine($"{line} \n");

                if (!Continue())
                {
                    break;
                }
            }
        }

        public static bool Continue()
        {
            Console.WriteLine($"Next Question? (Y) Add Question (A)");
            var response = char.ToLower(Console.ReadKey().KeyChar);

            switch (response)
            {
                case 'y':
                    return true;
                case 'a':
                    FileManager.AddQs();
                    return true;
                default:
                    return false;
            }
        }

        private static List<string> GetQuestionList()
        {
            FileManager.SeperateQuestions();

            Console.WriteLine("What kind of questions do you want?\n1. All Questions\n2.Safe Questions\n3.Not Safe for For Questions");
            int response;
            var isNumber = int.TryParse(Console.ReadLine(), out response);

            while (isNumber && response < 3 && response > 0)
            {
                Console.WriteLine("What kind of questions do you want?\n1. All Questions\n2.Safe Questions\n3.Not Safe for For Questions");
            }

            switch (response)
            {
                case 1:
                    return FileManager.AllQuestions;
                case 2:
                    return FileManager.SafeQuestions;
                case 3:
                    return FileManager.NsfwQuestions;
                default:
                    return FileManager.AllQuestions;
            }

        }
    }
}
