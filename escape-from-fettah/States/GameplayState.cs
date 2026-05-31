using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using escape_from_fettah.Managers;
using escape_from_fettah.World;

namespace escape_from_fettah.States
{
    public class GameplayState : State
    {
        private WorldManager _world;

        public GameplayState(GraphicsDevice graphicsDevice, GameStateManager stateManager)
            : base(graphicsDevice, stateManager)
        {
            _world = new WorldManager();
        }

        public override void Update(GameTime gameTime)
        {
            _world.Update(gameTime);
            
            if (_world.Player.Lives <= 0)
            {
                _stateManager.ChangeState(new GameOverState(_graphicsDevice, _stateManager, _world.Score));
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _graphicsDevice.Clear(Color.Black);
            
            _world.Draw(spriteBatch);

            for (int i = 0; i < _world.Player.Lives; i++)
            {
                Rectangle lifeRect = new Rectangle(20 + (i * 45), 20, 40, 40);
                if (Art.StaticObstacleTexture != null)
                {
                    spriteBatch.Draw(Art.StaticObstacleTexture, lifeRect, Color.White);
                }
                else
                {
                    spriteBatch.Draw(Art.Pixel, lifeRect, Color.Green);
                }
            }
            
            if (Art.Font != null)
            {
                spriteBatch.DrawString(Art.Font, "Score: " + _world.Score, new Vector2(20, 70), Color.White);
            }
        }
    }
}
