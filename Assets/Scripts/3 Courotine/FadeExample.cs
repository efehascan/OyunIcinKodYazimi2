using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

namespace CoroutineExamples
{
    public class FadeExample : MonoBehaviour
    {
        void Update()
        {
            if (Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame)
            {
                StartCoroutine(FadeCoroutine());
            }
            
            if (Keyboard.current != null && Keyboard.current.mKey.wasPressedThisFrame)
            {
                StartCoroutine(MoveCoroutine());
            }
        }
        
        // Fade: 0 → 1 (görünür hale gel)
        IEnumerator FadeCoroutine()
        {
            Debug.Log("Fade başladı");
            
            float alpha = 0f;
            
            while (alpha < 1f)
            {
                alpha += Time.deltaTime;
                Debug.Log("Alpha: " + alpha);
                yield return null;
            }
            
            Debug.Log("Fade tamamlandı!");
        }
        
        // Hareket: Lerp ile yumuşak geçiş
        IEnumerator MoveCoroutine()
        {
            Debug.Log("Hareket başladı");
            
            Vector3 baslangic = Vector3.zero;
            Vector3 hedef = new Vector3(5f, 0f, 0f);
            float sure = 2f;
            float gecen = 0f;
            
            while (gecen < sure)
            {
                gecen += Time.deltaTime;
                float t = gecen / sure;
                Vector3 mevcut = Vector3.Lerp(baslangic, hedef, t);
                Debug.Log("Pozisyon: " + mevcut);
                yield return null;
            }
            
            Debug.Log("Hedefe ulaşıldı!");
        }
    }
}

// F = Fade (0→1)
// M = Hareket (Lerp)
