using System;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace FlowControl
{
    class FlowControl
    { 

        enum MenuChoice
        {
            Quit = 0,
            CalculateTicketPrice = 1,
            CalculateGroupTicketPrice = 2,
            RepeatText = 3,
            PrintEveryThirdWord = 4,
        }
    
        static void Main(String[] args)
        {

           

            bool isRunning = true;

            int? choice = 0;

            while(isRunning)
            {
                Console.WriteLine("Huvudmenyn");
                Console.WriteLine("Här kan du gör ett val för att kunna");

                Console.WriteLine($"{(int)MenuChoice.Quit} För att avsluta");


                Console.WriteLine($"{(int)MenuChoice.RepeatText} För att upprepa text");

                choice = InputInt();

                if (choice is null)
                {
                    continue;
                }

                MenuChoice numericChoice = (MenuChoice)choice;
              

                switch (numericChoice)
                {
                    case MenuChoice.Quit:
                         isRunning = false;
                        break;

                    case MenuChoice.RepeatText:
                            HandelRepeatText();
                        break;

                    default:
                            Console.WriteLine("Invalid choice");
                            Console.WriteLine();
                        break;
                }
            }
        }

        static int? InputInt()
        {
            string? input = Console.ReadLine();
            if (!int.TryParse(input, out int choice))
            {
                Console.WriteLine("Invalid choice. Please enter a number.");
                Console.WriteLine();
                return null;
            }

            return choice;
        }

        static void RepeatText(string text)
        {

            for (int i = 1; i <= 10; i++)
            {
                Console.Write($"{i.ToString()}.{text} ");
            }

            Console.WriteLine();
        }

        static bool IsValidText(string? text)
        {
            return !string.IsNullOrWhiteSpace(text);
        }

        static void HandelRepeatText()
        {
            Console.Write("Skriv din text här: ");
            string text = Console.ReadLine();

            if (!IsValidText(text))
            {
                Console.WriteLine("Text can not be empty.");
                Console.WriteLine();
                return;
            }

            RepeatText(text);
        }
    }
}
