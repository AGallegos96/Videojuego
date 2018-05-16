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
    public class BolaMagma
    {
        private GraphicsDeviceManager graphics;
        private Texture2D magmaTextura;
        private Rectangle rectOrigen;
        private Rectangle rectDestino;
        private Vector2 posicion;
        private int anchoFrame, altoFrame;
        private int cantidadFrames;
        private int frameActual;
        private float paso, retraso;
        private int alturaMaxima;
        private bool bandera;
        private bool visible;
        private float time; //use gameTime.TotalGameTime.TotalSeconds and update it every frame
        private float speed = MathHelper.PiOver2; // in radians per second, this is 1/4 of a circle per second atm
        private float radius = 100.0f;
        private Vector2 origin = new Vector2(400,310); // change this if you want your circle's origin elsewhere


        #region Propiedades
        public Vector2 Posicion { get { return posicion; } }
        public Rectangle RectDestino{ get { return rectDestino; }}
        public bool Visible { get { return visible; } set { visible = value; } }
        public int AnchoFrame { get { return anchoFrame; } }
        #endregion

        public BolaMagma(GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;
            anchoFrame = 34;
            altoFrame = 30;
            cantidadFrames = 3;
            frameActual = 0;
            paso = 0;
            retraso = 80f;
            posicion = new Vector2((graphics.GraphicsDevice.Viewport.Width + anchoFrame), 385);
            rectOrigen = new Rectangle();
            rectDestino = new Rectangle();
            alturaMaxima = 344;
            bandera = false;
            visible = true;
        }

        public void LoadContent(ContentManager Content)
        {
            Content = new ContentManager(Content.ServiceProvider, "Content");
            magmaTextura = Content.Load<Texture2D>("Objetos/02_Volcan/magma");
        }

        public void Update(GameTime gameTime, int idMovimiento = 0)
        {
            time = (float)gameTime.TotalGameTime.TotalSeconds;
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

            Movimientos(idMovimiento);

            rectOrigen = new Rectangle(anchoFrame*frameActual, 0, anchoFrame, altoFrame);
            rectDestino = new Rectangle((int)posicion.X, (int)posicion.Y, anchoFrame, altoFrame);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(magmaTextura, rectDestino, rectOrigen, Color.White);
        }

        private void Movimientos(int idMovimiento = 0)
        {
            switch (idMovimiento)
            {
                case 0:
                    {
                        posicion.X -= new Random().Next(4, 6);
                        posicion.Y = 385;
                    }
                    break;
                case 1:
                    {
                        if (bandera == false)
                        {
                            posicion.Y--;
                        }
                        else
                        {
                            posicion.Y++;
                        }
                        if (posicion.Y==alturaMaxima)
                        {
                            bandera = true;
                        }
                        if (posicion.Y==413)
                        {
                            bandera = false;
                        }
                        posicion.X -= new Random().Next(3, 5);
                    }
                    break;
                case 2:
                    {
                        posicion.X = (float)(Math.Cos(time * speed) * radius + origin.X);
                        posicion.Y = (float)(Math.Sin(time * speed) * radius + origin.Y);
                    }
                    break;
            }
        }

    }
}
