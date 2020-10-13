﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SampleGame;
using System.Runtime.CompilerServices;

namespace LevelManagement
{
    public abstract class Menu<T> : Menu where T : Menu<T>
    {
        private static T _instance;

        public static T Instance { get => _instance;}

        protected virtual void Awake()
        {
            if(_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = (T)this;
            }
        }

        protected virtual void OnDestroy()
        {
            _instance = null;
        }

        public static void Open()
        {
            if(MenuManager.Instance != null && Instance != null)
            {
                MenuManager.Instance.OpenMenu(Instance);
            }
        }
    }

    [RequireComponent(typeof(Canvas))]
    public abstract class Menu : MonoBehaviour
    {
        //Called when back button is pressed.
        public virtual void OnBackPressed() 
        {
            if(MenuManager.Instance != null)
            {
                MenuManager.Instance.CloseMenu();
            }
        }
    }
}