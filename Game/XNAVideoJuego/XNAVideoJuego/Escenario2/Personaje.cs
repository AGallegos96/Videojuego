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

        Texture2D mago;
        public Rectangle cuadrado, cuadrado2;
        int x = 52, y = 444;
        public Vector2 posicion = new Vector2(0, 390);
        private Vector2 velocidad;
        private bool salto;
        float paso = 0; int capa = 0; float retraso =300;
        GameTime gametime;

        public Rectangle Cuadro
        {
            get { return cuadrado; }

        }

        public int X
        {
            get { return x; }

        }

        public Personaje(Texture2D mago,Rectangle c, int x, int y, Vector2 p)
        {
            this.mago = mago;
           this.cuadrado = c;
            this.x = x;
            posicion = p;
            salto = true;
            this.y = y;
            cuadrado = new Rectangle(x , y, 50, 51);

        }


          public void movimientos()
           {
            if (Keyboard.GetState().IsKeyDown(Keys.D))
               
                velocidad.X = 3f;
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
                
                velocidad.X = -3f;
            else velocidad.X = 0f;

               if (Keyboard.GetState().IsKeyDown(Keys.Space) && salto == false)
               {
                   posicion.Y -= 11f;
                   velocidad.Y = -4f;
                   salto = true;
               }
            if(salto==true)
            {
                // float i = 1;
                velocidad.Y += 0.15f;// * i;
            }
            if(posicion.Y+mago.Height>=450)
            {
                salto = false;
            }

            if(salto==false)
            {
                velocidad.Y = 0f;
            }
           }

    
        
        public void Update(GameTime gametime)
        {
            this.gametime = gametime;
            posicion += velocidad;
            
            movimientos();
            
        }

        public void drawMagoVivo(SpriteBatch sprite)
        {
            cuadrado = new Rectangle((int)posicion.X, (int)posicion.Y, 50, 51); ;
            Animaciones();
            sprite.Draw(mago, cuadrado,cuadrado2, Color.White);
        }

        public void drawMagoMuerto(SpriteBatch sprite)
        {
            cuadrado = new Rectangle((int)posicion.X, (int)posicion.Y, 50, 51); ;
            Animaciones();
            sprite.Draw(mago, cuadrado, Color.White);

        }

        public void Coliciones(Vida vida,Rectangle enemigo, int ancho)
        {
            if (cuadrado.Intersects(enemigo))
            {
                posicion.X = X - cuadrado.Width - 2;
                if (vida.NumeroVidas > 0)
                {
                    vida.NumeroVidas--;
                }
                else
                {
                    Console.WriteLine("Mago Muerto");
                    //magomuerto.drawMagoMuerto(spriteBatch);
                }

            }

            if (posicion.X < 0) posicion.X = 0;
            if (posicion.X > ancho - cuadrado.Width) posicion.X = ancho - cuadrado.Width;
            if (posicion.Y < 0) velocidad.X = 1f;
            
        }    
       
        public void Animaciones()
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
 