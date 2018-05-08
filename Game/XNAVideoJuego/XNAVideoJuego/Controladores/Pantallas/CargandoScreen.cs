using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XNAVideoJuego
{
    public class CargandoScreen : GameScreen
    {
        private string mensaje;
        private SpriteFont fuente;
        private float tiempoTranscurrido;
        private Vector2 posicionTexto;

        public CargandoScreen(GraphicsDeviceManager graphics) : base(graphics) { }

        public override void Initialize()
        {
            mensaje = "Cargando...";
            tiempoTranscurrido = 0;
        }

        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);
            fuente = Content.Load<SpriteFont>("Fuentes/fuenteJuego");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            tiempoTranscurrido += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (tiempoTranscurrido>1.5) //Espera 1.5 segundos
            {
                tiempoTranscurrido = 0;
                ScreenManager.Instance.AddScreen(new JugarScreen(graphics));
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            graphics.GraphicsDevice.Clear(Color.Black);
            posicionTexto = (new Vector2(graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height) - fuente.MeasureString(mensaje)) / 2;
            spriteBatch.DrawString(fuente, mensaje, posicionTexto, Color.White);
        }

    }
}
