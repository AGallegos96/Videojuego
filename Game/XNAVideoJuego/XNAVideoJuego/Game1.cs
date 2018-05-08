using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace XNAVideoJuego
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public static Game1 juegoMain;
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private bool noBlend;

        private string nombreJugador;
        private int nivelActual;
        private int tiempoEnJuego;
        private List<Mago> listaMagos;

        public Game1()
        {
            noBlend = true;
            graphics = new GraphicsDeviceManager(this);
            juegoMain = this;
            Content.RootDirectory = "Content";
        }

        #region Propiedades
        public string NombreJugador { get { return nombreJugador; } set { nombreJugador = value; } }
        public int TiempoEnJuego { get { return tiempoEnJuego; } set { tiempoEnJuego = value; } }
        public int NivelActual { get { return nivelActual; } set { nivelActual = value; } }
        public List<Mago> ListaMagos { get { return listaMagos; } set { listaMagos = value; } }
        public bool NoBlend { get { return noBlend; } set { noBlend = value; } }
        #endregion

        protected override void Initialize()
        {
            ScreenManager.Instance.Initialize(graphics);
            ScreenManager.Instance.Dimensions = new Vector2(graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
            graphics.PreferredBackBufferWidth = (int)ScreenManager.Instance.Dimensions.X;
            graphics.PreferredBackBufferHeight = (int)ScreenManager.Instance.Dimensions.Y;
            graphics.ApplyChanges();
            AudioManager.Initialize(this);
            this.IsMouseVisible = true;
            nombreJugador = String.Empty;
            nivelActual = tiempoEnJuego = 0;
            listaMagos = new List<Mago>();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ScreenManager.Instance.LoadContent(Content);
        }

        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {
            ScreenManager.Instance.Update(gameTime);
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            if (noBlend)
            {
                spriteBatch.Begin();
                ScreenManager.Instance.Draw(spriteBatch);
                spriteBatch.End();
            }
            else
            {
                spriteBatch.Begin(0, BlendState.Additive);
                ScreenManager.Instance.Draw(spriteBatch);
                spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}
