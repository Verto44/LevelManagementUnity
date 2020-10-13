using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelManagement
{
    public class LevelLoader : MonoBehaviour
    {
        private static int mainMenuIndex = 1;

        [SerializeField]
        private string nextLevelName;

        [SerializeField]
        private int nextLevelIndex;

        //Carrega uma cena específica pelo nome
        public static void LoadLevel(string levelName)
        {
            if (Application.CanStreamedLevelBeLoaded(levelName))
            {
                SceneManager.LoadScene(levelName);
            }
            else
            {
                Debug.LogWarning("LEVELLOADER LoadLevel error: invalid scene name specified!");
            }
        }

        //Carrega uma cena específica pelo index
        public static void LoadLevel(int levelIndex)
        {
            if (levelIndex >= 0 && levelIndex < SceneManager.sceneCountInBuildSettings)
            {
                if (levelIndex == mainMenuIndex)
                {
                    MainMenu.Open();
                }

                SceneManager.LoadScene(levelIndex);
            }
            else
            {
                Debug.LogWarning("LEVELLOADER LoadLevel error: invalid scene index specified!");
            }
        }

        // recarrega uma cena
        public static void ReloadLevel()
        {
            LoadLevel(SceneManager.GetActiveScene().name);
        }

        public static void LoadNextLevel()
        {
            //Scene currentScene = SceneManager.GetActiveScene();
            //int currentSceneIndex = currentScene.buildIndex;

            //int nextSceneIndex = currentSceneIndex + 1;

            //int totalSceneCount = SceneManager.sceneCountInBuildSettings;

            //if(nextSceneIndex == totalSceneCount)
            //{
            //    nextSceneIndex = 0;
            //}

            //Forma mais enxuta
            int nextSceneIndex = (SceneManager.GetActiveScene().buildIndex + 1) %
                SceneManager.sceneCountInBuildSettings;

            // Returns the min value if the given value is less than the min value. 
            // Returns the max value if the given value is greater than the max value.
            // Clamp(int value, int min, int max);
            nextSceneIndex = Mathf.Clamp(nextSceneIndex, mainMenuIndex, nextSceneIndex);

            LoadLevel(nextSceneIndex);
        }

        public static void LoadMainMenuLevel()
        {
            LoadLevel(mainMenuIndex);
        }

    } 
}
