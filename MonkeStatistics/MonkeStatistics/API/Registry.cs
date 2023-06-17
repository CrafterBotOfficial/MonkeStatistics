using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MonkeStatistics.API
{
    public class Registry
    {
        internal static List<Assembly> CachedAssemblies = new List<Assembly>();

        public static void Register()
        {
            var calling = Assembly.GetCallingAssembly();
            if (!CachedAssemblies.Contains(calling))
                CachedAssemblies.Add(calling);

            $"Found assembly {calling.FullName}".Log();
        }

        /*===================== Deprecated methods =====================*/

        //[Obsolete("We have moved to using a List.")]
        //internal static Assembly[] assemblies = new Assembly[0];
        /// <summary>
        /// This method is used to add an assembly to the list of assemblies that will be searched for pages.
        /// This must be executed before the player is initialized.
        /// </summary>
        [Obsolete("This method is obsolete, please use the new method")]
        public static void AddAssembly()
        {
            CachedAssemblies.Add(Assembly.GetCallingAssembly());
            Assembly.GetCallingAssembly().FullName.Log();
        }
    }
}
