
/**********************************************************/
/**                                                      **/
/**                Author: James Boddie                  **/
/**                Date: 2/2/2014                        **/
/**                                                      **/
/**********************************************************/

using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;

namespace SpriteReader
{
    /// <summary>
    /// This class is used to interpret an xml description
    /// file for a sprite sheet.
    /// </summary>
    public class SpriteReader
    {
        #region Class Member Variables

        // Hold the sprite information (name and location within sheet) for all images
        private Dictionary<string, SpriteInfo> _sprites;

        #endregion

        #region Public Methods and Constructors

        /// <summary>
        /// Constructor that reads in and parses the xml sheet that
        /// represents. Built within the constructor for ease of use.
        /// </summary>
        /// <param name="filepath">Location of file</param>
        /// <param name="filename">Name of file</param>
        public SpriteReader(string filepath, string filename)
        {
            _sprites = new Dictionary<string, SpriteInfo>();
            string xml = File.ReadAllText(filepath + filename);
            buildFromXML(xml);
        }

        /// <summary>
        /// Gets an information class that retains the location
        /// of the image with the given key in the sprite sheet.
        /// </summary>
        /// <param name="key">Name of image in xml doc</param>
        /// <returns>Sprite position and name information</returns>
        public SpriteInfo getSpriteInfo(string key)
        {
            if (!_sprites.ContainsKey(key))
                return null;
            return _sprites[key];
        }

        /// <summary>
        /// Was mainly used for testing to make sure correct 
        /// information was received. Outputs all the parsed 
        /// information
        /// </summary>
        /// <returns>String representation of the xml parsed data</returns>
        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            foreach (KeyValuePair<string, SpriteInfo> sprite in _sprites)
            {
                output.Append(sprite.Value.ToString() + "\n");
            }
            return output.ToString();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Parses the xml file in order to save it into a dictionary
        /// for ease of use and access. Called by constructor.
        ///     n - represents name of sprite
        ///     x - X location of image within sprite sheet
        ///     y - Y location of image within sprite sheet
        ///     w - Width of image
        ///     h - Height of image
        /// </summary>
        /// <param name="xml">All the text within the xml file</param>
        private void buildFromXML(string xml)
        {
            using (XmlReader reader = XmlReader.Create(new StringReader(xml)))
            {
                string name = string.Empty;
                Rectangle position = new Rectangle(0, 0, 0, 0);
                Rectangle offset = new Rectangle(0, 0, 0, 0);
                while (reader.Read())
                {
                    if (reader.Name == "sprite")
                    {
                        name = Path.GetFileNameWithoutExtension(reader.GetAttribute("n"));
                        position = new Rectangle(Convert.ToInt32(reader.GetAttribute("x")), Convert.ToInt32(reader.GetAttribute("y")),
                            Convert.ToInt32(reader.GetAttribute("w")), Convert.ToInt32(reader.GetAttribute("h")));
                        _sprites.Add(name, new SpriteInfo(name, position));
                    }
                }
            }
        }

        #endregion
    }
}
