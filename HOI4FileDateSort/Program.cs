// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
using HOI4FileDateSort;
using Microsoft.Extensions.Configuration;
using System;

class Program
{
    static void Main(string[] args)
    {
        string userInput;

        var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

        string gamePath = configuration["Directories:gamePath"];
        string modPath = configuration["Directories:modPath"];
        string groupsFile = configuration["Files:groupsFile"];
        string crossCheckFile = configuration["Files:crossCheckFile"];

        //Console.WriteLine(gamePath);
        //Console.WriteLine(modPath);
        HOI4FileDateSort.FileSystem fs = new HOI4FileDateSort.FileSystem(gamePath, modPath, groupsFile, crossCheckFile);

        Dictionary<string, bool> allowedExtensions = new Dictionary<string, bool>();
        allowedExtensions.Add("*.txt", true);
        allowedExtensions.Add("*.gui", true);

        while (true)
        {
            userInput = Console.ReadLine();
            if (userInput == "q")
            {
                Console.WriteLine("Quitting.");
                break;
            }
            if (userInput == "get")
            {
                List<HOI4FileDateSort.GameFile> gameFiles = fs.GetGameFiles(allowedExtensions);
                gameFiles = Sorter.SortDescending(gameFiles);

                List<HOI4FileDateSort.GameFile> modFiles = fs.GetModFiles(allowedExtensions);
                modFiles = Sorter.SortDescending(modFiles);

                foreach (GameFile gameFile in gameFiles)
                {
                    gameFile.GetIsolatedPath(gamePath, modPath, "game");
                }

                foreach (GameFile modFile in modFiles)
                {
                    modFile.GetIsolatedPath(gamePath, modPath, "mod");
                }

                Console.WriteLine("Paths isolated.");

                Dictionary<GameFile, GameFile> crossedFiles = new Dictionary<GameFile, GameFile>();

                crossedFiles = Sorter.CrossCheckFiles(gameFiles, modFiles);

                fs.WriteGroupsToFile(Sorter.FormatGroups(gameFiles));
                fs.WriteCrossCheckToFile(Sorter.FormatCrossCheck(crossedFiles, true));
            }

        }

    }


}