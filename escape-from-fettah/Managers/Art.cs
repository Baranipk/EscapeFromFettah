using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace escape_from_fettah.Managers
{
    public static class Art
    {
        public static Texture2D Pixel { get; private set; }
        public static SpriteFont Font { get; private set; }
        public static Texture2D RoadTexture { get; private set; }
        public static Texture2D[] MissileTextures { get; private set; }
        public static Texture2D[] PlayerTextures { get; private set; }
        
        public static Texture2D StaticRoadTexture { get; private set; }
        public static Texture2D StaticObstacleTexture { get; private set; }

        public static void Load(GraphicsDevice graphicsDevice, ContentManager content)
        {
            Pixel = new Texture2D(graphicsDevice, 1, 1);
            Pixel.SetData(new[] { Color.White });

            Font = content.Load<SpriteFont>("Font");
            
            RoadTexture = content.Load<Texture2D>("road");
            StaticRoadTexture = content.Load<Texture2D>("static_road");
            StaticObstacleTexture = content.Load<Texture2D>("static_obstacle");
            
            MissileTextures = new Texture2D[3];
            MissileTextures[0] = content.Load<Texture2D>("missile1");
            MissileTextures[1] = content.Load<Texture2D>("missile2");
            MissileTextures[2] = content.Load<Texture2D>("missile3");
            
            PlayerTextures = new Texture2D[3];
            PlayerTextures[0] = content.Load<Texture2D>("trump1");
            PlayerTextures[1] = content.Load<Texture2D>("trump2");
            PlayerTextures[2] = content.Load<Texture2D>("trump3");
        }
    }
}
