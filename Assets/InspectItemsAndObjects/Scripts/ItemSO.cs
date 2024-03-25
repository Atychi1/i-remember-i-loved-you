using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ItemSO : ScriptableObject
{

    public string itemName; // Name of the item

    [SerializeField, TextArea(3, 20)] // Specify the number of lines visible
    public string lore; // Lore or description of the item

    public Sprite sprite;
    public Transform prefab;
}
