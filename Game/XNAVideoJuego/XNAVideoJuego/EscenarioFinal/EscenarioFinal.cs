using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace XNAVideoJuego
{
    public class EscenarioFinal
    {
        private GraphicsDeviceManager graphics;
        private ContentManager content;
        private List<Texture2D> listaEscenariosTexturas;
        private Rectangle rectEscenario1;
        private Rectangle rectEscenario2;
        private Vector2 posicionEscenario1;
        private Vector2 posicionEscenario2;
        private Sombra sombra;
        private Mago mago;
        private bool nivelCompletado;
        Map mapa;

        #region Propiedades
        public bool NivelCompletado { get { return nivelCompletado; } }
        #endregion

        public EscenarioFinal(GraphicsDeviceManager graphics, Mago mago)
        {
            this.graphics = graphics;
            this.mago = mago;
            this.mago.Posicion = new Vector2(0, 405);
            listaEscenariosTexturas = new List<Texture2D>();
            posicionEscenario1 = new Vector2(0, 0);
            posicionEscenario2 = new Vector2();
            rectEscenario1 = new Rectangle();
            rectEscenario2 = new Rectangle();
            nivelCompletado = false;
            sombra = new Sombra(3000); //Se asigna el número de vidas de sombra
            sombra.Posicion = new Vector2(860, 381);
        }

        public void LoadContent(ContentManager Content)
        {
            content = Content;
            content = new ContentManager(content.ServiceProvider, "Content");
            listaEscenariosTexturas.Add(content.Load<Texture2D>("Escenarios/05_Escenario_Final/01"));
            listaEscenariosTexturas.Add(content.Load<Texture2D>("Escenarios/05_Escenario_Final/02"));
            posicionEscenario2 = new Vector2(listaEscenariosTexturas.ElementAt(0).Width, 0);
            rectEscenario1 = new Rectangle(0, 0, listaEscenariosTexturas.ElementAt(0).Width, listaEscenariosTexturas.ElementAt(0).Height);
            rectEscenario2 = new Rectangle(0, 0, listaEscenariosTexturas.ElementAt(1).Width, listaEscenariosTexturas.ElementAt(1).Height);
            sombra.LoadContent(Content);
            Tiles.Content = Content;
            Game1.juegoMain.Camara = new Camara(graphics.GraphicsDevice.Viewport);
            mapa = new Map();
            mapa.Generar(new int[,]{{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,  0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,                       0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,                       0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,  0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,                       0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,                       0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,  0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,                       0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,                       0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,  0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,                       0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,                       0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,  0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,                       0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,                       0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,  0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,                       0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,                       0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,  0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,                       0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,                       0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,  0,0,12,12,12,0,0,0,0,12,0,0,0,0,12,0,0,0,0,0,                  0,0,0,0,0,12,0,0,0,12,12,0,0,0,0,12,0,0,0,0,                   0,0,0,0,0,12,0,0,0,12,12,0,0,0,0,12,0,0,0,0},
                                {0,0,0,0,0,0,0,0,0,12,12,0,0,0,12,12,0,0,0,0,  0,0,0,0,0,12,0,0,0,12,12,12,12,12,12,0,0,0,0,0,                0,0,0,0,12,12,0,0,12,12,12,12,0,0,12,12,0,0,12,0,             0,12,0,0,0,0,0,0,0,0,0,0,0,0,12,12,12,0,0,0},
                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,  0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,                       0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,                       0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,  0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,                       0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,                       0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {10,11,10,11,10,11,10,11,10,11,10,11,10,11,10,11,10,11,10,11,  10,11,10,11,10,11,10,11,10,11,10,11,10,11,10,11,10,11,10,11,   10,11,10,11,10,11,10,11,10,11,10,11,10,11,10,11,10,11,10,11,   10,11,10,11,10,11,10,11,10,11,10,11,10,11,10,11,10,11,10,11}}, 40);
        }

        public void UnloadContent()
        {
            content.Unload();
        }

        public void Update(GameTime gameTime)
        {
            Game1.juegoMain.IndiceSpriteBatch = 2;
            if (!sombra.SombraMuerto)
            {
                if (!mago.MagoMuerto)
                {
                    UpdateEnemigo(gameTime);
                    foreach (CollisionTiles tile in mapa.CollisionTiles)
                    {
                        mago.Colision(tile.rectangle, mapa.Ancho, mapa.Altura);
                        Game1.juegoMain.Camara.Update(mago.Posicion, mapa.Ancho, mapa.Altura);
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                nivelCompletado = true;
            }
            UpdateMagoVida(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(listaEscenariosTexturas[0], posicionEscenario1, rectEscenario1, Color.White);
            spriteBatch.Draw(listaEscenariosTexturas[1], posicionEscenario2, rectEscenario2, Color.White);
            DrawEnemigo(spriteBatch);
            mapa.Draw(spriteBatch);
        }

        private void UpdateMagoVida(GameTime gameTime)
        {
            mago.ReducirVida(sombra.ListaAnimaciones[sombra.IndiceAnimacionActual].DestinationRect);
            if (sombra.ListaBolasFuego.Count > 0)
            {
                for (int i = 0; i < sombra.ListaBolasFuego.Count; i++)
                {
                    mago.ReducirVida(sombra.ListaBolasFuego[i].ListaAnimaciones[sombra.ListaBolasFuego[i].IndiceAnimacionActual].DestinationRect);
                }
            }
        }

        private void UpdateEnemigo(GameTime gameTime)
        {
            if (mago.ListaPoderesNormal.Count > 0)
            {
                for (int contPN = 0; contPN < mago.ListaPoderesNormal.Count; contPN++)
                {
                    sombra.ReducirVida(mago.ListaPoderesNormal[contPN].RectDestino, 15);
                }
            }
            if (mago.ListaPoderesAgua.Count > 0)
            {
                for (int contPA = 0; contPA < mago.ListaPoderesAgua.Count; contPA++)
                {
                    sombra.ReducirVida(mago.ListaPoderesAgua[contPA].RectDestino, 45);
                }
            }
            if (mago.ListaPoderesAire.Count > 0)
            {
                for (int contPA = 0; contPA < mago.ListaPoderesAire.Count; contPA++)
                {
                    sombra.ReducirVida(mago.ListaPoderesAire[contPA].RectDestino, 55);
                }
            }
            if (mago.ListaPoderesFuego.Count > 0)
            {
                for (int contPF = 0; contPF < mago.ListaPoderesFuego.Count; contPF++)
                {
                    sombra.ReducirVida(mago.ListaPoderesFuego[contPF].RectDestino, 35);
                }
            }
            if (mago.ListaPoderesTierra.Count > 0)
            {
                for (int contPT = 0; contPT < mago.ListaPoderesTierra.Count; contPT++)
                {
                    sombra.ReducirVida(mago.ListaPoderesTierra[contPT].RectDestino, 25);
                }
            }
            if (mago.Posicion.X>=sombra.Posicion.X)
            {
                sombra.SentidoMovimiento = true;
            }
            else
            {
                sombra.SentidoMovimiento = false;
            }
            sombra.Update(gameTime);
        }

        private void DrawEnemigo(SpriteBatch spriteBatch)
        {
            if (!sombra.SombraMuerto)
            {
                sombra.Draw(spriteBatch);
            }
        }

    }
}