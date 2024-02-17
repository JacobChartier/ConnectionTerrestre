using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    [Header("Description")]
    public Sprite icon;
    
    public new string name;
    public string description;

    public ItemCategory category;
    public Rarety rarety;

    public int price;

    [Header("Options")]
    public bool isStackable;
    public int stackSize;

    public bool isBreakable = false;
    public int durability;
}

public enum ItemCategory
{
    POTION,
    ESSENCE,
    SHIELD,
    OTHER
}

public enum Rarety
{
    COMMON,
    RARE,
    EPIC,
    LEGENDARY
}