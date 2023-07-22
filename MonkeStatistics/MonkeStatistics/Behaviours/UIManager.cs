using MonkeStatistics.Models;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MonkeStatistics.Behaviours
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;
        private const int MaxLines = 10;

        private GameObject BaseLine; // The "prefab" of the lines
        internal List<MenuLine> PooledLines;

        public Page CurrentPage;

        private async void Start()
        {
            Main.Log("Loading the MenuObj...");
            Transform MenuObj = (GameObject.Instantiate(await Main.LoadAsset("MenuObj")) as GameObject).transform;

            PooledLines = new List<MenuLine>();
            Main.Log("Filling the line pool...");

            Transform ButtonGrouping = MenuObj.GetChild(0).Find("Lines");
            BaseLine = ButtonGrouping.GetChild(0).gameObject;
            BaseLine.SetActive(false);

            for (int i = 0; i < MaxLines; i++)
            {
                GameObject Line = GameObject.Instantiate(BaseLine, ButtonGrouping);
                LineButton lineButton = Line.GetComponentInChildren<BoxCollider>().gameObject.AddComponent<LineButton>();

                Line.SetActive(false);
                PooledLines.Add(new MenuLine(Line, Line.GetComponentInChildren<Text>(), lineButton));
            }
        }

        private void ShowPage(Page type)
        {
            ClearCurrent();
            type.OnOpened();
        }

        internal void ClearCurrent()
        {
            for (int i = 0; i < PooledLines.Count; i++)
            {
                PooledLines[i].Line.SetActive(false);
            }
            CurrentPage.OnClosed();
        }
    }
}