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
        private EscenarioFinal escenarioFinal;
        private float tiempoTranscurrido;
        private bool audioEscenario1, audioEscenario2, audioEscenario3, audioEscenario4, audioEscenarioFinal, audioGameWinGameOver;
        private Texture2D backMenuTextura;
        private SpriteFont fuente2;
        private KeyboardState estadoTeclaAnterior;
        private Texture2D WinTextura;
        private Texture2D LoseTextura;

        public JugarScreen(GraphicsDeviceManager graphics) : base(graphics) { }

        public override void Initialize()
        {
            audioEscenario1 = audioEscenario2 = audioEscenario3 = audioEscenario4 = audioEscenarioFinal = audioGameWinGameOver = false;
            tiempoTranscurrido = 0;
            mago = new Mago(graphics);
            escenario1 = new Escenario1(graphics, mago);
            escenario2 = new Escenario2(graphics, mago);
            escenario3 = new Escenario3(graphics, mago);
            escenario4 = new Escenario4(graphics, mago);
            escenarioFinal = new EscenarioFinal(graphics, mago);
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
            escenarioFinal.LoadContent(content);
            fuente2 = content.Load<SpriteFont>("Fuentes/fuenteMsg");
            backMenuTextura = content.Load<Texture2D>("Screens/backMenu");
            WinTextura = content.Load<Texture2D>("Screens/you_win");
            LoseTextura = content.Load<Texture2D>("Screens/you_lose");
        }

        public override void UnloadContent()
        {
            mago.UnloadContent();
            escenario1.UnloadContent();
            escenario2.UnloadContent();
            escenario3.UnloadContent();
            escenario4.UnloadContent();
            escenarioFinal.UnloadContent();
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
           
           tiempoTranscurrido += (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if (tiempoTranscurrido >= 1)
            
            {
                tiempoTranscurrido = 0;
                Game1.juegoMain.TiempoEnJuego++;
            }
            if (!escenario1.NivelCompletado)
            {
                if (!audioEscenario1)
                {
                    AudioManager.PlaySoundtrack("sonido_escenario0", true);
                    audioEscenario1 = true;
                }
                Game1.juegoMain.NivelActual = 1;
                if (!mago.MagoMuerto)
                    escenario1.Update(gameTime);
                else
                    UpdateGameWin_GameOver(gameTime, false);
            }
            else if (!escenario2.NivelCompletado)
            {
                if (!audioEscenario2)
                {
                    AudioManager.PlaySoundtrack("sonido_escenario2", true);
                    audioEscenario2 = true;
                }
                Game1.juegoMain.NivelActual = 2;
                if (!mago.MagoMuerto)
                    escenario2.Update(gameTime);
                else
                    UpdateGameWin_GameOver(gameTime, false);
            }
            else if (!escenario3.NivelCompletado)
            {
                if (!audioEscenario3)
                {
                    AudioManager.PlaySoundtrack("sonido_escenario3", true);
                    audioEscenario3 = true;
                }
                Game1.juegoMain.NivelActual = 3;
                if (!mago.MagoMuerto)
                    escenario3.Update1(gameTime);
                else
                    UpdateGameWin_GameOver(gameTime, false);
            }
            else if (!escenario4.NivelCompletado)
            {
                if (!audioEscenario4)
                {
                    AudioManager.PlaySoundtrack("sonido_escenario4", true);
                    audioEscenario4 = true;
                }
                Game1.juegoMain.NivelActual = 4;
                if (!mago.MagoMuerto)
                    escenario4.Update1(gameTime);
                else
                    UpdateGameWin_GameOver(gameTime, false);
            }
            else if (!escenarioFinal.NivelCompletado)
            {
                if (!audioEscenarioFinal)
                {
                    AudioManager.PlaySoundtrack("sonido_escenarioFinal", true);
                    audioEscenarioFinal = true;
                }
                Game1.juegoMain.NivelActual = 5;
                if (!mago.MagoMuerto)
                    escenarioFinal.Update(gameTime);
                else
                    UpdateGameWin_GameOver(gameTime, false);
            }
            else
            {
                UpdateGameWin_GameOver(gameTime, true);
                return;
            }
            mago.Update(gameTime);
        }
    

        public override void Draw(SpriteBatch spriteBatch)
        {
            graphics.GraphicsDevice.Clear(Color.White);
           if (!escenario1.NivelCompletado)
            {
                escenario1.Draw(spriteBatch);
                if (mago.MagoMuerto)
                    DrawGameWin_GameOver(spriteBatch, false);
            }
            else if (!escenario2.NivelCompletado)
            {
                escenario2.Draw(spriteBatch);
                if (mago.MagoMuerto)
                    DrawGameWin_GameOver(spriteBatch, false);
            }
            else if (!escenario3.NivelCompletado)
            {
                escenario3.Draw(spriteBatch);
                if (mago.MagoMuerto)
                    DrawGameWin_GameOver(spriteBatch, false);
            }
            else if (!escenario4.NivelCompletado)
            {
                escenario4.Draw(spriteBatch);
                if (mago.MagoMuerto)
                    DrawGameWin_GameOver(spriteBatch, false);
            }
            else if (!escenarioFinal.NivelCompletado)
            {
                escenarioFinal.Draw(spriteBatch);
                if (mago.MagoMuerto)
                    DrawGameWin_GameOver(spriteBatch, false);
            }
            else
            { escenarioFinal.Draw(spriteBatch); DrawGameWin_GameOver(spriteBatch, true); return; }
            mago.Draw(spriteBatch);
            #region Dibujo de SpriteFonts
            spriteBatch.DrawString(fuente, (mago.NombreJugador), new Vector2(17 + ((int)-Game1.juegoMain.Camara.Transformacion.Translation.X), 36), Color.Black);
            spriteBatch.DrawString(fuente, ("X " + mago.Vida.NumeroVidas.ToString()), new Vector2(110 + ((int)-Game1.juegoMain.Camara.Transformacion.Translation.X), 14), Color.Black);
            spriteBatch.DrawString(fuente, ("Puntaje: " + mago.Puntos.ToString()), new Vector2(341 + ((int)-Game1.juegoMain.Camara.Transformacion.Translation.X), 10), Color.Black);
            spriteBatch.DrawString(fuente, ("Gemas: " + mago.Gemas.ToString() + "/4"), new Vector2(340 + ((int)-Game1.juegoMain.Camara.Transformacion.Translation.X), 36), Color.Black);
            spriteBatch.DrawString(fuente, ("Nivel: " + Game1.juegoMain.NivelActual.ToString()), new Vector2(645 + ((int)-Game1.juegoMain.Camara.Transformacion.Translation.X), 10), Color.Black);
            spriteBatch.DrawString(fuente, ("Tiempo: " + Game1.juegoMain.TiempoEnJuego.ToString()), new Vector2(645 + ((int)-Game1.juegoMain.Camara.Transformacion.Translation.X), 36), Color.Black);
            #endregion
        }

        private void UpdateGameWin_GameOver(GameTime gameTime, bool estadoJuego)
        {
            KeyboardState estadoTeclaActual = Keyboard.GetState();
            if (estadoJuego)
            {
                if (!audioGameWinGameOver)
                {
                    AudioManager.PlaySoundtrack("Winner");
                    audioGameWinGameOver = true;
                }
            }
            else
            {
                if (!audioGameWinGameOver)
                {
                    AudioManager.PlaySoundtrack("GameOver");
                    audioGameWinGameOver = true;
                }
            }
            if (estadoTeclaActual.IsKeyDown(Keys.Escape) && estadoTeclaAnterior.IsKeyUp(Keys.Escape))
            {
                Game1.juegoMain.ListaMagos.Add(mago);
                ScreenManager.Instance.AddScreen(new MenuScreen(graphics));
            }
            estadoTeclaAnterior = estadoTeclaActual;
        }

        private void DrawGameWin_GameOver(SpriteBatch spriteBatch, bool estadoJuego)
        {
            if (estadoJuego)
                spriteBatch.Draw(WinTextura, new Rectangle(273 + (int)-Game1.juegoMain.Camara.Transformacion.Translation.X, 219, WinTextura.Width, WinTextura.Height), Color.White);
            else
                spriteBatch.Draw(LoseTextura, new Rectangle(265 + (int)-Game1.juegoMain.Camara.Transformacion.Translation.X, 220, LoseTextura.Width, LoseTextura.Height), Color.White);
            spriteBatch.Draw(backMenuTextura, new Rectangle((int)-Game1.juegoMain.Camara.Transformacion.Translation.X, 0, backMenuTextura.Width, backMenuTextura.Height), Color.White);
            spriteBatch.DrawString(fuente2, "Presione la tecla Escape para retornar al menu...", new Vector2(154 + (int)-Game1.juegoMain.Camara.Transformacion.Translation.X, 400), Color.White);
        }
    }
}