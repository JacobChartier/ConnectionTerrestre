using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    [Header("Description")]
    public Sprite image;
    
    public string name;
    public string description;
    public ItemCategory category;
    public int price;
    public string[] stats;

    [Header("Options")]
    public bool isStackable;
    public int stackSize;

    public bool isBreakable = false;
    public int durability;
}

public enum ItemCategory
{
    potion,
    essence,
    shield
}