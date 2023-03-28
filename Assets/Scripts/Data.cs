using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using System;

public class EventData
{
    public System.Func<int[], bool> Check;
    public EventProcess[] eventProcesses;
    public Sprite eventSprite;
    public int eventType;
    // ノーマル：0 　レア：1 　スーパーレア：2 　ユニーク：3
}

public class EventProcess
{
    public int[] resources;
    public string message;
    public bool dead;
    public int newItem;
}

public class ItemData
{
    public string itemName;
    public Sprite itemSprite;
    public int itemType;
    // 所持：0 　消費：1 　ユニーク：2 　コレクション：3
}

public class AddressData
{
    public int[,] position = new int[3, 3];
    public int eventNumber;
}

public class Data : MonoBehaviour
{
    public List<Sprite> eventSprites = new List<Sprite>();
    public List<Sprite> itemSprites = new List<Sprite>();

    public List<ItemData> items = new List<ItemData>
    {
        new ItemData { itemName = "blank", itemSprite = null, itemType = 0 },
        new ItemData { itemName = "食料", itemSprite = null, itemType = 1 },
        new ItemData { itemName = "ナイフ", itemSprite = null, itemType = 1 },
        new ItemData { itemName = "鉄", itemSprite = null, itemType = 1 },
        new ItemData { itemName = "金", itemSprite = null, itemType = 1 }
    };

    public List<EventData> events = new List<EventData>
    {
        new EventData
        {
            Check = (int[] haveItems) => haveItems[2] > 0,
            eventProcesses = new EventProcess[]
            {
                new EventProcess { resources = new int[] {0, 1, 0, 0}, message = "狼の群れに襲われたが、ナイフで応戦した。", dead = false, newItem = 0 },
                new EventProcess { resources = new int[] {0, 0, 0, 0}, message = "狼の群れに襲われて力尽きた。", dead = true, newItem = 0 }
            },
            eventSprite = null,
            eventType = 1
        },
        new EventData
        {
            Check = null,
            eventProcesses = new EventProcess[]
            {
                new EventProcess { resources = new int[] {0, 0, 0, 0}, message = "キノコを発見した。食べられそうだ。", dead = false, newItem = 0 },
                null
            },
            eventSprite = null,
            eventType = 0
        }
    };
    public List<AddressData> uniqueAddresses = new List<AddressData>();


    void Start()
    {
    // Set the correct item sprites
    for (int i = 1; i <= 4; i++)
    {
        items[i].itemSprite = itemSprites[i];
    }

    // Set the correct event sprites
    for (int i = 0; i <= 1; i++)
    {
        events[i].eventSprite = eventSprites[i];
    }
    }
}