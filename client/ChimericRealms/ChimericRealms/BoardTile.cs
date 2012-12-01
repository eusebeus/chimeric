using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ChimericRealms
{
    class BoardTile
    {
        int _cornerTile;
        string _tileName;
        string _tileGroup;
        Texture2D _textureImage;

        public BoardTile(Texture2D textureImage, int cornerTile, string tileName, string tileGroup)
        {
            this._textureImage = textureImage;
            this._tileName = tileName;
            this._tileGroup = tileGroup;
            this._cornerTile = cornerTile;

        }




        public Point tileGridLocation { get; set; }

        public Texture2D TextureImage { get { return _textureImage; } set { _textureImage = value ;} }


    }
}
