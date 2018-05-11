using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StartfieldLibrary;

namespace XNAVideoJuego
{
    public class MenuScreen : GameScreen, IDisposable
    {
        private Texture2D texturaBtnJugar;
        private Rectangle rectBtnJugar;
        private Texture2D texturaBtnReportes;
        private Rectangle rectBtnReportes;
        private Texture2D texturaBtnSalir;
        private Rectangle rectBtnSalir;
        private Texture2D texturaBtnMusic;
        private Rectangle rectBtnMusic;
        private Texture2D texturaBtnInicio;
        private Rectangle rectBtnInicio;
        private SpriteFont fuente;
        private bool soundtrackPausado;
        private MouseState oldMouseState;

        #region Starfield Variables
        private const float starsParallaxPeriod = 30f; //The period of the parallax motion in the starfield.
        private const float starsParallaxAmplitude = 2048f; //The amplitude of the parallax motion in the starfield.
        private Starfield starfield; //The star field rendering in the background.
        private double movement; //Persistent movement tracker, used to slightly parallax the stars.
        private Vector2 position; //The position of the parallax motion in the starfield.
        #endregion

        #region Title Variables
        private Texture2D titleTexture;
        private Rectangle rectTitle;
        #endregion

        public MenuScreen(GraphicsDeviceManager graphics) : base(graphics) { }

        public override void Initialize()
        {
            AudioManager.PlaySoundtrack();
            position = Vector2.Zero;
            movement = 0f;
            soundtrackPausado = false;
            Game1.juegoMain.NombreJugador = (Game1.juegoMain.NombreJugador.Length == 1) ? Game1.juegoMain.NombreJugador.ToUpper() : Game1.juegoMain.NombreJugador.Substring(0, 1).ToUpper() + Game1.juegoMain.NombreJugador.Substring(1).ToLower();
        }

        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);
            fuente = content.Load<SpriteFont>("Fuentes/fuenteMsg");
            texturaBtnJugar = content.Load<Texture2D>("Screens/menuScreen/btn_jugar_1");
            texturaBtnReportes = content.Load<Texture2D>("Screens/menuScreen/btn_reportes_1");
            texturaBtnSalir = content.Load<Texture2D>("Screens/menuScreen/btn_salir_1");
            texturaBtnMusic = content.Load<Texture2D>("Screens/menuScreen/btn_music_1");
            texturaBtnInicio = content.Load<Texture2D>("Screens/menuScreen/btn_home_1");
            rectBtnJugar = new Rectangle((graphics.GraphicsDevice.Viewport.Width - texturaBtnJugar.Width) / 2, graphics.GraphicsDevice.Viewport.Height+texturaBtnJugar.Height, texturaBtnJugar.Width, texturaBtnJugar.Height);
            rectBtnReportes = new Rectangle((graphics.GraphicsDevice.Viewport.Width-texturaBtnReportes.Width)/2, graphics.GraphicsDevice.Viewport.Height + texturaBtnReportes.Height, texturaBtnReportes.Width, texturaBtnReportes.Height);
            rectBtnSalir = new Rectangle((graphics.GraphicsDevice.Viewport.Width - texturaBtnSalir.Width) / 2, graphics.GraphicsDevice.Viewport.Height + texturaBtnSalir.Height, texturaBtnSalir.Width, texturaBtnSalir.Height);
            rectBtnMusic = new Rectangle(-texturaBtnMusic.Width, 278, texturaBtnMusic.Width, texturaBtnMusic.Height);
            rectBtnInicio = new Rectangle(-texturaBtnInicio.Width, 346, texturaBtnInicio.Width, texturaBtnInicio.Height);
            titleTexture = content.Load<Texture2D>("Screens/menuScreen/title");
            rectTitle = new Rectangle((graphics.GraphicsDevice.Viewport.Width-titleTexture.Width)/2,-titleTexture.Height, titleTexture.Width, titleTexture.Height);
            starfield = new Starfield(Vector2.Multiply(new Vector2((float)Math.Cos(movement / starsParallaxPeriod), (float)Math.Sin(movement / starsParallaxPeriod)), starsParallaxAmplitude), graphics.GraphicsDevice, content);
            starfield.LoadContent();
        }

        public override void UnloadContent()
        {
            if (starfield != null)
            {
                starfield.UnloadContent();
                starfield = null;
            }
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            movement += gameTime.ElapsedGameTime.TotalSeconds;
            position = Vector2.Multiply(new Vector2((float)Math.Cos(movement / starsParallaxPeriod),(float)Math.Sin(movement / starsParallaxPeriod)),starsParallaxAmplitude);

            MouseState currentMouseState = Mouse.GetState();

            if (rectTitle.Y < 0)
                rectTitle.Y += 4;

            if (rectBtnJugar.Y > 243)
                rectBtnJugar.Y -= 4;
            if (rectBtnReportes.Y > 312)
                rectBtnReportes.Y -= 4;
            if (rectBtnSalir.Y > 380)
                rectBtnSalir.Y -= 4;
            if (rectBtnMusic.X < 11)
                rectBtnMusic.X += 4;
            if (rectBtnInicio.X <11)
                rectBtnInicio.X += 4;

            if (!rectBtnJugar.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y)))
                texturaBtnJugar = content.Load<Texture2D>("Screens/menuScreen/btn_jugar_1");
            else
            {
                texturaBtnJugar = content.Load<Texture2D>("Screens/menuScreen/btn_jugar_2");
                if (currentMouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
                {
                    AudioManager.StopSoundTrack();
                    ScreenManager.Instance.AddScreen(new CargandoScreen(graphics));
                }
            }
            if (!rectBtnReportes.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y)))
                texturaBtnReportes = content.Load<Texture2D>("Screens/menuScreen/btn_reportes_1");
            else
            {
                texturaBtnReportes = content.Load<Texture2D>("Screens/menuScreen/btn_reportes_2");
                if (currentMouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
                {
                    AudioManager.StopSoundTrack();
                    ScreenManager.Instance.AddScreen(new ReporteScreen(graphics));
                }
            }
            if (!rectBtnSalir.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y)))
                texturaBtnSalir = content.Load<Texture2D>("Screens/menuScreen/btn_salir_1");
            else
            {
                texturaBtnSalir = content.Load<Texture2D>("Screens/menuScreen/btn_salir_2");
                if (currentMouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
                {
                    AudioManager.StopSoundTrack();
                    Game1.juegoMain.Exit();
                }
            }

            if (!soundtrackPausado)
            {
                if (!rectBtnMusic.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y)))
                    texturaBtnMusic = content.Load<Texture2D>("Screens/menuScreen/btn_music_1");
                else
                {
                    texturaBtnMusic = content.Load<Texture2D>("Screens/menuScreen/btn_music_2");
                    if (currentMouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
                    {
                        AudioManager.StopSoundTrack();
                        soundtrackPausado = true;
                    }
                }
            }
            else
            {
                texturaBtnMusic = content.Load<Texture2D>("Screens/menuScreen/btn_music_3");
                if (rectBtnMusic.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y)))
                {
                    if (currentMouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
                    {
                        AudioManager.PlaySoundtrack();
                        soundtrackPausado = false;
                    }
                }
            }

            if (!rectBtnInicio.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y)))
                texturaBtnInicio = content.Load<Texture2D>("Screens/menuScreen/btn_home_1");
            else
            {
                texturaBtnInicio = content.Load<Texture2D>("Screens/menuScreen/btn_home_2");
                if (currentMouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
                {
                    AudioManager.StopSoundTrack();
                    ScreenManager.Instance.AddScreen(new IntroScreen(graphics));
                }
            }

            oldMouseState = currentMouseState;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            starfield.Draw(position);
            spriteBatch.Draw(titleTexture, rectTitle, Color.White);
            spriteBatch.Draw(texturaBtnJugar, rectBtnJugar, Color.White);
            spriteBatch.Draw(texturaBtnReportes, rectBtnReportes, Color.White);
            spriteBatch.Draw(texturaBtnSalir, rectBtnSalir, Color.White);
            spriteBatch.Draw(texturaBtnMusic, rectBtnMusic, Color.White);
            spriteBatch.Draw(texturaBtnInicio, rectBtnInicio, Color.White);
            spriteBatch.DrawString(fuente, "Jugador: " + Game1.juegoMain.NombreJugador, new Vector2(5,5), Color.White);
        }

        #region IDisposable Implementation
        ~MenuScreen() //Finalizes the BackgroundScreen object, calls Dispose(false)
        {
            Dispose(false);
        }

        public void Dispose() //Disposes the BackgroundScreen object.
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes this object.
        /// </summary>
        /// <param name="disposing">
        /// True if this method was called as part of the Dispose method.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                lock (this)
                {
                    if (starfield != null)
                    {
                        starfield.Dispose();
                        starfield = null;
                    }
                }
            }
        }
        #endregion
    }
}
