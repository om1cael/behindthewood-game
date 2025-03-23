using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Pickable", menuName = "Scriptable/Pickable")]
public class PickableSO : ScriptableObject
{
    public Item item;
    public int amount;
}
