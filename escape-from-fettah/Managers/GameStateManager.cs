using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using escape_from_fettah.States;

namespace escape_from_fettah.Managers
{
    public class GameStateManager
    {
        private State _currentState;
        private State _nextState;

        public void ChangeState(State state)
        {
            _nextState = state;
        }

        public void Update(GameTime gameTime)
        {
            if (_nextState != null)
            {
                _currentState = _nextState;
                _nextState = null;
            }

            _currentState?.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _currentState?.Draw(gameTime, spriteBatch);
        }
    }
}
