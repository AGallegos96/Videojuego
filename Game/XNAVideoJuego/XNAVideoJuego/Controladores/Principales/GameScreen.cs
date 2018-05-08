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
    public class GameScreen
    {
        protected ContentManager content;
        protected GraphicsDeviceManager graphics;

        public GameScreen(GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;
        }
        
        public virtual void Initialize() { }
        public virtual void LoadContent(ContentManager Content)
        {
            content = new ContentManager(Content.ServiceProvider, "Content");
        }
        public virtual void UnloadContent()
        {
            content.Unload();
        }
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(SpriteBatch spriteBatch) { }
    }
}
