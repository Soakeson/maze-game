using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CS5410
{
    public class Maze : Game
    {
        private GraphicsDeviceManager m_graphics;
        private SpriteBatch m_spriteBatch;
        private MazeGrid m_mazeGrid;
        private int m_screenWidth = 1920/2;
        private int m_screenHeight = 1080/2;
        private int m_cellWidth;
        private int m_cellHeight;
        private int m_xOffset;
        private int m_yOffset;
        private int m_xShift;
        private int m_yShift;
        private Texture2D[] texturePool = new Texture2D[17];
        private string[] textureFiles = new string[17] {
            "wall-0", "wall-1", "wall-2", "wall-3",
            "wall-4", "wall-5", "wall-6", "wall-7",
            "wall-8", "wall-9", "wall-10", "wall-11",
            "wall-12", "wall-13", "wall-14", "wall-15",
            "player", };

        public Maze()
        {
            m_graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            m_mazeGrid = new MazeGrid(15, 15);
            m_graphics.PreferredBackBufferWidth = m_screenWidth;
            m_graphics.PreferredBackBufferHeight = m_screenHeight;

            // place cells
            m_cellWidth = m_screenHeight/m_mazeGrid.Width;
            m_cellHeight = m_screenHeight/m_mazeGrid.Height;

            // texture alignment
            m_xOffset = m_cellWidth/9;
            m_yOffset = m_cellHeight/9;

            // centering
            // m_xShift = m_screenWidth % 4 == 0 ? (m_screenWidth/4) : (m_screenWidth/4) - (m_cellWidth/2);
            // m_yShift = m_screenWidth % 18 == 0 ? (m_screenHeight/18) : (m_screenHeight/18) + (m_cellHeight/2);
            m_xShift = m_screenWidth/4;
            m_yShift = (m_screenHeight/18);
            m_graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            m_spriteBatch = new SpriteBatch(GraphicsDevice);
            int i = 0;
            foreach (string textureFile in textureFiles)
            {
              texturePool[i] = this.Content.Load<Texture2D>($"images/{textureFile}");
              i++;
            }
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
          // maze rendering
          m_spriteBatch.Begin(SpriteSortMode.Deferred, samplerState:SamplerState.PointClamp);
          for (int y = 0; y < m_mazeGrid.Height; y++)
          {
            for (int x = 0; x < m_mazeGrid.Width; x++)
            {
              Cell curr = m_mazeGrid.Grid[(x,y)];
              m_spriteBatch.Draw(texturePool[(int)curr.Passage], new Rectangle( 
                  x: x == 0 ? (x*m_cellWidth) + m_xShift : (x*m_cellWidth - x*m_xOffset) + m_xShift,
                  y: y == 0 ? (y*m_cellHeight) + m_yShift : (y*m_cellHeight - y*m_yOffset) + m_yShift,
                  width:m_cellWidth,
                  height:m_cellHeight),
                Color.White);
            }
          }

          // player rendering
          m_spriteBatch.Draw(texturePool[16], new Rectangle( 
              x:0 + m_xShift, 
              y:0 + m_yShift,
              width:m_cellWidth,
              height:m_cellHeight),
            Color.Orange);
          m_spriteBatch.End();
          base.Draw(gameTime);
        }
    }
}
