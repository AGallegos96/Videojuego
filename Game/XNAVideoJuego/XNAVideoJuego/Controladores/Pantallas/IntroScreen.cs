using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ExplosionLibrary;

namespace XNAVideoJuego
{
    public class IntroScreen : GameScreen
    {
        private KeyboardState keyOldState;
        private SpriteFont fuente1, fuente2;
        private string cadenaTexto;
        private Vector2 posicionTexto;
        private Texture2D texturaFondo;
        private Rectangle rectTextura;
        private Texture2D texturaForm;
        private Rectangle rectForm;

        #region Explosion Variables
        private ParticleSystem explosion;
        private ParticleSystem smoke;
        private const float TimeBetweenExplosions = 2.0f;
        private float timeTillExplosion = 0.0f;
        #endregion

        public IntroScreen(GraphicsDeviceManager graphics) : base(graphics) { }

        public override void Initialize()
        {
            Game1.juegoMain.NoBlend = true;
            posicionTexto = new Vector2(278, 350);
            cadenaTexto = String.Empty;
            // create the particle systems and add them to the components list.
            explosion = new ParticleSystem(Game1.juegoMain, "ExplosionSettings") { DrawOrder = ParticleSystem.AdditiveDrawOrder };
            Game1.juegoMain.Components.Add(explosion);
            smoke = new ParticleSystem(Game1.juegoMain, "ExplosionSmokeSettings") { DrawOrder = ParticleSystem.AlphaBlendDrawOrder };
            Game1.juegoMain.Components.Add(smoke);
        }

        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);
            fuente1 = content.Load<SpriteFont>("Fuentes/fuenteNombreJugador");
            fuente2 = content.Load<SpriteFont>("Fuentes/fuenteMsg");
            texturaFondo = content.Load<Texture2D>("Screens/introScreen/bg");
            rectTextura = new Rectangle(0, 0, texturaFondo.Width, texturaFondo.Height);
            texturaForm = content.Load<Texture2D>("Screens/introScreen/form");
            rectForm = new Rectangle((graphics.GraphicsDevice.Viewport.Width - texturaForm.Width) / 2, -texturaForm.Height, texturaForm.Width, texturaForm.Height);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (rectForm.Y < 10)
            {
                rectForm.Y += 5;
            }
            else
            {
                KeyboardState keyCurrentState = Keyboard.GetState();
                if (keyCurrentState.GetPressedKeys().Length != 0
                    && keyCurrentState.GetPressedKeys()[0].GetHashCode() >= Keys.A.GetHashCode()
                    && keyCurrentState.GetPressedKeys()[0].GetHashCode() <= Keys.Z.GetHashCode()
                    && keyOldState.IsKeyUp(keyCurrentState.GetPressedKeys()[0]))
                {
                    if (cadenaTexto.Length <= 11)
                    {
                        cadenaTexto += keyCurrentState.GetPressedKeys()[0].ToString();
                    }
                }
                else if (keyCurrentState.IsKeyDown(Keys.Back) && keyOldState.IsKeyUp(Keys.Back))
                {
                    cadenaTexto = (cadenaTexto.Length > 0) ? cadenaTexto.Remove(cadenaTexto.Trim().Length - 1) : String.Empty;
                }
                else if (keyCurrentState.IsKeyDown(Keys.Enter) && keyOldState.IsKeyUp(Keys.Enter))
                {
                    if (cadenaTexto.Length != 0)
                    {
                        Game1.juegoMain.NombreJugador = cadenaTexto;
                        ScreenManager.Instance.AddScreen(new MenuScreen(graphics));
                    }
                }
                keyOldState = keyCurrentState;
            }

            //we should be demoing the explosions effect, check to see if it's time for a new explosion.
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            UpdateExplosions(dt);
        }

        // this function is called when we want to demo the explosion effect. it
        // updates the timeTillExplosion timer, and starts another explosion effect
        // when the timer reaches zero.
        private void UpdateExplosions(float dt)
        {
            timeTillExplosion -= dt;
            if (timeTillExplosion < 0)
            {
                Vector2 where = Vector2.Zero;
                // create the explosion at some random point on the screen.
                where.X = ParticleHelpers.RandomBetween(0, graphics.GraphicsDevice.Viewport.Width);
                where.Y = ParticleHelpers.RandomBetween(0, graphics.GraphicsDevice.Viewport.Height);

                // the overall explosion effect is actually comprised of two particle
                // systems: the fiery bit, and the smoke behind it. add particles to
                // both of those systems.
                explosion.AddParticles(where, Vector2.Zero);
                smoke.AddParticles(where, Vector2.Zero);
                // reset the timer.
                timeTillExplosion = TimeBetweenExplosions;

                AudioManager.PlaySoundEffect("explosion_medium");
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            graphics.GraphicsDevice.Clear(Color.White);
            spriteBatch.Draw(texturaFondo, rectTextura, Color.White);
            spriteBatch.Draw(texturaForm, rectForm, Color.White);
            spriteBatch.DrawString(fuente1, cadenaTexto, posicionTexto, Color.Black);
            if (rectForm.Y == 10)
                spriteBatch.DrawString(fuente2, "Ingrese su nombre y presione la tecla Enter para continuar...", new Vector2(92, 440), new Color(104, 46,26));
        }
    }
}
