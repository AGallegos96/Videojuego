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
   public class Escenario4
    {

        private GraphicsDeviceManager graphics;
        private ContentManager content;
        private List<Texture2D> listaEscenariosTexturas;
        private Rectangle rectEscenario1;
        private Rectangle rectEscenario2;
        private Vector2 posicionEscenario1;
        private Vector2 posicionEscenario2;
        private float velocidadTraslado;
        private List<Dragon> listadragon;
        private List<Fuego> listafuego;
        private int cantidadfuegos;
        private float tiempofuegos;
        private int cantidadfuego;
        private const int puntosPorEnemigo = 35;
        private Mago mago;
        private bool nivelCompletado;
        private Map mapa;

        #region Propiedades
        public bool NivelCompletado { get { return nivelCompletado; } }
        #endregion

        public Escenario4(GraphicsDeviceManager graphics, Mago mago)
        {
            this.graphics = graphics;
            this.mago = mago;
            this.mago.Posicion = new Vector2(0, 394);
            this.mago.PosicionMuerte = this.mago.Posicion;
            listaEscenariosTexturas = new List<Texture2D>();
            posicionEscenario1 = new Vector2(0, 0);
            posicionEscenario2 = new Vector2();
            rectEscenario1 = new Rectangle();
            rectEscenario2 = new Rectangle();
            velocidadTraslado = 1f;
            listadragon = new List<Dragon>();
            cantidadfuegos = 0;
            tiempofuegos = 0;
            cantidadfuego = 0;
            nivelCompletado = false;
            mapa = new Map();
        }

        public void LoadContent(ContentManager Content)
        {
            content = Content;
            content = new ContentManager(content.ServiceProvider, "Content");
            listaEscenariosTexturas.Add(content.Load<Texture2D>("Escenarios/04_Isla/01"));
            listaEscenariosTexturas.Add(content.Load<Texture2D>("Escenarios/04_Isla/02"));
            posicionEscenario2 = new Vector2(listaEscenariosTexturas.ElementAt(0).Width, 0);
            rectEscenario1 = new Rectangle(0, 0, listaEscenariosTexturas.ElementAt(0).Width, listaEscenariosTexturas.ElementAt(0).Height);
            rectEscenario2 = new Rectangle(0, 0, listaEscenariosTexturas.ElementAt(1).Width, listaEscenariosTexturas.ElementAt(1).Height);
            Tiles.Content = content;
            mapa.Generar(new int[,]{
                                   {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                   {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                   {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                   {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                   {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                   {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                   {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                   {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                   {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                   {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                   {1,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                   {4,0,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,4}}, 40);
            mago.LoadContent(content);
        }
        public void UnloadContent()
        {
            content.Unload();
        }

        public void Update1(GameTime gameTime)
        {
            Game1.juegoMain.IndiceSpriteBatch = 2;
            if (cantidadfuego <= 30)
            {
                if (!mago.MagoMuerto)
                {
                    UpdateEscenario(gameTime);
                    UpdateEnemigos(gameTime);
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
                mago.Gemas++;
                mago.ActivarPoderAgua = true;
                nivelCompletado = true;
                listadragon.Clear();
            }
            UpdateMagoVida(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            DrawEscenario(spriteBatch);
            mapa.Draw(spriteBatch);
            DrawEnemigos(spriteBatch);
        }

        public void UpdateEscenario(GameTime gameTime)
        {
            if (posicionEscenario1.X <= -listaEscenariosTexturas.ElementAt(0).Width)
            {
                posicionEscenario1.X = posicionEscenario1.X * -1;
            }
            if (posicionEscenario2.X <= -listaEscenariosTexturas.ElementAt(0).Width)
            {
                posicionEscenario2.X = posicionEscenario2.X * -1;
            }
            posicionEscenario1.X -= velocidadTraslado;
            posicionEscenario2.X -= velocidadTraslado;

        }

        private void UpdateMagoVida(GameTime gameTime)
        {
            if (listadragon.Count > 0)
            {
                foreach (Dragon dragon in listadragon)
                {
                    mago.ReducirVida(dragon.ListaAnimaciones[dragon.IndiceAnimacionActual].DestinationRect);
                    if (dragon.ListaFuegos.Count > 0)
                    {
                        for (int i = 0; i < dragon.ListaFuegos.Count; i++)
                        {
                            mago.ReducirVida(dragon.ListaFuegos[i].Animacion.DestinationRect);
                        }
                    }
                }
            }
        }

        private int DeterminarTiempoEsperaLenador()
        {
         if (cantidadfuego >= 6 && cantidadfuego < 12)
                return new Random().Next(4, 7); // entre 4 y 6 segundos
            return new Random().Next(6, 9); // entre 6 y 8 segundos
        }

        public void UpdateEnemigos(GameTime gameTime)
        {
            int tiempoEspera = new Random().Next(2, 6); //tiempo de esperar para crear un Erizo
            tiempofuegos += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (tiempofuegos > tiempoEspera)
            {
                CrearEnemigo();
                cantidadfuegos++;
                tiempofuegos = 0;
            }

            if (listadragon.Count > 0)
            {
                for (int i = 0; i < listadragon.Count; i++)
                {
                    if (mago.ListaPoderesNormal.Count > 0)
                    {
                    }
                    if (listadragon[i].Posicion.X <= -listadragon[i].AnchoFrame)
                    {
                        listadragon.RemoveAt(i);
                    }
                    listadragon[i].Update(gameTime);
                }
                
            }
        }

        public void CrearEnemigo()
        {
            Dragon dragon = new Dragon();
            dragon.Posicion = new Vector2((graphics.GraphicsDevice.Viewport.Width + dragon.AnchoFrame), 384);
            dragon.LoadContent(content);
            listadragon.Add(dragon);
        }

        private void DrawEscenario(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(listaEscenariosTexturas[0], posicionEscenario1, rectEscenario1, Color.White);
            spriteBatch.Draw(listaEscenariosTexturas[1], posicionEscenario2, rectEscenario2, Color.White);
        }

        private void DrawEnemigos(SpriteBatch spriteBatch)
        {
            if (listadragon.Count > 0)
            {
                foreach (Dragon dragon in listadragon)
                {
                    dragon.Draw(spriteBatch);
                }
            }
        }
    }
}
