using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public string description;
}

public class PlayerInventory : MonoBehaviour
{
    public event System.Action<int> OnBottlecapChange;

    public int bottlecaps = 0;
    public List<Item> items = new List<Item>();

    public void AddItem(Item item)
    {
        item = Instantiate(item);
        items.Add(item);
    }

    public void AddBottlecaps(int change)
    {
        bottlecaps += change;
        OnBottlecapChange?.Invoke(change);
    }
}
