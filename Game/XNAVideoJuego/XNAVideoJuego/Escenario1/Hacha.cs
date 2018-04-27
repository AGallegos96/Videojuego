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
    public class Hacha
    {
        private Texture2D hachaTextura;
        private int anchoFrame, altoFrame;
        private Vector2 posicion;
        private Animacion animacion;
        private bool visible;
        private Vector2 posicionInicial;
        private Vector2 velocidad;
        private Vector2 direccion;

        #region Propiedades
        public Animacion Animacion { get { return animacion; } }
        public bool Visible { get { return visible; }}
        #endregion

        public Hacha()
        {
            anchoFrame = 25;
            altoFrame = 25;
            visible = true;
            posicion = Vector2.Zero;
        }

        public void LoadContent(ContentManager Content)
        {
            Content = new ContentManager(Content.ServiceProvider, "Content");
            hachaTextura = Content.Load<Texture2D>("Objetos/01_Valle/hacha");
            animacion = new Animacion(hachaTextura, posicion, anchoFrame, altoFrame, 8, 80, Color.White, true);
        }

        public void Update(GameTime gameTime)
        {
            if (Vector2.Distance(posicionInicial, posicion) > 350) //Distancia Máxima de Alcance px
                visible = false;
            if (visible)
                posicion += direccion * velocidad * (float)gameTime.ElapsedGameTime.TotalSeconds;
            animacion.Update(gameTime, posicion);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (visible)
                animacion.Draw(spriteBatch);
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
