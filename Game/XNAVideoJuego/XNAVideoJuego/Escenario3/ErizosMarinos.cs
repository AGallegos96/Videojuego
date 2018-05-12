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
    public class ErizosMarinos
    {
        private GraphicsDeviceManager graphics;
        private Texture2D erizoTextura;
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

        #region Propiedades
        public Vector2 Posicion { get { return posicion; } }
        public Rectangle RectDestino { get { return rectDestino; } }
        public bool Visible { get { return visible; } set { visible = value; } }
        public int AnchoFrame { get { return anchoFrame; } }
        #endregion

        public ErizosMarinos(GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;
            anchoFrame = 70;
            altoFrame = 70;
            cantidadFrames = 4;
            frameActual = 0;
            paso = 0;
            retraso = 80f;
            posicion = new Vector2(840 + ((int)-Game1.juegoMain.Camara.Transformacion.Translation.X), 370);
            rectOrigen = new Rectangle();
            rectDestino = new Rectangle();
            alturaMaxima = 344;
            bandera = false;
            visible = true;
        }

        public void LoadContent(ContentManager Content)
        {
            Content = new ContentManager(Content.ServiceProvider, "Content");
            erizoTextura = Content.Load<Texture2D>("Personajes/Erizo/Movimiento_Izquierda");
        }

        public void Update(GameTime gameTime, int idMovimiento = 0)
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

            Movimientos(idMovimiento);

            rectOrigen = new Rectangle(anchoFrame * frameActual, 0, anchoFrame, altoFrame);
            rectDestino = new Rectangle((int)posicion.X, (int)posicion.Y, anchoFrame, altoFrame);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(erizoTextura, rectDestino, rectOrigen, Color.White);
        }

        private void Movimientos(int idMovimiento = 0)
        {
            switch (idMovimiento)
            {
                case 0:
                    {
                        posicion.X -= new Random().Next(1, 8);
                        posicion.Y = 370;
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
                        if (posicion.Y == alturaMaxima)
                        {
                            bandera = true;
                        }
                        if (posicion.Y == 370)
                        {
                            bandera = false;
                        }
                        posicion.X--;
                    }
                    break;
            }
        }
    }
}