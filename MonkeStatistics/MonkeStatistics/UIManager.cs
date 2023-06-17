/*
    This script was inspired by the ComputerInterface mod.
*/
using HarmonyLib;
using MonkeStatistics.API;
using MonkeStatistics.Behaviors;
using MonkeStatistics.Pages;
using System;
using System.Linq;
using UnityEngine;

namespace MonkeStatistics
{
    /// <summary>
    /// Please do not touch this class unless you know what you are doing.
    /// </summary>
    public class UIManager
    {
        public static UIManager Instance;
        public static Page CurrentPage;

        private GameObject WatchObj;
        public GameObject MenuObj;

        public Transform BaseLine;
        public Transform ButtonGrouping;

        public GameObject ScrollUp;
        public GameObject ScrollDown;

        internal UIManager(GameObject Watch)
        {
            Instance = this;
            WatchObj = Watch;
            LoadMenu();
        }

        private async void LoadMenu()
        {
            MenuObj = UnityEngine.Object.Instantiate(await Main.Instance.LoadAsset("MenuObj"));
            Transform MenuTransform = MenuObj.transform;
            //MenuTransform.SetParent(GorillaLocomotion.Player.Instance.leftHandFollower);
            MenuTransform.localPosition = Vector3.zero;

            ButtonGrouping = MenuTransform.GetChild(0).Find("Lines");

            MenuObj.AddComponent<MenuFollower>().Target = WatchObj.transform;

            BaseLine = ButtonGrouping.GetChild(0);
            BaseLine.gameObject.SetActive(false);

            Transform Panel = MenuTransform.GetChild(0);
            ScrollUp = Panel.Find("Up").gameObject;
            ScrollDown = Panel.Find("Down").gameObject;

            ScrollUp.AddComponent<ScrollButton>().IsUp = true;
            ScrollDown.AddComponent<ScrollButton>();
            Panel.Find("ReturnMain").gameObject.AddComponent<GoToMainMenuButton>();

            RegisterPages();
            ShowPage(typeof(MainPage));
            MenuObj.SetActive(false);
        }

        public void WatchButtonPressed() =>
            MenuObj.SetActive(!MenuObj.activeSelf);

        public static Type[] AllPages;
        public void ShowPage(Type type)
        {
            ClearPage();
            if (AllPages.Contains(type))
            {
                Page page = (Page)Activator.CreateInstance(type);
                page.OnPageOpen();
                CurrentPage = page;
                ("Page opened : " + type.Name).Log();
            }
            else
                ("Page not found : " + type.FullName).Log(BepInEx.Logging.LogLevel.Error);
        }
        public void ClearPage()
        {
            if (ButtonGrouping.childCount != 1)
                for (int i = 1; i < ButtonGrouping.childCount; i++)
                    UnityEngine.Object.Destroy(ButtonGrouping.GetChild(i).gameObject);
        }
        private void RegisterPages()
        {
            AllPages = Registry.CachedAssemblies
                .SelectMany(x => x.GetTypes())
                .Where(x => x.IsSubclassOf(typeof(Page)))
                .ToArray();
            "Registered All Pages".Log();
        }

        internal void ForceClose()
        {
            ClearPage();
            ShowPage(typeof(Pages.MainPage));
            "Force Closed".Log(BepInEx.Logging.LogLevel.Warning);
        }
    }
}