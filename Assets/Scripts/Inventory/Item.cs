using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public string itemName = "New Item";
    public Sprite icon; 
    public string itemDescription = "New Description";
    public enum Type { Default, Consumable, Weapon, Random}
    public Type type = Type.Default;

}
