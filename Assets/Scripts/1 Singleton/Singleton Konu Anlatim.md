# SINGLETON PATTERN

## Singleton Nedir?

**Singleton Pattern**, bir sınıftan **sadece bir örnek** oluşturulmasını garanti eden ve bu örneğe her yerden erişim sağlayan tasarım desenidir.

**Amaç:**
- Sınıftan sadece **tek bir instance** olması
- Global erişim sağlama
- Sahne geçişlerinde veriyi koruma

---

## Temel Yapı

```csharp
public class GameManager : MonoBehaviour
{
    // Static instance - global erişim
    public static GameManager Instance { get; private set; }
    
    private void Awake()
    {
        // Eğer başka instance varsa yok et
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);  // Sahne değişiminde kalıcı
    }
}
```

**Önemli Noktalar:**
1. `static` property - sınıf seviyesinde erişim
2. `private set` - sadece kendi içinden atanabilir
3. Duplicate control - ikinci instance'ı yok et
4. `DontDestroyOnLoad` - sahne geçişlerinde korur

---

## Kullanım

```csharp
// Herhangi bir yerden erişim
GameManager.Instance.AddScore(100);
AudioManager.Instance.PlayMusic(clip);
UIManager.Instance.ShowMainMenu();
```

---

## Ne Zaman Kullanılır?

**UYGUN:**
- GameManager (oyun durumu, skor)
- AudioManager (müzik, ses efektleri)
- UIManager (menüler, HUD)

**UYGUN DEĞİL:**
- Player (birden fazla oyuncu olabilir)
- Enemy (çok sayıda düşman olabilir)
- Bullet (çok sayıda mermi olabilir)

---

## Dikkat Edilmesi Gerekenler

### Null Check Yap
```csharp
// İYİ
if (GameManager.Instance != null)
{
    GameManager.Instance.AddScore(10);
}

// DAHA İYİ
GameManager.Instance?.AddScore(10);
```

### Her Şeyi Singleton Yapma
```csharp
// YANLIŞ
public class Player : Singleton<Player> { }
public class Enemy : Singleton<Enemy> { }

// Oyunda birden fazla player/enemy olabilir!
```

---

## Avantajlar vs Dezavantajlar

### Avantajlar
- Global erişim - her yerden kolayca erişilebilir
- Tek instance garantisi - memory verimli
- Sahne geçişlerinde kalıcı

### Dezavantajlar
- Test edilmesi zor
- Kod bağımlılığı artabilir
- Aşırı kullanım kodu karmaşıklaştırır

---

## Özet

**Singleton Pattern = Tek Instance + Global Erişim**

- Sadece gerçekten tek olması gereken sistemler için kullan  
- Null check yapmayı unutma  
- Her şeyi singleton yapma

---

**Örnekler:** GameManager.cs, AudioManager.cs, UIManager.cs
