using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LevelManagement.Missions;
using System;

namespace LevelManagement
{
    [RequireComponent(typeof(MissionSelector))]
    public class LevelSelectMenu : Menu<LevelSelectMenu>
    {
        #region INSPECTOR
        [SerializeField] protected Text _nameText;
        [SerializeField] protected Text _descriptionText;
        [SerializeField] protected Image _preiewImage;

        [SerializeField] protected float _playDelay = 0.5f;
        [SerializeField] protected TransitionFader _playTrasitionPrefab;
        #endregion

        #region PROTECTED
        protected MissionSelector _missionSelector;
        protected MissionSpecs _currentMission;
        #endregion

        protected override void Awake()
        {
            base.Awake();
            _missionSelector = GetComponent<MissionSelector>();
            UpdateInfo();
        }

        private void OnEnable()
        {
            UpdateInfo();
        }

        public void UpdateInfo()
        {
            _currentMission = _missionSelector.GetCurrentMission();

            _nameText.text = _currentMission?.Name;
            _descriptionText.text = _currentMission?.Description;
            _preiewImage.sprite = _currentMission?.Image;
            _preiewImage.color = Color.white;
        }

        public void OnNextPressed()
        {
            _missionSelector.IncrementIndex();
            UpdateInfo();
        }

        public void OnPreviousPressed()
        {
            _missionSelector.DecrementIndex();
            UpdateInfo();
        }

        public void OnPlayPressed()
        {
            StartCoroutine(PlayMissionRoutine(_currentMission?.SceneName));
        }

        private IEnumerator PlayMissionRoutine(string sceneName)
        {
            TransitionFader.PlayTransition(_playTrasitionPrefab);
            LevelLoader.LoadLevel(sceneName);
            yield return new WaitForSeconds(_playDelay);
            GameMenu.Open();
        }
    }
}
