# YAPILAN ÖRNEK HAKKINDA - SOLID PRENSİPLERİ UYGULAMASI

## Genel Bakış
Bu projede, Unity oyun geliştirme ortamında SOLID prensiplerinin nasıl uygulanacağını gösteren bir savaş/hasar sistemi örneği oluşturulmuştur.

## Sistem Mimarisi

### **Klasör Yapısı**
```
2 SOLID/
├── Interfaces/     → Soyutlamalar (Interface'ler)
├── Core/          → Ana oyun nesneleri (Player, Enemy, Health)
├── Attacks/       → Saldırı tipleri (SwordAttack, BowAttack)
├── Systems/       → Yardımcı sistemler (DamageCalculator)
└── Managers/      → Oyun yöneticileri (GameManager)
```

---

## SOLID Prensipleri Uygulaması

### **1. Single Responsibility Principle (Tek Sorumluluk Prensibi)**

Her sınıf yalnızca **bir sorumluluğa** sahiptir ve **bir değişme sebebi** vardır.

#### **Örnekler:**

**`Health.cs`** - Sadece can yönetiminden sorumlu
```csharp
- Can azaltma (TakeDamage)
- Ölüm kontrolü (IsDead)
```

**`DamageCalculator.cs`** - Sadece hasar hesaplamasından sorumlu
```csharp
- Saldırı tipine göre hasar hesaplama
```

**`GameManager.cs`** - Sadece oyun yönetiminden sorumlu
```csharp
- Skor takibi
- Singleton pattern ile tek instance yönetimi
```

**`Enemy.cs`** - Sadece düşman davranışlarından sorumlu
```csharp
- Hasar alma
- Ölüm durumunda skor verme
```

#### **Anti-Pattern (Kötü Örnek):**
Eğer Health sınıfı içinde skor yönetimi, düşman AI'ı ve hasar hesaplama olsaydı, SRP ihlal edilirdi.

---

### **2. Open/Closed Principle (Açık/Kapalı Prensibi)**

Sınıflar **genişletmeye açık**, **değişikliğe kapalı** olmalıdır.

#### **Uygulama:**

**`IAttack` Interface'i:**
```csharp
public interface IAttack
{
    int BaseDamage { get; }
}
```

**Mevcut Saldırı Türleri:**
- `SwordAttack.cs` → BaseDamage: 10
- `BowAttack.cs` → BaseDamage: 5

#### **Genişletilebilirlik:**
Yeni saldırı türü eklemek için **mevcut kodu değiştirmeye gerek yok**:
```csharp
public class MagicAttack : IAttack
{
    public int BaseDamage => 15;
}

public class AxeAttack : IAttack
{
    public int BaseDamage => 20;
}
```

`DamageCalculator` ve `Player` sınıflarında **hiçbir değişiklik gerekmez**!

---

### **3. Liskov Substitution Principle (Liskov'un Yerine Geçme Prensibi)**

Alt sınıflar, üst sınıfların yerine kullanılabilir olmalıdır.

#### **Uygulama:**

**`Player.cs` içinde:**
```csharp
private IAttack attackType;

void Start()
{
    attackType = new SwordAttack();  // SwordAttack kullanılabilir
    // veya
    attackType = new BowAttack();    // BowAttack kullanılabilir
}

void PerformAttack()
{
    int damage = damageCalculator.CalculateDamage(attackType);
    // IAttack tipindeki herhangi bir sınıf kullanılabilir
}
```

**`DamageCalculator.cs` içinde:**
```csharp
public int CalculateDamage(IAttack attack)
{
    // SwordAttack, BowAttack veya IAttack'ı implement eden
    // herhangi bir sınıf burada sorunsuz çalışır
    return attack.BaseDamage;
}
```

#### **Anahtar Nokta:**
`IAttack` interface'ini implement eden her sınıf, birbirinin yerine **sorunsuzca** kullanılabilir.

---

### **4. Interface Segregation Principle (Arayüz Ayrımı Prensibi)**

Sınıflar, kullanmadıkları metodları içeren interface'lere bağımlı olmamalıdır.

#### **Doğru Yaklaşım - Küçük ve Spesifik Interface'ler:**

**`IAttack.cs`** - Sadece saldırı için
```csharp
public interface IAttack
{
    int BaseDamage { get; }
}
```

**`IDamageable.cs`** - Sadece hasar alma için
```csharp
public interface IDamageable
{
    void ReceiveDamage(int damage);
}
```

#### **Yanlış Yaklaşım (Projede hatalı örnek olarak gösterilmiş):**

**`ICharacter.cs`** - Çok fazla sorumluluk
```csharp
public interface ICharacter
{
    void Attack();
    void Heal();
    void Fly();     // Her karakter uçamaz!
}
```

**Neden Hatalı?**
- Bir `Warrior` sınıfı `Fly()` metodunu kullanmayacaktır
- Bir `Archer` uçamaz
- Bu interface'i implement eden her sınıf, kullanmadığı metodu implement etmek zorunda kalır

#### **Doğru Çözüm:**
```csharp
public interface IAttackable { void Attack(); }
public interface IHealable { void Heal(); }
public interface IFlyable { void Fly(); }

// Kullanım:
public class Warrior : IAttackable, IHealable { }
public class Dragon : IAttackable, IFlyable { }
```

---

### **5. Dependency Inversion Principle (Bağımlılığı Tersine Çevirme Prensibi)**

Üst seviye modüller, alt seviye modüllere bağımlı olmamalı. Her ikisi de **soyutlamalara** bağımlı olmalıdır.

#### **Uygulama:**

**`Player.cs`** - Soyutlamaya bağımlı:
```csharp
// Yanlış: Somut sınıfa bağımlılık
// private SwordAttack attackType;

// Doğru: Interface'e bağımlılık
private IAttack attackType;

void Start()
{
    // Runtime'da somut sınıf belirlenir
    attackType = new SwordAttack();
}
```

**`DamageCalculator.cs`** - Soyutlamaya bağımlı:
```csharp
// Interface'e bağımlı, somut sınıfa değil
public int CalculateDamage(IAttack attack)
{
    return attack.BaseDamage;
}
```

#### **Avantajlar:**
1. **Esneklik**: Runtime'da farklı saldırı türleri kullanılabilir
2. **Test Edilebilirlik**: Mock nesneler ile kolayca test edilebilir
3. **Bağımsızlık**: `Player` sınıfı, hangi saldırı türünün kullanıldığını bilmek zorunda değil

---

## Sistem Akışı

### **Saldırı Mekanizması:**

```
1. Player.Start()
   ↓
2. IAttack attackType = new SwordAttack()  [DIP - Interface'e bağımlılık]
   ↓
3. Player.PerformAttack()
   ↓
4. DamageCalculator.CalculateDamage(attackType)  [LSP - Herhangi IAttack türü]
   ↓
5. Enemy.ReceiveDamage(damage)
   ↓
6. Health.TakeDamage(damage)  [SRP - Sadece can yönetimi]
   ↓
7. Health.IsDead() kontrolü
   ↓
8. GameManager.AddScore(10)  [SRP - Sadece skor yönetimi]
```

---

## Sınıf Sorumlulukları Özeti

| Sınıf | Sorumluluk | SOLID Prensibi |
|-------|-----------|----------------|
| `Health` | Can yönetimi | **SRP** |
| `Player` | Oyuncu davranışları | **SRP, DIP** |
| `Enemy` | Düşman davranışları | **SRP** |
| `DamageCalculator` | Hasar hesaplama | **SRP, DIP** |
| `GameManager` | Oyun yönetimi, skor | **SRP** |
| `IAttack` | Saldırı soyutlaması | **OCP, LSP, ISP** |
| `SwordAttack` | Kılıç saldırısı | **OCP, LSP** |
| `BowAttack` | Yay saldırısı | **OCP, LSP** |

---

## Sistemin Güçlü Yönleri

### **Modülerlik**
Her bileşen bağımsız şekilde geliştirilebilir ve test edilebilir.

### **Genişletilebilirlik**
Yeni saldırı türleri, düşman türleri veya hesaplama metodları **mevcut kodu bozmadan** eklenebilir.

### **Test Edilebilirlik**
Interface'ler sayesinde mock nesneler ile unit test yazılabilir.

### **Bakım Kolaylığı**
Her sınıfın tek sorumluluğu olduğu için kod okunması ve bakımı kolay.

### **Esneklik**
Runtime'da davranışlar değiştirilebilir (örn: oyuncunun silahını değiştirme).

---

## Geliştirme Önerileri

### **Eklenebilecek Özellikler:**

1. **Yeni Saldırı Türleri:**
   ```csharp
   - MagicAttack (Büyü saldırısı)
   - AxeAttack (Balta saldırısı)
   - PoisonAttack (Zehir saldırısı)
   ```

2. **Kritik Hasar Sistemi:**
   ```csharp
   public class CriticalDamageCalculator : DamageCalculator
   {
       public override int CalculateDamage(IAttack attack)
       {
           int baseDamage = base.CalculateDamage(attack);
           // %20 şans ile kritik vuruş
           return Random.value < 0.2f ? baseDamage * 2 : baseDamage;
       }
   }
   ```

3. **Zırh Sistemi:**
   ```csharp
   public interface IDefense
   {
       int DefenseValue { get; }
   }
   ```

4. **Özel Yetenekler (Special Abilities):**
   ```csharp
   public interface ISpecialAbility
   {
       void UseAbility();
       float Cooldown { get; }
   }
   ```

---

## Notlar

- Bu sistem, Unity oyun motorunda C# ile yazılmıştır
- SOLID prensipleri, **object-oriented programming** temelinde uygulanmıştır
- Kod, **genişletilebilir** ve **bakımı kolay** bir yapıya sahiptir
- `ICharacter` interface'i, **ISP ihlali örneği** olarak bilerek hatalı bırakılmıştır (eğitim amaçlı)

---

## Öğrenilen Dersler

1. **Her sınıf tek bir işten sorumlu olmalı**
2. **Yeni özellikler eklerken mevcut kodu değiştirmemeli**
3. **Interface'ler küçük ve spesifik olmalı**
4. **Somut sınıflar yerine soyutlamalara bağımlı olunmalı**
5. **Alt sınıflar, üst sınıfların yerine kullanılabilir olmalı**

---

**Hazırlayan:** SOLID Prensipleri Örnek Projesi  
**Tarih:** Mart 2026  
**Amaç:** Unity'de temiz kod yazımı ve SOLID prensiplerinin pratik uygulaması