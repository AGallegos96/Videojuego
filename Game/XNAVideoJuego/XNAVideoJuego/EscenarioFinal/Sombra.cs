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
    public class Sombra
    {
        private int anchoFrame,altoFrame;
        private Vector2 posicion;
        private int indiceAnimacionActual;
        private List<Animacion> listaAnimaciones;
        private List<BolaFuego> listaBolasFuego;
        private bool sentidoMovimiento;
        private bool sombraMuerto;
        private ContentManager content;
        private float tiempoBolasFuego;
        private int cantidadVidas;
        private SpriteFont fuente;
        #region Propiedades
        public Vector2 Posicion { get { return posicion; } set { posicion = value; } }
        public List<Animacion> ListaAnimaciones { get { return listaAnimaciones; } }
        public List<BolaFuego> ListaBolasFuego { get { return listaBolasFuego; } }
        public bool SombraMuerto { get { return sombraMuerto; } }
        public int IndiceAnimacionActual { get { return indiceAnimacionActual; } }
        public bool SentidoMovimiento { get { return sentidoMovimiento; }set { sentidoMovimiento = value; } }
        #endregion

        public Sombra(int cantidadVidas)
        {
            this.cantidadVidas = cantidadVidas;
            listaAnimaciones = new List<Animacion>();
            listaBolasFuego = new List<BolaFuego>();
            indiceAnimacionActual = 1;
            altoFrame = 58;
            posicion = Vector2.Zero;
            sentidoMovimiento = false; //True (Hacia la Derecha) | False (Hacia la Izquierda)
            sombraMuerto = false;
            tiempoBolasFuego = 0;
        }

        public void LoadContent(ContentManager Content)
        {
            content = Content;
            content = new ContentManager(content.ServiceProvider, "Content");
            fuente = content.Load<SpriteFont>("Fuentes/fuenteJuego");
            listaAnimaciones.Add(new Animacion(content.Load<Texture2D>("Personajes/Sombra/Derecha/Caminando"), posicion, 50, altoFrame, 4, 80, Color.White, true));
            listaAnimaciones.Add(new Animacion(content.Load<Texture2D>("Personajes/Sombra/Derecha/Atacando"), posicion, 60, altoFrame, 6, 80, Color.White, true));
            listaAnimaciones.Add(new Animacion(content.Load<Texture2D>("Personajes/Sombra/Izquierda/Caminando"), posicion, 50, altoFrame, 4, 80, Color.White, true));
            listaAnimaciones.Add(new Animacion(content.Load<Texture2D>("Personajes/Sombra/Izquierda/Atacando"), posicion, 60, altoFrame, 6, 80, Color.White, true));
        }

        public void Update(GameTime gameTime)
        {
            Console.WriteLine(cantidadVidas.ToString());
            anchoFrame = listaAnimaciones[indiceAnimacionActual].DestinationRect.Width;
            if (!sombraMuerto)
            {
                if (sentidoMovimiento) { FijarAnimacion("caminar", "der"); }
                else { FijarAnimacion("caminar", "izq"); }
                Mover();
                UpdateBolasFuego(gameTime);
            }
            listaAnimaciones[indiceAnimacionActual].Update(gameTime, posicion);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            listaAnimaciones[indiceAnimacionActual].Draw(spriteBatch);
            if (!sombraMuerto)
            {
                DrawBolasFuego(spriteBatch);
            }
        }

        private void FijarAnimacion(string nombreAccion = "caminar", string sentidoAccion = "izq")
        {
            switch (nombreAccion)
            {
                case "caminar":
                    if (sentidoAccion.Equals("der"))
                        indiceAnimacionActual = 0;
                    else if (sentidoAccion.Equals("izq"))
                        indiceAnimacionActual = 2;
                    break;
                case "atacar":
                    if (sentidoAccion.Equals("der"))
                        indiceAnimacionActual = 1;
                    else if (sentidoAccion.Equals("izq"))
                        indiceAnimacionActual = 3;
                    break;
                default:
                    indiceAnimacionActual = 0;
                    break;
            }
        }

        private void Mover()
        {
            int moverse = 1;
            if (sentidoMovimiento)
            {
                posicion.X += moverse;
            }
            else
            {
                posicion.X -= moverse;
            }
        }

        public void ReducirVida(Rectangle rectPoderMago, int cantidadReducir)
        {
            if (cantidadVidas > 0)
            {
                if (listaAnimaciones[indiceAnimacionActual].DestinationRect.Intersects(rectPoderMago))
                {
                    if(sentidoMovimiento)
                        posicion.X -= 200; //Retroceder 200 pixeles
                    else
                        posicion.X += 200; //Retroceder 200 pixeles
                    cantidadVidas -= cantidadReducir;
                }
            }
            else if (cantidadVidas <= 0)
            {
                sombraMuerto = true;
            }
        }

        private void UpdateBolasFuego(GameTime gameTime)
        {
            int tiempoEspera = new Random().Next(3, 7); //Entre 3 y 6 segundos se lanza una nueva bola de fuego
            tiempoBolasFuego += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (tiempoBolasFuego > tiempoEspera)
            {
                CrearDisparo();
                tiempoBolasFuego = 0;
            }
            if (listaBolasFuego.Count > 0)
            {
                for (int i = 0; i < listaBolasFuego.Count; i++)
                {
                    listaBolasFuego[i].Update(gameTime);
                    if (!listaBolasFuego[i].Visible)
                    {
                        listaBolasFuego.RemoveAt(i);
                    }
                }
            }
        }

        private void CrearDisparo()
        {
            BolaFuego bolaFuego = new BolaFuego();
            bolaFuego.LoadContent(content);
            Vector2 direccionDisparo = new Vector2(1, 0);
            if (!sentidoMovimiento)
            {
                bolaFuego.IndiceAnimacionActual = 1;
                direccionDisparo = new Vector2(-1, 0);
                FijarAnimacion("atacar", "izq");
            }
            else
            {
                bolaFuego.IndiceAnimacionActual = 0;
                FijarAnimacion("atacar", "der");
            }
            bolaFuego.Disparar(posicion + new Vector2(anchoFrame / 2, altoFrame / 4), new Vector2(200, 200), direccionDisparo);
            listaBolasFuego.Add(bolaFuego);
        }

        private void DrawBolasFuego(SpriteBatch spriteBatch)
        {
            if (listaBolasFuego.Count > 0)
            {
                foreach (BolaFuego bolaFuego in listaBolasFuego)
                {
                    bolaFuego.Draw(spriteBatch);
                }
            }
            spriteBatch.DrawString(fuente, ("Sombra: " + cantidadVidas.ToString()), new Vector2(645 + ((int)-Game1.juegoMain.Camara.Transformacion.Translation.X), 62), Color.Black);
        }
    }
}
