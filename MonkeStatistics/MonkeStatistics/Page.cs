using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeStatistics
{
    public class Page
    {
        /// <summary>
        /// Called when the page is opened.
        /// </summary>
        public virtual void OnOpened()
        {

        }

        /// <summary>
        /// Called whenever a button is pressed.
        /// </summary>
        public virtual void OnButtonPress()
        {

        }

        /// <summary>
        /// Called after the page is cleaned up for the new page.
        /// </summary>
        public virtual void OnClosed()
        {

        }
    }
}
