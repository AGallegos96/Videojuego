using System;
using System.Collections.Generic;
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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont fuente;
        int nivelActual, tiempoEnJuego;
        Mago mago;
        Escenario1 escenario1;
        Escenario2 escenario2;
        Escenario3 escenario3;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }

        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            mago = new Mago(graphics);
            escenario1 = new Escenario1(graphics, mago);
            escenario2 = new Escenario2(graphics, mago);
            escenario3 = new Escenario3(graphics, mago);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            fuente = Content.Load<SpriteFont>("Fuentes/fuenteJuego");
            escenario1.LoadContent(Content);
            escenario2.LoadContent(Content);
            escenario3.LoadContent(Content);
            mago.LoadContent(Content);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            tiempoEnJuego = (int)gameTime.TotalGameTime.TotalSeconds;

            if (!escenario1.NivelCompletado)
            {
                nivelActual = 1;
                escenario1.Update(gameTime);
            }
            else if (!escenario2.NivelCompletado)
            {
                nivelActual = 2;
                escenario2.Update(gameTime);
            }
            else if (!escenario3.NivelCompletado)
            {
                nivelActual = 3;
                escenario3.Update(gameTime);
            }
            mago.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) //Cerrar Aplicación
                this.Exit();

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();

            if (!escenario1.NivelCompletado)
                escenario1.Draw(spriteBatch);
            else
                if (!escenario2.NivelCompletado)
                escenario2.Draw(spriteBatch);
            else
                escenario3.Draw(spriteBatch);

            mago.Draw(spriteBatch);

            #region Dibujo de SpriteFonts
            spriteBatch.DrawString(fuente, ("Abandagi"), new Vector2(352, 10), Color.Black);
            spriteBatch.DrawString(fuente, ("X " + mago.Vida.NumeroVidas.ToString()), new Vector2(110, 14), Color.Black);
            spriteBatch.DrawString(fuente, ("  Nivel: " + nivelActual.ToString()), new Vector2(650, 10), Color.Black);
            spriteBatch.DrawString(fuente, (" Tiempo: " + tiempoEnJuego.ToString()), new Vector2(650, 36), Color.Black);
            spriteBatch.DrawString(fuente, (" Puntaje: " + mago.Puntos.ToString()), new Vector2(10, 36), Color.Black);
            spriteBatch.DrawString(fuente, ("Gemas: " + mago.Gemas.ToString() + "/4"), new Vector2(340, 36), Color.Black);
            #endregion

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}