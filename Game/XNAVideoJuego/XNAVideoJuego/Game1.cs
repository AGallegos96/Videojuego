using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using XNAVideoJuego.Escenario2;

namespace XNAVideoJuego
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Instancias de Puntajes
        SpriteFont puntajeFuente;
        int score;

        //escenario
        Escenario escenario;
        Rectangle es, cu;

        //Instancia de Tiempo
        SpriteFont tiempoFuente;
        int tiempo;
        Personaje mago;
        Rectangle r1,r2;

        #region Variables De Enemigos
        EnemigosLista enemigos;
        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            //Inicializando Enemigo
            this.IsMouseVisible = true;
            enemigos = new EnemigosLista();
            enemigos.Initialize(graphics);
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Carga fuente de puntaje
            puntajeFuente = Content.Load<SpriteFont>("Fuentes/Score");

            //Carga fuente de tiempo
            tiempoFuente = Content.Load<SpriteFont>("Fuentes/Tiempo");

            //Carga textura enemigos
            enemigos.LoadContent(Content.Load<Texture2D>("Objetos/02_Volcan/magma"));
            //carga textura escenario
            es = new Rectangle(0, 0, 800, 480);
            cu = new Rectangle(0, 0, 2048, 480);
            escenario = new Escenario(Content.Load<Texture2D>("Escenarios/02_Volcan/01"), cu, es, 0, 0);
            //carga textura personaje
            r1 = new Rectangle(0, 400, 50, 51);
            r2 = new Rectangle(0, 400, 341, 51);
            mago = new Personaje(Content.Load<Texture2D>("Personajes/Mago/Derecha/caminando"), r1, 0, 400);
            //Tiles.Content = Content;
            //mago.Load(Content);
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            //Invoca a Enemigos
            tiempo = (int)gameTime.TotalGameTime.TotalSeconds;
            escenario.Update(gameTime);
            enemigos.Update(gameTime);
            //
            int p = 0;
            if (Keyboard.GetState().IsKeyDown(Keys.Left)) p = 1;
            if (Keyboard.GetState().IsKeyDown(Keys.Right)) p = 2;
            if (Keyboard.GetState().IsKeyDown(Keys.Up)) p = 3;
            if (Keyboard.GetState().IsKeyDown(Keys.Down)) p = 4;
            mago.Update(p, gameTime);
            mago.Animaciones();

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);


            spriteBatch.Begin();
            escenario.Draw(spriteBatch);

            //Dibuja Enemigos
            enemigos.Draw(spriteBatch);

            //Dibuja Puntaje
            spriteBatch.DrawString(puntajeFuente, ("Puntaje: " + score.ToString()), new Vector2(640, 0), Color.Black);

            //Dibuja Puntaje
            spriteBatch.DrawString(tiempoFuente, (" Tiempo: " + tiempo.ToString()), new Vector2(640, 20), Color.Black);

            //dibuja personaje
            mago.drawMagoVivo(spriteBatch);

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
