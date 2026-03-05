# OBJECT POOL PATTERN

## Nedir?

Object Pool, sürekli oluşturup yok edilen nesneleri **geri dönüştürerek** performans kazandıran bir tasarım desenidir.

## Neden Kullanırız?

### Sorun:
```csharp
// Kötü: Sürekli Instantiate/Destroy
void Shoot()
{
    GameObject bullet = Instantiate(bulletPrefab);  // YAVAŞ!
    Destroy(bullet, 2f);                            // YAVAŞ!
}
```
- Her mermi için bellek tahsisi (allocation)
- Garbage Collector çalışır (donmalar)
- CPU'ya yük

### Çözüm: Object Pool
```csharp
// İyi: Pool'dan al, geri ver
void Shoot()
{
    GameObject bullet = pool.Get();      // HIZLI!
    // Kullan...
    pool.Return(bullet);                 // HIZLI!
}
```

## Nasıl Çalışır?

```
1. BAŞLANGIÇ: 10 mermi oluştur
   [●][●][●][●][●][●][●][●][●][●]
   
2. ATEŞ ET: Pool'dan al
   [○][●][●][●][●][●][●][●][●][●]  → Mermi 1 kullanımda
   
3. MERMİ BİTTİ: Pool'a geri ver
   [●][●][●][●][●][●][●][●][●][●]  → Tekrar kullanıma hazır
```

## İki Yöntem

### 1. Pattern Method (Global Pool)
**Klasör:** `1 Pattern Method`

Merkezi pool yöneticisi ile çalışır:
```csharp
// PoolManager.cs - Global pool
// BulletSpawner.cs - Pool'u kullanır
// Bullet.cs - Mermi davranışı

PoolManager.Instance.CreatePool(bulletPrefab, 10);
GameObject bullet = PoolManager.Instance.Get("Bullet");
PoolManager.Instance.Return(bullet);
```

**Avantajlar:**
- Tek merkezi pool
- Her yerden erişim
- Pool yönetimi ayrı

**Kullanım:**
- Mermi sistemleri
- Efektler
- Ses objeleri

### 2. Script Method (Local Pool)
**Klasör:** `2 Script Method`

Script içinde kendi pool'unu yönetir:
```csharp
// BulletSpawnerWithPool.cs - Her şey tek scriptte

Queue<GameObject> pool;
GameObject bullet = GetBullet();
ReturnBullet(bullet);
```

**Avantajlar:**
- Bağımsız çalışır
- Kolay kurulum
- Tek dosyada tüm kontrol

**Kullanım:**
- Düşman spawn
- Özel sistemler
- Prototipleme

## Test Sistemi

### Pattern Method (Global):
1. Boş GameObject → "PoolManager"
2. **PoolManager.cs** ekle
3. Başka GameObject → "BulletSpawner"
4. **BulletSpawner.cs** ekle
5. Play → **Space** ile ateş

### Script Method (Local):
1. Boş GameObject → "LocalSpawner"
2. **BulletSpawnerWithPool.cs** ekle
3. Play → **Space** ile ateş

## Mermi Sistemi Detayları

Her iki yöntemde de:
- **Spawn:** (0, 0, 0)
- **Hareket:** Sağa doğru (Vector3.right)
- **Hız:** 5 birim/saniye
- **Yaşam süresi:** 5 saniye
- **Pool boyutu:** 10 mermi
- **Kontrol:** Space tuşu

```
  Pool        →  Spawn    →   Hareket    →   Pool'a Dön
[●●●●●●●●●●]     (0,0,0)      →→→→→         [●●●●●●●●●●]
                              5 saniye
```

## Performans Karşılaştırma

```
Instantiate/Destroy: 100 mermi → 50ms
Object Pool:         100 mermi → 2ms

25x DAHA HIZLI!
```

## Ne Zaman Kullanılır?

**Kullan:**
- Mermi/roket sistemleri
- Parçacık efektleri
- Düşman spawn
- UI elementleri (skor popup)
- Ses objeleri

**Kullanma:**
- Tek seferlik objeler (Boss)
- Az sayıda obje (<5 adet)
- Uzun süre aktif objeler

## Karşılaştırma

| Özellik | Pattern Method | Script Method |
|---------|----------------|---------------|
| **Yapı** | Merkezi pool | Lokal pool |
| **Erişim** | Global | Tek script |
| **Dosya** | 3 ayrı script | 1 script |
| **Kullanım** | Karmaşık | Basit |
| **İdeal** | Büyük projeler | Küçük sistemler |
| **Kod** | `PoolManager.Instance` | `GetBullet()` |

## Özet

1. **Object Pool** = Nesneleri geri dönüştür
2. **2 Yöntem** = Global (Pattern) / Lokal (Script)
3. **Pattern** = Merkezi kontrol, çoklu kullanım
4. **Script** = Bağımsız, hızlı kurulum
5. **Kullan** = Mermi, efekt, düşman, UI

---

**Not:** Mobil oyunlarda mutlaka kullanılmalı!
