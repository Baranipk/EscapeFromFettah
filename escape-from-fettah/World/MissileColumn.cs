using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace escape_from_fettah.World
{
    public class MissileColumn : Column
    {
        private List<Entities.Missile> _missiles;
        private int _direction;
        private float _baseSpeed;

        public MissileColumn(int gridX, Random rand) : base(gridX)
        {
            _missiles = new List<Entities.Missile>();
            _direction = rand.Next(2) == 0 ? 1 : -1;
            _baseSpeed = rand.Next(50, 100);

            int missileCount = gridX < 5 ? 0 : rand.Next(0, 3);
            int startY = _direction == 1 ? -WorldManager.CellSize : WorldManager.GridHeight * WorldManager.CellSize;
            
            for (int i = 0; i < missileCount; i++)
            {
                float offset = i * WorldManager.CellSize * rand.Next(2, 5);
                float yPos = startY - (_direction * offset);
                
                _missiles.Add(new Entities.Missile(gridX, yPos, _direction, _baseSpeed));
            }
        }

        public override void Update(GameTime gameTime, float speedMultiplier)
        {
            foreach (var missile in _missiles)
            {
                missile.CurrentSpeedMultiplier = speedMultiplier;
                missile.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 cameraOffset)
        {
            if (escape_from_fettah.Managers.Art.RoadTexture != null)
            {
                for (int y = 0; y < WorldManager.GridHeight; y++)
                {
                    Vector2 drawPos = new Vector2(GridX * WorldManager.CellSize, y * WorldManager.CellSize) - cameraOffset;
                    Rectangle drawRect = new Rectangle((int)drawPos.X, (int)drawPos.Y, WorldManager.CellSize, WorldManager.CellSize);
                    spriteBatch.Draw(escape_from_fettah.Managers.Art.RoadTexture, drawRect, Color.White);
                }
            }

            foreach (var missile in _missiles)
            {
                missile.Draw(spriteBatch, cameraOffset);
            }
        }

        public override bool IsSolidAt(int gridY)
        {
            return false;
        }

        public override bool CheckMissileCollision(Rectangle playerBounds, out Entities.Missile hitMissile)
        {
            foreach (var missile in _missiles)
            {
                if (missile.CollisionBounds.Intersects(playerBounds))
                {
                    hitMissile = missile;
                    return true;
                }
            }
            hitMissile = null;
            return false;
        }
    }
}
