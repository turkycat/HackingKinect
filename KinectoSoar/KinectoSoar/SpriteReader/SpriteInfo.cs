
/**********************************************************/
/**                                                      **/
/**                Author: James Boddie                  **/
/**                Date: 2/2/2014                        **/
/**                                                      **/
/**********************************************************/

using Microsoft.Xna.Framework;

namespace SpriteReader
{
    /// <summary>
    /// Basically a data class that just holds information for
    /// a sprite image within a sprite sheet. After using it, I
    /// realized I could have just used a Rectangle considering
    /// that is the only information I really used. For extendability
    /// purposes I left this as is. There is some other information
    /// that could be added such as offset and collision information.
    /// </summary>
    public class SpriteInfo
    {
        #region Class Member Variables

        public string Name { get; private set; }
        public Rectangle Position { get; private set; }

        #endregion

        #region Public Methods and Constructors

        public SpriteInfo(string name, Rectangle position)
        {
            this.Name = name;
            this.Position = position;
        }

        /// <summary>
        /// Mainly used this for testing. Outputs the information in a readable 
        /// and easily understandable format.
        /// </summary>
        /// <returns>The string representation of the sprite information</returns>
        public override string ToString()
        {
            return Name + ", [" + Position.X + ", " + Position.Y + ", " + Position.Width + ", " + Position.Height + "]";
        }

        #endregion
    }
}
