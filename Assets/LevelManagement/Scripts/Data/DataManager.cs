using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement.Data
{
    public class DataManager : MonoBehaviour
    {
        private static DataManager _instance;

        public static DataManager Instance { get => _instance; }

        private SaveData _saveData;
        private JsonSaver _jsonSaver;

        public float MasterVolume 
        { 
            get { return _saveData.masterVolume; }
            set { _saveData.masterVolume = value; } 
        }

        public float SfxVolume
        {
            get { return _saveData.sfxVolume; }
            set { _saveData.sfxVolume = value; }
        }

        public float MusicVolume
        {
            get { return _saveData.musicVolume; }
            set { _saveData.musicVolume = value; }
        }

        public string PlayerName
        {
            get { return _saveData.playerName; }
            set { _saveData.playerName = value; }
        }

        private void Awake()
        {

            if (_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;

                _saveData = new SaveData();
                _jsonSaver = new JsonSaver();

                //Salva o MenuManager para a próxima cena
                DontDestroyOnLoad(gameObject);
            }
        }

        private void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }

        public void Save()
        {
            _jsonSaver.Save(_saveData);
        }

        public void Load()
        {
            _jsonSaver.Load(_saveData);
        }
    } 
}
