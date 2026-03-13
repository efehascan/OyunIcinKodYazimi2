using UnityEngine;

/// <summary>
/// En yuksek skoru PlayerPrefs ile kaydedip okuyan basit ornek.
/// Sahneye bos bir GameObject koy, bu scripti ekle, Play'e bas.
/// </summary>
public class ScoreManager : MonoBehaviour
{
    private int currentScore;
    private int highScore;

    private void Start()
    {
        // Kayitli en yuksek skoru oku (yoksa 0 doner)
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        Debug.Log("Kayitli en yuksek skor: " + highScore);
    }

    private void Update()
    {
        // Space tusuna basinca 10 puan ekle
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentScore += 10;
            Debug.Log("Skor: " + currentScore);

            // Yeni rekor mu?
            if (currentScore > highScore)
            {
                highScore = currentScore;
                PlayerPrefs.SetInt("HighScore", highScore);
                PlayerPrefs.Save();
                Debug.Log("Yeni rekor kaydedildi: " + highScore);
            }
        }

        // R tusuna basinca skoru sifirla
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentScore = 0;
            Debug.Log("Skor sifirlandi. Rekor hala duruyor: " + highScore);
        }

        // Delete tusuna basinca tum kayitlari sil
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            PlayerPrefs.DeleteKey("HighScore");
            PlayerPrefs.Save();
            highScore = 0;
            Debug.Log("Rekor silindi!");
        }
    }
}
