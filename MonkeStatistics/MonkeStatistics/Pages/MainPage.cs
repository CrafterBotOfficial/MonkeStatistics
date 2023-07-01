﻿using MonkeStatistics.API;
using MonkeStatistics.Behaviors;
using System;
using System.Linq;

namespace MonkeStatistics.Pages
{
    public class MainPage : Page
    {
        public override void OnPageOpen()
        {
            base.OnPageOpen();
            int SearchIndex = 0;
            foreach (Type page in UIManager.AllPages)
            {
                var Attribute = page.GetCustomAttributes(typeof(DisplayInMainMenu), false).FirstOrDefault() as DisplayInMainMenu;
                if (Attribute != null)
                    AddLine(Attribute.DisplayName, new ButtonInfo(Info_ButtonPressed, SearchIndex));
                SearchIndex++;
            }
            GoToMainMenuButton.ReturnPage = typeof(MainPage);

            SetLines();
            SetTitle(Main.Name);
            SetAuthor("By Crafterbot");
        }
        private void Info_ButtonPressed(object Sender, object[] Args)
        {
            int ReturnIndex = (int)Args[0];

            var Attribute = UIManager.AllPages[ReturnIndex].GetCustomAttributes(typeof(DisplayInMainMenu), false).FirstOrDefault() as DisplayInMainMenu;
            if (Attribute != null)
                if (Attribute.CanWorkInNoneModded)
                    ShowPage(UIManager.AllPages[ReturnIndex]);
                else if (Main.Instance.RoomModded)
                    ShowPage(UIManager.AllPages[ReturnIndex]);
                else
                    ShowPage<ModdedRoomPage>();
            else
                ShowPage(UIManager.AllPages[ReturnIndex]);
        }
    }
}
