using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNAVideoJuego
{
    public class Camara    {
        private Matrix transformacion;
        public Matrix Transformacion {
            get { return transformacion; }
        }
        private Vector2 centro;
        private Viewport viewport;
        public Camara(Viewport view) {
            viewport = view;
        }
        public void Update(Vector2 posicion, int xOffSet, int yOffSet) {
            if (posicion.X < viewport.Width / 2)
                centro.X = viewport.Width / 2;
            else if (posicion.X > xOffSet - (viewport.Width / 2))
                centro.X = xOffSet - (viewport.Width / 2);
            else centro.X = posicion.X;

            if (posicion.Y < viewport.Height / 2)
                centro.Y = viewport.Height / 2;
            else if (posicion.Y > yOffSet - (viewport.Height / 2))
                centro.Y = yOffSet - (viewport.Height / 2);
            else centro.Y = posicion.Y;

            transformacion=Matrix.CreateTranslation(new Vector3(-centro.X+(viewport.Width/2),-centro.Y+(viewport.Height/2),0));


        }
    }
}
