using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * Author: John Marnon
 * Date Created: 1/28/15
 * Last Edited By: John Marnon
 * Last Edited On: 1/28/15
 */

namespace IGVC_Controller.DataIO
{
    /// <summary>
    /// Class that is associated with a Windows Form and watches its key events
    /// so that the current state of the keyboard can be examined
    /// </summary>
    class Keyboard
    {
        /// <summary>
        /// Associates a state with each key that has had its state changed
        /// so far.  true = key_down | false = key_up
        /// </summary>
        private Dictionary<Keys, bool> m_keyTable = new Dictionary<Keys, bool>();

        /// <summary>
        /// Creates a Keyboard object with an associated Form
        /// </summary>
        /// <param name="form">
        /// The form that the keyboard will watch for keyboard input
        /// </param>
        public Keyboard(Form form)
        {
            form.KeyDown += form_KeyDown;
            form.KeyUp += form_KeyUp;
            form.KeyPreview = true;
        }

        void form_KeyUp(object sender, KeyEventArgs e)
        {
            if (!m_keyTable.ContainsKey(e.KeyCode))
                m_keyTable.Add(e.KeyCode, false);
            else
                m_keyTable[e.KeyCode] = false;
        }

        void form_KeyDown(object sender, KeyEventArgs e)
        {
            if (!m_keyTable.ContainsKey(e.KeyCode))
                m_keyTable.Add(e.KeyCode, true);
            else
                m_keyTable[e.KeyCode] = true;
        }

        /// <summary>
        /// Checks if the specified key is currently pressed
        /// </summary>
        /// <param name="key">The key to check</param>
        /// <returns>Will return true if the key is pressed</returns>
        public bool isKeyDown(Keys key)
        {
            if (m_keyTable.ContainsKey(key))
                return m_keyTable[key];
            return false;
        }

        /// <summary>
        /// Checks if the specified key is currently released
        /// </summary>
        /// <param name="key">The key to check</param>
        /// <returns>Will return true if the key is released</returns>
        public bool isKeyUp(Keys key)
        {
            if (m_keyTable.ContainsKey(key))
                return !m_keyTable[key];
            return true;
        }
    }
}
