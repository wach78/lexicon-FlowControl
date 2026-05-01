using System;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace FlowControl
{
    class FlowControl
    {
        // Selects one word for each interval of words.
        private const int WordSelectionInterval = 3;

        private const int RepeatCount = 10;
        private const int FirstRepeatNumber = 1;


        // Age limits used when calculating ticket price
        private const int FreeChildMaximumAge = 5;
        private const int YouthMaximumAge = 19;
        private const int SeniorMinimumAge = 65;
        private const int FreeSeniorMinimumAge = 100;

        // Ticket prices in SEK used when calculating cinema admission.
        private const int FreePrice = 0;
        private const int YouthPrice = 80;
        private const int AdultPrice = 120;
        private const int SeniorPrice = 90;

        private enum MenuChoice
        {
            Quit = 0,
            TicketPrice = 1,
            GroupTicketPrice = 2,
            RepeatText = 3,
            PrintWordInterval = 4,
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

        static void Main(String[] args)
        {

            bool isRunning = true;

            int? choice = 0;

            while (isRunning)
            {
                Console.WriteLine();
                Console.WriteLine("Huvudmeny");
                Console.WriteLine("----------");
                Console.WriteLine($"{(int)MenuChoice.Quit}. Avsluta");
                Console.WriteLine($"{(int)MenuChoice.TicketPrice}. Visa biljettpris");
                Console.WriteLine($"{(int)MenuChoice.GroupTicketPrice}. Visa biljettpris för grupp");
                Console.WriteLine($"{(int)MenuChoice.RepeatText}. Upprepa text");
                Console.WriteLine($"{(int)MenuChoice.PrintWordInterval}. Skriv ut vart {WordSelectionInterval}:e ord");
                Console.Write("Välj ett alternativ: ");

                choice = InputInt("Ogiltigt val. Ange ett nummer.");

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
                        HandleRepeatText();
                        break;

                    case MenuChoice.PrintWordInterval:
                        HandleWordInterval();
                        break;

                    default:
                        Console.WriteLine("Invalid choice");
                        Console.WriteLine();
                        break;
                }
            }
        }

        static int? InputInt(string errorMessage)
        {
            string? input = Console.ReadLine();
            if (!int.TryParse(input, out int choice))
            {
                Console.WriteLine(errorMessage);
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

            int? age = InputInt("Ange en giltig ålder.");

            if (age is null || !IsNotNegative(age.Value))
            {
                Console.WriteLine("Åldern är ogiltig");
                return;
            }

            var TicketPrice = CalculateTicketPrice(age.Value);


            Console.WriteLine($"{TicketPrice.PriceType}: {TicketPrice.Price}kr");

        }


        static int CalculateGroupTicketPrice(int[] ages)
        {

            int groupPrice = 0;

            foreach (int age in ages)
            {
                var ticketPrice = CalculateTicketPrice(age);

                groupPrice += ticketPrice.Price;

            }

            return groupPrice;
        }

        static void HandleGroupTicketPrice()
        {
            Console.Write("Skriv antalet bio besökare: ");

            int? visitors = InputInt("Ange ett giltigt antal biobesökare.");

            if (visitors is null || !IsPositive(visitors.Value))
            {
                Console.WriteLine("Ange ett giltigt antal biobesökare");
                return;
            }

            int? age;
            int[] ages = new int[visitors.Value];

            for (int i = 0; i < visitors; i++)
            {
                Console.Write("Skriv in ålder: ");
                age = InputInt("Ange en giltig ålder.");

                if (age is null || !IsNotNegative(age.Value))
                {
                    Console.WriteLine("Åldern är ogiltig");
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
            string separator = ", ";
            for (int i = FirstRepeatNumber; i <= RepeatCount; i++)
            {

                if (i % RepeatCount == 0)
                {
                    separator = ".";
                }


                Console.Write($"{i}.{text}{separator}");
            }

            Console.WriteLine();
        }

        static bool IsValidText(string? input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }

        static void HandleRepeatText()
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

        static void HandleWordInterval()
        {
            Console.Write($"Skriv in minst {WordSelectionInterval} ord här: ");

            string? text = Console.ReadLine();


            string[] words = GetWords(text);

            if (words.Length < WordSelectionInterval)
            {
                Console.Write($"Texten måste innehålla minst {WordSelectionInterval} ord.");
                Console.WriteLine();
                return;
            }

            for (int i = WordSelectionInterval - 1; i < words.Length; i += WordSelectionInterval)
            {
                Console.WriteLine(words[i]);
            }
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

        static bool IsNotNegative(int vaule)
        {
            return vaule >= 0;
        }

        static bool IsPositive(int vaule)
        {
            return vaule > 0;
        }
    }
}
