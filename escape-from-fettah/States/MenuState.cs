using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using escape_from_fettah.Managers;

namespace escape_from_fettah.States
{
    public class MenuState : State
    {
        private int _selectedIndex = 0;

        public MenuState(GraphicsDevice graphicsDevice, GameStateManager stateManager)
            : base(graphicsDevice, stateManager)
        {
            SaveManager.Load();
            AudioManager.ApplySettings(); // Ensure settings apply when returning to menu
        }

        public override void Update(GameTime gameTime)
        {
            if (InputManager.IsKeyPressed(Keys.W) || InputManager.IsKeyPressed(Keys.Up))
                _selectedIndex = 0;
            if (InputManager.IsKeyPressed(Keys.S) || InputManager.IsKeyPressed(Keys.Down))
                _selectedIndex = 1;

            if (InputManager.IsKeyPressed(Keys.Enter) || InputManager.IsKeyPressed(Keys.Space))
            {
                if (_selectedIndex == 0)
                {
                    _stateManager.ChangeState(new GameplayState(_graphicsDevice, _stateManager));
                }
                else if (_selectedIndex == 1)
                {
                    _stateManager.ChangeState(new SettingsState(_graphicsDevice, _stateManager));
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(Color.DarkBlue);
            
            if (Art.Font != null)
            {
                string title = "ESCAPE FROM FETTAH";
                string startText = _selectedIndex == 0 ? "> Start Game <" : "Start Game";
                string settingsText = _selectedIndex == 1 ? "> Settings <" : "Settings";
                
                string lastScoreText = $"Last Score: {SaveManager.LastScore}";
                string maxScoreText = $"Max Score: {SaveManager.MaxScore}";

                spriteBatch.DrawString(Art.Font, title, new Vector2(100, 100), Color.White);
                spriteBatch.DrawString(Art.Font, startText, new Vector2(100, 200), _selectedIndex == 0 ? Color.Yellow : Color.White);
                spriteBatch.DrawString(Art.Font, settingsText, new Vector2(100, 250), _selectedIndex == 1 ? Color.Yellow : Color.White);
                
                spriteBatch.DrawString(Art.Font, lastScoreText, new Vector2(100, 350), Color.LightGray);
                spriteBatch.DrawString(Art.Font, maxScoreText, new Vector2(100, 400), Color.Gold);
                
                string controls = "W/S: Navigate   ENTER: Select";
                spriteBatch.DrawString(Art.Font, controls, new Vector2(100, 500), Color.Gray);
            }
        }
    }
}
