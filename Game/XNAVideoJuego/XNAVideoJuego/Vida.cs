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
    class Vida
    {
        private GraphicsDeviceManager graphics;
        private List<Texture2D> vidaTextura;
        private int indiceVidaActual;
        private Rectangle rectVida;
        private int numeroVidas;
        private int ancho, alto;
        private Vector2 posicion;

        public List<Texture2D> VidaTextura
        {
            get { return vidaTextura; }
            set { vidaTextura = value; }

        }

        public int NumeroVidas
        {
            get { return numeroVidas; }
            set { numeroVidas = value; }

        }


        public void Initialize(GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;
            numeroVidas = 3;
            vidaTextura = new List<Texture2D>();
            ancho = 90;
            alto = 30;
            posicion = new Vector2(17,7);
            rectVida = new Rectangle(0, 0, ancho, alto);
        }

        public void LoadContent(List<Texture2D> vidaTextura)
        {
            this.vidaTextura = vidaTextura;
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
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(vidaTextura[indiceVidaActual], posicion, rectVida, Color.White);
        }

    }
}
