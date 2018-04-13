using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XNAVideoJuego.Escenario1
{
    class Enemigo
    {
        GraphicsDeviceManager graphics;
        Random rnd = new Random();
        private Texture2D magmaTextura;
        private Rectangle rectOrigen;
        private Rectangle rectDestino;
        private Vector2 posicion;
        int anchoFrame, altoFrame;
        int cantidadFrames;
        int frameActual;
        float paso, retraso;

        public void Initialize(GraphicsDeviceManager graphics, Texture2D magmaTextura)
        {
            this.graphics = graphics;
            this.magmaTextura = magmaTextura;
            anchoFrame = 34;
            altoFrame = 30;
            cantidadFrames = 3;
            frameActual = 0;
            paso = 0;
            retraso = 80f;
            posicion = new Vector2((graphics.GraphicsDevice.Viewport.Width + anchoFrame), 412);
            //rnd.Next(graphics.GraphicsDevice.Viewport.X, graphics.GraphicsDevice.Viewport.X+ancho);
            //rnd.Next(0, graphics.GraphicsDevice.Viewport.Height - alto);
            rectOrigen = new Rectangle();
            rectDestino = new Rectangle();

        }

        public void Update(GameTime gameTime)
        {
            paso += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (paso > retraso)
            {
                frameActual++;
                if (frameActual == cantidadFrames)
                {
                    frameActual = 0;
                }
                paso = 0;
            }

            if (posicion.X<-anchoFrame)
            {
                posicion.X = graphics.GraphicsDevice.Viewport.Width + anchoFrame;
            }
            posicion.X -= rnd.Next(1, 2);
            
            rectOrigen = new Rectangle(anchoFrame*frameActual, 0, anchoFrame, altoFrame);
            rectDestino = new Rectangle((int)posicion.X, (int)posicion.Y, anchoFrame, altoFrame);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(magmaTextura, rectDestino, rectOrigen, Color.Red);
        }

    }
}
