using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SampleGame;
using UnityEngine.UI;
using LevelManagement.Data;

namespace LevelManagement
{
    public class MainMenu : Menu<MainMenu>
    {
        //[SerializeField]
        //private float _playDelay = 0.5f;

        //[SerializeField]
        //private TransitionFader startTransitionPrefab;

        private DataManager _dataManager;

        [SerializeField]
        private InputField _inputField;

        protected override void Awake()
        {
            base.Awake();
            _dataManager = DataManager.Instance;
        }

        private void Start()
        {
            LoadData();
        }

        private void LoadData()
        {
            if(_dataManager != null && _inputField != null)
            {
                _dataManager.Load();
                _inputField.text = _dataManager.PlayerName;
            }
        }

        //Chamado enquanto digita o nome
        public void OnPlayerNameValueChanged(string name)
        {
            if(_dataManager != null)
            {
                _dataManager.PlayerName = name;
            }
        }

        //Cgamado quando termina de digitar o nome
        public void OnPlayerNameEndEdit()
        {
            if (_dataManager != null)
            {
                _dataManager.Save();
            }
        }

        // Called when play button is pressed.
        public void OnPlayPressed()
        {
            LevelSelectMenu.Open();
        }

        //private IEnumerator OnPlayPressedRoutine()
        //{
        //    TransitionFader.PlayTransition(startTransitionPrefab);
        //    LevelLoader.LoadNextLevel();
        //   yield return new WaitForSeconds(_playDelay);
        //    GameMenu.Open();
        //}

        public void OnSettingsPressed()
        {
            SettingsMenu.Open();

        }
        public void OnCreditsPressed()
        {
            CreditsScreen.Open();
        }

        public override void OnBackPressed()
        {
            // Close Application
            Application.Quit();
        }
    } 
}
