using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : MonoBehaviour
{
    [Header("Karakter Verisi")]
    [SerializeField] private CharacterData data;

    [SerializeField] private int currentHealth;
    [SerializeField] private Key attackKey = Key.Space;

    private void Start()
    {
        currentHealth = data.health;
        Debug.Log(data.characterName + " olusturuldu! Can: " + currentHealth);
    }

    private void Update()
    {

        if (Keyboard.current != null && Keyboard.current[attackKey].wasPressedThisFrame)
        {
            TakeDamage(data.damage);
        }   
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log(data.characterName + " hasar aldi! Kalan can: " + currentHealth);
    }
}
