using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    class Animation
    {

        private Texture2D spriteStrip; //La imagen animada representada por un grupo de imágenes
        private float scale; //Valor para escalar el sprite
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
        private Vector2 Position; //Posición del sprite

        public void Initialize(Texture2D texture, Vector2 position, int frameWidth, int frameHeight, int frameCount, int frametime, Color color, float scale, bool looping)
        {
            //Mantener copias locales de los valores pasados
            this.color = color;
            this.frameCount = frameCount;
            this.scale = scale;
            this.frameTime = frametime;
            this.spriteStrip = texture;
            this.FrameWidth = frameWidth;
            this.FrameHeight = frameHeight;
            this.Looping = looping;
            this.Position = position;

            //Hacer reset a los tiempos
            this.elapsedTime = 0;
            this.currentFrame = 0;

            //Activar la animación por defecto
            this.Active = true;
        }

        public void Update(GameTime gameTime)
        {
            if (!this.Active) //No actualizar si la imagen está desactivada
            {
                return;
            }

            this.elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds; //Actualizar tiempo transcurrido

            if (this.elapsedTime > this.frameTime) //Si elapsedTime es mayor que frame time debemos cambiar de imagen
            {
                this.currentFrame++; //Movemos a la siguiente imagen

                if (this.currentFrame == this.frameCount) //Si currentFrame es igual al frameCount hacemos reset currentFrame a cero
                {
                    this.currentFrame = 0;

                    if (!this.Looping) //Si no queremos repetir la animación asignamos Active a falso
                    {
                        this.Active = false;
                    }
                }

                this.elapsedTime = 0; //Reiniciamos elapsedTime a cero
            }

            //Tomamos la imagen correcta miltiplicando el currentFrame por el ancho de la imagen
            this.sourceRect = new Rectangle(this.currentFrame * this.FrameWidth, 0, this.FrameWidth, this.FrameHeight);

            //Actualizamos la posición de la imagen en caso que ésta se desplace por la pantalla
            this.destinationRect = new Rectangle((int)this.Position.X - (int)(this.FrameWidth * this.scale) / 2, (int)this.Position.Y - (int)(this.FrameHeight * this.scale) / 2, (int)(this.FrameWidth * this.scale), (int)(this.FrameHeight * this.scale));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.Active)
            {
                spriteBatch.Draw(this.spriteStrip, this.destinationRect, this.sourceRect, this.color);
            }
        }
    }
}
