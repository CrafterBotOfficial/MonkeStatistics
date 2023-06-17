using System;

namespace MonkeStatistics.API
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    //[Obsolete("This attribute is obsolete, please use ShowInMainPage instead.")]
    public class DisplayInMainMenu : Attribute
    {
        public string DisplayName;
        /// <summary>
        /// If true, this button will work in all lobbies. 
        /// If false, it will only work in modded lobbies.
        /// </summary>
        public bool CanWorkInNoneModded;
        public DisplayInMainMenu(string DisplayName)
        {
            this.DisplayName = DisplayName;
        }
        public DisplayInMainMenu(string DisplayName, bool CanWorkInNoneModded)
        {
            this.DisplayName = DisplayName;
            this.CanWorkInNoneModded = CanWorkInNoneModded;
        }
    }
}
