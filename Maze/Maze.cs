using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CS5410
{
    public class Maze : Game
    {
        Texture2D whiteRectangle;
        private GraphicsDeviceManager m_graphics;
        private SpriteBatch m_spriteBatch;
        private MazeGrid m_grid;
        private int m_screenHeight;
        private int m_screenWidth;

        private Texture2D m_wall0;
        private Texture2D m_wall1;
        private Texture2D m_wall2;
        private Texture2D m_wall3;
        private Texture2D m_wall4;
        private Texture2D m_wall5;
        private Texture2D m_wall6;
        private Texture2D m_wall7;
        private Texture2D m_wall8;
        private Texture2D m_wall9;
        private Texture2D m_wall10;
        private Texture2D m_wall11;
        private Texture2D m_wall12;
        private Texture2D m_wall13;
        private Texture2D m_wall14;

        public Maze()
        {
            m_graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            m_screenHeight = m_graphics.PreferredBackBufferHeight;
            m_screenWidth = m_graphics.PreferredBackBufferWidth;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            m_grid = new MazeGrid(20, 20);
            m_grid.print();
            m_graphics.PreferredBackBufferWidth = 1920;
            m_graphics.PreferredBackBufferHeight = 1080;
            m_graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            m_spriteBatch = new SpriteBatch(GraphicsDevice);
            m_wall0 = this.Content.Load<Texture2D>("images/wall-0");
            m_wall1 = this.Content.Load<Texture2D>("images/wall-1");
            m_wall2 = this.Content.Load<Texture2D>("images/wall-2");
            m_wall3 = this.Content.Load<Texture2D>("images/wall-3");
            m_wall4 = this.Content.Load<Texture2D>("images/wall-4");
            m_wall5 = this.Content.Load<Texture2D>("images/wall-5");
            m_wall6 = this.Content.Load<Texture2D>("images/wall-6");
            m_wall7 = this.Content.Load<Texture2D>("images/wall-7");
            m_wall8 = this.Content.Load<Texture2D>("images/wall-8");
            m_wall9 = this.Content.Load<Texture2D>("images/wall-9");
            m_wall10 = this.Content.Load<Texture2D>("images/wall-10");
            m_wall11 = this.Content.Load<Texture2D>("images/wall-11");
            m_wall12 = this.Content.Load<Texture2D>("images/wall-12");
            m_wall13 = this.Content.Load<Texture2D>("images/wall-13");
            m_wall14 = this.Content.Load<Texture2D>("images/wall-14");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            // TODO: Add your drawing code here
            int cellWidth = m_screenHeight/m_grid.m_width;
            int cellHeight = m_screenHeight/m_grid.m_height;
            for (int y = 0; y < m_grid.m_height; y++)
            {
              for (int x = 0; x < m_grid.m_width; x++)
              {
                Cell curr = m_grid.m_grid[(x,y)];
                m_spriteBatch.Begin(SpriteSortMode.Deferred);
                switch(((int)curr.passage))
                {
                  case 1:
                  m_spriteBatch.Draw(m_wall1, new Rectangle( 
                        x:x*cellWidth, 
                        y:y*cellHeight,
                        width:cellWidth,
                        height:cellHeight),
                      Color.White);
                  break;
                  case 2:
                  m_spriteBatch.Draw(m_wall2, new Rectangle( 
                        x:x*cellWidth, 
                        y:y*cellHeight,
                        width:cellWidth,
                        height:cellHeight),
                      Color.White);
                  break;
                  case 3:
                  m_spriteBatch.Draw(m_wall3, new Rectangle( 
                        x:x*cellWidth, 
                        y:y*cellHeight,
                        width:cellWidth,
                        height:cellHeight),
                      Color.White);
                  break;
                  case 4:
                  m_spriteBatch.Draw(m_wall4, new Rectangle( 
                        x:x*cellWidth, 
                        y:y*cellHeight,
                        width:cellWidth,
                        height:cellHeight),
                      Color.White);
                  break;
                  case 5:
                  m_spriteBatch.Draw(m_wall5, new Rectangle( 
                        x:x*cellWidth, 
                        y:y*cellHeight,
                        width:cellWidth,
                        height:cellHeight),
                      Color.White);
                  break;
                  case 6:
                  m_spriteBatch.Draw(m_wall6, new Rectangle( 
                        x:x*cellWidth, 
                        y:y*cellHeight,
                        width:cellWidth,
                        height:cellHeight),
                      Color.White);
                  break;
                  case 7:
                  m_spriteBatch.Draw(m_wall7, new Rectangle( 
                        x:x*cellWidth, 
                        y:y*cellHeight,
                        width:cellWidth,
                        height:cellHeight),
                      Color.White);
                  break;
                  case 8:
                  m_spriteBatch.Draw(m_wall8, new Rectangle( 
                        x:x*cellWidth, 
                        y:y*cellHeight,
                        width:cellWidth,
                        height:cellHeight),
                      Color.White);
                  break;
                  case 9:
                  m_spriteBatch.Draw(m_wall9, new Rectangle( 
                        x:x*cellWidth, 
                        y:y*cellHeight,
                        width:cellWidth,
                        height:cellHeight),
                      Color.White);
                  break;
                  case 10:
                  m_spriteBatch.Draw(m_wall10, new Rectangle( 
                        x:x*cellWidth, 
                        y:y*cellHeight,
                        width:cellWidth,
                        height:cellHeight),
                      Color.White);
                  break;
                  case 11:
                  m_spriteBatch.Draw(m_wall11, new Rectangle( 
                        x:x*cellWidth, 
                        y:y*cellHeight,
                        width:cellWidth,
                        height:cellHeight),
                      Color.White);
                  break;
                  case 12:
                  m_spriteBatch.Draw(m_wall12, new Rectangle( 
                        x:x*cellWidth, 
                        y:y*cellHeight,
                        width:cellWidth,
                        height:cellHeight),
                      Color.White);
                  break;
                  case 13:
                  m_spriteBatch.Draw(m_wall13, new Rectangle( 
                        x:x*cellWidth, 
                        y:y*cellHeight,
                        width:cellWidth,
                        height:cellHeight),
                      Color.White);
                  break;
                  case 14:
                  m_spriteBatch.Draw(m_wall14, new Rectangle( 
                        x:x*cellWidth, 
                        y:y*cellHeight,
                        width:cellWidth,
                        height:cellHeight),
                      Color.White);
                  break;
                }
                m_spriteBatch.End();
              }
            }
            base.Draw(gameTime);
        }
    }
}
