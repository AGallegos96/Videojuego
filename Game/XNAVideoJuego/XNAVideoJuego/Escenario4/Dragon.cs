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
   public  class Dragon
    {
        private GraphicsDeviceManager graphics;
        private int anchoFrame, altoFrame;
        private Vector2 posicion;
        private int indiceAnimacionActual;
        private List<Animacion> listaAnimaciones;
        private List<Fuego> listaFuegos;
        private bool sentidoMovimiento;
        private bool dragonv;
        private ContentManager content;
        private float tiempofuego;
        private bool visible;
        private Rectangle rectDestino;



        #region Propiedades
        public Vector2 Posicion { get { return posicion; } set { posicion = value; } }
        public List<Animacion> ListaAnimaciones { get { return listaAnimaciones; } }
        public List<Fuego> ListaFuegos { get { return listaFuegos; } }
        public bool Dragonv { get { return dragonv; } }
        public int AnchoFrame { get { return anchoFrame; } }
       
        public Rectangle RectDestino { get { return rectDestino; } }
        public int IndiceAnimacionActual { get { return indiceAnimacionActual; } }
        public bool Visible { get { return visible; } set { visible = value; } }
        #endregion

        public Dragon()
        {
            listaAnimaciones = new List<Animacion>();
            listaFuegos = new List<Fuego>();
            indiceAnimacionActual = 1 ;
            anchoFrame = 105;
            altoFrame = 102;
            posicion = Vector2.Zero;
            sentidoMovimiento = false; //True (Hacia la Derecha) | False (Hacia la Izquierda)
            tiempofuego = 0;
            dragonv = false;
        }

        public void LoadContent(ContentManager Content)
        {
            content = Content;
            content = new ContentManager(content.ServiceProvider, "Content");
            listaAnimaciones.Add(new Animacion(content.Load<Texture2D>("Personajes/Dragón/ataque_izquierda"), posicion, 105, altoFrame, 4, 80, Color.White, true));
            listaAnimaciones.Add(new Animacion(content.Load<Texture2D>("Personajes/Dragón/ataque_derecha"), posicion, 105, altoFrame, 4, 80, Color.White, true));
     }

        public void Update(GameTime gameTime)
        {
              anchoFrame = listaAnimaciones[indiceAnimacionActual].DestinationRect.Width;

         
                if (sentidoMovimiento) { FijarAnimacion("correr", "ataque_izquierda"); }
                else { FijarAnimacion("correr", "ataque_derecha");
                Mover();
                UpdateFuego(gameTime);
                }


                listaAnimaciones[indiceAnimacionActual].Update(gameTime, posicion);


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            listaAnimaciones[indiceAnimacionActual].Draw(spriteBatch);
            if (!dragonv)
            {
                DrawFuego(spriteBatch);
            }

        }

        private void FijarAnimacion(string nombreAccion = "correr", string sentidoAccion = "ataque_izquierda")
        {
            switch (nombreAccion)
            {
                case "correr":
                    if (sentidoAccion.Equals("ataque_izquierda"))
                        indiceAnimacionActual = 0;
                    else if (sentidoAccion.Equals("ataque_derecha"))
                        indiceAnimacionActual = 1;
                    break;
               
                default:
                    indiceAnimacionActual = 0;
                    break;
            }
        }

        private void Mover()
        {
            int moverse = 2;
            posicion.Y = -30;
            if (sentidoMovimiento)
                posicion.X += moverse;
               
            else
                posicion.X -= moverse;
        }

     

        private void UpdateFuego(GameTime gameTime)
        {
            int tiempoEspera = new Random().Next(2, 6); //Entre 3 y 6 segundos se lanza una nueva 
            tiempofuego += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (tiempofuego > tiempoEspera)
            {
                CrearDisparo();
                tiempofuego = 0;
            }
            if (listaFuegos.Count > 0)
            {
                for (int i = 0; i < listaFuegos.Count; i++)
                {
                    listaFuegos[i].Update(gameTime);
                    if (!listaFuegos[i].Visible)
                    {
                        listaFuegos.RemoveAt(i);
                    }
                }
            }
        }

        private void CrearDisparo()
        {
            Fuego fuego = new Fuego();
            fuego.LoadContent(content);
            FijarAnimacion("correr", "ataque_derecha");
            Vector2 direccionDisparo = new Vector2(0, 1);
            fuego.Disparar(posicion + new Vector2(anchoFrame / 2, altoFrame / 4), new Vector2(200, 200), direccionDisparo);
            listaFuegos.Add(fuego);
        }

        private void DrawFuego(SpriteBatch spriteBatch)
        {
            if (listaFuegos.Count > 0)
            {
                foreach (Fuego fuego in listaFuegos)
                {
                    fuego.Draw(spriteBatch);
                }
            }
        }

    }
}
