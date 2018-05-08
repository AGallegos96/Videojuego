﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using ParticleTextLibrary;
namespace XNAVideoJuego
{
    public class SplashScreen : GameScreen
    {
        private ParticleText particleText;
        private SpriteFont font;
        private Texture2D ParticleTextTexture;
        private KeyboardState lastKeyboardState;
        private string mensaje;

        public SplashScreen(GraphicsDeviceManager graphics) : base(graphics){}

        public override void Initialize()
        {
            Game1.juegoMain.NoBlend = false;
            mensaje = "Presione tecla Espacio para omitir...";
        }

        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);

            // Load our particle text font.
            font = Content.Load<SpriteFont>("Fuentes/fuenteSplashScreen");
            ParticleTextTexture = Content.Load<Texture2D>("TextParticle");

            var view = graphics.GraphicsDevice.Viewport;
            particleText = new ParticleText(graphics.GraphicsDevice, font, "Abandagi", ParticleTextTexture);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            // Spacebar resets the simulation
            if (keyboardState.IsKeyDown(Keys.Space) && lastKeyboardState.IsKeyUp(Keys.Space))
            {
                ScreenManager.Instance.AddScreen(new IntroScreen(graphics));
            }
            particleText.Update();
            lastKeyboardState = keyboardState;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            graphics.GraphicsDevice.Clear(Color.Black);
            // Draw the particles using additive blending. You can experiment with using different blend modes and
            // different particle textures.
            particleText.Draw(spriteBatch);
            spriteBatch.DrawString(font, mensaje, new Vector2(251, 400), Color.White, 0f, Vector2.Zero, 0.5f, 0, 0);
        }

    }
}