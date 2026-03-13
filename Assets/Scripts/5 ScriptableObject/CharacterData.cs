using UnityEngine;


[CreateAssetMenu(menuName = "Veriler/Karakter")]
public class CharacterData : ScriptableObject
{
    [Header("Karakter Bilgileri")]
    public string characterName;
    public int health = 100;
    public float speed = 5f;
}
