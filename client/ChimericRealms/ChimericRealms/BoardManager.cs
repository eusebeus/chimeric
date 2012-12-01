using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace ChimericRealms
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class BoardManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        List<BoardTile> tileHolder = new List<BoardTile>();
        int[,] generatedBoard = new int[4 * _boardSize - 4, 5];
        List<BoardSprite> tileList = new List<BoardSprite>();
        List<Player> playerList = new List<Player>();
        BoardSprite[,] tileArray = new BoardSprite[_boardSize, _boardSize];

        Point[] destinationPoint = new Point[4];

        MouseState prevMouseState;
        KeyboardState prevKeyboardState;
        Camera2d cam = new Camera2d();  //Main window
        Vector2 pos1 = new Vector2(500.0f, 200.0f);  //Main window start location
        float zoom = 0.5f;
        float speed1 = 12f;   //Scroll speed
        float speed2 = 0.1f;  //Zoom speed

        int tileSize = 1024; // Size of the board tiles
        static int _boardSize = 4;
        // int boardLevels = 1;
        int[] boardArray = new int[8];

        public int TileSize
        {
            get { return tileSize; }
        }


        public BoardManager(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
            prevKeyboardState = Keyboard.GetState();

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            int h = 0;
            int j = 0;
            int k = 0;

            tileHolder.Add(new BoardTile(Game.Content.Load<Texture2D>(@"Images\Alhambra"), 1, "Alhambra", "Red"));
            tileHolder.Add(new BoardTile(Game.Content.Load<Texture2D>(@"Images\Bedlam"), 1, "Bedlam", "Red"));
            tileHolder.Add(new BoardTile(Game.Content.Load<Texture2D>(@"Images\Chapel"), 1, "Chapel", "Red"));
            tileHolder.Add(new BoardTile(Game.Content.Load<Texture2D>(@"Images\City"), 1, "City", "Red"));
            tileHolder.Add(new BoardTile(Game.Content.Load<Texture2D>(@"Images\Fields"), 0, "Fields", "Red"));
            tileHolder.Add(new BoardTile(Game.Content.Load<Texture2D>(@"Images\Forest"), 0, "Forest", "Red"));
            tileHolder.Add(new BoardTile(Game.Content.Load<Texture2D>(@"Images\Hills"), 0, "Hills", "Red"));
            tileHolder.Add(new BoardTile(Game.Content.Load<Texture2D>(@"Images\Island"), 0, "Island", "Red"));

            for (int i = 0; i < 4 * _boardSize - 4; i++)
            {
                int f = ((Game1)Game).rnd.Next(0, 7);

                generatedBoard[i, 0] = f;
                generatedBoard[i, 1] = h;
                generatedBoard[i, 2] = j;

                if (k == 0)
                {
                    if (h < _boardSize - 1)
                    {
                        h = h + 1;
                    }
                    else if (j < _boardSize - 1)
                    {
                        j = j + 1;
                    }
                }
                else
                {
                    if (h > 0)
                    {
                        h = h - 1;
                    }
                    else if (j > 0)
                    {
                        j = j - 1;
                    }
                }
                if (j == _boardSize - 1) { k = 1; }


            }

            //for (int i = 1; i < _boardSize; i++)
            //{
            //    int f = ((Game1)Game).rnd.Next(0, 7);

            //    generatedBoard[h, 0] = f;
            //    generatedBoard[h, 1] = _boardSize - 1;
            //    generatedBoard[h, 2] = i;
            //    generatedBoard[h, 3] = (_boardSize - 1) * tileSize;
            //    generatedBoard[h, 4] = i * tileSize;

            //    h = h + 1;
            //}

            //for (int i = _boardSize - 1; i == 0; i--)
            //{
            //    int f = ((Game1)Game).rnd.Next(0, 7);

            //    generatedBoard[h, 0] = f;
            //    generatedBoard[h, 1] = i;
            //    generatedBoard[h, 2] = _boardSize;
            //    generatedBoard[h, 3] = i * tileSize;
            //    generatedBoard[h, 4] = _boardSize * tileSize;

            //    h = h + 1;
            //}


            for (int i = 0; i < 4 * _boardSize - 4; i++)
            {
                tileList.Add(new BoardSprite(tileHolder[generatedBoard[i,0]].TextureImage, generatedBoard[i,1], generatedBoard[i,2], new Point(tileSize, tileSize), 0f, this));
            }

            playerList.Add(new Player(Game.Content.Load<Texture2D>(@"Images\helmetfish"), 0.5f, 0.5f, new Point(56, 85), 0, 3, 2, 1, 2, 1f, this));

            //tileArray[0, 0] = new BoardSprite(tileHolder[0].TextureImage, new Vector2(tileSize * tileHolder[0].tileGridLocation.X, tileSize * tileHolder[0].tileGridLocation.Y), new Point(tileSize, tileSize));
            //tileArray[1, 0] = new BoardSprite(tileHolder[4].TextureImage, new Vector2(tileSize * 1, tileSize * 0), new Point(tileSize, tileSize));
            //tileArray[2, 0] = new BoardSprite(tileHolder[1].TextureImage, new Vector2(tileSize * 2, tileSize * 0), new Point(tileSize, tileSize));
            //tileArray[0, 1] = new BoardSprite(tileHolder[5].TextureImage, new Vector2(tileSize * 0, tileSize * 1), new Point(tileSize, tileSize));
            //tileArray[2, 1] = new BoardSprite(tileHolder[6].TextureImage, new Vector2(tileSize * 2, tileSize * 1), new Point(tileSize, tileSize));
            //tileArray[0, 2] = new BoardSprite(tileHolder[2].TextureImage, new Vector2(tileSize * 0, tileSize * 2), new Point(tileSize, tileSize));
            //tileArray[1, 2] = new BoardSprite(tileHolder[7].TextureImage, new Vector2(tileSize * 1, tileSize * 2), new Point(tileSize, tileSize));
            //tileArray[2, 2] = new BoardSprite(tileHolder[2].TextureImage, new Vector2(tileSize * 2, tileSize * 2), new Point(tileSize, tileSize));


            //boardArray[0] = 0;
            //boardArray[1] = 2;
            //boardArray[2] = 5;
            //boardArray[3] = 7;
            //boardArray[4] = 1;
            //boardArray[5] = 3;
            //boardArray[6] = 4;
            //boardArray[7] = 6;

            //for (int i = 0; i < _boardSize; i++)
            //{
            //    for (int j = 0; j < _boardSize; j++)
            //    {
            //        if (i == 0 || j == 0 || i == 2 || j == 2)
            //        {
            //            tileList.Add(new BoardTile(tileHolder[boardArray[h]], new Vector2(tileSize * i, tileSize * j), new Point(tileSize, tileSize)));
            //            h = h + 1;
            //        }
            //    }
            //}

        }

        public int[,] GeneratedBoard
        {
            get { return generatedBoard; }
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // Arrow keys move the camera

            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Left))
                pos1.X -= speed1;
            if (keyboardState.IsKeyDown(Keys.Right))
                pos1.X += speed1;
            if (keyboardState.IsKeyDown(Keys.Up))
                pos1.Y -= speed1;
            if (keyboardState.IsKeyDown(Keys.Down))
                pos1.Y += speed1;


            if (playerList[0].PlayerStopped)
            {
                if (keyboardState.IsKeyDown(Keys.D))
                {
                    if (prevKeyboardState.IsKeyUp(Keys.D))
                    {
                        playerList[0].PlayerStopped = false;

                        if (playerList[0].CurrentLocation < 4 * _boardSize - 5)
                            playerList[0].MovePlayer(playerList[0].CurrentLocation + 1);
                        else
                            playerList[0].MovePlayer(0);
                    }
                }
                if (keyboardState.IsKeyDown(Keys.A))
                {
                    if (prevKeyboardState.IsKeyUp(Keys.A))
                    {
                        playerList[0].PlayerStopped = false;

                        if (playerList[0].CurrentLocation > 0)
                            playerList[0].MovePlayer(playerList[0].CurrentLocation - 1);
                        else
                            playerList[0].MovePlayer(4 * _boardSize - 5);
                    }
                }
            }

            if (playerList[0].PlayerStopped == false)
            {
                if (playerList[0].DestinationGridX > playerList[0].GridX)
                {
                    playerList[0].GridX = Math.Round(playerList[0].GridX + 0.1, 1);
                }
                else if (playerList[0].DestinationGridX < playerList[0].GridX)
                {
                    playerList[0].GridX = Math.Round(playerList[0].GridX - 0.1, 1);
                }
                if (playerList[0].DestinationGridY > playerList[0].GridY)
                {
                    playerList[0].GridY = Math.Round(playerList[0].GridY + 0.1, 1);
                }
                else if (playerList[0].DestinationGridY < playerList[0].GridY)
                {
                    playerList[0].GridY = Math.Round(playerList[0].GridY - 0.1, 1);
                }
                if (playerList[0].DestinationGridY == playerList[0].GridY & playerList[0].DestinationGridX == playerList[0].GridX)
                {
                    playerList[0].PlayerStopped = true;
                }


            }



            MouseState mouseState = Mouse.GetState();
            if (mouseState.ScrollWheelValue < prevMouseState.ScrollWheelValue)
                zoom = zoom - speed2;
            else if (mouseState.ScrollWheelValue > prevMouseState.ScrollWheelValue)
                zoom = zoom + speed2;

            prevMouseState = mouseState;
            prevKeyboardState = keyboardState;


            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            cam.Pos = pos1;
            cam.Zoom = zoom;

            GraphicsDevice.Viewport = ((Game1)Game).leftViewport;

            spriteBatch.Begin(SpriteSortMode.FrontToBack,
                        BlendState.AlphaBlend,
                        null,
                        null,
                        null,
                        null,
                        cam.get_transformation(GraphicsDevice));

            foreach (Player p in playerList)
            {
                p.Draw(gameTime, spriteBatch);
            }

            foreach (BoardSprite b in tileList)
            {
                b.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }



    }
}
