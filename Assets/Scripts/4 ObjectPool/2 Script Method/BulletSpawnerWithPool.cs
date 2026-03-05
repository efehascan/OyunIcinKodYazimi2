using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

namespace ScriptMethod
{
    /// <summary>
    /// YÖNTEM 2: Script içinde kendi pool'unu yöneten spawner
    /// Tüm pool işlemleri bu script içinde
    /// </summary>
    public class BulletSpawnerWithPool : MonoBehaviour
    {
        [Header("Mermi Ayarları")]
        public GameObject bulletPrefab;
        public int poolSize = 10;
        public float bulletSpeed = 5f;
        public float bulletLifetime = 5f;
        
        [Header("Spawn Ayarları")]
        public Vector3 spawnPosition = Vector3.zero;
        
        // Lokal pool
        private Queue<GameObject> pool = new Queue<GameObject>();
        private int shotCount = 0;
        
        void Start()
        {
            // Prefab yoksa oluştur
            if (bulletPrefab == null)
            {
                bulletPrefab = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                bulletPrefab.transform.localScale = Vector3.one * 0.3f;
                bulletPrefab.SetActive(false);
                bulletPrefab.name = "LocalBullet";
                
                Debug.Log("Mermi prefab otomatik oluşturuldu");
            }
            
            // Pool'u oluştur
            CreatePool();
            
            Debug.Log("=== SCRIPT METHOD TEST ===");
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
        
        /// <summary>
        /// Başlangıçta pool oluştur
        /// </summary>
        void CreatePool()
        {
            for (int i = 0; i < poolSize; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform);
                bullet.SetActive(false);
                pool.Enqueue(bullet);
            }
            
            Debug.Log($"Lokal pool oluşturuldu: {poolSize} mermi");
        }
        
        /// <summary>
        /// Pool'dan mermi al
        /// </summary>
        GameObject GetBullet()
        {
            if (pool.Count > 0)
            {
                GameObject bullet = pool.Dequeue();
                bullet.SetActive(true);
                Debug.Log($"Pool'dan alındı (Kalan: {pool.Count})");
                return bullet;
            }
            
            Debug.LogWarning("Pool boş!");
            return null;
        }
        
        /// <summary>
        /// Pool'a geri ver
        /// </summary>
        void ReturnBullet(GameObject bullet)
        {
            bullet.SetActive(false);
            bullet.transform.SetParent(transform);
            pool.Enqueue(bullet);
            Debug.Log($"Pool'a döndü (Toplam: {pool.Count})");
        }
        
        /// <summary>
        /// Ateş et
        /// </summary>
        void Shoot()
        {
            GameObject bullet = GetBullet();
            
            if (bullet != null)
            {
                shotCount++;
                bullet.transform.position = spawnPosition;
                Debug.Log($"ATEŞ #{shotCount} - Pozisyon: {spawnPosition}");
                
                // Mermi hareketini başlat
                StartCoroutine(BulletBehavior(bullet));
            }
            else
            {
                Debug.LogWarning("Pool boş! Mermi ateşlenemedi.");
            }
        }
        
        /// <summary>
        /// Mermi hareketi ve yaşam süresi
        /// </summary>
        IEnumerator BulletBehavior(GameObject bullet)
        {
            Debug.Log($"Mermi harekete geçti: {bullet.transform.position}");
            
            float elapsed = 0f;
            
            while (elapsed < bulletLifetime)
            {
                if (bullet.activeInHierarchy)
                {
                    // Sağa doğru hareket
                    bullet.transform.position += Vector3.right * bulletSpeed * Time.deltaTime;
                }
                
                elapsed += Time.deltaTime;
                yield return null;
            }
            
            Debug.Log($"Mermi yaşam süresi doldu: {bullet.transform.position}");
            
            // Pool'a geri dön
            ReturnBullet(bullet);
        }
    }
}

// Space tuşu = Ateş et
// Mermi (0,0,0)'dan başlar, sağa gider, 5 saniye sonra pool'a döner
// Tüm pool yönetimi bu script içinde
