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

        //Instancias de Fuente
        SpriteFont fuente;

        //Instancias de Nivel
        int nivelActual;

        //Instancias de Gemas
        int gemas;

        //Instancias de Puntajes
        int score;

        //escenario
        Escenario escenario;
        Rectangle es, cu;

        //Instancia de Tiempo
        int tiempo;
        Personaje mago;
        Rectangle r1, r2;

        //Instancia Vida
        Vida vida;

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
            nivelActual = 2;
            gemas = 1;
            vida = new Vida();
            vida.Initialize(graphics);
            enemigos = new EnemigosLista();
            enemigos.Initialize(graphics);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Carga fuente
            fuente = Content.Load<SpriteFont>("Fuentes/Fuente");

            //Cargando texturaVidas
            vida.VidaTextura = new List<Texture2D> { Content.Load<Texture2D>("Objetos/Vidas/3_vidas"), Content.Load<Texture2D>("Objetos/Vidas/2_vidas"), Content.Load<Texture2D>("Objetos/Vidas/1_vidas"), Content.Load<Texture2D>("Objetos/Vidas/0_vidas") };

            //Carga textura enemigos
            enemigos.LoadContent(Content.Load<Texture2D>("Objetos/02_Volcan/magma"));
            //carga textura escenario
            cu = new Rectangle(0, 0, 800, 480);
            es = new Rectangle(0, 0, 2048, 480);
            escenario = new Escenario(Content.Load<Texture2D>("Escenarios/02_Volcan/01"), cu, es, 0, 0);
            //carga textura personaje
            r1 = new Rectangle(0, 400, 50, 51);
            r2 = new Rectangle(0, 400, 341, 51);
            mago = new Personaje(Content.Load<Texture2D>("Personajes/Mago/Derecha/corriendo"), r1, 0, 393,new Vector2(0,390));
            
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            //Invoca a Enemigos
            tiempo = (int)gameTime.TotalGameTime.TotalSeconds;
            vida.Update(gameTime);
            escenario.Update(gameTime);
            enemigos.Update(gameTime);

              //mago
            mago.Update(gameTime);
            

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);


            spriteBatch.Begin();
            escenario.Draw(spriteBatch);

            //Dibuja Enemigos
            enemigos.Draw(spriteBatch);

            //Dibuja Textura Vida
            vida.Draw(spriteBatch);

            //Dibuja Nombre Personaje
            spriteBatch.DrawString(fuente, ("Abandagi"), new Vector2(352, 10), Color.Black);

            //Dibuja Contador Vida
            spriteBatch.DrawString(fuente, ("X " + vida.NumeroVidas.ToString()), new Vector2(110, 10), Color.Black);

            //Dibuja Número Nivel
            spriteBatch.DrawString(fuente, ("  Nivel: " + nivelActual.ToString()), new Vector2(650, 10), Color.Black);

            //Dibuja Tiempo
            spriteBatch.DrawString(fuente, (" Tiempo: " + tiempo.ToString()), new Vector2(650, 34), Color.Black);

            //Dibuja Puntaje
            spriteBatch.DrawString(fuente, (" Puntaje: " + score.ToString()), new Vector2(10, 34), Color.Black);

            //Dibuja Número Gemas
            spriteBatch.DrawString(fuente, ("Gemas: " + gemas.ToString() + "/4"), new Vector2(340, 34), Color.Black);

            //dibuja personaje
            mago.drawMagoVivo(spriteBatch);

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
