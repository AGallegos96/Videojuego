using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace XNAVideoJuego
{
    public class Personaje
    {

        Texture2D mago; public Rectangle cuadrado, cuadrado2; int x = 52, y = 444;
        float paso = 0; int capa = 0; float retraso = 20;

        public Personaje(Texture2D mago, Rectangle c, int x, int y)
        {
            this.mago = mago;
            this.cuadrado = c;
            this.x = x;
            this.y = y;
            cuadrado = new Rectangle(x, y, 50, 51);

        }
        public void movimientos(int coordenada)
        {
            switch (coordenada)
            {
                case 1: x += 2; break;
                case 2: y += 2; break;

            }

        }

        public void update(int movimiento)
        {
            movimientos(movimiento);

        }

        public void drawMagoVivo(SpriteBatch sprite)
        {
            cuadrado = new Rectangle(x, y, 50, 51);
            sprite.Draw(mago, cuadrado, Color.White);
        }

        public void drawMagoMuerto(SpriteBatch sprite)
        {
            cuadrado = new Rectangle(x, y, 50, 51);
            sprite.Draw(mago, cuadrado, Color.White);

        }
        
        public bool Coliciones(Rectangle mago, Rectangle enemigo)
        {
            if (mago.Intersects(enemigo))
                return true;
            else
                return false;
        }    
       
        public void Animaciones(int n, GameTime gametime)
        {
            paso += (float)gametime.ElapsedGameTime.TotalMilliseconds;
            if(paso >= retraso && paso >0)
            {
                if (capa >= 6) { capa = 0; }
                else { capa++; }
                paso = 0;
            }
            cuadrado2 = new Rectangle(50 * capa, 0, 50, 50);
        }

    }
}
