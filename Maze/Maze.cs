using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CS5410.Input;
using System;

namespace CS5410
{
    public class Maze : Game
    {
        private GraphicsDeviceManager m_graphics;
        private SpriteBatch m_spriteBatch;
        private MazeLevelGenerator m_mazeGenerator;
        private MazeLevel m_level;
        private Player m_player;
        private int m_screenWidth = 1920/2;
        private int m_screenHeight = 1080/2;
        private int m_cellWidth;
        private int m_cellHeight;
        private int m_xOffset;
        private int m_yOffset;
        private int m_xShift;
        private int m_yShift;
        private Texture2D[] wallTexturePool = new Texture2D[16];
        private Texture2D playerTexture;
        private KeyboardInput m_inputKeyboard;

        public Maze()
        {
            m_graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            m_mazeGenerator = new MazeLevelGenerator();
            m_level = m_mazeGenerator.Generate(10, 10);

            m_graphics.PreferredBackBufferWidth = m_screenWidth;
            m_graphics.PreferredBackBufferHeight = m_screenHeight;

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
            m_player = new Player((0, 0));

            m_inputKeyboard = new KeyboardInput();

            // m_inputKeyboard.registerCommand(Keys.W, false, new IInputDevice.CommandDelegate(onMoveUp));
            // m_inputKeyboard.registerCommand(Keys.S, false, new IInputDevice.CommandDelegate(onMoveDown));
            // m_inputKeyboard.registerCommand(Keys.A, false, new IInputDevice.CommandDelegate(onMoveLeft));
            // m_inputKeyboard.registerCommand(Keys.D, false, new IInputDevice.CommandDelegate(onMoveRight));
            //
            // m_inputKeyboard.registerCommand(Keys.Up, false, new IInputDevice.CommandDelegate(onMoveUp));
            // m_inputKeyboard.registerCommand(Keys.Down, false, new IInputDevice.CommandDelegate(onMoveDown));
            // m_inputKeyboard.registerCommand(Keys.Left, false, new IInputDevice.CommandDelegate(onMoveLeft));
            // m_inputKeyboard.registerCommand(Keys.Right, false, new IInputDevice.CommandDelegate(onMoveRight));
            //
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
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            m_inputKeyboard.Update(gameTime);
            // TODO: Add your update logic here
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
              Cell curr = m_level.getCell(x, y);
              m_spriteBatch.Draw(wallTexturePool[(int)curr.Passage], new Rectangle( 
                  x: x == 0 ? (x*m_cellWidth) + m_xShift : (x*m_cellWidth - x*m_xOffset) + m_xShift,
                  y: y == 0 ? (y*m_cellHeight) + m_yShift : (y*m_cellHeight - y*m_yOffset) + m_yShift,
                  width:m_cellWidth,
                  height:m_cellHeight),
                Color.White);
            }
          }

          // player rendering
          m_spriteBatch.Draw(playerTexture, new Rectangle( 
              x: m_player.getX() == 0 ? (m_player.getX()*m_cellWidth) + m_xShift : (m_player.getX()*m_cellWidth - m_player.getX()*m_xOffset) + m_xShift,
              y: m_player.getY() == 0 ? (m_player.getY()*m_cellWidth) + m_yShift : (m_player.getY()*m_cellWidth - m_player.getY()*m_yOffset) + m_yShift,
              width:m_cellWidth,
              height:m_cellHeight),
            Color.Red);
          m_spriteBatch.End();
          base.Draw(gameTime);
        }
        //
        // private void onMoveUp(GameTime gameTime, float value)
        // {
        //   m_player.updatePosition(m_mazeGrid.move(m_player.getCord(), Direction.North));
        // }
        //
        // private void onMoveDown(GameTime gameTime, float value)
        // {
        //   m_player.updatePosition(m_mazeGrid.move(m_player.getCord(), Direction.South));
        // }
        //
        // private void onMoveLeft(GameTime gameTime, float value)
        // {
        //   m_player.updatePosition(m_mazeGrid.move(m_player.getCord(), Direction.West));
        // }
        //
        // private void onMoveRight(GameTime gameTime, float value)
        // {
        //   m_player.updatePosition(m_mazeGrid.move(m_player.getCord(), Direction.East));
        // }
    }
}
