using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using escape_from_fettah.Managers;
using escape_from_fettah.World;

namespace escape_from_fettah.Entities
{
    public class Player : Entity
    {
        public int Lives = 3;
        private WorldManager _world;

        private Vector2 _targetPosition;
        private float _moveSpeed = 15f;
        private float _hitFlashTimer = 0f;
        private bool _stepToggle = false;

        public Player(int gridX, int gridY, WorldManager world) 
            : base(gridX, gridY, WorldManager.CellSize, WorldManager.CellSize)
        {
            Color = Color.White;
            _world = world;
            UpdateTargetPosition();
            Position = _targetPosition;
        }

        private void UpdateTargetPosition()
        {
            _targetPosition = new Vector2(GridX * WorldManager.CellSize, GridY * WorldManager.CellSize);
        }

        public override void Update(GameTime gameTime)
        {
            HandleInput();

            Position = Vector2.Lerp(Position, _targetPosition, _moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);

            if (_hitFlashTimer > 0)
            {
                _hitFlashTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        private void HandleInput()
        {
            int nextX = GridX;
            int nextY = GridY;

            if (InputManager.IsKeyPressed(Keys.D)) nextX++;
            if (InputManager.IsKeyPressed(Keys.A)) nextX--;
            if (InputManager.IsKeyPressed(Keys.W)) nextY--;
            if (InputManager.IsKeyPressed(Keys.S)) nextY++;

            if (nextX != GridX || nextY != GridY)
            {
                if (!_world.IsSolid(nextX, nextY))
                {
                    GridX = nextX;
                    GridY = nextY;
                    UpdateTargetPosition();
                    _stepToggle = !_stepToggle;
                    
                    if (Managers.AudioManager.HopSound != null)
                        Managers.AudioManager.HopSound.Play(0.5f, 0f, 0f);
                }
            }
        }

        public void TakeHit()
        {
            if (_hitFlashTimer > 0) return;

            Lives--;
            _hitFlashTimer = 1.5f;
            
            if (Managers.AudioManager.HitSound != null)
                Managers.AudioManager.HitSound.Play();

            GridX--;
            UpdateTargetPosition();
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 cameraOffset)
        {
            Color drawColor = _hitFlashTimer > 0 && (_hitFlashTimer % 0.2f < 0.1f) ? Color.Red : Color;
            
            Vector2 drawPos = Position - cameraOffset;
            Rectangle drawRect = new Rectangle((int)drawPos.X, (int)drawPos.Y, _width, _height);

            float dist = Vector2.Distance(Position, _targetPosition);
            
            if (Art.PlayerTextures != null && Art.PlayerTextures[1] != null)
            {
                Texture2D tex = dist > 1f ? (_stepToggle ? Art.PlayerTextures[0] : Art.PlayerTextures[2]) : Art.PlayerTextures[1];
                spriteBatch.Draw(tex, drawRect, drawColor);
            }
            else
            {
                if (dist > 1f)
                {
                    drawRect.Y += 5;
                    drawRect.Height -= 10;
                }
                spriteBatch.Draw(Art.Pixel, drawRect, drawColor);
            }
        }
    }
}
