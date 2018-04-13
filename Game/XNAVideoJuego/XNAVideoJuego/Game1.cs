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

        #region Variables De Enemigo
        Texture2D texturaMagma;
        Enemigo enemigo;
        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            //Inicializando Enemigo
            enemigo = new Enemigo();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Carga fuente de puntaje
            puntajeFuente = Content.Load<SpriteFont>("Fuentes/Score");

            //Cargando imagen Magma
            texturaMagma = Content.Load<Texture2D>("Objetos/02_Volcan/magma");
            enemigo.Initialize(graphics, texturaMagma);

        }
        
        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            enemigo.Update(gameTime);

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);


            spriteBatch.Begin();

            //Dibuja Enemigo
            enemigo.Draw(spriteBatch);
            //Dibuja Puntaje
            spriteBatch.DrawString(puntajeFuente, ("Puntaje: " + score.ToString()), new Vector2(640, 0), Color.Black);


            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
