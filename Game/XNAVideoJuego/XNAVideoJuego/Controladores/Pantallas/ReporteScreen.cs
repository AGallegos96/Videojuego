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
    public class ReporteScreen : GameScreen
    {

        public ReporteScreen(GraphicsDeviceManager graphics) : base(graphics) { }

        public override void Initialize()
        {

        }

        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);


        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            graphics.GraphicsDevice.Clear(Color.White);

        }
    }
}
