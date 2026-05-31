using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using escape_from_fettah.Managers;

namespace escape_from_fettah.States
{
    public class GameOverState : State
    {
        private int _finalScore;

        public GameOverState(GraphicsDevice graphicsDevice, GameStateManager stateManager, int score)
            : base(graphicsDevice, stateManager)
        {
            _finalScore = score;
            
            SaveManager.LastScore = _finalScore;
            if (_finalScore > SaveManager.MaxScore)
            {
                SaveManager.MaxScore = _finalScore;
            }
            SaveManager.Save();
        }

        public override void Update(GameTime gameTime)
        {
            if (InputManager.IsKeyPressed(Keys.Enter) || InputManager.IsKeyPressed(Keys.Space))
            {
                _stateManager.ChangeState(new MenuState(_graphicsDevice, _stateManager));
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(Color.DarkRed);
            
            if (Art.Font != null)
            {
                string title = "GAME OVER";
                string scoreText = $"Final Score: {_finalScore}";
                string maxScoreText = $"Max Score: {SaveManager.MaxScore}";
                string instText = "Press ENTER to return to Menu";

                spriteBatch.DrawString(Art.Font, title, new Vector2(100, 100), Color.White);
                spriteBatch.DrawString(Art.Font, scoreText, new Vector2(100, 200), Color.LightGray);
                spriteBatch.DrawString(Art.Font, maxScoreText, new Vector2(100, 250), Color.Gold);
                spriteBatch.DrawString(Art.Font, instText, new Vector2(100, 350), Color.Gray);
            }
        }
    }
}
