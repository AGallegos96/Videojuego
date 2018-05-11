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
    public class ReporteScreen : GameScreen, IDisposable
    {
        private Texture2D puntajeTextura;
        private Rectangle rectPuntaje;
        private Texture2D texturaBtnOk;
        private Rectangle rectBtnOk;
        private SpriteFont fuente;
        private MouseState oldMouseState;
        private string texto;
        private Vector2 posicionFuente;

        #region Starfield Variables
        private const float starsParallaxPeriod = 30f; //The period of the parallax motion in the starfield.
        private const float starsParallaxAmplitude = 2048f; //The amplitude of the parallax motion in the starfield.
        private Starfield starfield; //The star field rendering in the background.
        private double movement; //Persistent movement tracker, used to slightly parallax the stars.
        private Vector2 position; //The position of the parallax motion in the starfield.
        #endregion

        public ReporteScreen(GraphicsDeviceManager graphics) : base(graphics) { }

        public override void Initialize()
        {
            position = Vector2.Zero;
            movement = 0f;
            posicionFuente = new Vector2(215,120);
            texto = String.Empty;
        }

        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);
            puntajeTextura = content.Load<Texture2D>("Screens/reportesScreen/puntaje_form");
            fuente = content.Load<SpriteFont>("Fuentes/fuenteMsg");
            texturaBtnOk = content.Load<Texture2D>("Screens/btn_ok_1");
            rectBtnOk = new Rectangle(-texturaBtnOk.Width, 209, texturaBtnOk.Width, texturaBtnOk.Height);
            rectPuntaje = new Rectangle((graphics.GraphicsDevice.Viewport.Width - puntajeTextura.Width) / 2, -puntajeTextura.Height, puntajeTextura.Width, puntajeTextura.Height);
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
            position = Vector2.Multiply(new Vector2((float)Math.Cos(movement / starsParallaxPeriod), (float)Math.Sin(movement / starsParallaxPeriod)), starsParallaxAmplitude);

            MouseState currentMouseState = Mouse.GetState();

            if (rectPuntaje.Y < 15)
                rectPuntaje.Y += 4;

            if (rectBtnOk.X < 11)
                rectBtnOk.X += 4;

            if (!rectBtnOk.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y)))
                texturaBtnOk = content.Load<Texture2D>("Screens/btn_ok_1");
            else
            {
                texturaBtnOk = content.Load<Texture2D>("Screens/btn_ok_2");
                if (currentMouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
                {
                    ScreenManager.Instance.AddScreen(new MenuScreen(graphics));
                }
            }

            oldMouseState = currentMouseState;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            starfield.Draw(position);
            spriteBatch.Draw(puntajeTextura, rectPuntaje, Color.White);
            spriteBatch.Draw(texturaBtnOk, rectBtnOk, Color.White);
            if (rectPuntaje.Y>=15)
            {
                if (Game1.juegoMain.ListaMagos.Count > 0)
                {
                    foreach (Mago mago in Game1.juegoMain.ListaMagos)
                    {
                        texto = "Nombre: " + mago.NombreJugador + " Puntos: " + mago.Puntos + " Gemas: " + mago.Gemas;
                        spriteBatch.DrawString(fuente, texto, posicionFuente, Color.Black);
                        posicionFuente.Y += 15;
                    }
                    posicionFuente.Y = 120;
                }
            }
        }

        #region IDisposable Implementation
        ~ReporteScreen() //Finalizes the BackgroundScreen object, calls Dispose(false)
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
