using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RhythmsGonnaGetYou
{
    class Program
    {
        static void DisplayGreeting()
        {
            Console.WriteLine("Welcome to the music database!");
        }

        static string PromptForString(string prompt)
        {
            Console.Write(prompt);
            var userInput = Console.ReadLine();

            return userInput;
        }

        static bool PromptForBool(string prompt)
        {
            Console.Write(prompt);
            var userInput = Console.ReadLine();

            if (userInput == "y" || userInput == "Y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static int PromptForInteger(string prompt)
        {
            Console.Write(prompt);
            int userInput;
            var isThisGoodInput = Int32.TryParse(Console.ReadLine(), out userInput);

            if (isThisGoodInput)
            {
                return userInput;
            }
            else
            {
                Console.WriteLine("Sorry, that isn't a valid input, I'm using 'No' as your answer.");
                return 'N';
            }
        }

        static void Main(string[] args)
        {
            var context = new RhythmsGonnaGetYouContext();

            var keepGoing = true;

            DisplayGreeting();

            while (keepGoing)
            {
                Console.WriteLine();
                Console.Write("What do you want to do?\n (A)dd a band:\n (V)iew all Bands:\n (F)ind a band\n or (Q)uit: ");

                var choice = Console.ReadLine().ToUpper();

                switch (choice)
                {
                    case "Q":
                        keepGoing = false;
                        break;
                    case "A":
                        addBand();
                        break;
                    case "V":
                        break;
                    case "F":
                        break;

                    default:
                        Console.WriteLine("Nope");
                        break;
                }
            }
        }
        static Band addBand()
        {
            var newBand = new Band();
            {
                var context = new RhythmsGonnaGetYouContext();

                newBand.Name = PromptForString("What is the name of the new band? ");
                newBand.CountryOfOrigin = PromptForString("What is the country of origin? ");
                newBand.NumberOfMembers = PromptForInteger("How many band members are there? ");
                newBand.Website = PromptForString("what's the band web address? ");
                newBand.Style = PromptForString("What music genre do they play? ");
                newBand.IsSigned = PromptForBool("Are they signed to a record label? ");
                newBand.ContactName = PromptForString("Who is the main contact? ");
                newBand.ContactPhoneNumber = PromptForString("What is the contact phone number? ");

                context.Bands.Add(newBand);
                context.SaveChanges();
            }
            return newBand;
        }
        private static void findBand(RhythmsGonnaGetYouContext context)
        {
            var name = PromptForString("What band are you looking for? ");

            Band foundBand = context.FindOneBand(name);

            if (foundBand == null)
            {
                Console.WriteLine("Band doesn't exist in this database");
            }
            else
            {
                var newAlbum = new Album();
                Console.WriteLine($"Do you want to add an album to {foundBand.Name}? [Y/N] ");
                var answer = Console.ReadLine();

                if (answer == "Y")
                {
                    newAlbum.Title = PromptForString("What is the name of the album you want to add? ");
                    newAlbum.IsExplicit = PromptForBool("Does the album have any explicit tracks? [Y/N]");
                    newAlbum.Band = foundBand;

                    Console.WriteLine($"What's the release date? (YYYY-MM-DD) ");
                    var ReleaseDate = DateTime.Parse(Console.ReadLine());

                    var inputValueUTC = ReleaseDate.ToUniversalTime();

                    newAlbum.ReleaseDate = inputValueUTC;
                    context.Albums.Add(newAlbum);
                    context.SaveChanges();
                }
            }
        }
        private static void viewAllBands(RhythmsGonnaGetYouContext context)
        {
            Console.WriteLine("These are our bands: ");
            foreach (var viewBand in context.Bands)
            {
                Console.WriteLine($" - {viewBand.Name}");
            }
        }
    }
}