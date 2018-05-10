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
    public class Vida
    {
        private List<Texture2D> vidaTextura;
        private int indiceVidaActual;
        private Rectangle rectVida;
        private int numeroVidas;
        private int ancho, alto;
        private Vector2 posicion;

        #region Propiedades
        public int NumeroVidas{get { return numeroVidas; }set { numeroVidas = value; }}
        #endregion

        public Vida(int numeroVidas)
        {
            indiceVidaActual = this.numeroVidas = numeroVidas;
            vidaTextura = new List<Texture2D>();
            ancho = 90;
            alto = 30;
            posicion = new Vector2(17,7);
            rectVida = new Rectangle(0, 0, ancho, alto);
        }

        public void LoadContent(ContentManager Content)
        {
            Content = new ContentManager(Content.ServiceProvider, "Content");
            vidaTextura.Add(Content.Load<Texture2D>("Objetos/Vidas/3_vidas"));
            vidaTextura.Add(Content.Load<Texture2D>("Objetos/Vidas/2_vidas"));
            vidaTextura.Add(Content.Load<Texture2D>("Objetos/Vidas/1_vidas"));
            vidaTextura.Add(Content.Load<Texture2D>("Objetos/Vidas/0_vidas"));
        }

        public void Update(GameTime gameTime)
        {
            switch (numeroVidas)
            {
                case 3:
                    {
                        indiceVidaActual = 0;
                    }
                    break;
                case 2:
                    {
                        indiceVidaActual = 1;
                    }
                    break;
                case 1:
                    {
                        indiceVidaActual = 2;
                    }
                    break;
                case 0:
                    {
                        indiceVidaActual = 3;
                    }
                    break;
            }
            posicion.X = 17 + ((int)-Game1.juegoMain.Camara.Transformacion.Translation.X);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(vidaTextura[indiceVidaActual], posicion, rectVida, Color.White);
        }

    }
}
