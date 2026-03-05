# SOLID Prensipleri -- Basit ve Anlaşılır Anlatım

SOLID, temiz ve sürdürülebilir yazılım geliştirmek için kullanılan 5
temel prensiptir.

Amaç: - Kodun daha okunabilir olması - Değişikliklere dayanıklı olması -
Test edilebilir olması - Büyüyebilir olması

------------------------------------------------------------------------

# S --- Single Responsibility Principle (Tek Sorumluluk Prensibi)

Bir sınıfın sadece **tek bir sorumluluğu** olmalıdır.

## Yanlış Örnek

``` csharp
class Student
{
    public void SaveToDatabase() { }
    public void SendEmail() { }
    public double CalculateAverage() { return 0; }
}
```

## Doğru Örnek

``` csharp
class Student
{
    public double CalculateAverage() { return 0; }
}

class StudentRepository
{
    public void Save(Student student) { }
}

class EmailService
{
    public void SendEmail() { }
}
```

Özet: Bir sınıfın değişmesi için tek bir sebep olmalı.

## Gündelik Örnek

Bir kişi: Uyanır → Yemek Yer → Duşa Girer → İşe Gider

Her adım farklı bir sorumluluktur. Bir insan hem doktorluk hem
mühendislik hem muhasebe yapmaya çalışırsa sistem karışır. Yazılımda da
her sınıf tek iş yapmalıdır.

## Çalışma Mantığı Diyagramı

    \[Student\] ---\> Not Hesaplar\
    \[StudentRepository\] ---\> Veriyi Kaydeder\
    \[EmailService\] ---\> Bildirim Gönderir

------------------------------------------------------------------------

# O --- Open/Closed Principle (Genişlemeye Açık, Değişime Kapalı)

Kod, yeni özellikler için genişletilebilir olmalı ancak mevcut kod
değiştirilmemelidir.

## Yanlış Örnek

``` csharp
class DiscountCalculator
{
    public double Calculate(string type)
    {
        if(type == "student") return 10;
        if(type == "teacher") return 20;
        return 0;
    }
}
```

## Doğru Örnek

``` csharp
interface IDiscount
{
    double Calculate();
}

class StudentDiscount : IDiscount
{
    public double Calculate() => 10;
}

class TeacherDiscount : IDiscount
{
    public double Calculate() => 20;
}
```

Özet: Var olan kodu bozma, genişleterek ilerle.

## Gündelik Örnek

Kişi: Uyanır → Yemek Yer → Duşa Girer → İşe Gider

Bir gün spor ekler: Uyanır → Yemek Yer → Duşa Girer → Spor Yapar → İşe
Gider

Eski rutin değişmedi, sadece yeni davranış eklendi.

## Diyagram

            IDiscount
                |
      ----------------------
      |                    |
    StudentDiscount TeacherDiscount  | YeniDiscount (eklenebilir)

------------------------------------------------------------------------

# L --- Liskov Substitution Principle

Alt sınıf, üst sınıfın yerine geçebilmelidir.

## Yanlış Örnek

``` csharp
class Bird
{
    public virtual void Fly() { }
}

class Penguin : Bird
{
    public override void Fly()
    {
        throw new Exception("Penguins can't fly");
    }
}
```

## Doğru Örnek

``` csharp
class Bird { }

class FlyingBird : Bird
{
    public virtual void Fly() { }
}

class Sparrow : FlyingBird { }
class Penguin : Bird { }
```

Özet: Alt sınıf, üst sınıfın mantığını bozmamalı.

## Gündelik Örnek

Sistem "uçabilen bir canlı" ister. Eğer yerine uçamayan bir canlı
verilirse sistem bozulur. Yani yerine geçen şey aynı davranış sözünü
tutmalıdır.

## Diyagram

            Bird
              |
      -----------------
      |               |
    FlyingBird        Penguin
    |
    Sparrow

ingBird gereken
yerde Sparrow
kullanılabilir.
  -----------------

# I --- Interface Segregation Principle

Sınıflar, kullanmadıkları metotları implement etmek zorunda
kalmamalıdır.

## Yanlış Örnek

``` csharp
interface IWorker
{
    void Work();
    void Eat();
}

class Robot : IWorker
{
    public void Work() { }

    public void Eat()
    {
        throw new Exception();
    }
}
```

## Doğru Örnek

``` csharp
interface IWorkable
{
    void Work();
}

interface IEatable
{
    void Eat();
}
```

Özet: Büyük interface yazma, küçük ve anlamlı parçalara böl.

## Gündelik Örnek

İnsan: Çalışır + Yemek Yer

Robot: Çalışır ama yemek yemez

Robotu yemek yemeye zorlamak mantıksızdır.

## Diyagram

      IWorkable        IEatable
          ^                 ^
          |                 |
       ---------         ---------
       |       |         |       |
     Human   Robot      Human   (—)

------------------------------------------------------------------------

# D --- Dependency Inversion Principle

Somut sınıflara değil, soyutlamalara bağımlı ol.

## Yanlış Örnek

``` csharp
class Car
{
    private Engine engine = new Engine();
}
```

## Doğru Örnek

``` csharp
interface IEngine
{
    void Start();
}

class Engine : IEngine
{
    public void Start() { }
}

class Car
{
    private IEngine engine;

    public Car(IEngine engine)
    {
        this.engine = engine;
    }
}
```

Özet: Detaya değil, soyutlamaya bağımlı ol.

## Gündelik Örnek

Araba benzine değil, "motor" kavramına bağlı olmalı. Motor değişebilir
ama araba değişmemeli.

## Diyagram

            Car ----> IEngine
             ^
             |
      -----------------
      |               |
    Engine      ElectricEngine

# Genel Çalışma Mantığı (Tüm Sistem Diyagramı)

Bir yazılımın günlük hayat gibi çalıştığını düşün:

Başlat \| v İş Mantığı \| v Hesaplama \| v Veri Kaydetme \| v Bildirim
\| v Bitiş

Her adım ayrı bir sorumlulukta çalışır. Bu yapı sayesinde sistem
büyüdüğünde bozulmaz.

------------------------------------------------------------------------

# Genel Özet

-   S → Tek sorumluluk
-   O → Genişlet, değiştirme
-   L → Yerine geçebilirlik
-   I → Küçük interface
-   D → Soyutlamaya bağımlılık

SOLID, büyüyen projelerde kodun dağılmasını engelleyen temel tasarım
prensipleridir.
