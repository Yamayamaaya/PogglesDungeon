using System.Globalization;
using System.Collections.Generic;
using UnityEngine;


public class EventData
{
    public System.Func<bool> Check;
    public EventProcess[] eventProcesses;
    public Sprite eventSprite;
    public int eventType;
    //ノーマル：0 　レア：1 　スーパーレア：2 　ユニーク：3
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
    public int itemCount;
    public Sprite itemSprite;
    public int itemType;
    //所持：0 　消費：1 　ユニーク：2 　コレクション：3
}


public class Data : MonoBehaviour
{
    public List<ItemData> items = new List<ItemData>();
    public List<EventData> events = new List<EventData>();
    public List<Sprite> eventSprites = new List<Sprite>();
    public List<Sprite> itemSprites = new List<Sprite>();
    void Start()
    {
        events.AddRange(new List<EventData>
        {
            new EventData { 
                Check = () => items[2].itemCount > 0,
                eventProcesses = new EventProcess[]
                {
                    new EventProcess { resources = new int[] {0, 1, 0, 0}, message = "狼の群れに襲われたが、ナイフで応戦した。", dead = false, newItem = 0},
                    new EventProcess { resources = new int[] {0, 0, 0, 0}, message = "狼の群れに襲われて力尽きた。", dead = true, newItem = 0}
                },
                eventSprite = eventSprites[0],
                eventType = 1
            },
            new EventData { 
                Check = null,
                eventProcesses = new EventProcess[]
                {
                    new EventProcess { resources = new int[] {0, 0, 0, 0}, message = "キノコを発見した。食べられそうだ。", dead = false, newItem = 0},
                    null
                }
                ,eventSprite = eventSprites[0],
                eventType = 0
            }
        });

        items.Add(new ItemData { itemName = "ナイフ", itemCount = 1 , itemSprite = itemSprites[0], itemType = 1});
    }
}