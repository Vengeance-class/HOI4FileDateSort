using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOI4FileDateSort
{
    public class GameFile
    {
        public string fileName;
        public string path;

        public string isolatedPath;

        public DateTime lastModified;
        public GameFile(string path) 
        { 
            this.path = path;
            this.fileName = Path.GetFileName(path);
            GetLastModified();
        }

        private void GetLastModified()
        {
            this.lastModified = System.IO.File.GetLastWriteTime(this.path);
        }
        /// <summary>
        /// Returns a path that is relative to the root of the game structure 
        /// (so, root folder for mod, or root folder for game, without all the absolute prerequisite path)
        /// </summary>
        /// <returns></returns>
        public string GetIsolatedPath(string gamePath, string modPath, string mode)
        {
            if (mode == "game")
            {
                this.isolatedPath = Path.GetRelativePath(gamePath, path);
                return isolatedPath;
            }
            else if (mode == "mod")
            {
                this.isolatedPath = Path.GetRelativePath(modPath, path);
                return isolatedPath;
            }

            else
            {
                return null;
            }
            
        }

    }
}
