using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace XNAVideoJuego
{
    public class FadeAnimation
    {
        private Texture2D image;
        private string text;
        private SpriteFont font;
        private Color color;
        private Rectangle sourceRect;
        private float rotation;
        private Vector2 origin, position;
        private ContentManager content;
        private bool isActive;
        private float alpha;
        private float scale;
        private bool increase;
        private float fadeSpeed;
        private TimeSpan defaultTime, timer;
        private float activateValue;
        private bool stopUpdating;
        private float defaultAlpha;

        public bool IsActive { get => isActive; set => isActive = value; }
        public float Scale { set => scale = value; }

        public float Alpha
        {
            get { return alpha; }
            set
            {
                alpha = value;
                if (alpha == 1.0f)
                {
                    increase = false;
                }
                else if (alpha == 0.0f)
                {
                    increase = true;
                }
            }
        }

        public float ActivateValue { get => activateValue; set => activateValue = value; }
        public float FadeSpeed { get => fadeSpeed; set => fadeSpeed = value; }
        public TimeSpan Timer
        {
            get { return timer; }
            set
            {
                defaultTime = value;
                timer = defaultTime;
            }
        }

        public void LoadContent(ContentManager Content, Texture2D image, string text, Vector2 position)
        {
            content = new ContentManager(Content.ServiceProvider, "Content");
            this.image = image;
            this.text = text;
            this.position = position;
            if (text != String.Empty)
            {
                font = Content.Load<SpriteFont>("Fuentes/fuenteJuego");
                color = new Color(114, 77, 255);
            }
            if (image != null)
            {
                sourceRect = new Rectangle(0, 0, image.Width, image.Height);
            }
            rotation = 0.0f;
            scale = alpha = 1.0f;
            isActive = false;
            increase = false;
            fadeSpeed = 1.0f;
            defaultTime = new TimeSpan(0, 0, 1);
            timer = defaultTime;
            activateValue = 0.0f;
            stopUpdating = false;
            defaultAlpha = Alpha;
        }

        public void UnloadContent()
        {
            content.Unload();
            text = String.Empty;
            position = Vector2.Zero;
            sourceRect = Rectangle.Empty;
            image = null;
        }

        public void Update(GameTime gameTime)
        {
            if (isActive)
            {
                if (!stopUpdating)
                {
                    if (!increase)
                    {
                        alpha -= fadeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    else
                    {
                        alpha += fadeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    alpha = MathHelper.Clamp(alpha, 0.0f, 1.0f);
                }

                if (alpha == activateValue)
                {
                    stopUpdating = true;
                    timer -= gameTime.ElapsedGameTime;
                    if (timer.TotalSeconds <= 0)
                    {
                        increase = !increase;
                        timer = defaultTime;
                        stopUpdating = false;
                    }
                }
            }
            else
            {
                alpha = defaultAlpha;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (image != null)
            {
                origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);
                spriteBatch.Draw(image, position + origin, sourceRect, Color.White * alpha, rotation, origin, scale, SpriteEffects.None, 0.0f);
            }

            if (text != String.Empty)
            {
                origin = new Vector2(font.MeasureString(text).X / 2, font.MeasureString(text).Y / 2);
                spriteBatch.DrawString(font, text, position + origin, color * alpha, rotation, origin, scale, SpriteEffects.None, 0.0f);
            }
        }

    }
}
