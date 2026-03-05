using UnityEngine;

namespace OrnekSOLID.Assets.Scripts.Managers
{

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        private int score = 0;
        
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
        
        public void AddScore(int amount)
        {
            score += amount;
            Debug.Log("Skor: " + score);
        }
    }
}