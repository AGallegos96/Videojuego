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
        public Vector2 posicion = new Vector2(0, 400);
        private Vector2 velocidad;
        private bool salto = false;
        float paso = 0; int capa = 0; float retraso = 20;

        public Personaje(Texture2D mago, Rectangle c, int x, int y)
        {
            this.mago = mago;
            this.cuadrado = c;
            this.x = x;
            this.y = y;
            cuadrado = new Rectangle(x, y, 50, 51);

        }

        public void Load(Texture2D mago)
        {
            this.mago = mago;

        } 

        public void movimientos(GameTime gametime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                velocidad.X = (float)gametime.ElapsedGameTime.TotalMilliseconds / 4;
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                velocidad.X = -(float)gametime.ElapsedGameTime.TotalMilliseconds / 4;
            else velocidad.X = 0f;

            if (Keyboard.GetState().IsKeyDown(Keys.Up) && salto == false)
            {
                posicion.Y -= 5f;
                velocidad.Y = -11f;
                salto = true;
            }

        }

        public void update(GameTime gametime)
        {
            movimientos(gametime);
            posicion += velocidad;
            cuadrado = new Rectangle((int)posicion.X, (int)posicion.Y, 50, 52);
            if (velocidad.Y < 10) velocidad.Y += 0.4f;

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
