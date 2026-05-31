using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace escape_from_fettah.States
{
    public abstract class State
    {
        protected GraphicsDevice _graphicsDevice;
        protected Managers.GameStateManager _stateManager;

        public State(GraphicsDevice graphicsDevice, Managers.GameStateManager stateManager)
        {
            _graphicsDevice = graphicsDevice;
            _stateManager = stateManager;
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
