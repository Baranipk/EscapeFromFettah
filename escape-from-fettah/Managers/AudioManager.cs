using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;

namespace escape_from_fettah.Managers
{
    public static class AudioManager
    {
        public static SoundEffect HopSound { get; private set; }
        public static SoundEffect HitSound { get; private set; }
        
        public static Song Music1 { get; private set; }
        public static Song Music2 { get; private set; }

        public static void Load(ContentManager content)
        {
            HopSound = content.Load<SoundEffect>("Hop");
            HitSound = content.Load<SoundEffect>("Hit");
            
            Music1 = content.Load<Song>("music1");
            Music2 = content.Load<Song>("music2");
            
            ApplySettings();
        }

        private static int _activeTrack = -1;

        public static void ApplySettings()
        {
            SoundEffect.MasterVolume = SaveManager.SfxVolume;
            MediaPlayer.Volume = SaveManager.MusicVolume;
            MediaPlayer.IsRepeating = true;
            
            if (_activeTrack != SaveManager.CurrentTrack)
            {
                _activeTrack = SaveManager.CurrentTrack;
                
                if (SaveManager.CurrentTrack == 0)
                {
                    MediaPlayer.Stop();
                }
                else if (SaveManager.CurrentTrack == 1)
                {
                    MediaPlayer.Play(Music1);
                }
                else if (SaveManager.CurrentTrack == 2)
                {
                    MediaPlayer.Play(Music2);
                }
            }
        }
    }
}
