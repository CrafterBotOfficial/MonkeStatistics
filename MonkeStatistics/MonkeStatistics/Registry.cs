using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MonkeStatistics
{
    public static class Registry
    {
        internal static Dictionary<string, Assembly> CachedAssemblies = new Dictionary<string, Assembly>();
        internal static List<(string Id, MainMenuItem item)> MainMenuItems = new List<(string Id, MainMenuItem item)>();

        /// <summary>
        /// Registers your plugin's assembly to the registry. This can be called at anytime, however your mod will not appear in the main page until its added.
        /// Notice: This will loop through every type in your assembly, so it is recommended to call RegisterAssembly<T>(string) instead. This is only meant to be used if you have mutliple items for the main menus. 
        /// </summary>
        /// <param name="Id">A unique ID for your assembky</param>
        public static async void RegisterAssembly(string Id)
        {
            var callingAssembly = Assembly.GetCallingAssembly();

            if (AddToCache(Id, callingAssembly))
            {
                var mainMenuItems = callingAssembly.GetTypes().Where(x => x.GetCustomAttribute<MainMenuItem>() is object).GetEnumerator();
                while (mainMenuItems.MoveNext())
                {
                    var mainMenuItem = mainMenuItems.Current.GetCustomAttribute<MainMenuItem>();
                    MainMenuItems.Add((Id, mainMenuItem));

                    await System.Threading.Tasks.Task.Yield(); // Reduces lag for larger assemblies
                }

                Main.Log("Registered assembly with ID \"" + Id + "\"");
            }
        }

        /// <summary>
        /// Registers your plugin's assembly to the registry. This can be called at anytime, however your mod will not appear in the main page until its added.
        /// Notice:
        /// 
        /// </summary>
        /// <param name="Id">A unique ID for your assembky</param>
        public static void RegisterAssembly<T>(string Id) where T : Page
        {
            var callingAssembly = Assembly.GetCallingAssembly();

            if (AddToCache(Id, callingAssembly))
            {
                var mainMenuItem = typeof(T).GetCustomAttribute<MainMenuItem>();
                MainMenuItems.Add((Id, mainMenuItem));

                Main.Log("Registered assembly with ID \"" + Id + "\"");
            }
        }

        /// <summary>
        /// Handlers any errors that may occur when registering an assembly.
        /// </summary>
        /// <returns>Returns true if the assembly was added to the cache, false if it was not.</returns>
        private static bool AddToCache(string Id, Assembly callingAssembly)
        {
            if (CachedAssemblies.ContainsKey(Id))
            {
                Main.Log("A assembly with the ID \"" + Id + "\" has already been registered", BepInEx.Logging.LogLevel.Warning);
                return false;
            }
            else
            {
                CachedAssemblies.Add(Id, callingAssembly);
                Main.Log("Registered assembly with ID \"" + Id + "\"");
                return true;
            }
        }


        /// <summary>
        /// Manually adds a main menu item to the registry. This can be called at anytime, however your mod will not appear in the main page until its added.
        /// </summary>
        /// <typeparam name="T">The page</typeparam>
        /// <param name="Id"></param>
        public static void AddMainMenuItem<T>(string Id) where T : Page
        {
            MainMenuItems.Add((Id, typeof(T).GetCustomAttribute<MainMenuItem>()));
        }
    }
}
