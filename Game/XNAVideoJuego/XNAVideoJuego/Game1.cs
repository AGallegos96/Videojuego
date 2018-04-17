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
        int capa = 0; float paso, retraso = 20;
        int x = 0, y = 0;

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
            enemigos = new EnemigosLista();
            enemigos.Initialize(graphics);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Carga fuente de puntaje
            puntajeFuente = Content.Load<SpriteFont>("Fuentes/Score");

            //Carga textura enemigos
            enemigos.LoadContent(Content.Load<Texture2D>("Objetos/02_Volcan/magma"));
            es = new Rectangle(0, 0, 800, 480);
            cu = new Rectangle(0, 0, 2048, 480);
            escenario = new Escenario(Content.Load<Texture2D>("Escenarios/02_Volcan/01"), cu, es, 0, 0);


        }
        
        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            //Invoca a Enemigos
            score = (int)gameTime.TotalGameTime.TotalSeconds;
            enemigos.Update(gameTime);
            escenario.Update(gameTime);
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

            
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
