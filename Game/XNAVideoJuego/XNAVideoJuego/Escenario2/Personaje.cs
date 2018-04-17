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
        public Rectangle cuadrado, cuadrado2, cuadrado3;
        int x = 52, y = 444;
        public Vector2 posicion = new Vector2(0, 400);
        private Vector2 velocidad;
        private bool salto = false;
        float paso = 0; int capa = 0; float retraso =300;
        GameTime gametime;

        public Personaje(Texture2D mago, Rectangle c, int x, int y)
        {
            this.mago = mago;
            this.cuadrado = c;
            this.x = x;
            this.y = y;
            cuadrado = new Rectangle(x , y, 50, 51);

        }
        /*   public Vector2 Posicion
           {
               get { return posicion; }
           }

           public void Load(ContentManager contentmanager)
           {
               mago = contentmanager.Load<Texture2D>("Personajes/Mago/Derecha/caminando");
           }


           public void movimientos(GameTime gametime)
           {
               if (Keyboard.GetState().IsKeyDown(Keys.D))
                   velocidad.X = (float)gametime.ElapsedGameTime.TotalMilliseconds / 4;
               else if (Keyboard.GetState().IsKeyDown(Keys.A))
                   velocidad.X = -(float)gametime.ElapsedGameTime.TotalMilliseconds / 4;
               else velocidad.X = 0f;

               if (Keyboard.GetState().IsKeyDown(Keys.Space) && salto == false)
               {
                   posicion.Y -= 5f;
                   velocidad.Y = -11f;
                   salto = true;
               }

           }

           public void update(GameTime gametime)
           {

               posicion += velocidad;
               cuadrado = new Rectangle((int)posicion.X, (int)posicion.Y, 50, 51);
               movimientos(gametime);
               if (velocidad.Y < 10) velocidad.Y += 0.4f;

           }
           */
        public void movimientos(int p)
        {
            switch (p)
            {
                case 1: x -= 2; break;
                case 2: x += 2; break;
                case 3: y -= 2; break;
                case 4: y += 2; break;
            }
        }

        public void Update(int m,GameTime gametime)
        {
            this.gametime = gametime;
            movimientos(m);
        }

        public void drawMagoVivo(SpriteBatch sprite)
        {
            cuadrado = new Rectangle(x, y, 50, 51);
            Animaciones();
            sprite.Draw(mago, cuadrado,cuadrado2, Color.White);
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
 