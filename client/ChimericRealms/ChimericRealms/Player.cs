using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ChimericRealms
{
    class Player : Sprite
    {
        int _initialLocation;
        int _currentLocation;
        bool _playerStopped = true;
        int[] initialStats = new int[4];
        int[] currentStats = new int[4];
        int[] trophiesLevel = new int[3];
        int[] trophiesStrength = { 0, 0, 0 };

        public Player(Texture2D textureImage, double gridX, double gridY, Point frameSize, int initialLocation, int maxHealth, int initialStrength, int initialWits, int initialMagic, float layerDepth, BoardManager boardManager)
            : base(textureImage, gridX, gridY, frameSize, layerDepth, boardManager)
        {
            this._initialLocation = initialLocation;
            this._currentLocation = initialLocation;
            this.initialStats[3] = maxHealth;
            this.initialStats[0] = initialStrength;
            this.initialStats[2] = initialWits;
            this.initialStats[3] = initialMagic;
            this.currentStats[3] = maxHealth;
            this.currentStats[0] = initialStrength;
            this.currentStats[1] = initialWits;
            this.currentStats[2] = initialMagic;
        }

        public void PlayerStatChange(int statSelector, int changeLevel)
        {
            currentStats[statSelector] = currentStats[statSelector] + changeLevel;
        }

        public int CurrentLocation
        {
            get { return _currentLocation; }
            set { _currentLocation = value; }
        }

        public void MovePlayer(int newLocation)
        {
            CurrentLocation = newLocation;
            DestinationGridX = BoardManager.GeneratedBoard[newLocation, 1] + 0.5;
            DestinationGridY = BoardManager.GeneratedBoard[newLocation, 2] + 0.5;
        }

        public bool PlayerStopped { get { return _playerStopped; } set { _playerStopped = value;} }

        public double DestinationGridX { get; set; }

        public double DestinationGridY { get; set; }

    }
}
