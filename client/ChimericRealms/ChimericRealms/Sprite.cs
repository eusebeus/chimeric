using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ChimericRealms
{
    abstract class Sprite
    {
        Texture2D textureImage;
        protected Vector2 position;
        protected Point frameSize;
        double gridX;
        double gridY;
        BoardManager boardManager;
        float layerDepth;

        public Sprite(Texture2D textureImage, double gridX, double gridY, Point frameSize, float layerDepth, BoardManager boardManager)
        {
            this.textureImage = textureImage;
            this.gridX = gridX;
            this.gridY = gridY;
            this.frameSize = frameSize;
            this.boardManager = boardManager;
            this.layerDepth = layerDepth;
        }

        
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureImage,
                new Vector2((float)gridX * boardManager.TileSize, (float)gridY * boardManager.TileSize),
                new Rectangle(0, 0, frameSize.X, frameSize.Y),
                Color.White, 0, Vector2.Zero,
                1f, SpriteEffects.None, layerDepth);
        }

        public double GridX { get { return gridX; } set { gridX = value; } }

        public double GridY { get { return gridY; } set { gridY = value; } }

        public BoardManager BoardManager { get { return boardManager; } }


    }
}
