using UnityEngine;
using UnityEngine.InputSystem;

namespace PatternMethod
{
    /// <summary>
    /// Pool'dan mermi alıp ateş eden spawner
    /// PoolManager'a pool boyutunu bildirir
    /// </summary>
    public class BulletSpawner : MonoBehaviour
    {
        [Header("Mermi Ayarları")]
        public GameObject bulletPrefab;
        public int poolSize = 10;
        
        [Header("Spawn Ayarları")]
        public Vector3 spawnPosition = Vector3.zero;
        
        private int shotCount = 0;
        
        void Start()
        {
            // Prefab yoksa oluştur
            if (bulletPrefab == null)
            {
                bulletPrefab = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                bulletPrefab.transform.localScale = Vector3.one * 0.3f;
                bulletPrefab.AddComponent<Bullet>();
                bulletPrefab.SetActive(false);
                bulletPrefab.name = "Bullet";
                
                Debug.Log("Mermi prefab otomatik oluşturuldu");
            }
            
            // PoolManager'a pool boyutunu bildir
            PoolManager.Instance.CreatePool(bulletPrefab, poolSize);
            
            Debug.Log("=== PATTERN METHOD TEST ===");
            Debug.Log("Space = Ateş et");
            Debug.Log($"Pool boyutu: {poolSize}");
        }
        
        void Update()
        {
            if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                Shoot();
            }
        }
        
        void Shoot()
        {
            // Pool'dan mermi al
            GameObject bullet = PoolManager.Instance.Get(bulletPrefab.name);
            
            if (bullet != null)
            {
                shotCount++;
                bullet.transform.position = spawnPosition;
                Debug.Log($"ATEŞ #{shotCount} - Pozisyon: {spawnPosition}");
            }
            else
            {
                Debug.LogWarning("Pool boş! Mermi ateşlenemedi.");
            }
        }
    }
}

// Space tuşu = Ateş et
// Mermi (0,0,0)'dan başlar, sağa gider, 5 saniye sonra pool'a döner
