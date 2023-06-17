/*
    This is a simple example project for MonkeStatistics
*/
using BepInEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoreboard
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.NAME, PluginInfo.VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public void Start()
        {
            Logger.LogInfo("Scoreboard plugin loaded!");
            MonkeStatistics.API.Registry.Register();
        }
    }
}
