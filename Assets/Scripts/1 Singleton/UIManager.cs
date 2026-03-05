using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SingletonPattern
{
    /// <summary>
    /// Basit UIManager - UI panelleri ve text güncellemeleri
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        #region Singleton
        
        public static UIManager Instance { get; private set; }
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        #endregion
        
        #region UI Elements
        
        [Header("Panels")]
        [SerializeField] private GameObject mainMenuPanel;
        [SerializeField] private GameObject gameplayPanel;
        [SerializeField] private GameObject pausePanel;
        
        [Header("HUD")]
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI livesText;
        
        #endregion
        
        #region Panel Methods
        
        public void ShowMainMenu()
        {
            HideAllPanels();
            if (mainMenuPanel != null)
                mainMenuPanel.SetActive(true);
        }
        
        public void ShowGameplay()
        {
            HideAllPanels();
            if (gameplayPanel != null)
                gameplayPanel.SetActive(true);
        }
        
        public void ShowPause()
        {
            if (pausePanel != null)
                pausePanel.SetActive(true);
        }
        
        public void HidePause()
        {
            if (pausePanel != null)
                pausePanel.SetActive(false);
        }
        
        private void HideAllPanels()
        {
            if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
            if (gameplayPanel != null) gameplayPanel.SetActive(false);
            if (pausePanel != null) pausePanel.SetActive(false);
        }
        
        #endregion
        
        #region HUD Updates
        
        public void UpdateScore(int score)
        {
            if (scoreText != null)
                scoreText.text = $"Score: {score}";
        }
        
        public void UpdateLives(int lives)
        {
            if (livesText != null)
                livesText.text = $"Lives: {lives}";
        }
        
        #endregion
    }
}

/* 
 * KULLANIM ÖRNEKLERİ:
 * 
 * // Panel göster
 * UIManager.Instance.ShowMainMenu();
 * UIManager.Instance.ShowGameplay();
 * UIManager.Instance.ShowPause();
 * 
 * // HUD güncelle
 * UIManager.Instance.UpdateScore(1000);
 * UIManager.Instance.UpdateLives(3);
 */
