using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeStatistics
{
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class MainMenuItem : Attribute
    {
        internal string DisplayName;
        public MainMenuItem(string displayName)
        {
            DisplayName = displayName;
        }
    }
}
