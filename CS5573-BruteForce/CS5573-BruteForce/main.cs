using System;
using _CS5573_BruteForce.Driver;

namespace _CS5573_BruteForce
{
    class main
    {
        private static CommandDriver _commandProcessor;

        static void Main(string[] args)
        {
            Init();
            _commandProcessor.Process(args);

            while (true)
            {
                Console.WriteLine("Press any key to try again or 'q' to quit.");
                var input = Console.In.ReadLine();

                if (input.ToLower() == "q")
                {
                    break;
                }
                _commandProcessor.Process();
            }
        }

        public static void Init()
        {
            _commandProcessor = new CommandDriver();
        }

    }
}
