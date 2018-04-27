using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace XNAVideoJuego
{
    public class Animacion
    {

        private Texture2D spriteStrip; //La imagen animada representada por un grupo de imágenes
        private int elapsedTime; //Tiempo desde la última vez que se actualizó la imagen
        private int frameTime; //Tiempo de despliegue por imagen
        private int frameCount; //Número de imágenes que conforman la animación
        private int currentFrame; //Índice de la imagen actual
        private Color color; //Color de la imagen que vamos a desplegar
        private Rectangle sourceRect = new Rectangle(); //El área de la imagen que vamos a desplegar
        private Rectangle destinationRect = new Rectangle(); //El área donde queremos desplegar la imagen
        private int FrameWidth; //Ancho de una imagen
        private int FrameHeight; //Alto de una imagen
        private bool Active; //Estado de la animación
        private bool Looping; //Repetir animación
        private Vector2 position; //Posición del sprite

        #region Propiedades
        public Rectangle DestinationRect { get { return destinationRect; } }
        public Vector2 Position { get { return position; } }
        #endregion

        public Animacion(Texture2D texture, Vector2 position, int frameWidth, int frameHeight, int frameCount, int frametime, Color color, bool looping)
        {
            //Mantener copias locales de los valores pasados
            this.color = color;
            this.frameCount = frameCount;
            frameTime = frametime;
            spriteStrip = texture;
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
            Looping = looping;
            this.position = position;

            //Hacer reset a los tiempos
            elapsedTime = 0;
            currentFrame = 0;

            //Activar la animación por defecto
            Active = true;
        }

        public void Update(GameTime gameTime, Vector2 position)
        {
            this.position = position;
            if (!Active) //No actualizar si la imagen está desactivada
            {
                return;
            }

            elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds; //Actualizar tiempo transcurrido

            if (elapsedTime > frameTime) //Si elapsedTime es mayor que frame time debemos cambiar de imagen
            {
                currentFrame++; //Movemos a la siguiente imagen

                if (currentFrame == frameCount) //Si currentFrame es igual al frameCount hacemos reset currentFrame a cero
                {
                    currentFrame = 0;

                    if (!Looping) //Si no queremos repetir la animación asignamos Active a falso
                    {
                        Active = false;
                    }
                }

                elapsedTime = 0; //Reiniciamos elapsedTime a cero
            }

            //Tomamos la imagen correcta miltiplicando el currentFrame por el ancho de la imagen
            sourceRect = new Rectangle(currentFrame * FrameWidth, 0, FrameWidth, FrameHeight);

            //Actualizamos la posición de la imagen en caso que ésta se desplace por la pantalla
            destinationRect = new Rectangle((int)position.X, (int)position.Y,FrameWidth,FrameHeight);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Active)
            {
                spriteBatch.Draw(spriteStrip, destinationRect, sourceRect, color);
            }
        }
    }
}
