# PlayerPrefs

## Nedir?

PlayerPrefs, Unity'nin sunduqu basit bir **veri kaydetme sistemidir**.  
Oyuncu oyunu kapatip tekrar actiqinda verilerin kaybolmamasini saglar.

Verileri **cihazin diskine** yazar. Windows'ta Registry'ye, Android'de SharedPreferences'a kaydeder.  
Yani oyun kapansa bile veriler durur.

Sadece 3 veri tipi destekler:
- **int** -- tam sayi (skor, seviye, para)
- **float** -- ondalikli sayi (ses seviyesi, hassasiyet)
- **string** -- metin (oyuncu adi)

---

## Ne Zaman Kullanilir?

- En yuksek skor kaydetmek
- Ses ve muzik seviyesini hatirlamak
- Oyuncunun kaldigi seviyeyi saklamak
- Oyuncu adini tutmak
- Ayarlar (dil, grafik, kontrol tercihleri)

---

## Ne Zaman Kullanilmaz?

- Buyuk ve karmasik veriler (envanter, save sistemi) -- bunun icin JSON veya dosya yazma tercih edilir.
- Guvenliq gerektiren veriler -- PlayerPrefs sifreli degildir, kolayca duzenlenebilir.

---

## Temel Metodlar

### Veri Yazma

```csharp
PlayerPrefs.SetInt("HighScore", 500);
PlayerPrefs.SetFloat("MusicVolume", 0.8f);
PlayerPrefs.SetString("PlayerName", "Ali");
PlayerPrefs.Save(); // Diske yaz (onemli!)
```

### Veri Okuma

```csharp
int skor = PlayerPrefs.GetInt("HighScore", 0);         // key yoksa 0 doner
float ses = PlayerPrefs.GetFloat("MusicVolume", 1f);    // key yoksa 1 doner
string ad = PlayerPrefs.GetString("PlayerName", "Oyuncu"); // key yoksa "Oyuncu" doner
```

Ikinci parametre **varsayilan degerdir**. Key daha once kaydedilmemisse bu deger doner.

### Kontrol ve Silme

```csharp
PlayerPrefs.HasKey("HighScore");   // Bu key var mi? true/false
PlayerPrefs.DeleteKey("HighScore"); // Tek bir key'i sil
PlayerPrefs.DeleteAll();            // Tum verileri sil
```

---

## Dikkat Edilecekler

- `PlayerPrefs.Save()` cagrilmazsa veri kaybolabilir. Onemli anlarda mutlaka cagir.
- Key isimleri buyuk-kucuk harfe duyarlıdir. `"Score"` ile `"score"` farkli key'lerdir.
- Sadece int, float, string desteklenir. bool kaydetmek icin int kullan (0 = false, 1 = true).
- Guvenliq yoktur. Oyuncu degerlerle oynayabilir. Rekabetci oyunlarda tek basina guvenilmez.

---

## Klasordeki Dosyalar

| Dosya | Ne Yapar |
|---|---|
| ScoreManager.cs | En yuksek skoru kaydedip okuyan ornek |
| SettingsManager.cs | Ses seviyesi ve oyuncu adi kaydeden ayar ornegi |

### Test Etmek Icin

1. Sahneye bos bir GameObject koy
2. ScoreManager veya SettingsManager scriptini ekle
3. Play'e bas -- Console'da kayit ve okuma sonuclarini gor
4. Oyunu durdur, tekrar baslat -- veriler hala duruyor
