using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HOI4FileDateSort
{
    public class FileSystem
    {
        private string gamePath;
        private string modPath;

        private string isolatedGamePath;
        private string isolatedModPath;

        private string groupsFile;
        private string crossCheckFile;

        public FileSystem(string gamePath, string modPath, string groupsFile, string crossCheckFile)
        {
            this.gamePath = gamePath;
            this.modPath = modPath;
            this.groupsFile = groupsFile;
            this.crossCheckFile = crossCheckFile;
        }
        /// <summary>
        /// extensions should be formatted like "*.*", so for txt it's "*.txt".
        /// </summary>
        /// <param name="allowedExtensions"></param>
        /// <returns></returns>
        public List<GameFile> GetGameFiles(Dictionary<string, bool> allowedExtensions)
        {

            string[] GetGameFilesExt(string extension)
            {
                string[] filesOfThisExtension;

                if (allowedExtensions[extension])
                {
                    // get all  files of this extension and put them in an array
                    filesOfThisExtension = Directory.GetFiles(gamePath, extension, SearchOption.AllDirectories);
                    return filesOfThisExtension;
                }
                else
                {
                    return null;
                }
                
            }

            List<string[]> allAllowedGameFiles = new List<string[]>();

            List<GameFile> gameFiles = new List<GameFile>();

            foreach (string extension in allowedExtensions.Keys)
            {
                string[] filesExt = GetGameFilesExt(extension);
                if (filesExt != null)
                {
                    allAllowedGameFiles.Add(filesExt);
                }
            }

            foreach (string[] allowedFiles in allAllowedGameFiles)
            {
                foreach (string allowedFile in allowedFiles)
                {
                    gameFiles.Add(new GameFile(allowedFile));
                }
            }

            Console.WriteLine("Game files retrieved.");

            return gameFiles;

        }

        public List<GameFile> GetModFiles(Dictionary<string, bool> allowedExtensions) 
        {
            string[] GetGameFilesExt(string extension)
            {
                string[] filesOfThisExtension;

                if (allowedExtensions[extension])
                {
                    // get all  files of this extension and put them in an array
                    filesOfThisExtension = Directory.GetFiles(modPath, extension, SearchOption.AllDirectories);
                    return filesOfThisExtension;
                }
                else
                {
                    return null;
                }

            }

            List<string[]> allAllowedGameFiles = new List<string[]>();

            List<GameFile> gameFiles = new List<GameFile>();

            foreach (string extension in allowedExtensions.Keys)
            {
                string[] filesExt = GetGameFilesExt(extension);
                if (filesExt != null)
                {
                    allAllowedGameFiles.Add(filesExt);
                }
            }

            foreach (string[] allowedFiles in allAllowedGameFiles)
            {
                foreach (string allowedFile in allowedFiles)
                {
                    gameFiles.Add(new GameFile(allowedFile));
                }
            }

            Console.WriteLine("Mod files retrieved.");

            return gameFiles;
        }

        public void WriteGroupsToFile(string content)
        {
            if (File.Exists(groupsFile))
            {
                using (StreamWriter outputFile = new StreamWriter(groupsFile))
                {
                    outputFile.WriteLine(content);
                }
            }
            else
            {
                File.Create(groupsFile);
                //File.
                using (StreamWriter outputFile = new StreamWriter(groupsFile))
                {
                    outputFile.WriteLine(content);
                }
            }
            Console.Write("Groups written to file.");
        }

        public void WriteCrossCheckToFile(string content)
        {
            if (File.Exists(crossCheckFile))
            {
                using (StreamWriter outputFile = new StreamWriter(crossCheckFile))
                {
                    outputFile.WriteLine(content);
                }
            }
            else
            {
                File.Create(crossCheckFile);
                using (StreamWriter outputFile = new StreamWriter(crossCheckFile))
                {
                    outputFile.WriteLine(content);
                }
            }
            Console.Write("CrossCheck written to file.");
        }
    }
}
