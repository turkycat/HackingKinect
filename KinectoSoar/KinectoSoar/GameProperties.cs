using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectoSoar
{
    class GameProperties
    {
        private static GameProperties _active;
        
        private SpriteManager.Bird _bird;
        public bool GameOver { get; set; }
        public float BorderDensity { get; set; }
        public bool Start { get; set; }
        public bool Ready { get; set; }
        public bool Reset { get; set; }
        public bool Screech { get; set; }
        public int HighScore = 0;
        public int Score = 0;
        public int Multiplier = 1;


        #region Bird related methods

        public void setBird(SpriteManager.Bird bird)
        {
            _bird = bird;
        }

        public void MoveBirdUp(float speed)
        {
            if (_bird != null)
            {
                _bird.MoveUp(speed);
            }
        }

        public void MoveBirdLeft(float speed)
        {
            if (_bird != null)
            {
                _bird.MoveLeft(speed);
            }
        }

        public void MoveBirdRight(float speed)
        {
            if (_bird != null)
            {
                _bird.MoveRight(speed);
            }
        }

        #endregion


        #region Game controls

        public void ResetGame()
        {
            Reset = true;
            Start = false;   //unnecessary but failsafe
            Ready = false;
            Score = 0;
        }

        #endregion

        #region GameProperties Instance

        public static GameProperties Instance
        {
            get
            {
                if (_active == null)
                {
                    _active = new GameProperties();
                }
                return _active;
            }
        }

        #endregion

        #region Private Singleton Constructor

        private GameProperties()
        {
            this.Start = false;
            this.Reset = false;
            this.Ready = false;
            this.Screech = false;
            this.GameOver = false;
            this.BorderDensity = 75f;
            this.Score = 0;
            this.HighScore = 0;
            this.Multiplier = 1;
        }

        #endregion
    }
}
