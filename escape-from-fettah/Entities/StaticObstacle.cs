using Microsoft.Xna.Framework;
using escape_from_fettah.World;

namespace escape_from_fettah.Entities
{
    public class StaticObstacle : Entity
    {
        public StaticObstacle(int gridX, int gridY) 
            : base(gridX, gridY, WorldManager.CellSize, WorldManager.CellSize)
        {
            Color = Color.Gray;
            Position = new Vector2(gridX * WorldManager.CellSize, gridY * WorldManager.CellSize);
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Vector2 cameraOffset)
        {
            Vector2 drawPos = Position - cameraOffset;
            Rectangle drawRect = new Rectangle((int)drawPos.X, (int)drawPos.Y, _width, _height);
            
            if (escape_from_fettah.Managers.Art.StaticObstacleTexture != null)
            {
                spriteBatch.Draw(escape_from_fettah.Managers.Art.StaticObstacleTexture, drawRect, Color.White);
            }
            else
            {
                spriteBatch.Draw(escape_from_fettah.Managers.Art.Pixel, drawRect, Color);
            }
        }
    }
}
