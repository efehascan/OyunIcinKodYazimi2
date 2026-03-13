using UnityEngine;


[CreateAssetMenu(menuName = "Veriler/Karakter")]
public class CharacterData : ScriptableObject
{
    [Header("Karakter Bilgileri")]
    public string characterName;
    public CharacterType characterType;
    public int health = 100;
    public int damage = 5;
}
