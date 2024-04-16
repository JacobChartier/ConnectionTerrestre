using System;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public abstract class Item : MonoBehaviour, IItemBase
{
    [field: SerializeField] public int Id { get; protected set; } = -1;

    [field: Header("Art")]
    [field: ReadOnly] public Sprite Icon { get; protected set; } = null;
    [field: ReadOnly] public Mesh Model { get; protected set; } = null;

    [field: Header("Data")]
    [field: SerializeField] public string Name { get; set; } = $"Invalid Item";
    [field: SerializeField] public string Description { get; set; } = $"";

    [field: Space]
    [field: ReadOnly, SerializeField] public Rarety Rarety { get; protected set; } = Rarety.INVALID;
    [field: ReadOnly, SerializeField] public Category Category { get; protected set; } = Category.INVALID;

    [field: Space]
    [field: ReadOnly, SerializeField] public bool IsUsableOnlyInCombat { get; protected set; } = false;
    [field: ReadOnly, SerializeField] public bool IsUsableOnlyInOverworld { get; protected set; } = false;

    [field: Space]
    [field: ReadOnly, SerializeField] public bool IsStackable { get; protected set; } = true;
    [field: ReadOnly, SerializeField] public int StackSize { get; protected set; } = 5;

    [field: Space]
    [field: ReadOnly, SerializeField] public bool IsBreakable { get; protected set; } = false;
    [field: ReadOnly, SerializeField] public int MaxUses { get; protected set; } = 5;
    [field: SerializeField] public int RemainingUses { get; set; } = 5;

    [field: Space]
    [field: ReadOnly, SerializeField] public bool IsBuyable { get; protected set; } = true;
    [field: SerializeField] public int Price { get; set; } = 0;

    protected void Start()
    {
        Load();

        if (Name == "Invalid Item")
            Debug.LogWarning($"An invalid item of type <b>{this.GetType().Name}</b> was created.");
        else
            name = Name;

        // Check if the texture has loaded.
        if (Icon == null)
        {
            Debug.LogWarning($"Unable to load the texture from <b>{this.GetType().Name} ({typeof(Item)})</b>.", Icon);
            Icon = Resources.Load<Sprite>("Sprites/missing_sprite");
        }

        transform.GetComponentInChildren<Image>().sprite = Icon;

        // Check is the mesh has loaded.
        if (Model == null)
        {
            Debug.LogWarning($"Unable to load the mesh from <b>{this.GetType().Name} ({typeof(Item)})</b>.", Model);
            Model = Resources.Load<Mesh>("Mesh/missing_mesh");
        }
    }

    public virtual void Use()
    {
        Player.Instance.inventory.Remove(this);
        InventoryLoader.Save(Player.Instance.inventory);
    }

    protected virtual void Load()
    {

    }

    public Slot GetSlot()
    {
        if (gameObject.GetComponentInParent<Slot>(true) == null)
            return null;
        else
            return gameObject.GetComponentInParent<Slot>(true);
    }

    public int GetSlotID()
    {
        if (gameObject.GetComponentInParent<Slot>(true) == null)
            return -1;
        else
            return gameObject.GetComponentInParent<Slot>(true).ID;
    }

    public int GeneratePrice(int min, int max)
    {
        return UnityEngine.Random.Range(min, max);
    }

    public bool Purchase(EntityStats player)
    {
        if (Price <= player.Coins)
        {
            player.Coins -= Price;
            MenuHandler.Instance.GetMenu<InventoryUI>().Refresh();

            return true;
        }

        return false;
    }
}

public enum Category
{
    POTION,
    ESSENCE,
    SHIELD,
    LEAF,

    INVALID = -1,
    DEBUG = -2
}

public enum Rarety
{
    COMMON,
    RARE,
    EPIC,
    LEGENDARY,

    INVALID = -1,
    DEBUG = -2
}