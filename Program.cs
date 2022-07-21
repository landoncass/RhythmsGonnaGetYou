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
                        viewAllBands(context);
                        break;
                    case "F":
                        findBand(context);
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
                newBand.IsSigned = PromptForBool("Are they signed to a record label? [Y/N] ");
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
                Console.WriteLine("What would you like to do with this band?\n (A)dd a new album \n (V)iew all albums by this artist \n (R)elease a band go from their label \n (S)ign a band to a label \n (Q)uit : ");
                var bandMenu = Console.ReadLine().ToUpper();
                switch (bandMenu)
                {
                    case "A":
                        var newAlbum = new Album();
                        newAlbum.Title = PromptForString("What is the name of the album you want to add? ");
                        newAlbum.IsExplicit = PromptForBool("Does the album have any explicit tracks? [Y/N]");
                        newAlbum.Band = foundBand;
                        Console.WriteLine($"What's the release date? (YYYY-MM-DD) ");
                        var ReleaseDate = DateTime.Parse(Console.ReadLine());
                        var inputValueUTC = ReleaseDate.ToUniversalTime();
                        newAlbum.ReleaseDate = inputValueUTC;
                        context.Albums.Add(newAlbum);
                        context.SaveChanges();

                        Console.WriteLine($"Do you want to add a song to {newAlbum.Title}? [Y/N] ");
                        var addSongsResponse = Console.ReadLine();
                        while (addSongsResponse == "Y")
                        {
                            var newSong = new Song();
                            newSong.Title = PromptForString("What is the name of the song? ");
                            newSong.TrackNumber = PromptForInteger("Which track number is it? ");
                            newSong.Duration = PromptForInteger("How long is the song? (00:00:00) ");
                            newSong.AlbumID = newAlbum.Id;
                            context.Songs.Add(newSong);
                            context.SaveChanges();

                            Console.WriteLine($"Do you want to add another song to {newAlbum.Title}? [Y/N] ");
                            addSongsResponse = Console.ReadLine();
                        }

                        break;
                    default:
                        break;
                }


                // Console.WriteLine($"Do you want to add an album to {foundBand.Name}? [Y/N] ");
                // var answer = Console.ReadLine();
                // if (answer == "Y")

            }
        }
    }
    private static void viewAllBands(RhythmsGonnaGetYouContext context)
    {
        Console.WriteLine();
        Console.WriteLine("These are the bands in our database: ");
        foreach (var viewBand in context.Bands)
        {
            Console.WriteLine($" - {viewBand.Name}");
        }
    }
}
}