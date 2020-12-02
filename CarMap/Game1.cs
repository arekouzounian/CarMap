using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace CarMap
{
    /*
     * 1) Prototype: Using a set Map, create pt A and pt B, a stop sign tile, and an intersection
     * that can use commands such as north, south, east, west, and stop to move the car and thus demonstrate a simple algorithm.
     * 2) Larger Plans: Check for this to work with different maps
     * 3) Biggest Plans: Have a visual level-editor like interface that allows you to click and use the mouse to create the driving path.
     */
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Grid grd;

        public Dictionary<char, Texture2D> textures
        {
            get
            {
                return new Dictionary<char, Texture2D>
                {
                    {'p', Content.Load<Texture2D>("tile") },
                    {'0', Content.Load<Texture2D>("nullTile") },
                    {'s', Content.Load<Texture2D>("stopTile") },
                    {'a', Content.Load<Texture2D>("tileA") },
                    {'b', Content.Load<Texture2D>("tileB") },
                };
            }
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.AllowUserResizing = true;
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            
            grd = new Grid(@"C:\Users\agouz\Desktop\path.txt", textures); 
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.AntiqueWhite);
            int cellSize = 30;

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            grd.drawMap(spriteBatch,
                new Vector2((GraphicsDevice.Viewport.Bounds.Width / 2) - ((cellSize * grd.Map.GetLength(0)) / 2), 
                    (GraphicsDevice.Viewport.Bounds.Height / 2) - ((cellSize * grd.Map.GetLength(1)) / 2)),
                        cellSize);
            //grd.drawMap(spriteBatch, new Vector2(300, 50), cellSize, cellSize);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
