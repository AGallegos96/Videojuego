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
    public class Lenador
    {
        private int anchoFrame, altoFrame;
        private Vector2 posicion;
        private int indiceAnimacionActual;
        private List<Animacion> listaAnimaciones;
        private List<Hacha> listaHachas;
        private bool sentidoMovimiento;
        private bool lenadorMuerto;
        private ContentManager content;
        private float tiempoHachas;

        #region Propiedades
        public Vector2 Posicion { get { return posicion; } set { posicion = value; } }
        public List<Animacion> ListaAnimaciones { get { return listaAnimaciones; } }
        public List<Hacha> ListaHachas { get { return listaHachas; } }
        public bool LenadorMuerto { get { return lenadorMuerto; }}
        public int AnchoFrame { get { return anchoFrame; } }
        public int IndiceAnimacionActual { get { return indiceAnimacionActual; } }
        #endregion

        public Lenador()
        {
            listaAnimaciones = new List<Animacion>();
            listaHachas = new List<Hacha>();
            indiceAnimacionActual = 1;
            anchoFrame = 40;
            altoFrame = 50;
            posicion = Vector2.Zero;
            sentidoMovimiento = false; //True (Hacia la Derecha) | False (Hacia la Izquierda)
            lenadorMuerto = false;
            tiempoHachas = 0;
        }

        public void LoadContent(ContentManager Content)
        {
            content = Content;
            content = new ContentManager(content.ServiceProvider, "Content");
            listaAnimaciones.Add(new Animacion(content.Load<Texture2D>("Personajes/Leñador/Derecha/corriendo"), posicion, 40, altoFrame, 6, 80, Color.White, true));
            listaAnimaciones.Add(new Animacion(content.Load<Texture2D>("Personajes/Leñador/Derecha/muerto"), posicion, 45, altoFrame, 3, 80, Color.White, true));
            listaAnimaciones.Add(new Animacion(content.Load<Texture2D>("Personajes/Leñador/Derecha/parado"), posicion, 36, altoFrame, 1, 80, Color.White, true));
            listaAnimaciones.Add(new Animacion(content.Load<Texture2D>("Personajes/Leñador/Izquierda/corriendo"), posicion, 40, altoFrame, 6, 80, Color.White, true));
            listaAnimaciones.Add(new Animacion(content.Load<Texture2D>("Personajes/Leñador/Izquierda/muerto"), posicion, 45, altoFrame, 3, 80, Color.White, true));
            listaAnimaciones.Add(new Animacion(content.Load<Texture2D>("Personajes/Leñador/Izquierda/parado"), posicion, 36, altoFrame, 1, 80, Color.White, true));
        }

        public void Update(GameTime gameTime)
        {
            anchoFrame = listaAnimaciones[indiceAnimacionActual].DestinationRect.Width;
            if (!lenadorMuerto)
            {
                if (sentidoMovimiento) { FijarAnimacion("correr", "der"); }
                else { FijarAnimacion("correr", "izq"); }
                Mover();
                UpdateHachas(gameTime);
            }
            else
            {
                if (sentidoMovimiento) { FijarAnimacion("morir", "der"); }
                else { FijarAnimacion("morir", "izq"); }
            }
            listaAnimaciones[indiceAnimacionActual].Update(gameTime, posicion);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            listaAnimaciones[indiceAnimacionActual].Draw(spriteBatch);
            if (!lenadorMuerto)
            {
                DrawHachas(spriteBatch);
            }
        }

        private void FijarAnimacion(string nombreAccion = "correr", string sentidoAccion = "izq")
        {
            switch (nombreAccion)
            {
                case "correr":
                        if (sentidoAccion.Equals("der"))
                            indiceAnimacionActual = 0;
                        else if (sentidoAccion.Equals("izq"))
                            indiceAnimacionActual = 3;
                        break;
                case "morir":
                        if (sentidoAccion.Equals("der"))
                            indiceAnimacionActual = 1;
                        else if (sentidoAccion.Equals("izq"))
                            indiceAnimacionActual = 4;
                        break;
                case "parar":
                        if (sentidoAccion.Equals("der"))
                            indiceAnimacionActual = 2;
                        else if (sentidoAccion.Equals("izq"))
                            indiceAnimacionActual = 5;
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
                posicion.X += moverse;
            else
                posicion.X -= moverse;
        }

        public void Morir(Rectangle rectPoderMago)
        {
            if (!lenadorMuerto)
            {
                if (listaAnimaciones[indiceAnimacionActual].DestinationRect.Intersects(rectPoderMago))
                {
                    lenadorMuerto = true;
                }
            }
        }

        private void UpdateHachas(GameTime gameTime)
        {
            int tiempoEspera = new Random().Next(3,7); //Entre 3 y 6 segundos se lanza una nueva hacha
            tiempoHachas += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (tiempoHachas > tiempoEspera)
            {
                CrearDisparo();
                tiempoHachas = 0;
            }
            if (listaHachas.Count>0)
            {
                for (int i = 0; i < listaHachas.Count; i++)
                {
                    listaHachas[i].Update(gameTime);
                    if (!listaHachas[i].Visible)
                    {
                        listaHachas.RemoveAt(i);
                    }
                }
            }
        }

        private void CrearDisparo()
        {
            Hacha hacha = new Hacha();
            hacha.LoadContent(content);
            FijarAnimacion("parado", "izq");
            Vector2 direccionDisparo = new Vector2(1, 0);
            if (!sentidoMovimiento) { direccionDisparo = new Vector2(-1, 0); }
            hacha.Disparar(posicion + new Vector2(anchoFrame / 2, altoFrame / 4), new Vector2(200, 200), direccionDisparo);
            listaHachas.Add(hacha);
        }

        private void DrawHachas(SpriteBatch spriteBatch)
        {
            if (listaHachas.Count>0)
            {
                foreach (Hacha hacha in listaHachas)
                {
                    hacha.Draw(spriteBatch);
                }
            }
        }
    }
}
