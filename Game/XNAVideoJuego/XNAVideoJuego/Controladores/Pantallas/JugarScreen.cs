using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XNAVideoJuego
{
    public class JugarScreen : GameScreen
    {
        private SpriteFont fuente;
      private Mago mago;
        private Escenario1 escenario1;
        private Escenario2 escenario2;
        private Escenario3 escenario3;
        private Escenario4 escenario4;

        private float tiempoTranscurrido;

        public JugarScreen(GraphicsDeviceManager graphics) : base(graphics) { }

        public override void Initialize()
        {
            tiempoTranscurrido = 0;
            mago = new Mago(graphics);
            escenario1 = new Escenario1(graphics, mago);
            escenario2 = new Escenario2(graphics, mago);
            escenario3 = new Escenario3(graphics, mago);
            escenario4 = new Escenario4(graphics, mago);
        }

        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);
            fuente = Content.Load<SpriteFont>("Fuentes/fuenteJuego");
            mago.LoadContent(Content);
            escenario1.LoadContent(content);
            escenario2.LoadContent(content);
            escenario3.LoadContent(content);
            escenario4.LoadContent(content);
        }

        public override void UnloadContent()
        {
            mago.UnloadContent();
            escenario1.UnloadContent();
            escenario2.UnloadContent();
            escenario3.UnloadContent();
            escenario4.UnloadContent();
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            tiempoTranscurrido += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (tiempoTranscurrido>=1)
            {
                tiempoTranscurrido = 0;
                Game1.juegoMain.TiempoEnJuego++;
            }

            /*if (!escenario1.NivelCompletado)
            {
                Game1.juegoMain.NivelActual = 1;
                escenario1.Update(gameTime);
            }
            else if (!escenario2.NivelCompletado)
            {
                Game1.juegoMain.NivelActual = 2;
                escenario2.Update(gameTime);
            }
            if (!escenario3.NivelCompletado)
            {
                Game1.juegoMain.NivelActual = 3;
                escenario3.Update1(gameTime);
            }
            */
            if (!escenario4.NivelCompletado)
            {
                Game1.juegoMain.NivelActual = 4;
                escenario4.Update1(gameTime);
            }
            else
            {
                //Como el mago pasó a todos los niveles se le agrega a lista de registro de magos.
                Game1.juegoMain.ListaMagos.Add(mago);
                //ScreenManager.Instance.AddScreen(new LevelCompletadoScreen(graphics));
            }
            mago.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            graphics.GraphicsDevice.Clear(Color.White);
            /*if (!escenario1.NivelCompletado) {
                escenario1.Draw(spriteBatch);
            }
            else if (!escenario2.NivelCompletado)
            {
                escenario2.Draw(spriteBatch);
            }
            if (!escenario3.NivelCompletado)
            {
                escenario3.Draw(spriteBatch);
            }
            */
            if (!escenario4.NivelCompletado)
            {
                escenario4.Draw(spriteBatch);
            }
            else
            {
                return;
            }
            mago.Draw(spriteBatch);
            #region Dibujo de SpriteFonts
            spriteBatch.DrawString(fuente, (Game1.juegoMain.NombreJugador), new Vector2(17, 36), Color.Black);
            spriteBatch.DrawString(fuente, ("X " + mago.Vida.NumeroVidas.ToString()), new Vector2(110, 14), Color.Black);
            spriteBatch.DrawString(fuente, ("Puntaje: " + mago.Puntos.ToString()), new Vector2(341,10), Color.Black);
            spriteBatch.DrawString(fuente, ("Gemas: " + mago.Gemas.ToString() + "/4"), new Vector2(340, 36), Color.Black);
            spriteBatch.DrawString(fuente, ("Nivel: " + Game1.juegoMain.NivelActual.ToString()), new Vector2(660, 10), Color.Black);
            spriteBatch.DrawString(fuente, ("Tiempo: " + Game1.juegoMain.TiempoEnJuego.ToString()), new Vector2(660, 36), Color.Black);
            #endregion
        }
    }
}
