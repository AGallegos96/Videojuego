﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XNAVideoJuego
{
    public class Enemigo
    {
        private GraphicsDeviceManager graphics;
        private Random rnd = new Random();
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

        public Vector2 Posicion
        {
            get { return posicion; }
            set { posicion = value; }

        }

        public Rectangle RectDestino
        {
            get { return rectDestino; }

        }

        public void Initialize(GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;
            anchoFrame = 34;
            altoFrame = 30;
            cantidadFrames = 3;
            frameActual = 0;
            paso = 0;
            retraso = 80f;
            posicion = new Vector2((graphics.GraphicsDevice.Viewport.Width + anchoFrame), 410);
            rectOrigen = new Rectangle();
            rectDestino = new Rectangle();

            alturaMaxima = 400;
            bandera = false;
        }

        public void LoadContent(Texture2D magmaTextura)
        {
            this.magmaTextura = magmaTextura;
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
                        posicion.X -= rnd.Next(1, 4);
                        posicion.Y = 410;
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
                        if (posicion.Y==412)
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
