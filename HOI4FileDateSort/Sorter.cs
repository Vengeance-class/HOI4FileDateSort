using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOI4FileDateSort
{
    public static class Sorter
    {
        public static List<GameFile> SortDescending(List<GameFile> gameFiles)
        {
            gameFiles.Sort(delegate (GameFile file1, GameFile file2) { return DateTime.Compare(file2.lastModified, file1.lastModified); });
            Console.WriteLine("Files sorted in descending order.");
            return gameFiles;
        }

        public static List<GameFile> SortAscending(List<GameFile> gameFiles)
        {
            gameFiles.Sort(delegate (GameFile file1, GameFile file2) { return DateTime.Compare(file1.lastModified, file2.lastModified); });
            Console.WriteLine("Files sorted in ascending order.");
            return gameFiles;
        }

        public static string FormatGroups(List<GameFile> gameFiles)
        {
            string groupsFormatted;

            string groupKey;

            StringBuilder sb = new StringBuilder();

            Dictionary<string, List<GameFile>> groups = new Dictionary<string, List<GameFile>>();

            for (int i = 0; i < gameFiles.Count; i++)
            {
                groupKey = gameFiles[i].lastModified.Year.ToString() + "." + gameFiles[i].lastModified.Month.ToString() + "." + gameFiles[i].lastModified.Day.ToString();
                if (groups.ContainsKey(groupKey))
                {
                    groups[groupKey].Add(gameFiles[i]);
                }

                else
                {
                    groups.Add(groupKey, new List<GameFile>());
                    groups[groupKey].Add(gameFiles[i]);
                }
            }

            foreach (KeyValuePair<string, List<GameFile>> group in groups)
            {
                sb.Append("============\n");
                sb.Append($"Group Updated On {group.Key}:\n");
                foreach (GameFile fileFromGroup in group.Value)
                {
                    sb.Append($"{fileFromGroup.path} - {fileFromGroup.lastModified}");
                    sb.Append("\n");
                }
            }

            groupsFormatted = sb.ToString();

            Console.WriteLine("Files list formatted into readable string.");

            return groupsFormatted;
        }

        public static Dictionary<GameFile, GameFile> CrossCheckFiles(List<GameFile> gameFiles, List<GameFile> modFiles)
        {

            Dictionary<GameFile, GameFile> crossedFiles = new Dictionary<GameFile, GameFile>();
            for (int i = 0; i < gameFiles.Count; i++)
            {
                for (int x = 0; x < modFiles.Count; x++)
                {
                    GameFile gameFile = gameFiles[i];
                    GameFile modFile = modFiles[x];

                    if (gameFile.isolatedPath == modFile.isolatedPath)
                    {
                        crossedFiles.Add(gameFile, modFile);
                    }

                }
            }

            Console.WriteLine($"Cross-check complete. Files overlapping: {crossedFiles.Keys.Count}");

            return crossedFiles;
        }

        public static string FormatCrossCheck(Dictionary<GameFile, GameFile> crossCheckedFiles, bool ignoreSimilarTimes)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Overlapping Files:\n\n=========\n");

            if (ignoreSimilarTimes)
            {
                sb.Append("Please note: Similar Edit Times are ignored (made on the same hour)! \n\n");
            }

            foreach (KeyValuePair<GameFile, GameFile> crossedFile in crossCheckedFiles)
            {

                if (crossedFile.Key.lastModified.Day == crossedFile.Value.lastModified.Day &&
                        crossedFile.Key.lastModified.Hour == crossedFile.Value.lastModified.Hour)
                {
                    if (!ignoreSimilarTimes)
                    {
                        sb.Append($"\n\nREPORT:\nGameFile: {crossedFile.Key.isolatedPath} - LastModified: {crossedFile.Key.lastModified} \nModFile: {crossedFile.Value.isolatedPath} - LastModified: {crossedFile.Value.lastModified}");
                    }
                }
                else
                {
                    sb.Append($"\n\nREPORT:\nGameFile: {crossedFile.Key.isolatedPath} - LastModified: {crossedFile.Key.lastModified} \nModFile: {crossedFile.Value.isolatedPath} - LastModified: {crossedFile.Value.lastModified}");
                }

                    
                if (crossedFile.Key.lastModified < crossedFile.Value.lastModified)
                {
                    // Gamefile was modified earlier
                    // ModFile is latest
                    sb.Append("\n --- Mod File is latest.");
                }
                else
                {
                    // Modfile was modified earlier
                    // GameFile is latest
                    if (crossedFile.Key.lastModified.Day == crossedFile.Value.lastModified.Day && 
                        crossedFile.Key.lastModified.Hour == crossedFile.Value.lastModified.Hour)
                    {
                        // It could be a false positive
                        if (!ignoreSimilarTimes) 
                        {
                            sb.Append("\n --- Vanilla File is latest. WARNING: COULD BE A FALSE POSITIVE! (the times are very similar!)\n\n");
                        }
                        else
                        {
                            // just ignore it.
                        }
                    }
                    else
                    {
                        sb.Append("\n --- Vanilla File is latest!");
                    }
                    

                }
            }

            return sb.ToString();
        }
    }

}
