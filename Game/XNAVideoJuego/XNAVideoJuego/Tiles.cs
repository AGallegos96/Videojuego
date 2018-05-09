using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNAVideoJuego
{
    public class Tiles  {
        public Texture2D texture;

        public Rectangle rectangle;
        public Rectangle Rectangle {
            get { return rectangle; }
            protected set { rectangle = value; }
        }

        public static ContentManager content;
        public static ContentManager Content {
            protected get { return content; }
             set { content = value; }
        }

        public void Draw(SpriteBatch spriteBatch)  {            
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }

    public class CollisionTiles : Tiles {
        public CollisionTiles(int i, Rectangle rectangle) {
            texture = Content.Load<Texture2D>("Tile" + i);
            this.Rectangle = rectangle;
        }
    }
}
