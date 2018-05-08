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
    public class Escenario3 
    {
        private GraphicsDeviceManager graphics;
        private ContentManager content;
        private List<Texture2D> listaEscenariosTexturas;
        private Rectangle rectEscenario1;
        private Rectangle rectEscenario2;
        private Vector2 posicionEscenario1;
        private Vector2 posicionEscenario2;
        private float velocidadTraslado;
        private List<ErizosMarinos> listaErizosMarinos;
        private int cantidadErizosMarinos;
        private float tiempoErizosMarinos;
        private int cantidadErizosMarinosEliminados;
        private const int puntosPorEnemigo = 35;
        private Mago mago;
        private bool nivelCompletado;
        private Camara camara;
        private Map mapa;
        
        #region Propiedades
        public bool NivelCompletado { get { return nivelCompletado; } }
        #endregion

        public Escenario3(GraphicsDeviceManager graphics, Mago mago)
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
            listaErizosMarinos = new List<ErizosMarinos>();
            cantidadErizosMarinos = 0;
            tiempoErizosMarinos = 0;
            cantidadErizosMarinosEliminados = 0;
            nivelCompletado = false;
            camara = new Camara(graphics.GraphicsDevice.Viewport);
        }

        public void LoadContent(ContentManager Content)
        {
            content = Content;
            content = new ContentManager(content.ServiceProvider, "Content");
            listaEscenariosTexturas.Add(content.Load<Texture2D>("Escenarios/03_Playa/01"));
            listaEscenariosTexturas.Add(content.Load<Texture2D>("Escenarios/03_Playa/02"));
            posicionEscenario2 = new Vector2(listaEscenariosTexturas.ElementAt(0).Width, 0);
            rectEscenario1 = new Rectangle(0, 0, listaEscenariosTexturas.ElementAt(0).Width, listaEscenariosTexturas.ElementAt(0).Height);
            rectEscenario2 = new Rectangle(0, 0, listaEscenariosTexturas.ElementAt(1).Width, listaEscenariosTexturas.ElementAt(1).Height);
            Tiles.Content = content;
            mapa.Generar(new int[,] {   {0,0,0,0,0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                                        {0,0,0,0,0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0}, }, 10);
            mago.LoadContent(content);
        }

        public void Update1(GameTime gameTime)
        {
            if (cantidadErizosMarinosEliminados <= 30)
            {
                if (!mago.MagoMuerto)
                {
                    UpdateEscenario(gameTime);
                    UpdateEnemigos(gameTime);
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
                listaErizosMarinos.Clear();
            }
            UpdateMagoVida(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            DrawEscenario(spriteBatch);
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
            camara.Update(mago.Posicion, mapa.Ancho, mapa.Altura);
        }

        private void UpdateMagoVida(GameTime gameTime)
        {
            if (listaErizosMarinos.Count > 0)
            {
                foreach (ErizosMarinos erizoMarino in listaErizosMarinos)
                {
                    mago.ReducirVida(erizoMarino.RectDestino);
                }
            }
        }

        public void UpdateEnemigos(GameTime gameTime)
        {
            int tiempoEspera = new Random().Next(2, 6); //tiempo de esperar para crear un Erizo
            tiempoErizosMarinos += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (tiempoErizosMarinos > tiempoEspera)
            {
                CrearEnemigo();
                cantidadErizosMarinos++;
                tiempoErizosMarinos = 0;
            }

            if (listaErizosMarinos.Count > 0)
            {
                for (int i = 0; i < listaErizosMarinos.Count; i++)
                {
                    if (mago.ListaPoderesNormal.Count > 0)
                    {
                        for (int contPN = 0; contPN < mago.ListaPoderesNormal.Count; contPN++)
                        {
                            if (mago.ListaPoderesNormal[contPN].RectDestino.Intersects(listaErizosMarinos[i].RectDestino))
                            {
                                cantidadErizosMarinosEliminados++;
                                mago.Puntos += puntosPorEnemigo;
                                listaErizosMarinos[i].Visible = false;
                            }
                        }
                    }
                    if (mago.ListaPoderesTierra.Count > 0)
                    {
                        for (int contPT = 0; contPT < mago.ListaPoderesTierra.Count; contPT++)
                        {
                            if (mago.ListaPoderesTierra[contPT].RectDestino.Intersects(listaErizosMarinos[i].RectDestino))
                            {
                                mago.Puntos += puntosPorEnemigo;
                                listaErizosMarinos[i].Visible = false;
                            }
                        }
                    }

                    if (mago.ListaPoderesFuego.Count > 0)
                    {
                        for (int contPT = 0; contPT < mago.ListaPoderesFuego.Count; contPT++)
                        {
                            if (mago.ListaPoderesFuego[contPT].RectDestino.Intersects(listaErizosMarinos[i].RectDestino))
                            {
                                mago.Puntos += puntosPorEnemigo;
                                listaErizosMarinos[i].Visible = false;
                            }
                        }
                    }

                    if (listaErizosMarinos[i].Posicion.X <= -listaErizosMarinos[i].AnchoFrame)
                    {
                        listaErizosMarinos[i].Visible = false;
                    }
                    if (cantidadErizosMarinos <= 15)
                    {
                        listaErizosMarinos[i].Update(gameTime);
                    }
                    else
                    {
                        listaErizosMarinos[i].Update(gameTime, 1);
                    }
                }
                for (int i = 0; i < listaErizosMarinos.Count; i++)
                {
                    if (!listaErizosMarinos[i].Visible)
                    {
                        listaErizosMarinos.RemoveAt(i);
                    }
                }
            }
        }

        public void CrearEnemigo()
        {
            ErizosMarinos enemigo = new ErizosMarinos(graphics);
            enemigo.LoadContent(content);
            listaErizosMarinos.Add(enemigo);
        }

        private void DrawEscenario(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camara.Transformacion);
            spriteBatch.Draw(listaEscenariosTexturas[0], posicionEscenario1, rectEscenario1, Color.White);
            spriteBatch.Draw(listaEscenariosTexturas[1], posicionEscenario2, rectEscenario2, Color.White);
        }

        private void DrawEnemigos(SpriteBatch spriteBatch)
        {
            if (listaErizosMarinos.Count > 0)
            {
                foreach (ErizosMarinos ErizosMarinos in listaErizosMarinos)
                {
                    ErizosMarinos.Draw(spriteBatch);
                }
            }
        }
    }
}
