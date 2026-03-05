using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

namespace CoroutineExamples
{
    public class TimerExample : MonoBehaviour
    {
        void Update()
        {
            if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                StartCoroutine(CountdownCoroutine());
            }
            
            if (Keyboard.current != null && Keyboard.current.tKey.wasPressedThisFrame)
            {
                StartCoroutine(RepeatingCoroutine());
            }
        }
        
        // Geri sayım: 3-2-1-Başla!
        IEnumerator CountdownCoroutine()
        {
            Debug.Log("Geri sayım başladı!");
            
            for (int i = 3; i > 0; i--)
            {
                Debug.Log(i + "...");
                yield return new WaitForSeconds(1f);
            }
            
            Debug.Log("Başla!");
        }
        
        // Her 2 saniyede tekrar et
        IEnumerator RepeatingCoroutine()
        {
            for (int i = 1; i <= 3; i++)
            {
                Debug.Log("Tekrar #" + i);
                yield return new WaitForSeconds(2f);
            }
            
            Debug.Log("Bitti!");
        }
    }
}

// Space = Geri sayım (3-2-1)
// T = Tekrarlayan (3 kez)
