using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace XNAVideoJuego
{
    public class VideoScreen : GameScreen
    {
        private Video videoIntro;
        private VideoPlayer videoPlayer;
        private Rectangle rectVideo;
        private Texture2D videoTextura;

        public VideoScreen(GraphicsDeviceManager graphics) : base(graphics) { }

        public override void Initialize()
        {
            videoPlayer = new VideoPlayer();
        }

        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);
            videoIntro = content.Load<Video>("Videos/introESRB");
            rectVideo = new Rectangle(graphics.GraphicsDevice.Viewport.X, graphics.GraphicsDevice.Viewport.Y, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
            videoPlayer.Play(videoIntro);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (videoPlayer.State == MediaState.Stopped)
            {
                ScreenManager.Instance.AddScreen(new SplashScreen(graphics));
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            graphics.GraphicsDevice.Clear(Color.Black);
            videoTextura = videoPlayer.GetTexture();
            spriteBatch.Draw(videoTextura, rectVideo, Color.White);
        }

    }
}
