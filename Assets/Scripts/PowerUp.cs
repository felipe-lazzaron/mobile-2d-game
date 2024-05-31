using UnityEngine;

[CreateAssetMenu(fileName = "New PowerUp", menuName = "PowerUp")]
public class PowerUp : ScriptableObject
{
    public string powerUpName;
    public string description;
    public Sprite icon;
    public float duration;
    // Outros atributos espec�ficos para o power-up
}
