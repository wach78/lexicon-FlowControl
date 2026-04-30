using System;

namespace FlowControl
{
    class FlowControl
    { 

        enum MenuChoice
        {
            Quit = 0,

        }
    
        static void Main(String[] args)
        {

           

            bool isRunning = true;

            int? choice = 0;

            while(isRunning)
            {



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
                return null;
            }

            return choice;
        }
    }
}
