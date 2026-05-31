using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace escape_from_fettah.World
{
    public class StaticColumn : Column
    {
        private List<Entities.StaticObstacle> _obstacles;

        public StaticColumn(int gridX, Random rand) : base(gridX)
        {
            _obstacles = new List<Entities.StaticObstacle>();
            
            int obstacleCount = gridX < 3 ? 0 : rand.Next(0, 3);
            List<int> availableTiles = new List<int>();
            for(int i = 0; i < WorldManager.GridHeight; i++) availableTiles.Add(i);

            for (int i = 0; i < obstacleCount; i++)
            {
                int tileIndex = rand.Next(availableTiles.Count);
                int gridY = availableTiles[tileIndex];
                availableTiles.RemoveAt(tileIndex);

                _obstacles.Add(new Entities.StaticObstacle(gridX, gridY));
            }
        }

        public override void Update(GameTime gameTime, float speedMultiplier)
        {
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 cameraOffset)
        {
            if (escape_from_fettah.Managers.Art.StaticRoadTexture != null)
            {
                for (int y = 0; y < WorldManager.GridHeight; y++)
                {
                    Vector2 drawPos = new Vector2(GridX * WorldManager.CellSize, y * WorldManager.CellSize) - cameraOffset;
                    Rectangle drawRect = new Rectangle((int)drawPos.X, (int)drawPos.Y, WorldManager.CellSize, WorldManager.CellSize);
                    spriteBatch.Draw(escape_from_fettah.Managers.Art.StaticRoadTexture, drawRect, Color.White);
                }
            }
            
            foreach (var obs in _obstacles)
            {
                obs.Draw(spriteBatch, cameraOffset);
            }
        }

        public override bool IsSolidAt(int gridY)
        {
            return _obstacles.Exists(o => o.GridY == gridY);
        }

        public override bool CheckMissileCollision(Rectangle playerBounds, out Entities.Missile hitMissile)
        {
            hitMissile = null;
            return false;
        }
    }
}
