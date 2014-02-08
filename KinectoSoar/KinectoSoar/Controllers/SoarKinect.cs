using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace KinectoSoar.Controllers
{
    using Microsoft.Kinect;
    using Microsoft.Speech.Recognition;
    using System.IO;

    public class SoarKinect : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private KinectSensor _sensor;
        private SpeechRecognitionEngine speechEngine;

        private SpriteBatch _spriteBatch;

        private float highestY = 0f;


        //TODO delete this
        int totalFrames = 0;
        float distanceX = 0;
        float distanceY = 0;
        float distanceZ = 0;
        float currentY = 0;



        public SoarKinect( Game game ) : base( game )
        {

            _spriteBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }



        public override void Initialize()
        {

            foreach (var potentialSensor in KinectSensor.KinectSensors)
            {
                if (potentialSensor.Status == KinectStatus.Connected)
                {
                    this._sensor = potentialSensor;
                    break;
                }
            }

            if (this._sensor != null)
            {
                // Turn on the skeleton stream to receive skeleton frames
                this._sensor.SkeletonStream.Enable();

                //we only care about the arms and above
                this._sensor.SkeletonStream.TrackingMode = SkeletonTrackingMode.Seated;

                // Add an event handler to be called whenever there is new color frame data
                this._sensor.SkeletonFrameReady += this.SensorSkeletonFrameReady;

                // Start the sensor!
                try
                {
                    this._sensor.Start();
                }
                catch (IOException)
                {
                    this._sensor = null;
                }
            }

            if (this._sensor == null)
            {
                //nothing to do
            }

            base.Initialize();
        }



        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.DrawString(Resources.Instance.GetFont("Cooper"), "X: " + distanceX.ToString(), new Vector2(50, 200), Color.White);
            _spriteBatch.DrawString(Resources.Instance.GetFont("Cooper"), "Y: " + distanceY.ToString(), new Vector2(50, 250), Color.White);
            _spriteBatch.DrawString(Resources.Instance.GetFont("Cooper"), "Z: " + distanceZ.ToString(), new Vector2(50, 300), Color.White);
            _spriteBatch.DrawString(Resources.Instance.GetFont("Cooper"), "c: " + currentY.ToString(), new Vector2(50, 350), Color.White);
            base.Draw(gameTime);
        }


        /**
         * 
         */
        private void SensorSkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            Skeleton[] skeletons = new Skeleton[0];

            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame != null)
                {
                    ++totalFrames;
                    skeletons = new Skeleton[skeletonFrame.SkeletonArrayLength];
                    skeletonFrame.CopySkeletonDataTo(skeletons);
                }

                if (skeletons.Length != 0 && skeletons[0] != null)
                {
                    foreach (Skeleton skel in skeletons)
                    {
                        if (skel.TrackingState == SkeletonTrackingState.Tracked)
                        {
                            SkeletonPoint left = skel.Joints[JointType.WristLeft].Position;
                            SkeletonPoint right = skel.Joints[JointType.WristRight].Position;
                            distanceY = left.Y - right.Y;
                            distanceZ = left.Z - right.Z;
                            distanceX = left.X - right.X;
                            if (Math.Abs(left.Y - right.Y) < 0.2f)
                            {
                                if( left.Y < currentY && Math.Abs( left.Y - currentY ) > 0.05f )
                                    Resources.Instance.MoveBirdUp(140f * Math.Abs(left.Y - currentY));
                                
                                currentY = left.Y;
                            }
                            else if ((left.Y - right.Y) < -0.4f)
                            {
                                Resources.Instance.MoveBirdLeft(20f * (Math.Abs(right.Y - left.Y) - .5f));
                            }
                            else if ((left.Y - right.Y) > 0.4f)
                            {
                                Resources.Instance.MoveBirdRight(20f * (Math.Abs(left.Y - right.Y) - .5f));
                            }
                        }
                    }
                }
            }


        }




        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (null != this._sensor)
            {
                this._sensor.Stop();
            }
        }

    }

}