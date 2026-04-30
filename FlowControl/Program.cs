using System;

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
    }
}
