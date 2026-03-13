using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [Header("Karakter Verisi")]
    [SerializeField] private CharacterData data;

    private int currentHealth;

    private void Start()
    {
        currentHealth = data.health;
        Debug.Log(data.characterName + " olusturuldu! Can: " + currentHealth);
    }

    private void Update()
    {
        // WASD hareket -- hiz degeri ScriptableObject'ten okunuyor
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(h, 0, v) * data.speed * Time.deltaTime);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log(data.characterName + " hasar aldi! Kalan can: " + currentHealth);
    }
}
