using UnityEngine;

namespace SingletonPattern
{
    /// <summary>
    /// Basit AudioManager - Müzik ve ses efekti yönetimi
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        #region Singleton
        
        public static AudioManager Instance { get; private set; }
        
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
        
        #region Audio Sources
        
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;
        
        #endregion
        
        #region Music Methods
        
        public void PlayMusic(AudioClip clip)
        {
            if (musicSource != null && clip != null)
            {
                musicSource.clip = clip;
                musicSource.loop = true;
                musicSource.Play();
            }
        }
        
        public void StopMusic()
        {
            if (musicSource != null)
            {
                musicSource.Stop();
            }
        }
        
        #endregion
        
        #region Sound Effects
        
        public void PlaySFX(AudioClip clip)
        {
            if (sfxSource != null && clip != null)
            {
                sfxSource.PlayOneShot(clip);
            }
        }
        
        #endregion
        
        #region Volume Control
        
        public void SetMusicVolume(float volume)
        {
            if (musicSource != null)
            {
                musicSource.volume = Mathf.Clamp01(volume);
            }
        }
        
        public void SetSFXVolume(float volume)
        {
            if (sfxSource != null)
            {
                sfxSource.volume = Mathf.Clamp01(volume);
            }
        }
        
        #endregion
    }
}

/* 
 * KULLANIM ÖRNEKLERİ:
 * 
 * // Müzik çalma
 * AudioManager.Instance.PlayMusic(musicClip);
 * AudioManager.Instance.StopMusic();
 * 
 * // Ses efekti
 * AudioManager.Instance.PlaySFX(jumpSound);
 * 
 * // Volume
 * AudioManager.Instance.SetMusicVolume(0.5f);
 * AudioManager.Instance.SetSFXVolume(0.8f);
 */
