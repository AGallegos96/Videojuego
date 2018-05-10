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
    public class Escenario1
    {
        private GraphicsDeviceManager graphics;
        private ContentManager content;
        private List<Texture2D> listaEscenariosTexturas;
        private Rectangle rectEscenario1;
        private Rectangle rectEscenario2;
        private Vector2 posicionEscenario1;
        private Vector2 posicionEscenario2;
        private float velocidadEscenario;
        private List<Lenador> listaLenadores;
        private int cantidadLenadores;
        private float tiempoLenadores;
        private int cantidadLenadoresEliminados;
        private const int puntosPorEnemigo = 25;
        private Mago mago;
        private bool nivelCompletado;
        private Map mapa;

        #region Propiedades
        public bool NivelCompletado { get { return nivelCompletado; } }
        #endregion

        public Escenario1(GraphicsDeviceManager graphics, Mago mago)
        {
            this.graphics = graphics;
            this.mago = mago;
            this.mago.Posicion = new Vector2(0, 384);
            this.mago.PosicionMuerte = this.mago.Posicion;
            listaEscenariosTexturas = new List<Texture2D>();
            posicionEscenario1 = new Vector2(0, 0);
            posicionEscenario2 = new Vector2();
            rectEscenario1 = new Rectangle();
            rectEscenario2 = new Rectangle();
            velocidadEscenario = 1f;
            listaLenadores = new List<Lenador>();
            cantidadLenadores = 0;
            cantidadLenadoresEliminados = 0;
            tiempoLenadores = 0;
            nivelCompletado = false;
            mapa = new Map();
        }

        public void LoadContent(ContentManager Content)
        {
            this.content = Content;
            content = new ContentManager(content.ServiceProvider, "Content");
            listaEscenariosTexturas.Add(content.Load<Texture2D>("Escenarios/01_Valle/01"));
            listaEscenariosTexturas.Add(content.Load<Texture2D>("Escenarios/01_Valle/02"));
            posicionEscenario2 = new Vector2(listaEscenariosTexturas.ElementAt(0).Width, 0);
            rectEscenario1 = new Rectangle(0, 0, listaEscenariosTexturas.ElementAt(0).Width, listaEscenariosTexturas.ElementAt(0).Height);
            rectEscenario2 = new Rectangle(0, 0, listaEscenariosTexturas.ElementAt(1).Width, listaEscenariosTexturas.ElementAt(1).Height);
            Tiles.Content = Content;
            Game1.juegoMain.Camara = new Camara(graphics.GraphicsDevice.Viewport);
            mapa.Generar(new int[,]{{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                                    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                                    {7,8,7,8,7,8,7,8,7,8,7,8,7,8,7,8,7,8,7,8 }}, 40);
        }

        public void UnloadContent()
        {
            content.Unload();
        }

        public void Update(GameTime gameTime)
        {
            
            if (cantidadLenadoresEliminados <= 20)
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
                mago.ActivarPoderTierra = true;
                nivelCompletado = true;
                listaLenadores.Clear();
            }
            UpdateMagoVida(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            DrawEscenario(spriteBatch);
            DrawEnemigos(spriteBatch);
            mapa.Draw(spriteBatch);
        }

        private void UpdateEscenario(GameTime gameTime)
        {
            if (posicionEscenario1.X <= -listaEscenariosTexturas.ElementAt(0).Width)
                posicionEscenario1.X = posicionEscenario1.X * -1;
            if (posicionEscenario2.X <= -listaEscenariosTexturas.ElementAt(0).Width)
                posicionEscenario2.X = posicionEscenario2.X * -1;
            posicionEscenario1.X -= velocidadEscenario;
            posicionEscenario2.X -= velocidadEscenario;
        }

        private void UpdateMagoVida(GameTime gameTime)
        {
            if (listaLenadores.Count > 0) {
                foreach (Lenador lenador in listaLenadores)
                {
                    mago.ReducirVida(lenador.ListaAnimaciones[lenador.IndiceAnimacionActual].DestinationRect);
                    if (lenador.ListaHachas.Count>0)
                    {
                        for (int i = 0; i < lenador.ListaHachas.Count; i++)
                        {
                            mago.ReducirVida(lenador.ListaHachas[i].Animacion.DestinationRect);
                        }
                    }
                }
            }
        }


        private int DeterminarTiempoEsperaLenador()
        {
            if (cantidadLenadoresEliminados >= 12)
                new Random().Next(2, 4); // entre 2 y 3 segundos
            else if (cantidadLenadores >= 6 && cantidadLenadores < 12)
                return new Random().Next(4, 7); // entre 4 y 6 segundos
            return new Random().Next(6, 9); // entre 6 y 8 segundos
        }

        private void UpdateEnemigos(GameTime gameTime)
        {
            int tiempoEspera = DeterminarTiempoEsperaLenador(); //depende de DeterminarTiempoEsperaLenador() aparece un nuevo leñador
            tiempoLenadores += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (tiempoLenadores > tiempoEspera)
            {
                CrearLenador();
                cantidadLenadores++;
                tiempoLenadores = 0;
            }

            if (listaLenadores.Count>0)
            {
                for (int i = 0; i < listaLenadores.Count; i++)
                {
                    if (mago.ListaPoderesNormal.Count > 0)
                    {
                        for (int contPN = 0; contPN < mago.ListaPoderesNormal.Count; contPN++)
                        {
                            listaLenadores[i].Morir(mago.ListaPoderesNormal[contPN].RectDestino);
                        }
                    }
                    listaLenadores[i].Update(gameTime);
                }
                for (int i = 0; i < listaLenadores.Count; i++)
                {
                    if (listaLenadores[i].LenadorMuerto)
                    {
                        cantidadLenadoresEliminados++;
                        mago.Puntos += puntosPorEnemigo;
                        listaLenadores.RemoveAt(i);
                    }
                }
            }
        }

        public void CrearLenador()
        {
            Lenador lenador = new Lenador();
            lenador.Posicion = new Vector2((graphics.GraphicsDevice.Viewport.Width + lenador.AnchoFrame), 384);
            lenador.LoadContent(content);
            listaLenadores.Add(lenador);
        }

        private void DrawEscenario(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(listaEscenariosTexturas[0], posicionEscenario1, rectEscenario1, Color.White);
            spriteBatch.Draw(listaEscenariosTexturas[1], posicionEscenario2, rectEscenario2, Color.White);
        }

        private void DrawEnemigos(SpriteBatch spriteBatch)
        {
            if (listaLenadores.Count>0)
            {
                foreach (Lenador lenador in listaLenadores)
                {
                    lenador.Draw(spriteBatch);
                }
            }
        }
    }
}