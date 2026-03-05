using UnityEngine;
using System.Collections;

namespace PatternMethod
{
    /// <summary>
    /// Mermi hareketi ve yaşam süresi
    /// </summary>
    public class Bullet : MonoBehaviour
    {
        public float speed = 5f;
        public float lifetime = 5f;
        
        private Coroutine lifeCoroutine;
        
        void OnEnable()
        {
            // Aktif olunca yaşam süresini başlat
            if (lifeCoroutine != null)
                StopCoroutine(lifeCoroutine);
            
            lifeCoroutine = StartCoroutine(LifetimeCoroutine());
        }
        
        void Update()
        {
            // Sağa doğru hareket
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        
        IEnumerator LifetimeCoroutine()
        {
            Debug.Log($"Mermi harekete geçti: {transform.position}");
            
            yield return new WaitForSeconds(lifetime);
            
            Debug.Log($"Mermi yaşam süresi doldu: {transform.position}");
            
            // Pool'a geri dön
            PoolManager.Instance.Return(gameObject);
        }
    }
}
