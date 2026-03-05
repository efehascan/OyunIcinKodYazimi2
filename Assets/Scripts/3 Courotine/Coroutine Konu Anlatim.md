# COROUTINE (EŞYORDAMLAR)

## Coroutine Nedir?

**Coroutine**, Unity'de **zamana yayılmış işlemleri** gerçekleştirmek için kullanılan özel fonksiyonlardır. Normal fonksiyonlardan farklı olarak, işlem tamamlanana kadar **bekleyebilir** ve ardından kaldığı yerden devam edebilir.

**Temel Fark:**
- **Normal Fonksiyon:** Baştan sona tek seferde çalışır
- **Coroutine:** Duraklatılabilir, bekleyebilir, devam edebilir

---

## Ne İşe Yarar?

**Kullanım Alanları:**
- Zamanlayıcılar (countdown, timer)
- Animasyonlar (fade, lerp, smooth movement)
- Bekleme işlemleri (delay, cooldown)
- Tekrar eden işlemler (spawn, respawn)
- Asenkron işlemler (loading, downloading)

---

## Temel Yapı

### Basit Örnek:

```csharp
IEnumerator MyCoroutine()
{
    Debug.Log("Başladı");
    
    yield return new WaitForSeconds(2f);  // 2 saniye bekle
    
    Debug.Log("2 saniye sonra");
    
    yield return new WaitForSeconds(1f);  // 1 saniye daha bekle
    
    Debug.Log("Bitti!");
}

// Başlatma:
void Start()
{
    StartCoroutine(MyCoroutine());
}
```

**Çıktı:**
```
Başladı
(2 saniye bekler)
2 saniye sonra
(1 saniye bekler)
Bitti!
```

---

## Önemli Noktalar

### 1. Dönüş Tipi: `IEnumerator`
```csharp
IEnumerator MyCoroutine()  // Doğru
{
    yield return null;
}

void MyCoroutine()  // Yanlış - Coroutine değil
{
    // ...
}
```

### 2. `yield return` Kullanımı
```csharp
// Bir sonraki frame'e kadar bekle
yield return null;

// Belirli süre bekle
yield return new WaitForSeconds(2f);

// Fizik güncellemesine kadar bekle
yield return new WaitForFixedUpdate();

// Frame sonuna kadar bekle
yield return new WaitForEndOfFrame();

// Başka bir coroutine bitene kadar bekle
yield return StartCoroutine(OtherCoroutine());
```

### 3. Başlatma ve Durdurma
```csharp
// Başlatma
Coroutine myCoroutine = StartCoroutine(MyCoroutine());

// Durdurma
StopCoroutine(myCoroutine);

// Veya
StopCoroutine("MyCoroutine");

// Tüm coroutine'leri durdur
StopAllCoroutines();
```

---

## Pratik Örnekler

### Örnek 1: Geri Sayım
```csharp
IEnumerator Countdown(int seconds)
{
    for (int i = seconds; i > 0; i--)
    {
        Debug.Log(i);
        yield return new WaitForSeconds(1f);
    }
    Debug.Log("BAŞLA!");
}

// Kullanım
StartCoroutine(Countdown(3));
// Çıktı: 3... 2... 1... BAŞLA!
```

---

### Örnek 2: Fade Efekti
```csharp
IEnumerator FadeOut(CanvasGroup group, float duration)
{
    float elapsedTime = 0f;
    float startAlpha = group.alpha;
    
    while (elapsedTime < duration)
    {
        elapsedTime += Time.deltaTime;
        group.alpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / duration);
        yield return null;  // Her frame'de güncelle
    }
    
    group.alpha = 0f;
}

// Kullanım
StartCoroutine(FadeOut(canvasGroup, 2f));
```

---

### Örnek 3: Tekrarlayan Spawn
```csharp
IEnumerator SpawnEnemies()
{
    while (true)  // Sonsuz döngü
    {
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        yield return new WaitForSeconds(3f);  // 3 saniyede bir
    }
}

// Başlatma
StartCoroutine(SpawnEnemies());

// İhtiyaç olunca durdurma
StopCoroutine(SpawnEnemies());
```

---

### Örnek 4: Smooth Hareket
```csharp
IEnumerator MoveToPosition(Transform obj, Vector3 target, float duration)
{
    Vector3 startPos = obj.position;
    float elapsedTime = 0f;
    
    while (elapsedTime < duration)
    {
        elapsedTime += Time.deltaTime;
        float t = elapsedTime / duration;
        obj.position = Vector3.Lerp(startPos, target, t);
        yield return null;
    }
    
    obj.position = target;  // Tam hedefi ayarla
}

// Kullanım
StartCoroutine(MoveToPosition(player.transform, targetPos, 2f));
```

---

## Dikkat Edilmesi Gerekenler

### 1. GameObject Aktif Olmalı
```csharp
void Start()
{
    StartCoroutine(MyCoroutine());
    gameObject.SetActive(false);  // Coroutine durur!
}
```

### 2. Component Aktif Olmalı
```csharp
void Start()
{
    StartCoroutine(MyCoroutine());
    enabled = false;  // Coroutine durur!
}
```

### 3. Destroy Edilince Durur
```csharp
void Start()
{
    StartCoroutine(MyCoroutine());
    Destroy(gameObject);  // Coroutine durur!
}
```

### 4. Sonsuz Döngü Riski
```csharp
// YANLIŞ - Oyunu donduracak!
IEnumerator BadCoroutine()
{
    while (true)
    {
        Debug.Log("Çalışıyor");
        // yield return yok!
    }
}

// DOĞRU
IEnumerator GoodCoroutine()
{
    while (true)
    {
        Debug.Log("Çalışıyor");
        yield return null;  // Her frame bekle
    }
}
```

---

## 🆚 Coroutine vs Diğer Yöntemler

### Update vs Coroutine

**Update:**
```csharp
float timer = 0f;

void Update()
{
    timer += Time.deltaTime;
    if (timer >= 2f)
    {
        DoSomething();
        timer = 0f;
    }
}
```

**Coroutine:**
```csharp
void Start()
{
    StartCoroutine(TimerCoroutine());
}

IEnumerator TimerCoroutine()
{
    while (true)
    {
        yield return new WaitForSeconds(2f);
        DoSomething();
    }
}
```

**Coroutine Avantajları:**
- Daha temiz kod
- Daha okunabilir
- Timer değişkenine gerek yok

---

### Invoke vs Coroutine

**Invoke:**
```csharp
void Start()
{
    Invoke("DoSomething", 2f);  // 2 saniye sonra bir kere
    InvokeRepeating("DoSomething", 0f, 2f);  // Her 2 saniyede
}
```

**Coroutine:**
```csharp
IEnumerator DoSomethingCoroutine()
{
    yield return new WaitForSeconds(2f);
    DoSomething();
}
```

**Coroutine Avantajları:**
- Parametre gönderilebilir
- Durdurmak daha kolay
- Daha esnek kontrol

---

## WaitFor Türleri

```csharp
// Saniye bazlı bekleme
yield return new WaitForSeconds(2f);

// Scaled time görmezden gelir (pause'da çalışır)
yield return new WaitForSecondsRealtime(2f);

// Bir frame bekle
yield return null;

// Fizik güncellemesini bekle
yield return new WaitForFixedUpdate();

// Frame sonunu bekle
yield return new WaitForEndOfFrame();

// Koşul sağlanana kadar bekle
yield return new WaitUntil(() => health <= 0);

// Koşul false olana kadar bekle
yield return new WaitWhile(() => isMoving);
```

---

## İpuçları

### 1. Coroutine Referansı Sakla
```csharp
private Coroutine fadeCoroutine;

void StartFading()
{
    if (fadeCoroutine != null)
        StopCoroutine(fadeCoroutine);
    
    fadeCoroutine = StartCoroutine(FadeOut());
}
```

### 2. Null Check
```csharp
IEnumerator MyCoroutine()
{
    if (transform == null)  // Destroyed mi kontrol et
        yield break;  // Coroutine'i sonlandır
    
    // İşlemler...
}
```

### 3. Parametre Gönderme
```csharp
IEnumerator FlashColor(Color color, float duration)
{
    renderer.material.color = color;
    yield return new WaitForSeconds(duration);
    renderer.material.color = Color.white;
}

// Kullanım
StartCoroutine(FlashColor(Color.red, 0.5f));
```

---

## Özet

| Özellik | Açıklama |
|---------|----------|
| **Dönüş Tipi** | `IEnumerator` |
| **Başlatma** | `StartCoroutine()` |
| **Durdurma** | `StopCoroutine()` |
| **Ana Kullanım** | `yield return` |
| **Avantaj** | Zamana yayılmış işlemler |
| **Dezavantaj** | GameObject aktif olmalı |

---

## Ne Zaman Kullanılır?

**UYGUN:**
- Zamanlayıcılar, geri sayımlar
- Animasyonlar, geçişler
- Cooldown sistemleri
- Periyodik işlemler
- Loading ekranları

**UYGUN DEĞİL:**
- Sürekli input kontrolü (Update kullan)
- Her frame çalışması gereken fizik (FixedUpdate kullan)
- Basit hesaplamalar (normal fonksiyon yeterli)

---

## Sonuç

**Coroutine = Zamana yayılmış işlemler için ideal çözüm**

- Temiz ve okunabilir kod  
- Bekleme işlemleri için mükemmel  
- Animasyon ve geçişler için ideal  
- GameObject aktif olmalı  
- Sonsuz döngülerde `yield return` unutma

---

**Örnekler:** TimerExample.cs, FadeExample.cs, SpawnExample.cs
