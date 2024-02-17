using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CS5410.Input;

namespace CS5410
{
    public class Maze : Game
    {
        private GraphicsDeviceManager m_graphics;
        private SpriteBatch m_spriteBatch;
        private MazeLevelGenerator m_mazeGenerator;
        private MazeLevel m_level;
        private int m_screenWidth = 1920/2;
        private int m_screenHeight = 1080/2;
        private int m_cellWidth;
        private int m_cellHeight;
        private int m_xOffset;
        private int m_yOffset;
        private int m_xShift;
        private int m_yShift;
        private bool m_showPath = false;
        private bool m_showBreadcrumb = false;
        private bool m_showHint = false;
        private Texture2D[] wallTexturePool = new Texture2D[16];
        private Texture2D playerTexture;
        private Texture2D startTexture;
        private Texture2D trailTexture;
        private Texture2D breadTexture;
        private Texture2D goalTexture;
        private KeyboardInput m_inputKeyboard;

        public Maze()
        {
            m_graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        private void CalculateLevelScreenSizing()
        {
            // place cells
            m_cellWidth = m_screenHeight/m_level.Width;
            m_cellHeight = m_screenHeight/m_level.Height;

            // texture alignment
            m_xOffset = m_cellWidth/9;
            m_yOffset = m_cellHeight/9;

            // centering
            // m_xShift = m_screenWidth % 4 == 0 ? (m_screenWidth/4) : (m_screenWidth/4) - (m_cellWidth/2);
            // m_yShift = m_screenWidth % 18 == 0 ? (m_screenHeight/18) : (m_screenHeight/18) + (m_cellHeight/2);
            m_xShift = m_screenWidth/4;
            m_yShift = (m_screenHeight/18);
        }

        protected override void Initialize()
        {
            m_mazeGenerator = new MazeLevelGenerator();
            m_level = m_mazeGenerator.Generate(3, 3);

            m_graphics.PreferredBackBufferWidth = m_screenWidth;
            m_graphics.PreferredBackBufferHeight = m_screenHeight;

            CalculateLevelScreenSizing();

            m_inputKeyboard = new KeyboardInput();

            m_inputKeyboard.registerCommand(Keys.W, true, new IInputDevice.CommandDelegate(onMoveUp));
            m_inputKeyboard.registerCommand(Keys.S, true, new IInputDevice.CommandDelegate(onMoveDown));
            m_inputKeyboard.registerCommand(Keys.A, true, new IInputDevice.CommandDelegate(onMoveLeft));
            m_inputKeyboard.registerCommand(Keys.D, true, new IInputDevice.CommandDelegate(onMoveRight));

            m_inputKeyboard.registerCommand(Keys.Up, true, new IInputDevice.CommandDelegate(onMoveUp));
            m_inputKeyboard.registerCommand(Keys.Down, true, new IInputDevice.CommandDelegate(onMoveDown));
            m_inputKeyboard.registerCommand(Keys.Left, true, new IInputDevice.CommandDelegate(onMoveLeft));
            m_inputKeyboard.registerCommand(Keys.Right, true, new IInputDevice.CommandDelegate(onMoveRight));

            m_inputKeyboard.registerCommand(Keys.F1, true, new IInputDevice.CommandDelegate(onFive));
            m_inputKeyboard.registerCommand(Keys.F2, true, new IInputDevice.CommandDelegate(onTen));
            m_inputKeyboard.registerCommand(Keys.F3, true, new IInputDevice.CommandDelegate(onFifteen));
            m_inputKeyboard.registerCommand(Keys.F4, true, new IInputDevice.CommandDelegate(onTwenty));
            m_inputKeyboard.registerCommand(Keys.H, true, new IInputDevice.CommandDelegate(onHint));
            m_inputKeyboard.registerCommand(Keys.P, true, new IInputDevice.CommandDelegate(onPath));
            m_inputKeyboard.registerCommand(Keys.B, true, new IInputDevice.CommandDelegate(onBreadcrumb));

            m_graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            m_spriteBatch = new SpriteBatch(GraphicsDevice);
            for (int i = 0; i < 16; i++)
            {
              wallTexturePool[i] = this.Content.Load<Texture2D>($"images/wall-{i}");
            }
            playerTexture = this.Content.Load<Texture2D>("images/player");
            trailTexture = this.Content.Load<Texture2D>("images/trail");
            goalTexture = this.Content.Load<Texture2D>("images/goal");
            breadTexture = this.Content.Load<Texture2D>("images/bread");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            if (m_level.Won)
            {
              m_level = m_mazeGenerator.Generate(m_level.Width, m_level.Height);
              CalculateLevelScreenSizing();
            }
            processInput(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
          GraphicsDevice.Clear(Color.Black);
          // maze rendering
          m_spriteBatch.Begin(SpriteSortMode.Deferred, samplerState:SamplerState.PointClamp);
          for (int y = 0; y < m_level.Height; y++)
          {
            for (int x = 0; x < m_level.Width; x++)
            {
              Cell curr = m_level.GetCell(x, y);
              m_spriteBatch.Draw(wallTexturePool[(int)curr.Passage], new Rectangle( 
                  x: x == 0 ? (x*m_cellWidth) + m_xShift : (x*m_cellWidth - x*m_xOffset) + m_xShift,
                  y: y == 0 ? (y*m_cellHeight) + m_yShift : (y*m_cellHeight - y*m_yOffset) + m_yShift,
                  width:m_cellWidth,
                  height:m_cellHeight),
                Color.White);
            }
          }

          if (m_showBreadcrumb)
          {
            foreach(((int x, int y) Cord, Cell bread) in m_level.Player.History)
            {
              m_spriteBatch.Draw(breadTexture, new Rectangle( 
                    x: bread.Cord.x == 0 ? (bread.Cord.x*m_cellWidth) + m_xShift : (bread.Cord.x*m_cellWidth - bread.Cord.x*m_xOffset) + m_xShift,
                    y: bread.Cord.y == 0 ? (bread.Cord.y*m_cellWidth) + m_yShift : (bread.Cord.y*m_cellWidth - bread.Cord.y*m_yOffset) + m_yShift,
                    width:m_cellWidth,
                    height:m_cellHeight),
                  Color.White);
            }
          }

          if (m_showPath)
          {
            foreach(Cell trail in m_level.Path)
            {
              m_spriteBatch.Draw(trailTexture, new Rectangle( 
                    x: trail.Cord.x == 0 ? (trail.Cord.x*m_cellWidth) + m_xShift : (trail.Cord.x*m_cellWidth - trail.Cord.x*m_xOffset) + m_xShift,
                    y: trail.Cord.y == 0 ? (trail.Cord.y*m_cellWidth) + m_yShift : (trail.Cord.y*m_cellWidth - trail.Cord.y*m_yOffset) + m_yShift,
                    width:m_cellWidth,
                    height:m_cellHeight),
                  Color.White);
            }
          }

          if (m_showHint)
          {
            Cell top;
            if (m_level.Path.TryPeek(out top))
            {
              m_spriteBatch.Draw(trailTexture, new Rectangle( 
                    x: top.Cord.x == 0 ? (top.Cord.x*m_cellWidth) + m_xShift : (top.Cord.x*m_cellWidth - top.Cord.x*m_xOffset) + m_xShift,
                    y: top.Cord.y == 0 ? (top.Cord.y*m_cellWidth) + m_yShift : (top.Cord.y*m_cellWidth - top.Cord.y*m_yOffset) + m_yShift,
                    width:m_cellWidth,
                    height:m_cellHeight),
                  Color.White);
            }
          }

          // goal rendering
          m_spriteBatch.Draw(goalTexture, new Rectangle( 
              x: ((m_level.Width - 1)*m_cellWidth - (m_level.Width - 1)*m_xOffset) + m_xShift,
              y: ((m_level.Height - 1)*m_cellWidth - (m_level.Height - 1)*m_yOffset) + m_yShift,
              width:m_cellWidth,
              height:m_cellHeight),
            Color.White);

          // player rendering
          m_spriteBatch.Draw(playerTexture, new Rectangle( 
              x: m_level.Player.Pos.Cord.x == 0 ? (m_level.Player.Pos.Cord.x*m_cellWidth) + m_xShift : (m_level.Player.Pos.Cord.x*m_cellWidth - m_level.Player.Pos.Cord.x*m_xOffset) + m_xShift,
              y: m_level.Player.Pos.Cord.y == 0 ? (m_level.Player.Pos.Cord.y*m_cellWidth) + m_yShift : (m_level.Player.Pos.Cord.y*m_cellWidth - m_level.Player.Pos.Cord.y*m_yOffset) + m_yShift,
              width:m_cellWidth,
              height:m_cellHeight),
            Color.Red);
          m_spriteBatch.End();
          base.Draw(gameTime);

        }

        private void processInput(GameTime gameTime)
        {
            m_inputKeyboard.Update(gameTime);
        }

        private void onMoveUp(GameTime gameTime, float value)
        {
          m_level.MovePlayer(Direction.North);
        }

        private void onMoveDown(GameTime gameTime, float value)
        {
          m_level.MovePlayer(Direction.South);
        }

        private void onMoveLeft(GameTime gameTime, float value)
        {
          m_level.MovePlayer(Direction.West);
        }

        private void onMoveRight(GameTime gameTime, float value)
        {
          m_level.MovePlayer(Direction.East);
        }

        private void onFive(GameTime gameTime, float value)
        {
          m_level = m_mazeGenerator.Generate(5, 5);
          CalculateLevelScreenSizing();
        }

        private void onTen(GameTime gameTime, float value)
        {
          m_level = m_mazeGenerator.Generate(10, 10);
          CalculateLevelScreenSizing();
        }

        private void onFifteen(GameTime gameTime, float value)
        {
          m_level = m_mazeGenerator.Generate(15, 15);
          CalculateLevelScreenSizing();
        }

        private void onTwenty(GameTime gameTime, float value)
        {
          m_level = m_mazeGenerator.Generate(20, 20);
          CalculateLevelScreenSizing();
        }

        private void onHint(GameTime gameTime, float value)
        {
          m_showHint = !m_showHint;
          // if path active turn it off
          if (m_showPath) m_showPath = !m_showPath;
        }

        private void onPath(GameTime gameTime, float value)
        {
          m_showPath = !m_showPath;
          // if hint active turn it off
          if (m_showHint) m_showHint = !m_showHint;
        }

        private void onBreadcrumb(GameTime gameTime, float value)
        {
          m_showBreadcrumb = !m_showBreadcrumb;
        }
    }
}
