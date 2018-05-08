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
        private int anchoFrame, altoFrame;
        private Vector2 posicion;
        private int indiceAnimacionActual;
        private List<Animacion> listaAnimaciones;
        private const int velocidad = 160;
        private Vector2 posicionInicial;
        private Vector2 posicionMuerte;
        private Vector2 direccion;
        private Vector2 velocidadVector;
        private enum Estado{Caminando,Saltando}Estado EstadoActual;
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
            altoFrame = 51;
            posicion = Vector2.Zero;
            posicionInicial = Vector2.Zero;
            posicionMuerte = Vector2.Zero;
            direccion = Vector2.Zero;
            velocidadVector = Vector2.Zero;
            EstadoActual = Estado.Caminando;
            activarPoderAgua = false;
            activarPoderAire = false;
            activarPoderFuego = false;
            activarPoderNormal = true;
            activarPoderTierra = false;
            sentidoMovimiento = true; //True (Hacia la Derecha) | False (Hacia la Izquierda)
            magoMuerto = false;
            puntos = 0;
            gemas = 0;
        }

        public void LoadContent(ContentManager Content)
        {
            content = Content;
            content = new ContentManager(Content.ServiceProvider, "Content");
            vida.LoadContent(Content);
            listaAnimaciones.Add(new Animacion(content.Load<Texture2D>("Personajes/Mago/Derecha/atacando"), posicion, anchoFrame, altoFrame, 5, 80, Color.White, true));
            listaAnimaciones.Add(new Animacion(content.Load<Texture2D>("Personajes/Mago/Derecha/caminando"), posicion, anchoFrame, altoFrame, 7, 80, Color.White, true));
            listaAnimaciones.Add(new Animacion(content.Load<Texture2D>("Personajes/Mago/Derecha/corriendo"), posicion, anchoFrame, altoFrame, 7, 80, Color.White, true));
            listaAnimaciones.Add(new Animacion(content.Load<Texture2D>("Personajes/Mago/Derecha/IDLE"), posicion, anchoFrame, altoFrame, 6, 80, Color.White, true));
            listaAnimaciones.Add(new Animacion(content.Load<Texture2D>("Personajes/Mago/Derecha/muerto"), posicion, anchoFrame, altoFrame, 6, 80, Color.White, true));
            listaAnimaciones.Add(new Animacion(content.Load<Texture2D>("Personajes/Mago/Derecha/saltando"), posicion, anchoFrame, altoFrame, 5, 80, Color.White, true));
            listaAnimaciones.Add(new Animacion(content.Load<Texture2D>("Personajes/Mago/Izquierda/atacando"), posicion, anchoFrame, altoFrame, 5, 80, Color.White, true));
            listaAnimaciones.Add(new Animacion(content.Load<Texture2D>("Personajes/Mago/Izquierda/caminando"), posicion, anchoFrame, altoFrame, 7, 80, Color.White, true));
            listaAnimaciones.Add(new Animacion(content.Load<Texture2D>("Personajes/Mago/Izquierda/corriendo"), posicion, anchoFrame, altoFrame, 7, 80, Color.White, true));
            listaAnimaciones.Add(new Animacion(content.Load<Texture2D>("Personajes/Mago/Izquierda/IDLE"), posicion, anchoFrame, altoFrame, 6, 80, Color.White, true));
            listaAnimaciones.Add(new Animacion(content.Load<Texture2D>("Personajes/Mago/Izquierda/muerto"), posicion, anchoFrame, altoFrame, 6, 80, Color.White, true));
            listaAnimaciones.Add(new Animacion(content.Load<Texture2D>("Personajes/Mago/Izquierda/saltando"), posicion, anchoFrame, altoFrame, 5, 80, Color.White, true));
        }

        public void Update(GameTime gameTime)
        {
            this.gameTime = gameTime;
            vida.Update(gameTime);

            KeyboardState teclaNuevoEstado = Keyboard.GetState();

            if (!magoMuerto) {
                if (sentidoMovimiento) { FijarAnimacion("caminar", "der"); }
                else { FijarAnimacion("caminar", "izq"); }
                UpdateMover(teclaNuevoEstado);
                UpdateSaltar(teclaNuevoEstado);
                UpdatePoderes(listaPoderesAgua, teclaNuevoEstado);
                UpdatePoderes(listaPoderesAire, teclaNuevoEstado);
                UpdatePoderes(listaPoderesFuego, teclaNuevoEstado);
                UpdatePoderes(listaPoderesNormal, teclaNuevoEstado);
                UpdatePoderes(listaPoderesTierra, teclaNuevoEstado);
                posicion += direccion * velocidadVector * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (posicion.X < 0)
                    posicion.X = 0;
                if (posicion.X > graphics.GraphicsDevice.Viewport.Width - anchoFrame)
                    posicion.X = graphics.GraphicsDevice.Viewport.Width - anchoFrame;
                if (posicion.Y < 0)
                    posicion.Y = 0;
                if (posicion.Y > graphics.GraphicsDevice.Viewport.Height - altoFrame)
                    posicion.Y = graphics.GraphicsDevice.Viewport.Height - altoFrame;
            }else{
                if (sentidoMovimiento)
                    FijarAnimacion("morir", "der");
                else
                    FijarAnimacion("morir", "izq");
            }
            listaAnimaciones[indiceAnimacionActual].Update(gameTime, posicion);
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

        private void UpdateSaltar(KeyboardState teclaNuevoEstado)
        {
            if (EstadoActual == Estado.Caminando)
            {
                if (teclaNuevoEstado.IsKeyDown(Keys.Space) == true && teclaEstadoAnterior.IsKeyUp(Keys.Space))
                {
                    if (EstadoActual != Estado.Saltando)
                    {
                        EstadoActual = Estado.Saltando;
                        posicionInicial = posicion;
                        direccion.Y = -9f;
                        velocidadVector = new Vector2(velocidad, velocidad);
                    }
                }
            }
            if (EstadoActual == Estado.Saltando)
            {
                if (sentidoMovimiento)
                    FijarAnimacion("saltar", "der");
                else
                    FijarAnimacion("saltar", "izq");
                if (posicionInicial.Y - posicion.Y > 75) //Altura de Salto px
                    direccion.Y = 1;
                
                if (posicion.Y > posicionInicial.Y)
                {
                    posicion.Y = posicionInicial.Y;
                    EstadoActual = Estado.Caminando;
                    direccion = Vector2.Zero;
                }
            }
        }

        private void UpdateMover(KeyboardState teclaNuevoEstado)
        {
            if (EstadoActual == Estado.Caminando)
            {
                velocidadVector = Vector2.Zero;
                direccion = Vector2.Zero;
                if (teclaNuevoEstado.IsKeyDown(Keys.A) == true)
                {
                    sentidoMovimiento = false;
                    velocidadVector.X = velocidad;
                    direccion.X = -1;
                }
                else if (teclaNuevoEstado.IsKeyDown(Keys.D) == true)
                {
                    sentidoMovimiento = true;
                    velocidadVector.X = velocidad;
                    direccion.X = 1;
                }
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
                FijarAnimacion("atacar", "der");
                CrearDisparo(listaPoderesAgua, "poder_agua", 300);
            }
            if (activarPoderAire && teclaNuevoEstado.IsKeyDown(Keys.B) && teclaEstadoAnterior.IsKeyUp(Keys.B))
            {
                FijarAnimacion("atacar", "der");
                CrearDisparo(listaPoderesAire, "poder_aire", 350);
            }
            if (activarPoderFuego && teclaNuevoEstado.IsKeyDown(Keys.C) && teclaEstadoAnterior.IsKeyUp(Keys.C))
            {
                FijarAnimacion("atacar", "der");
                CrearDisparo(listaPoderesFuego, "poder_fuego", 250);
            }
            if (activarPoderNormal && teclaNuevoEstado.IsKeyDown(Keys.Z) && teclaEstadoAnterior.IsKeyUp(Keys.Z))
            {
                FijarAnimacion("atacar", "der");
                CrearDisparo(listaPoderesNormal, "poder_normal", 150);
            }
            if (activarPoderTierra && teclaNuevoEstado.IsKeyDown(Keys.X) && teclaEstadoAnterior.IsKeyUp(Keys.X))
            {
                FijarAnimacion("atacar", "der");
                CrearDisparo(listaPoderesTierra, "poder_tierra", 200);
            }
            teclaEstadoAnterior = teclaNuevoEstado;
        }

        private void CrearDisparo(List<PoderMago> listaPoderes, string identificador = "poder_normal", int alcanceMaximo = 100)
        {

                PoderMago poder = new PoderMago(identificador, alcanceMaximo);
                poder.LoadContent(content);
                Vector2 direccionDisparo = new Vector2(1, 0);
                if (!sentidoMovimiento) { direccionDisparo = new Vector2(-1, 0); }
                poder.Disparar(posicion + new Vector2(anchoFrame / 2, altoFrame / 2), new Vector2(200, 200), direccionDisparo);
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
    }
}