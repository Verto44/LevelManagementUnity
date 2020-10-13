using SampleGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManagement
{
    public class GameMenu : Menu<GameMenu>
    {
        public void OnPausedPressed()
        {
            Time.timeScale = 0;

            PauseMenu.Open();
        }

    }

}