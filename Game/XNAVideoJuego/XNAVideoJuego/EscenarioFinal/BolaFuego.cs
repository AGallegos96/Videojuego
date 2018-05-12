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
    public class BolaFuego
    {
        private int anchoFrame, altoFrame;
        private Vector2 posicion;
        private List<Animacion> listaAnimaciones;
        private int indiceAnimacionActual;
        private bool visible;
        private Vector2 posicionInicial;
        private Vector2 velocidad;
        private Vector2 direccion;

        #region Propiedades
        public List<Animacion> ListaAnimaciones { get { return listaAnimaciones; } }
        public int IndiceAnimacionActual { get { return indiceAnimacionActual; }set { indiceAnimacionActual = value; } }
        public bool Visible { get { return visible; } }
        #endregion

        public BolaFuego()
        {
            indiceAnimacionActual = 1;
            anchoFrame = 27;
            altoFrame = 30;
            visible = true;
            posicion = Vector2.Zero;
            listaAnimaciones = new List<Animacion>();
        }

        public void LoadContent(ContentManager Content)
        {
            Content = new ContentManager(Content.ServiceProvider, "Content");
            listaAnimaciones.Add(new Animacion(Content.Load<Texture2D>("Objetos/05_Escenario_Final/Derecha/bola_sombra"), posicion, anchoFrame, altoFrame, 4, 80, Color.White, true));
            listaAnimaciones.Add(new Animacion(Content.Load<Texture2D>("Objetos/05_Escenario_Final/Izquierda/bola_sombra"), posicion, anchoFrame, altoFrame, 4, 80, Color.White, true));
        }

        public void Update(GameTime gameTime)
        {
            if (Vector2.Distance(posicionInicial, posicion) > 400) //Distancia Máxima de Alcance px
                visible = false;
            if (visible)
                posicion += direccion * velocidad * (float)gameTime.ElapsedGameTime.TotalSeconds;
            listaAnimaciones[indiceAnimacionActual].Update(gameTime, posicion);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (visible)
                listaAnimaciones[indiceAnimacionActual].Draw(spriteBatch);
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
