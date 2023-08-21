using System;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer
{
    public class InGameMenu : MonoBehaviour
    {
        GameObject gameOverPanel;
        
        [SerializeField]
        private CanvasGroup canvasGroup;

        [SerializeField] private Button restartButton;
        [SerializeField] private Button quitButton;

        [SerializeField]
        private Text scoreDisplayed;
        
        private void Awake()
        {
            restartButton.onClick.AddListener(CloseMenu);
            quitButton.onClick.AddListener(Application.Quit);
            EndPoint.OnGameComplete += Show;
        }

        private void CloseMenu()
        {
            Debug.Log("Close Menu");
            canvasGroup.interactable = false; 
            canvasGroup.alpha = 0;
            Time.timeScale = 1;
            GameManager.instance.ResetGame();
        }

        private void Show()
        {
            canvasGroup.interactable = true;
            canvasGroup.alpha = 1;
            Time.timeScale = 0;
            Cursor.visible = true;
            scoreDisplayed.text = $"{ScoringSystem.instance.Score}";
        }
    }
}
