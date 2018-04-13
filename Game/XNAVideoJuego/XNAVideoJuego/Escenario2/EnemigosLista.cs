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
    class EnemigosLista
    {
        private List<Enemigo> enemigos;
        private int cantidadEnemigos;
        private float tiempoEnemigos;
        private Texture2D magmaTextura;
        private GraphicsDeviceManager graphics;
        private Random rnd = new Random();

        public List<Enemigo> Enemigos
        {
            get { return enemigos; }
        }

        public void Initialize(GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;
            enemigos = new List<Enemigo>();
            cantidadEnemigos = 0;
            tiempoEnemigos = 0;
        }

        public void LoadContent(Texture2D magmaTextura)
        {
            this.magmaTextura = magmaTextura;
        }

        public void Update(GameTime gameTime)
        {
            int tiempoEspera = rnd.Next(2, 9);
            tiempoEnemigos += (float)gameTime.ElapsedGameTime.TotalSeconds;
            while (tiempoEnemigos > tiempoEspera)
            {
                CrearEnemigo();
                cantidadEnemigos++;
                tiempoEnemigos = 0;
 
            }
            foreach (Enemigo enemigo in enemigos)
            {
                enemigo.Update(gameTime);
            }
        }

        public void CrearEnemigo()
        {
            Enemigo enemigo = new Enemigo();
            enemigo.Initialize(graphics);
            enemigo.LoadContent(magmaTextura);
            enemigos.Add(enemigo);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Enemigo enemigo in enemigos)
            {
                enemigo.Draw(spriteBatch);
            }
        }
    }
}
