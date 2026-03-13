using UnityEngine;

/// <summary>
/// Ses seviyesi ve oyuncu adini PlayerPrefs ile kaydeden ayar ornegi.
/// Sahneye bos bir GameObject koy, bu scripti ekle, Play'e bas.
/// </summary>
public class SettingsManager : MonoBehaviour
{
    private float musicVolume;
    private string playerName;

    private void Start()
    {
        // Kayitli ayarlari oku (yoksa varsayilan degerler doner)
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        playerName = PlayerPrefs.GetString("PlayerName", "Oyuncu");

        Debug.Log("Hosgeldin " + playerName + "! Muzik seviyesi: " + musicVolume);
    }

    private void Update()
    {
        // Yukari ok: sesi ac
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            musicVolume = Mathf.Min(musicVolume + 0.1f, 1f);
            SaveVolume();
        }

        // Asagi ok: sesi kis
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            musicVolume = Mathf.Max(musicVolume - 0.1f, 0f);
            SaveVolume();
        }

        // N tusuna basinca isim degistir
        if (Input.GetKeyDown(KeyCode.N))
        {
            playerName = "Oyuncu_" + Random.Range(1, 100);
            PlayerPrefs.SetString("PlayerName", playerName);
            PlayerPrefs.Save();
            Debug.Log("Yeni isim kaydedildi: " + playerName);
        }
    }

    private void SaveVolume()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.Save();
        Debug.Log("Muzik seviyesi: " + musicVolume.ToString("F1"));
    }
}
