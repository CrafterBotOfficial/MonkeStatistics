using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using MonkeStatistics.Pages;
using MonkeStatistics.UI;

namespace MonkeStatistics;

public sealed class Register
{
    private static Lazy<Register> instance = new Lazy<Register>(() => new Register());
    internal static Register Instance => instance.Value;

    private static object padLock = new object();

    private List<IPage> pages;

    public Register()
    {
        FindAllAuto();
        pages.AddRange([
            new RoomInfo()
        ]);
    }

    public IPage[] GetPages()
    {
        if (pages == null)
        {
            return [];
        }
        return pages.ToArray();
    }

    private void FindAllAuto()
    {
        var assemblies = BepInEx.Bootstrap.Chainloader.PluginInfos.Select(x => x.Value.Instance.GetType().Assembly).Where(x => x != typeof(Main).Assembly);
        var types = assemblies.SelectMany(x => x.GetTypes());
        var autoRegisterTypes = types.Where(x => x.GetCustomAttributes(typeof(AutoRegisterAttribute), false).Any() && IsPage(x));
        pages = autoRegisterTypes.Select(x => Activator.CreateInstance(x) as IPage).ToList();
    }

    /// <summary>
    /// Registered pages will be visible in the main menu, to manually activate a page use the UIManager.SwitchPage() method
    /// </summary>
    public static void RegisterPage<T>() where T : IPage
    {
        RegisterPage(typeof(T));
    }

    public static void RegisterPage(Type page)
    {
        if (!IsPage(page))
        {
            Main.Log("Page doesn't impliment IPage interface", BepInEx.Logging.LogLevel.Error);
            return;
        }

        Main.Log("Registering " + page.FullName, BepInEx.Logging.LogLevel.Debug);
        lock (padLock)
        {
            IPage pageInstance = (IPage)Activator.CreateInstance(page);
            Instance.pages.Add(pageInstance);
        }
    }

    private static bool IsPage(Type t)
    {
        return t.GetInterfaces().Select(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IPage)).Any();
    }
}
