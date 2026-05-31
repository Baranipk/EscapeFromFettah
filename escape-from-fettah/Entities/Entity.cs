using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using escape_from_fettah.Managers;

namespace escape_from_fettah.Entities
{
    public abstract class Entity
    {
        public Vector2 Position;
        public Color Color = Color.White;
        
        public int GridX;
        public int GridY;

        protected int _width;
        protected int _height;

        public Rectangle Bounds => new Rectangle((int)Position.X, (int)Position.Y, _width, _height);
        
        public virtual Rectangle CollisionBounds 
        {
            get 
            {
                return new Rectangle((int)Position.X + 4, (int)Position.Y + 4, _width - 8, _height - 8);
            }
        }

        public Entity(int gridX, int gridY, int width, int height)
        {
            GridX = gridX;
            GridY = gridY;
            _width = width;
            _height = height;
        }

        public abstract void Update(GameTime gameTime);

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 cameraOffset)
        {
            Vector2 drawPos = Position - cameraOffset;
            Rectangle drawRect = new Rectangle((int)drawPos.X, (int)drawPos.Y, _width, _height);
            spriteBatch.Draw(Art.Pixel, drawRect, Color);
        }
    }
}
