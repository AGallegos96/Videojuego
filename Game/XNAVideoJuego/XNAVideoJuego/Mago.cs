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
    public class Mago
    {
        private GraphicsDeviceManager graphics;
        private ContentManager content;
        private GameTime gameTime;
        private int anchoFrame;
        private Vector2 posicion;
        private int indiceAnimacionActual;
        private List<Animacion> listaAnimaciones;
        private Vector2 posicionMuerte;
        private Vector2 velocidad;
        private bool salto;
        private Vida vida;
        private bool activarPoderAgua, activarPoderAire, activarPoderFuego, activarPoderNormal, activarPoderTierra;
        private List<PoderMago> listaPoderesAgua;
        private List<PoderMago> listaPoderesAire;
        private List<PoderMago> listaPoderesFuego;
        private List<PoderMago> listaPoderesNormal;
        private List<PoderMago> listaPoderesTierra;
        private KeyboardState teclaEstadoAnterior;
        private bool sentidoMovimiento;
        private bool magoMuerto;
        private int puntos;
        private int gemas;
        private string nombreJugador;

        #region Propiedades
        public List<Animacion> ListaAnimaciones { get { return listaAnimaciones; } }
        public List<PoderMago> ListaPoderesAgua{get { return listaPoderesAgua; } }
        public List<PoderMago> ListaPoderesAire { get { return listaPoderesAire; } }
        public List<PoderMago> ListaPoderesFuego { get { return listaPoderesFuego; } }
        public List<PoderMago> ListaPoderesNormal { get { return listaPoderesNormal; } }
        public List<PoderMago> ListaPoderesTierra { get { return listaPoderesTierra; } }
        public bool ActivarPoderAgua { get { return activarPoderAgua; } set { activarPoderAgua = value; } }
        public bool ActivarPoderAire { get { return activarPoderAire; } set { activarPoderAire = value; } }
        public bool ActivarPoderFuego { get { return activarPoderFuego; } set { activarPoderFuego = value; } }
        public bool ActivarPoderTierra { get { return activarPoderTierra; } set { activarPoderTierra = value; } }
        public Vida Vida { get { return vida; } }
        public Vector2 Posicion { get { return posicion; } set { posicion = value; } }
        public Vector2 PosicionMuerte { get { return posicionMuerte; } set { posicionMuerte = value; } }
        public int Puntos { get { return puntos; } set { puntos = value; } }
        public int Gemas { get { return gemas; } set { gemas = value; } }
        public bool MagoMuerto { get { return magoMuerto; } }
        public string NombreJugador { get { return nombreJugador; } }
        #endregion

        public Mago(GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;
            listaAnimaciones = new List<Animacion>();
            listaPoderesAgua = new List<PoderMago>();
            listaPoderesAire = new List<PoderMago>();
            listaPoderesFuego = new List<PoderMago>();
            listaPoderesNormal = new List<PoderMago>();
            listaPoderesTierra = new List<PoderMago>();
            indiceAnimacionActual = 1;
            vida = new Vida(3);
            anchoFrame = 50;
            posicion = Vector2.Zero;
            posicionMuerte = Vector2.Zero;
            velocidad = Vector2.Zero;
            activarPoderAgua = false;
            activarPoderAire = false;
            activarPoderFuego = false;
            activarPoderNormal = true;
            activarPoderTierra = false;
            sentidoMovimiento = true; //True (Hacia la Derecha) | False (Hacia la Izquierda)
            magoMuerto = false;
            puntos = 0;
            gemas = 0;
            salto = false;
            nombreJugador = Game1.juegoMain.NombreJugador;
        }

        public void LoadContent(ContentManager Content)
        {
            content = Content;
            content = new ContentManager(Content.ServiceProvider, "Content");
            vida.LoadContent(Content);
            listaAnimaciones.Add(new Animacion(content.Load<Texture2D>("Personajes/Mago/Derecha/atacando"), posicion, anchoFrame, 49, 5, 80, Color.White, true));
            listaAnimaciones.Add(new Animacion(content.Load<Texture2D>("Personajes/Mago/Derecha/caminando"), posicion, anchoFrame, 47, 7, 80, Color.White, true));
            listaAnimaciones.Add(new Animacion(content.Load<Texture2D>("Personajes/Mago/Derecha/corriendo"), posicion, anchoFrame, 48, 7, 80, Color.White, true));
            listaAnimaciones.Add(new Animacion(content.Load<Texture2D>("Personajes/Mago/Derecha/IDLE"), posicion, anchoFrame, 50, 6, 80, Color.White, true));
            listaAnimaciones.Add(new Animacion(content.Load<Texture2D>("Personajes/Mago/Derecha/muerto"), posicion, anchoFrame, 50, 6, 80, Color.White, true));
            listaAnimaciones.Add(new Animacion(content.Load<Texture2D>("Personajes/Mago/Derecha/saltando"), posicion, anchoFrame, 49, 5, 80, Color.White, true));
            listaAnimaciones.Add(new Animacion(content.Load<Texture2D>("Personajes/Mago/Izquierda/atacando"), posicion, anchoFrame, 49, 5, 80, Color.White, true));
            listaAnimaciones.Add(new Animacion(content.Load<Texture2D>("Personajes/Mago/Izquierda/caminando"), posicion, anchoFrame, 47, 7, 80, Color.White, true));
            listaAnimaciones.Add(new Animacion(content.Load<Texture2D>("Personajes/Mago/Izquierda/corriendo"), posicion, anchoFrame, 48, 7, 80, Color.White, true));
            listaAnimaciones.Add(new Animacion(content.Load<Texture2D>("Personajes/Mago/Izquierda/IDLE"), posicion, anchoFrame, 50, 6, 80, Color.White, true));
            listaAnimaciones.Add(new Animacion(content.Load<Texture2D>("Personajes/Mago/Izquierda/muerto"), posicion, anchoFrame, 50, 6, 80, Color.White, true));
            listaAnimaciones.Add(new Animacion(content.Load<Texture2D>("Personajes/Mago/Izquierda/saltando"), posicion, anchoFrame, 49, 5, 80, Color.White, true));
        }

        public void UnloadContent()
        {
            content.Unload();
        }

        public void Update(GameTime gameTime)
        {
            this.gameTime = gameTime;
            vida.Update(gameTime);

            KeyboardState teclaNuevoEstado = Keyboard.GetState();

            if (!magoMuerto) {
                if (teclaNuevoEstado.IsKeyUp(Keys.A) && !sentidoMovimiento)
                    FijarAnimacion("IDLE", "izq");
                else if (teclaNuevoEstado.IsKeyUp(Keys.D) && sentidoMovimiento)
                    FijarAnimacion("IDLE", "der");

                posicion += velocidad;
                Input(teclaNuevoEstado);
                if (velocidad.Y < 15) velocidad.Y += 0.4f;

                UpdatePoderes(listaPoderesAgua, teclaNuevoEstado);
                UpdatePoderes(listaPoderesAire, teclaNuevoEstado);
                UpdatePoderes(listaPoderesFuego, teclaNuevoEstado);
                UpdatePoderes(listaPoderesNormal, teclaNuevoEstado);
                UpdatePoderes(listaPoderesTierra, teclaNuevoEstado);
            }
            else
            {
                if (sentidoMovimiento)
                    FijarAnimacion("morir", "der");
                else
                    FijarAnimacion("morir", "izq");
            }
            listaAnimaciones[indiceAnimacionActual].Update(gameTime, posicion);
            teclaEstadoAnterior = teclaNuevoEstado;
        }

        private void Input(KeyboardState teclaNuevoEstado)
        {
            if (teclaNuevoEstado.IsKeyDown(Keys.D))
            {
                velocidad.X = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 4;
                FijarAnimacion("caminar", "der");
                sentidoMovimiento = true;
            }
            else if (teclaNuevoEstado.IsKeyDown(Keys.A))
            {
                velocidad.X = -(float)gameTime.ElapsedGameTime.TotalMilliseconds / 4;
                FijarAnimacion("caminar", "izq");
                sentidoMovimiento = false;
            }
            else velocidad.X = 0f;

            if (teclaNuevoEstado.IsKeyDown(Keys.Space) == true && teclaEstadoAnterior.IsKeyUp(Keys.Space) && salto == false)
            {
                if (sentidoMovimiento)
                    FijarAnimacion("saltar", "der");
                else
                    FijarAnimacion("saltar", "izq");
                posicion.Y -= 5f;
                velocidad.Y = -10f; //Aquí se configura el salto
                salto = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            vida.Draw(spriteBatch);
            listaAnimaciones[indiceAnimacionActual].Draw(spriteBatch);
            if (!magoMuerto){
                DrawPoderes(spriteBatch, listaPoderesAgua);
                DrawPoderes(spriteBatch, listaPoderesAire);
                DrawPoderes(spriteBatch, listaPoderesFuego);
                DrawPoderes(spriteBatch, listaPoderesNormal);
                DrawPoderes(spriteBatch, listaPoderesTierra);
            }
        }

        private void DrawPoderes(SpriteBatch spriteBatch, List<PoderMago> listaPoderes)
        {
            foreach (PoderMago poder in listaPoderes)
            {
                poder.Draw(spriteBatch);
            }
        }

        private void FijarAnimacion(string nombreAccion = "caminar", string sentidoAccion = "der")
        {
            switch (nombreAccion)
            {
                case "atacar":
                        if (sentidoAccion.Equals("der"))
                            indiceAnimacionActual = 0;
                        else if (sentidoAccion.Equals("izq"))
                            indiceAnimacionActual = 6;
                        break;
                case "caminar":
                        if (sentidoAccion.Equals("der"))
                            indiceAnimacionActual = 1;
                        else if (sentidoAccion.Equals("izq"))
                            indiceAnimacionActual = 7;
                        break;
                case "correr":
                        if (sentidoAccion.Equals("der"))
                            indiceAnimacionActual = 2;
                        else if (sentidoAccion.Equals("izq"))
                            indiceAnimacionActual = 8;
                        break;
                case "IDLE":
                        if (sentidoAccion.Equals("der"))
                            indiceAnimacionActual = 3;
                        else if (sentidoAccion.Equals("izq"))
                            indiceAnimacionActual = 9;
                        break;
                case "morir":
                        if (sentidoAccion.Equals("der"))
                            indiceAnimacionActual = 4;
                        else if (sentidoAccion.Equals("izq"))
                            indiceAnimacionActual = 10;
                        break;
                case "saltar":
                        if (sentidoAccion.Equals("der"))
                            indiceAnimacionActual = 5;
                        else if (sentidoAccion.Equals("izq"))
                            indiceAnimacionActual = 11;
                        break;
                default:
                        indiceAnimacionActual = 1;
                        break;
            }
        }

        private void UpdatePoderes(List<PoderMago> listaPoderes, KeyboardState teclaNuevoEstado)
        {
            if (listaPoderes.Count>0)
            {
                for (int i = 0; i < listaPoderes.Count; i++)
                {
                    listaPoderes[i].Update(gameTime);
                    if (!listaPoderes[i].Visible)
                        listaPoderes.RemoveAt(i);
                }
            }
            if (activarPoderAgua && teclaNuevoEstado.IsKeyDown(Keys.V) && teclaEstadoAnterior.IsKeyUp(Keys.V))
            {
                CrearDisparo(listaPoderesAgua, "poder_agua", 300);
            }
            if (activarPoderAire && teclaNuevoEstado.IsKeyDown(Keys.B) && teclaEstadoAnterior.IsKeyUp(Keys.B))
            {
                CrearDisparo(listaPoderesAire, "poder_aire", 350);
            }
            if (activarPoderFuego && teclaNuevoEstado.IsKeyDown(Keys.C) && teclaEstadoAnterior.IsKeyUp(Keys.C))
            {
                CrearDisparo(listaPoderesFuego, "poder_fuego", 250);
            }
            if (activarPoderNormal && teclaNuevoEstado.IsKeyDown(Keys.Z) && teclaEstadoAnterior.IsKeyUp(Keys.Z))
            {
                CrearDisparo(listaPoderesNormal, "poder_normal", 150); 
            }
            if (activarPoderTierra && teclaNuevoEstado.IsKeyDown(Keys.X) && teclaEstadoAnterior.IsKeyUp(Keys.X))
            {
                CrearDisparo(listaPoderesTierra, "poder_tierra", 200);
            }
            
        }

        private void CrearDisparo(List<PoderMago> listaPoderes, string identificador = "poder_normal", int alcanceMaximo = 100)
        {
            if (sentidoMovimiento)
                FijarAnimacion("atacar", "der");
            else
                FijarAnimacion("atacar", "izq");
            PoderMago poder = new PoderMago(identificador, alcanceMaximo);
            poder.LoadContent(content);
            Vector2 direccionDisparo = new Vector2(1, 0);
            if (!sentidoMovimiento) { direccionDisparo = new Vector2(-1, 0); }
            poder.Disparar(posicion + new Vector2(anchoFrame / 2, 48.5f / 2), new Vector2(200, 200), direccionDisparo);
            listaPoderes.Add(poder);
        }

        public void ReducirVida(Rectangle rectEnemigo)
        {
            if (vida.NumeroVidas > 0)
            {
                if (listaAnimaciones[indiceAnimacionActual].DestinationRect.Intersects(rectEnemigo))
                {
                    posicion.X -= 200; //Retroceder 200 pixeles
                    vida.NumeroVidas--;
                }
            }
            else if (vida.NumeroVidas == 0)
            {
                magoMuerto = true;
            }
        }

        public void Colision(Rectangle rect, int xOffSet, int yOffSet)
        {

            if (listaAnimaciones[indiceAnimacionActual].DestinationRect.TouchTopOf(rect))
            {
                posicion.Y = rect.Y - listaAnimaciones[indiceAnimacionActual].DestinationRect.Height;
                velocidad.Y = 0f;
                salto = false;
            }
            if (listaAnimaciones[indiceAnimacionActual].DestinationRect.TouchLeftOf(rect))
            {
                posicion.X = rect.X - listaAnimaciones[indiceAnimacionActual].DestinationRect.Width - 2;
            }
            if (listaAnimaciones[indiceAnimacionActual].DestinationRect.TouchRightOf(rect))
            {
                posicion.X = rect.X + listaAnimaciones[indiceAnimacionActual].DestinationRect.Width + 2;
            }
            if (listaAnimaciones[indiceAnimacionActual].DestinationRect.TouchBottomOf(rect))
            {
                velocidad.Y = 1f;
            }
            if (posicion.X < 0) posicion.X = 0;
            if (posicion.X > xOffSet - listaAnimaciones[indiceAnimacionActual].DestinationRect.Width) posicion.X = xOffSet - listaAnimaciones[indiceAnimacionActual].DestinationRect.Width;
            if (posicion.Y < 0) velocidad.X = 1f;
            if (posicion.Y > yOffSet - listaAnimaciones[indiceAnimacionActual].DestinationRect.Height) posicion.Y = yOffSet - listaAnimaciones[indiceAnimacionActual].DestinationRect.Height;
        }

    }
}