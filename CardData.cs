using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Cards/CardData")]
public class CardData : ScriptableObject
{
    public string cardName;
    public Sprite icon;

    public float fireEffect;
    public float waterEffect;
    public float airEffect;
    public float earthEffect;
}
