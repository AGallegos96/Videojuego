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
    public class Escenario
    {
        Texture2D escenario;
        GameTime gameTime;
        Rectangle cuadro;
        Rectangle cuadro2;
        int capa = 0; float paso, retraso = 20;
        int x = 0, y = 0;
        public Escenario(Texture2D e, Rectangle c, Rectangle co, int x, int y)
        {
            this.escenario = e;
            this.cuadro = c;
            this.cuadro2 = co;
            this.x = x;
            this.y = y;
            cuadro = new Rectangle(0, 0, 800, 480);
            cuadro2 = new Rectangle(0, 0, 2048, 480);
        }

        public void Update(GameTime gameTime)
        {
            this.gameTime = gameTime;
            avanzar();
        }
        public void avanzar()
        {
            paso += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (paso >= retraso) { if (capa >= 2)  capa = 0; else { capa++; x++; } paso = 0; }
            if (x >= 2048) x = 0; cuadro = new Rectangle(x, y, 800, 480);
        }
        public void Draw(SpriteBatch spritebatch)
        {
            cuadro2 = new Rectangle(0, 0, 800, 480);
            cuadro = new Rectangle(0, 0, 2048, 480);
            avanzar();
            spritebatch.Draw(escenario, cuadro2, cuadro, Color.White);
        }

    }

}
