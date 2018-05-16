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
    public class ESRBScreen : GameScreen
    {
        private Video videoIntro;
        private VideoPlayer videoPlayer;
        private Rectangle rectVideo;
        private Texture2D videoTextura;
        private KeyboardState lastKeyboardState;

        public ESRBScreen(GraphicsDeviceManager graphics) : base(graphics) { }

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
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Space) && lastKeyboardState.IsKeyUp(Keys.Space) || videoPlayer.State == MediaState.Stopped)
            {
                videoPlayer.Stop();
                ScreenManager.Instance.AddScreen(new SplashScreen(graphics));
            }
            lastKeyboardState = keyboardState;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            graphics.GraphicsDevice.Clear(Color.Black);
            videoTextura = videoPlayer.GetTexture();
            spriteBatch.Draw(videoTextura, rectVideo, Color.White);
        }

    }
}
