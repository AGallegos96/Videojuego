using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace XNAVideoJuego
{
    public class PoderMago
    {
        private Texture2D poderTextura;
        private Vector2 posicion;
        private Vector2 posicionInicial;
        private Vector2 velocidad;
        private Vector2 direccion;
        private bool visible;
        private string identificador;
        private Rectangle rectDestino;
        private int alcanceMaximo;

        #region Propiedades
        public bool Visible { get { return visible; } }
        public Rectangle RectDestino { get { return rectDestino; }}
        #endregion
    
        public PoderMago(string identificador = "poder_normal", int alcanceMaximo = 100)
        {
            this.identificador = identificador;
            this.alcanceMaximo = alcanceMaximo;
            posicion = Vector2.Zero;
            visible = true;
            rectDestino = new Rectangle();
        }

        public void LoadContent(ContentManager Content)
        {
            Content = new ContentManager(Content.ServiceProvider, "Content");
            poderTextura = Content.Load<Texture2D>("Objetos/Poderes/" + identificador);
        }

        public void Update(GameTime gameTime)
        {
            if (Vector2.Distance(posicionInicial, posicion) > alcanceMaximo) //Distancia Máxima de Alcance px
                visible = false;
            if (visible)
                posicion += direccion * velocidad * (float)gameTime.ElapsedGameTime.TotalSeconds;
            rectDestino = new Rectangle((int)posicion.X, (int)posicion.Y, poderTextura.Width, poderTextura.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(visible)
                spriteBatch.Draw(poderTextura, rectDestino, Color.White);
        }

        public void Disparar(Vector2 posicionInicial, Vector2 velocidad, Vector2 direccion)
        {
            posicion = posicionInicial;
            this.posicionInicial = posicionInicial;
            this.velocidad = velocidad;
            this.direccion = direccion;
        }
    }
}
