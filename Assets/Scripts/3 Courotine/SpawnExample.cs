using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

namespace CoroutineExamples
{
    public class SpawnExample : MonoBehaviour
    {
        void Update()
        {
            if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
            {
                StartCoroutine(SpawnCoroutine());
            }
            
            if (Keyboard.current != null && Keyboard.current.wKey.wasPressedThisFrame)
            {
                StartCoroutine(WaveCoroutine());
            }
        }
        
        // Düşman spawn (her 2 saniyede)
        IEnumerator SpawnCoroutine()
        {
            Debug.Log("Spawn başladı");
            
            for (int i = 1; i <= 10; i++)
            {
                Debug.Log("Düşman #" + i + " spawn edildi");
                yield return new WaitForSecondsRealtime(2f); // Gerçek zamanlı bekleme (Time.timeScale etkilenmez)
            }
            
            Debug.Log("Tüm düşmanlar spawn oldu!");
        }
        
        // Wave sistemi (2 dalga)
        IEnumerator WaveCoroutine()
        {
            for (int wave = 1; wave <= 2; wave++)
            {
                Debug.Log("Wave " + wave + " başladı");
                
                for (int i = 1; i <= 10; i++)
                {
                    Debug.Log("  Düşman " + i);
                    yield return new WaitForSeconds(1f);
                }
                
                Debug.Log("Wave " + wave + " bitti");
                yield return new WaitForSeconds(3f);
            }
            
            Debug.Log("Tüm wave'ler tamamlandı!");
        }
    }
}

// E = Spawn (her 2 saniye)
// W = Wave (2 dalga)
