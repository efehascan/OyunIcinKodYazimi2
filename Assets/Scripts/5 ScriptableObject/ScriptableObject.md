# ScriptableObject

## Nedir?

ScriptableObject, Unity'nin sunduqu ozel bir siniftir.  
Normal scriptler (MonoBehaviour) bir GameObject'e eklenerek calisir.  
ScriptableObject ise **hicbir GameObject'e eklenmez**. Project klasorunde `.asset` uzantili bir dosya olarak durur.

Tek amaci **veri tutmaktir**. Hareket etmez, carpisma algılamaz, render yapmaz.  
Sadece icindeki degerleri saklar ve diger scriptlerin okumasina izin verir.

---

## Neden Gerekli?

Diyelim oyununda 3 farkli dusman var: Goblin, Iskelet, Ejderha.  
Her birinin adi, cani ve hizi farkli.

**ScriptableObject olmadan:**  
Her dusman scriptine ayri ayri deger yazarsin. 50 dusman varsa 50 yerde ayni kodu tekrarlarsin.  
Bir degeri degistirmek istersen tek tek bulup duzenlemen gerekir.

**ScriptableObject ile:**  
Her dusman icin bir `.asset` dosyasi olusturursun (Goblin.asset, Iskelet.asset, Ejderha.asset).  
Deger degistirmek istersen sadece ilgili asset'i ac, Inspector'dan duzenle. Kod'a dokunmazsin.

---

## MonoBehaviour ile Farki

| | MonoBehaviour | ScriptableObject |
|---|---|---|
| Nereye eklenir? | GameObject'e | Hicbir yere (asset dosyasi) |
| Update/Start var mi? | Evet | Hayir |
| Sahne degisince ne olur? | Sahneyle birlikte yok olabilir | Etkilenmez, kalici |
| Ne ise yarar? | Davranis (hareket, atis, carpisma) | Veri (isim, can, hiz, hasar) |

Kisa ozet: **MonoBehaviour = davranis**, **ScriptableObject = veri**.

---

## 3 Adimda Kullanim

### Adim 1 -- ScriptableObject sinifini yaz

```csharp
[CreateAssetMenu(menuName = "Veriler/Karakter")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public int health = 100;
    public float speed = 5f;
}
```

### Adim 2 -- Unity Editor'da asset olustur

Project paneli > Sag tik > Create > Veriler > Karakter  
Olusturulan `.asset` dosyasini Inspector'dan doldur.

### Adim 3 -- Baska bir scriptte kullan

```csharp
public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private CharacterData data;

    private void Start()
    {
        Debug.Log(data.characterName + " - Can: " + data.health);
    }
}
```

Inspector'dan `data` alanina asset'i surukle-birak. Oyunu baslat.

---

## Dikkat Edilecekler

- ScriptableObject'te `Update()` ve `Start()` calismaz.
- Play Mode'da degistirilen degerler geri donmez, kalici olur.
- Birden fazla nesne ayni asset'i paylasabilir (bellek tasarrufu).

---

## Klasordeki Dosyalar

| Dosya | Ne Yapar |
|---|---|
| CharacterData.cs | Karakter verisi tutan ScriptableObject |
| PlayerCharacter.cs | CharacterData'yi okuyup WASD hareket yapan script |

### Test Etmek Icin

1. Create > Veriler > Karakter ile asset olustur, Inspector'dan doldur
2. Sahneye bir Cube koy
3. Cube'e PlayerCharacter scriptini ekle
4. Inspector'dan asset'i data alanina surukle
5. Play'e bas -- Cube WASD ile hareket eder
