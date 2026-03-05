using UnityEngine;
using System.Collections.Generic;

namespace PatternMethod
{
    /// <summary>
    /// YÖNTEM 1: Global Pool Manager
    /// Tüm pool işlemlerini merkezi olarak yönetir
    /// </summary>
    public class PoolManager : MonoBehaviour
    {
        public static PoolManager Instance;
        
        private Dictionary<string, Queue<GameObject>> pools = new Dictionary<string, Queue<GameObject>>();
        
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        /// <summary>
        /// Başlangıçta pool oluştur
        /// </summary>
        public void CreatePool(GameObject prefab, int size)
        {
            string key = prefab.name;
            
            if (pools.ContainsKey(key))
            {
                Debug.Log($"Pool zaten var: {key}");
                return;
            }
            
            pools[key] = new Queue<GameObject>();
            
            for (int i = 0; i < size; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.name = key;
                obj.SetActive(false);
                obj.transform.SetParent(transform);
                pools[key].Enqueue(obj);
            }
            
            Debug.Log($"Pool oluşturuldu: {key} x{size}");
        }
        
        /// <summary>
        /// Pool'dan obje al
        /// </summary>
        public GameObject Get(string prefabName)
        {
            if (!pools.ContainsKey(prefabName) || pools[prefabName].Count == 0)
            {
                Debug.LogWarning($"Pool boş: {prefabName}");
                return null;
            }
            
            GameObject obj = pools[prefabName].Dequeue();
            obj.SetActive(true);
            Debug.Log($"Pool'dan alındı: {prefabName} (Kalan: {pools[prefabName].Count})");
            return obj;
        }
        
        /// <summary>
        /// Pool'a geri ver
        /// </summary>
        public void Return(GameObject obj)
        {
            string key = obj.name;
            
            obj.SetActive(false);
            obj.transform.SetParent(transform);
            
            if (!pools.ContainsKey(key))
            {
                pools[key] = new Queue<GameObject>();
            }
            
            pools[key].Enqueue(obj);
            Debug.Log($"Pool'a döndü: {key} (Toplam: {pools[key].Count})");
        }
    }
}
