using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace XNAVideoJuego
{
    public class ScreenManager
    {
        #region Variables

        //Creating custom ContentManager
        private ContentManager content;

        private GameScreen currentScreen;
        private GameScreen newScreen;

        //ScreenManager Instance
        private static ScreenManager instance;

        //Screen Stack
        private Stack<GameScreen> screenStack = new Stack<GameScreen>();

        //Screens width and height
        private Vector2 dimensions;

        bool transition;
        FadeAnimation fade;
        Texture2D fadeTexture;

        #endregion

        #region Properties
        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ScreenManager();
                }
                return instance;
            }
        }
        public Vector2 Dimensions { get => dimensions; set => dimensions = value; }
        #endregion

        #region Main Methods

        public void AddScreen(GameScreen screen)
        {
            transition = true;
            newScreen = screen;
            fade.IsActive = true;
            fade.Alpha = 0.0f;
            fade.ActivateValue = 1.0f;
        }

        public void Initialize(GraphicsDeviceManager graphics)
        {
            currentScreen = new ESRBScreen(graphics);
            currentScreen.Initialize();
            fade = new FadeAnimation();
        }
        public void LoadContent(ContentManager Content)
        {
            content = new ContentManager(Content.ServiceProvider, "Content");
            currentScreen.LoadContent(Content);
            fadeTexture = content.Load<Texture2D>("fade");
            fade.LoadContent(content, fadeTexture, "", Vector2.Zero);
            fade.Scale = dimensions.X;
        }
        public void Update(GameTime gameTime)
        {
            if (!transition)
            {
                currentScreen.Update(gameTime);
            }
            else
            {
                Transition(gameTime);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            currentScreen.Draw(spriteBatch);
            if (transition)
            {
                fade.Draw(spriteBatch);
            }
        }
        #endregion

        #region Private Methods
        private void Transition(GameTime gameTime)
        {
            fade.Update(gameTime);
            if (fade.Alpha == 1.0f && fade.Timer.TotalSeconds == 1.0f)
            {
                if (screenStack.Count>0)
                {
                    screenStack.Pop();
                }
                screenStack.Push(newScreen);
                currentScreen.UnloadContent();
                currentScreen = newScreen;
                currentScreen.Initialize();
                currentScreen.LoadContent(content);
            }
            else if (fade.Alpha == 0.0f)
            {
                Game1.juegoMain.Camara.Transformacion = Matrix.CreateTranslation(new Vector3(0, 0, 0));
                transition = false;
                fade.IsActive = false;
            }
        }
        #endregion
    }
}
