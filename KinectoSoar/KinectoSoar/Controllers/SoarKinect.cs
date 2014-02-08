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
    //using Microsoft.Speech.AudioFormat;
    using System.IO;
    using Microsoft.Speech.AudioFormat;

    public class SoarKinect : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private KinectSensor _sensor;
        private SpeechRecognitionEngine speechEngine;

        private SpriteBatch _spriteBatch;


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

            //speech recognizer
            RecognizerInfo ri = GetKinectRecognizer();

            if (ri != null)
            {
                this.speechEngine = new SpeechRecognitionEngine(ri.Id);

                var speech = new Choices();
                speech.Add(new SemanticResultValue("kah", "SCREECH"));
                speech.Add(new SemanticResultValue("caw", "SCREECH"));
                speech.Add(new SemanticResultValue("cah", "SCREECH"));
                speech.Add(new SemanticResultValue("cahh", "SCREECH"));
                speech.Add(new SemanticResultValue("kahh", "SCREECH"));
                speech.Add(new SemanticResultValue("kaw", "SCREECH"));
                speech.Add(new SemanticResultValue("caww", "SCREECH"));
                speech.Add(new SemanticResultValue("caaaawww", "SCREECH"));
                speech.Add(new SemanticResultValue("start", "START"));
                speech.Add(new SemanticResultValue("begin", "START"));
                speech.Add(new SemanticResultValue("murica", "START"));
                speech.Add(new SemanticResultValue("america", "START"));
                
                var gb = new GrammarBuilder { Culture = ri.Culture };
                gb.Append(speech);
                
                var g = new Grammar(gb);

                speechEngine.LoadGrammar(g);
                speechEngine.SpeechRecognized += SpeechRecognized;
                speechEngine.SpeechRecognitionRejected += SpeechRejected;

                speechEngine.SetInputToAudioStream(
                    _sensor.AudioSource.Start(), new SpeechAudioFormatInfo(EncodingFormat.Pcm, 16000, 16, 1, 32000, 2, null));
                speechEngine.RecognizeAsync(RecognizeMode.Multiple);
            }
            else
            {

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





        #region Voice Recognition


        /// <summary>
        /// Gets the metadata for the speech recognizer (acoustic model) most suitable to
        /// process audio from Kinect device.
        /// </summary>
        /// <returns>
        /// RecognizerInfo if found, <code>null</code> otherwise.
        /// </returns>
        private static RecognizerInfo GetKinectRecognizer()
        {
            foreach (RecognizerInfo recognizer in SpeechRecognitionEngine.InstalledRecognizers())
            {
                string value;
                recognizer.AdditionalInfo.TryGetValue("Kinect", out value);
                if ("True".Equals(value, StringComparison.OrdinalIgnoreCase) && "en-US".Equals(recognizer.Culture.Name, StringComparison.OrdinalIgnoreCase))
                {
                    return recognizer;
                }
            }

            return null;
        }




        /// <summary>
        /// Handler for recognized speech events.
        /// </summary>
        /// <param name="sender">object sending the event.</param>
        /// <param name="e">event arguments.</param>
        private void SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            // Speech utterance confidence below which we treat speech as if it hadn't been heard
            const double ConfidenceThreshold = 0.2;


            if (e.Result.Confidence >= ConfidenceThreshold)
            {
                switch (e.Result.Semantics.Value.ToString())
                {
                    case "SCREECH":
                        Resources.Instance.GetSound("EagleCry").Play();
                        break;

                    case "START":
                        //put start event code here
                        break;

                }
            }
        }

        /// <summary>
        /// Handler for rejected speech events.
        /// </summary>
        /// <param name="sender">object sending the event.</param>
        /// <param name="e">event arguments.</param>
        private void SpeechRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            //do nothing
        }


        #endregion


        #region Skeleton Callback

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

        #endregion



        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (null != this._sensor)
            {
                this._sensor.Stop();
            }
        }

    }

}