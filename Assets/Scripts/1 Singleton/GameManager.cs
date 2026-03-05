using UnityEngine;
using UnityEngine.SceneManagement;

namespace SingletonPattern
{
    /// <summary>
    /// Basit GameManager - Skor, can ve oyun durumu yönetimi
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        #region Singleton
        
        public static GameManager Instance { get; private set; }
        
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
        
        #region Variables
        
        private int score = 0;
        private int lives = 3;
        private bool isPaused = false;
        
        #endregion
        
        #region Properties
        
        public int Score => score;
        public int Lives => lives;
        public bool IsPaused => isPaused;
        
        #endregion
        
        #region Score Methods
        
        public void AddScore(int points)
        {
            score += points;
            Debug.Log($"Score: {score}");
        }
        
        public void ResetScore()
        {
            score = 0;
        }
        
        #endregion
        
        #region Lives Methods
        
        public void LoseLife()
        {
            lives--;
            Debug.Log($"Lives: {lives}");
            
            if (lives <= 0)
            {
                GameOver();
            }
        }
        
        public void AddLife()
        {
            lives++;
        }
        
        #endregion
        
        #region Game Control
        
        public void PauseGame()
        {
            isPaused = true;
            Time.timeScale = 0f;
        }
        
        public void ResumeGame()
        {
            isPaused = false;
            Time.timeScale = 1f;
        }
        
        public void GameOver()
        {
            Debug.Log("Game Over!");
            Time.timeScale = 0f;
        }
        
        public void RestartLevel()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        #endregion
    }
}

/* 
 * KULLANIM ÖRNEKLERİ:
 * 
 * // Skor
 * GameManager.Instance.AddScore(100);
 * 
 * // Can
 * GameManager.Instance.LoseLife();
 * GameManager.Instance.AddLife();
 * 
 * // Oyun kontrolü
 * GameManager.Instance.PauseGame();
 * GameManager.Instance.ResumeGame();
 * GameManager.Instance.RestartLevel();
 */
