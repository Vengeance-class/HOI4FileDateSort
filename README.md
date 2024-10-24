A HOI4 (Hearts of Iron 4) tool that I use to compare game files and mod files lastModified date.

It's written in C#.

When the game is updated, it becomes very tedious to manually keep track of all the files that have been updated in the latest patch. 
So to help with that process, this tool will do these things:
1. It will scan all the files and put them in groups that reflect the day on which the changes were made (so, files that were last modified on October 1st, 2024 — they are in their own group, but files from September 3rd are put in another group). This helps keep track of how big the update actually was, as sometimes it's a very small hotfix or something like that;
2. It will scan all the mod files and cross-check them with the vanilla files, and build a report that tells you which file is latest. If your mod's file is latest, then you're golden. If not, then vanilla file was modified AFTER you last modified your mod file, which might indicate the need to update the mod file. 

Obviously it's a very small utility and it's pretty much bootstrap. Maybe I'll refactor the thing when I get the time and/or motivation.
