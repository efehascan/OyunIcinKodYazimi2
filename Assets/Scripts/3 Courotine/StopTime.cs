using UnityEngine;
using UnityEngine.InputSystem;

public class StopTime : MonoBehaviour
{

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.sKey.wasPressedThisFrame)
        {
            Time.timeScale = 0f; // Zamanı durdur
            Debug.Log("Zaman durduruldu!");
        }
        
        if (Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
        {
            Time.timeScale = 1f; // Zamanı yeniden başlat
            Debug.Log("Zaman yeniden başlatıldı!");
        }
    }
}