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
    public class Escenario2
    {
        private GraphicsDeviceManager graphics;
        private ContentManager content;
        private List<Texture2D> listaEscenariosTexturas;
        private Rectangle rectEscenario1;
        private Rectangle rectEscenario2;
        private Vector2 posicionEscenario1;
        private Vector2 posicionEscenario2;
        private float velocidadTraslado;
        private List<BolaMagma> listaBolasMagma;
        private int cantidadBolasMagma;
        private float tiempoBolasMagma;
        private int cantidadBolasMagmaEliminadas;
        private const int puntosPorEnemigo = 35;
        private Mago mago;
        private bool nivelCompletado;
        private Map mapa;
        #region Propiedades
        public bool NivelCompletado { get { return nivelCompletado; } }
        #endregion

        public Escenario2(GraphicsDeviceManager graphics, Mago mago)
        {
            this.graphics = graphics;
            this.mago = mago;
            this.mago.Posicion = new Vector2(0, 394);
            listaEscenariosTexturas = new List<Texture2D>();
            posicionEscenario1 = new Vector2(0, 0);
            posicionEscenario2 = new Vector2();
            rectEscenario1 = new Rectangle();
            rectEscenario2 = new Rectangle();
            velocidadTraslado = 1f;
            listaBolasMagma = new List<BolaMagma>();
            cantidadBolasMagma = 0;
            tiempoBolasMagma = 0;
            cantidadBolasMagmaEliminadas = 0;
            nivelCompletado = false;
            mapa = new Map();
            AudioManager.PlaySoundtrack("volcal_erupsion", true);
        }

        public void LoadContent(ContentManager Content)
        {
            content = Content;
            content = new ContentManager(content.ServiceProvider, "Content");
            listaEscenariosTexturas.Add(content.Load<Texture2D>("Escenarios/02_Volcan/01"));
            listaEscenariosTexturas.Add(content.Load<Texture2D>("Escenarios/02_Volcan/02"));
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
                                    {9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9 }}, 40);
        }

        public void UnloadContent()
        {
            content.Unload();
        }

        public void Update(GameTime gameTime)
        {
            Game1.juegoMain.IndiceSpriteBatch = 2;
            if (cantidadBolasMagmaEliminadas <= 5)
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
                mago.ActivarPoderFuego = true;
                nivelCompletado = true;
                listaBolasMagma.Clear();
            }
            UpdateMagoVida(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            DrawEscenario(spriteBatch);
            DrawEnemigos(spriteBatch);
            mapa.Draw(spriteBatch);
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
            if (listaBolasMagma.Count > 0)
            {
                foreach (BolaMagma bolaMagma in listaBolasMagma)
                {
                    mago.ReducirVida(bolaMagma.RectDestino);
                }
            }
        }

        public void UpdateEnemigos(GameTime gameTime)
        {
            int tiempoEspera = new Random().Next(3, 6); //tiempo de esperar para crear una nueva Bola Magma
            tiempoBolasMagma += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (tiempoBolasMagma > tiempoEspera)
            {
                CrearEnemigo();
                cantidadBolasMagma++;
                tiempoBolasMagma = 0;
            }

            if (listaBolasMagma.Count>0)
            {
                for (int i = 0; i < listaBolasMagma.Count; i++)
                {
                    if (mago.ListaPoderesNormal.Count>0)
                    {
                        for (int contPN = 0; contPN < mago.ListaPoderesNormal.Count; contPN++)
                        {
                            if (mago.ListaPoderesNormal[contPN].RectDestino.Intersects(listaBolasMagma[i].RectDestino))
                            {
                                listaBolasMagma[i].Visible = false;
                            }
                        }
                    }
                    if (mago.ListaPoderesTierra.Count > 0)
                    {
                        for (int contPT = 0; contPT < mago.ListaPoderesTierra.Count; contPT++)
                        {
                            if (mago.ListaPoderesTierra[contPT].RectDestino.Intersects(listaBolasMagma[i].RectDestino))
                            {
                                listaBolasMagma[i].Visible = false;
                            }
                        }
                    }
                    if (listaBolasMagma[i].Posicion.X <= -listaBolasMagma[i].AnchoFrame)
                    {
                        listaBolasMagma.RemoveAt(i);
                    }
                    if (cantidadBolasMagma<=5)
                    {
                        listaBolasMagma[i].Update(gameTime, 0);
                    }
                    else if(cantidadBolasMagma > 5 && cantidadBolasMagma<11)
                    {
                        listaBolasMagma[i].Update(gameTime, 1);
                    }
                    else
                    {
                        listaBolasMagma[i].Update(gameTime, 2);
                    }
                }
                for (int i = 0; i < listaBolasMagma.Count; i++)
                {
                    if (!listaBolasMagma[i].Visible)
                    {
                        mago.Puntos += puntosPorEnemigo;
                        cantidadBolasMagmaEliminadas++;
                        listaBolasMagma.RemoveAt(i);
                    }
                }
            }
        }

        public void CrearEnemigo()
        {
            BolaMagma enemigo = new BolaMagma(graphics);
            enemigo.LoadContent(content);
            listaBolasMagma.Add(enemigo);
        }

        private void DrawEscenario(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(listaEscenariosTexturas[0], posicionEscenario1, rectEscenario1, Color.White);
            spriteBatch.Draw(listaEscenariosTexturas[1], posicionEscenario2, rectEscenario2, Color.White);
        }

        private void DrawEnemigos(SpriteBatch spriteBatch)
        {
            if (listaBolasMagma.Count > 0)
            {
                foreach (BolaMagma bolaMagma in listaBolasMagma)
                {
                    bolaMagma.Draw(spriteBatch);
                }
            }
        }
    }
}