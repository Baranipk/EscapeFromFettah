using System;
using System.IO;

namespace escape_from_fettah.Managers
{
    public static class SaveManager
    {
        private static string _saveFolder = "Saves";
        private static string _savePath = Path.Combine("Saves", "save.dat");

        public static int MaxScore { get; set; } = 0;
        public static int LastScore { get; set; } = 0;
        public static float SfxVolume { get; set; } = 1.0f;
        public static float MusicVolume { get; set; } = 0.5f;
        public static int CurrentTrack { get; set; } = 1;

        public static void Load()
        {
            if (File.Exists(_savePath))
            {
                try
                {
                    string[] lines = File.ReadAllLines(_savePath);
                    if (lines.Length >= 5)
                    {
                        MaxScore = int.Parse(lines[0]);
                        LastScore = int.Parse(lines[1]);
                        SfxVolume = float.Parse(lines[2]);
                        MusicVolume = float.Parse(lines[3]);
                        CurrentTrack = int.Parse(lines[4]);
                    }
                    else if (lines.Length >= 4)
                    {
                        MaxScore = int.Parse(lines[0]);
                        SfxVolume = float.Parse(lines[1]);
                        MusicVolume = float.Parse(lines[2]);
                        CurrentTrack = int.Parse(lines[3]);
                    }
                }
                catch { }
            }
        }

        public static void Save()
        {
            try
            {
                if (!Directory.Exists(_saveFolder))
                {
                    Directory.CreateDirectory(_saveFolder);
                }
                string content = $"{MaxScore}\n{LastScore}\n{SfxVolume}\n{MusicVolume}\n{CurrentTrack}";
                File.WriteAllText(_savePath, content);
            }
            catch { }
        }
    }
}
