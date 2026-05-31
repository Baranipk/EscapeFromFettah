using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace escape_from_fettah.World
{
    public abstract class Column
    {
        public int GridX { get; private set; }
        
        public Column(int gridX)
        {
            GridX = gridX;
        }

        public abstract void Update(GameTime gameTime, float speedMultiplier);
        public abstract void Draw(SpriteBatch spriteBatch, Vector2 cameraOffset);
        public abstract bool IsSolidAt(int gridY);
        public abstract bool CheckMissileCollision(Rectangle playerBounds, out Entities.Missile hitMissile);
    }
}
