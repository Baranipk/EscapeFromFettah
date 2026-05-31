using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using escape_from_fettah.World;

namespace escape_from_fettah.Entities
{
    public class Missile : Entity
    {
        private int _direction;
        private float _baseSpeed;
        public float CurrentSpeedMultiplier = 1f;

        private float _animationTimer = 0f;
        private int _currentFrame = 0;

        public Missile(int gridX, float startY, int direction, float baseSpeed) 
            : base(gridX, -1, WorldManager.CellSize, WorldManager.CellSize)
        {
            Color = Color.White;
            Position = new Vector2(gridX * WorldManager.CellSize, startY);
            _direction = direction;
            _baseSpeed = baseSpeed;
        }

        public override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position.Y += _direction * _baseSpeed * CurrentSpeedMultiplier * dt;

            _animationTimer += dt;
            if (_animationTimer >= 0.1f)
            {
                _animationTimer = 0f;
                _currentFrame = (_currentFrame + 1) % 3;
            }

            if (_direction == 1 && Position.Y > WorldManager.GridHeight * WorldManager.CellSize)
            {
                Position.Y = -WorldManager.CellSize;
            }
            else if (_direction == -1 && Position.Y < -WorldManager.CellSize)
            {
                Position.Y = WorldManager.GridHeight * WorldManager.CellSize;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 cameraOffset)
        {
            Vector2 drawPos = Position - cameraOffset;
            Rectangle drawRect = new Rectangle((int)drawPos.X, (int)drawPos.Y, _width, _height);

            if (escape_from_fettah.Managers.Art.MissileTextures != null && escape_from_fettah.Managers.Art.MissileTextures.Length > 0)
            {
                Texture2D tex = escape_from_fettah.Managers.Art.MissileTextures[_currentFrame];
                
                SpriteEffects effect = _direction == 1 ? SpriteEffects.FlipVertically : SpriteEffects.None;
                
                spriteBatch.Draw(tex, drawRect, null, Color.White, 0f, Vector2.Zero, effect, 0f);
            }
            else
            {
                spriteBatch.Draw(escape_from_fettah.Managers.Art.Pixel, drawRect, Color.Red);
            }
        }
    }
}
