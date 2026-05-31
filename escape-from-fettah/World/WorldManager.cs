using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace escape_from_fettah.World
{
    public class WorldManager
    {
        public const int CellSize = 64;
        public const int GridHeight = 17;

        private List<Column> _columns;
        public Entities.Player Player { get; private set; }

        private int _maxColReached = 0;
        private int _currentColGenerationX = 0;
        private Random _random;

        public Vector2 CameraOffset;
        public int Score
        {
            get
            {
                int total = 0;
                for (int i = 1; i <= _maxColReached; i++)
                {
                    if (i <= 15) total += 1;
                    else if (i <= 50) total += 2;
                    else if (i <= 100) total += 3;
                    else if (i <= 150) total += 4;
                    else total += 5 + ((i - 151) / 50);
                }
                return total;
            }
        }
        
        public float SpeedMultiplier => 1.0f + (_maxColReached * 0.05f);

        public WorldManager()
        {
            _columns = new List<Column>();
            _random = new Random();
            
            Player = new Entities.Player(0, GridHeight / 2, this);

            for (int i = -3; i <= 35; i++)
            {
                GenerateNextColumn();
            }
        }

        private void GenerateNextColumn()
        {
            Column newCol;
            if (_currentColGenerationX % 2 == 0)
            {
                newCol = new StaticColumn(_currentColGenerationX, _random);
            }
            else
            {
                newCol = new MissileColumn(_currentColGenerationX, _random);
            }

            _columns.Add(newCol);
            _currentColGenerationX++;
        }

        public void Update(GameTime gameTime)
        {
            Player.Update(gameTime);

            if (Player.GridX > _maxColReached)
            {
                _maxColReached = Player.GridX;
                GenerateNextColumn();
            }

            float targetCamX = (Player.GridX * CellSize) - (1920 / 4) + CellSize;
            CameraOffset.X += (targetCamX - CameraOffset.X) * 0.1f;
            CameraOffset.Y = 0;

            int thresholdX = Player.GridX - 3;
            _columns.RemoveAll(c => c.GridX < thresholdX);

            foreach (var col in _columns)
            {
                col.Update(gameTime, SpeedMultiplier);
            }

            CheckCollisions();
        }

        private void CheckCollisions()
        {
            foreach (var col in _columns)
            {
                if (col.CheckMissileCollision(Player.CollisionBounds, out var hitMissile))
                {
                    Player.TakeHit();
                    break;
                }
            }
        }

        public bool IsSolid(int gridX, int gridY)
        {
            if (gridY < 0 || gridY >= GridHeight) return true;

            var col = _columns.Find(c => c.GridX == gridX);
            if (col == null) return true;

            return col.IsSolidAt(gridY);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var col in _columns)
            {
                col.Draw(spriteBatch, CameraOffset);
            }

            Player.Draw(spriteBatch, CameraOffset);
        }
    }
}
