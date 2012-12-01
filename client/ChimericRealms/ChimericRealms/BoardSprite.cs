using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ChimericRealms
{
    class BoardSprite : Sprite
    {
        BoardTile spriteTile;

        public BoardSprite(Texture2D textureImage, float gridX, float gridY, Point frameSize, float layerDepth, BoardManager boardManager)
            : base(textureImage, gridX, gridY, frameSize, layerDepth, boardManager)
        {

        }

    }
}
