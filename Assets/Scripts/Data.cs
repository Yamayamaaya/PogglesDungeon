using System.Collections.Generic;
using UnityEngine;


public class EventData
{
    public System.Func<bool> Check;
    public EventProcess[] eventProcesses;
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
}


public class Data : MonoBehaviour
{
    private List<ItemData> items = new List<ItemData>();
    private List<EventData> events = new List<EventData>();

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
                }
            },
            new EventData { 
                Check = null,
                eventProcesses = new EventProcess[]
                {
                    new EventProcess { resources = new int[] {0, 0, 0, 0}, message = "キノコを発見した。食べられそうだ。", dead = false, newItem = 0},
                    null
                }
            }
        });

        items.Add(new ItemData { itemName = "ナイフ", itemCount = 1 });
    }
}
