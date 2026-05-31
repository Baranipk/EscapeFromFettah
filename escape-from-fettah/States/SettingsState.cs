using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using escape_from_fettah.Managers;
using System;

namespace escape_from_fettah.States
{
    public class SettingsState : State
    {
        private int _selectedIndex = 0;
        private const int MaxIndex = 3;

        public SettingsState(GraphicsDevice graphicsDevice, GameStateManager stateManager)
            : base(graphicsDevice, stateManager)
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (InputManager.IsKeyPressed(Keys.W) || InputManager.IsKeyPressed(Keys.Up))
                _selectedIndex = Math.Max(0, _selectedIndex - 1);
            if (InputManager.IsKeyPressed(Keys.S) || InputManager.IsKeyPressed(Keys.Down))
                _selectedIndex = Math.Min(MaxIndex, _selectedIndex + 1);

            bool rightPressed = InputManager.IsKeyPressed(Keys.D) || InputManager.IsKeyPressed(Keys.Right);
            bool leftPressed = InputManager.IsKeyPressed(Keys.A) || InputManager.IsKeyPressed(Keys.Left);

            if (rightPressed || leftPressed)
            {
                if (_selectedIndex == 0) // SFX
                {
                    float newVol = SaveManager.SfxVolume + (rightPressed ? 0.05f : -0.05f);
                    SaveManager.SfxVolume = (float)Math.Round(Math.Clamp(newVol, 0f, 1f) * 20f) / 20f;
                    AudioManager.ApplySettings();
                    if (AudioManager.HopSound != null) AudioManager.HopSound.Play(); // Test sound
                }
                else if (_selectedIndex == 1) // Music
                {
                    float newVol = SaveManager.MusicVolume + (rightPressed ? 0.05f : -0.05f);
                    SaveManager.MusicVolume = (float)Math.Round(Math.Clamp(newVol, 0f, 1f) * 20f) / 20f;
                    AudioManager.ApplySettings();
                }
                else if (_selectedIndex == 2) // Track
                {
                    SaveManager.CurrentTrack = (SaveManager.CurrentTrack + (rightPressed ? 1 : 2)) % 3; // 0, 1, 2 loop
                    AudioManager.ApplySettings();
                }
            }

            if (InputManager.IsKeyPressed(Keys.Enter) || InputManager.IsKeyPressed(Keys.Space))
            {
                if (_selectedIndex == 3) // Back
                {
                    SaveManager.Save();
                    _stateManager.ChangeState(new MenuState(_graphicsDevice, _stateManager));
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(Color.DarkSlateBlue);
            
            if (Art.Font != null)
            {
                spriteBatch.DrawString(Art.Font, "SETTINGS", new Vector2(100, 100), Color.White);

                string[] options = new string[4];
                options[0] = $"SFX Volume: {Math.Round(SaveManager.SfxVolume * 100)}%";
                options[1] = $"Music Volume: {Math.Round(SaveManager.MusicVolume * 100)}%";
                
                string trackName = SaveManager.CurrentTrack == 0 ? "Off" : $"Music {SaveManager.CurrentTrack}";
                options[2] = $"Music Track: < {trackName} >";
                options[3] = "Back to Menu";

                for (int i = 0; i <= MaxIndex; i++)
                {
                    Color color = _selectedIndex == i ? Color.Yellow : Color.White;
                    string prefix = _selectedIndex == i ? "> " : "  ";
                    spriteBatch.DrawString(Art.Font, prefix + options[i], new Vector2(100, 200 + (i * 50)), color);
                }

                string controls = "W/S: Navigate   A/D: Adjust   ENTER: Select";
                spriteBatch.DrawString(Art.Font, controls, new Vector2(100, 500), Color.Gray);
            }
        }
    }
}
