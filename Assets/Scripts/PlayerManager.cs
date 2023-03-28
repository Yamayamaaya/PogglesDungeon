using System.Data.Common;
using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour
{
    public ItemData itemData;
    public Data data;
    public int turnCount;

    public int[,] position = {{5,5,0},{0,0,0}};
    public int[] items ;
    public int[] consumableItems;

void Start()
    {
        items = new int[data.items.Count];
    }

void Update() {
    Array.Copy( items,1,  consumableItems, 0, 4);
}
}