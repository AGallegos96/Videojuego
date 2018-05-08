using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XNAVideoJuego
{
    class Map
    {
        private List<CollisionTiles> collisionTiles = new List<CollisionTiles>();
        public List<CollisionTiles> CollisionTiles {
            get { return collisionTiles; }
        }
        private int ancho, altura;
        public int Ancho  {
            get { return ancho; }
        }
        public int Altura   {
            get { return altura; }
        }

        public Map(){
        }

        public void Generar(int [,] map, int tam){
            for(int x=0;x<map.GetLength(1);x++){  
                for(int y=0;y<map.GetLength(0);y++){
                    int numero=map[y,x];

                    if(numero>0) 
                        collisionTiles.Add(new CollisionTiles(numero,new Rectangle(x*tam, y*tam, tam, tam )));
                    ancho=(x+1)*tam;
                    altura=(y+1)*tam;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(CollisionTiles tile in CollisionTiles)
                tile.Draw(spriteBatch);
        }
    }
}
