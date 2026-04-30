using System;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace FlowControl
{
    class FlowControl
    {
        private const int MinimumWordCount = 3;

        private enum MenuChoice
        {
            Quit = 0,
            TicketPrice = 1,
            GroupTicketPrice = 2,
            RepeatText = 3,
            PrintEveryThirdWord = 4,
        }

        private enum TicketPriceType
        {
            FreeChild,
            Youth,
            Adult,
            Senior,
            FreeSenior,
        }

        private sealed record TicketPrice(
            TicketPriceType Type,
            string PriceType,
            int Price
        );


        private const int FreeChildMaximumAge = 5;
        private const int YouthMaximumAge = 19;
        private const int SeniorMinimumAge = 65;
        private const int FreeSeniorMinimumAge = 100;

        private const int FreePrice = 0;
        private const int YouthPrice = 80;
        private const int AdultPrice = 120;
        private const int SeniorPrice = 90;


        static void Main(String[] args)
        {

            bool isRunning = true;

            int? choice = 0;

            while(isRunning)
            {
                Console.WriteLine("Huvudmenyn");
                Console.WriteLine("Här kan du gör ett val för att kunna");

                Console.WriteLine($"{(int)MenuChoice.Quit} För att avsluta");
                Console.WriteLine($"{(int)MenuChoice.TicketPrice} Visa biljet pris");
                Console.WriteLine($"{(int)MenuChoice.GroupTicketPrice} Visa biljet pris för en group");

                Console.WriteLine($"{(int)MenuChoice.RepeatText} För att upprepa text");
                Console.WriteLine($"{(int)MenuChoice.PrintEveryThirdWord} Skriv in dina ord här med mellanslag i mellan");

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

                    case MenuChoice.TicketPrice:
                        HandleTicketPrice();
                        break;

                    case MenuChoice.GroupTicketPrice:
                        HandleGroupTicketPrice();
                        break;

                    case MenuChoice.RepeatText:
                            HandelRepeatText();
                        break;

                    case MenuChoice.PrintEveryThirdWord:
                        HandleEveryThirdWord();
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

        static TicketPrice CalculateTicketPrice(int age)
        { 

            if (age < FreeChildMaximumAge)
            {
                return new TicketPrice(
                    TicketPriceType.FreeChild,
                    GetTicketPriceDescription(TicketPriceType.FreeChild),
                    FreePrice
                );
            }

            if (age > FreeSeniorMinimumAge)
            {
                return new TicketPrice(
                    TicketPriceType.FreeSenior,
                    GetTicketPriceDescription(TicketPriceType.FreeSenior),
                    FreePrice
                );
            }

            if (age <= YouthMaximumAge)
            {
                return new TicketPrice(
                    TicketPriceType.Youth,
                    GetTicketPriceDescription(TicketPriceType.Youth),
                    YouthPrice
                );
            }

            if (age >= SeniorMinimumAge)
            {
                return new TicketPrice(
                    TicketPriceType.Senior,
                    GetTicketPriceDescription(TicketPriceType.Senior),
                    SeniorPrice
                );
            }

            return new TicketPrice(
                TicketPriceType.Adult,
                GetTicketPriceDescription(TicketPriceType.Adult),
                AdultPrice
            );
        }

        static void HandleTicketPrice()
        {
            Console.Write("Skriv in ålder: ");

            int? age = InputInt();

            if (age is null)
            {
                Console.WriteLine("Age is invalid");
                return;
            }

            var TicketPrice = CalculateTicketPrice(age.Value);


            Console.WriteLine($"{TicketPrice.PriceType}: {TicketPrice.Price} ");

        }


        static int CalculateGroupTicketPrice(int[] ages)
        {
           
            int groupPrice = 0;

            foreach (int age in ages)
            {
                var TicketPrice = CalculateTicketPrice(age);

                groupPrice += TicketPrice.Price;

            }

            return groupPrice;
        }

        static void HandleGroupTicketPrice()
        {
            Console.Write("Skriv antalet bio besökare: ");

            int? visitors = InputInt();

            if (visitors is null)
            {
                Console.WriteLine("visitors is invalid");
                return;
            }

            int? age;
            int[] ages = new int[visitors.Value];

            for (int i = 0; i < visitors; i++)
            {
                Console.Write("Skriv in ålder: ");
                age = InputInt();
                if (age is null)
                {
                    Console.WriteLine("Age is invalid");
                    return;
                }

                ages[i] = age.Value;

            }


            int groupPrice = CalculateGroupTicketPrice(ages);

            Console.WriteLine();
            Console.WriteLine($"Antalet personer {visitors} Total kostand {groupPrice}kr");
            Console.WriteLine();

        }

        static void RepeatText(string text)
        {

            for (int i = 1; i <= 10; i++)
            {
                Console.Write($"{i.ToString()}.{text} ");
            }

            Console.WriteLine();
        }

        static bool IsValidText(string? input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }

        static void HandelRepeatText()
        {
            Console.Write("Skriv din text här: ");
            string? input = Console.ReadLine();

            if (!IsValidText(input))
            {
                Console.WriteLine("Text can not be empty.");
                Console.WriteLine();
                return;
            }
            

            RepeatText(input);
        }


        static string[] GetWords(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return [];
            }

            return input.Split(
                   ' ',
                   StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries
               );
        }

        static void HandleEveryThirdWord()
        {
            Console.Write($"Skriv in minst {MinimumWordCount} ord här: ");
            Console.WriteLine();

            string? text = Console.ReadLine();


            string[] words = GetWords(text);

            if (words.Length < MinimumWordCount)
            {
                Console.Write("Invalid number of words");
                Console.WriteLine();
            }

            for (int i = MinimumWordCount - 1; i < words.Length; i += MinimumWordCount)
            {
                Console.WriteLine(words[i]);
            }

            Console.WriteLine();
        }

        static string GetTicketPriceDescription(TicketPriceType type)
        {
            return type switch
            {
                TicketPriceType.FreeChild => "Barn under 5 år: gratis",
                TicketPriceType.Youth => "Ungdomspris",
                TicketPriceType.Adult => "Standardpris",
                TicketPriceType.Senior => "Pensionärspris",
                TicketPriceType.FreeSenior => "Person över 100 år: gratis",
                _ => "Okänd pristyp",
            };
        }
    }
}
