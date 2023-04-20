using System.Diagnostics;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class DataManager : MonoBehaviour
{
    // テキストアセットの変数
    public TextAsset EventFile;
    public TextAsset ItemFile;
    public TextAsset LocationFile;

    // アイテム、イベント情報を格納するリスト
    public List<Item> Items = new List<Item>();
    public List<Event> Events = new List<Event>();

    // イベント情報を種類ごとに格納するリスト
    public List<Event> NormalEvents = new List<Event>();
    public List<Event> RareEvents = new List<Event>(); 
    public List<Event> SuperRareEvents = new List<Event>();
    public List<Event> UniqueEvents = new List<Event>();

    // データ読み込み中かどうかを示すフラグ
    public bool Loading = true;
    void Start()
    {
        Load();
    }

    void Update() {
    }
    
    void Load()
    {
        StringReader readerEvent = new StringReader(EventFile.text);
        readerEvent.ReadLine();
        while (readerEvent.Peek() != -1)
        {
            string line = readerEvent.ReadLine();
            string[] values = line.Split(',');
            
            Event eventData = new Event();
            
            eventData.name = values[0];
            eventData.code = values[1];
            eventData.rarity = values[2];
            
            eventData.rootcoordinate = new int[3];
            eventData.rootcoordinate[0] = int.Parse(values[3]);
            eventData.rootcoordinate[1] = int.Parse(values[4]);
            eventData.rootcoordinate[2] = int.Parse(values[5]);
            
            eventData.detailcoordinate = new int[3];
            eventData.detailcoordinate[0] = int.Parse(values[6]);
            eventData.detailcoordinate[1] = int.Parse(values[7]);
            eventData.detailcoordinate[2] = int.Parse(values[8]);
            
            Condition condition_0 = new Condition();
            condition_0.key_item = values[9];
            condition_0.condition = values[10];
            condition_0.condition_number = values[11];
            eventData.condition_0 = condition_0;
            
            Condition condition_1 = new Condition();
            condition_1.key_item = values[12];
            condition_1.condition = values[13];
            condition_1.condition_number = values[14];
            eventData.condition_1 = condition_1;
            
            Result success_result = new Result();
            success_result.status_change = new int[4];
            success_result.status_change[0] = int.Parse(values[15]);
            success_result.status_change[1] = int.Parse(values[16]);
            success_result.status_change[2] = int.Parse(values[17]);
            success_result.status_change[3] = int.Parse(values[18]);
            success_result.status_change[3] = int.Parse(values[19]);
            success_result.message = values[20];
            success_result.is_game_over = bool.Parse(values[21]);
            success_result.item_found = values[22];
            eventData.success_result = success_result;
            
            Result failure_result = new Result();
            failure_result.status_change = new int[4];
            failure_result.status_change[0] = int.Parse(values[23]);
            failure_result.status_change[1] = int.Parse(values[24]);
            failure_result.status_change[2] = int.Parse(values[25]);
            failure_result.status_change[3] = int.Parse(values[26]);
            failure_result.status_change[3] = int.Parse(values[27]);
            failure_result.message = values[28];
            failure_result.is_game_over = bool.Parse(values[29]);
            failure_result.item_found = values[30];
            eventData.failure_result = failure_result;

            Events.Add(eventData);
        }
        //イベントの種類ごとに分ける
        foreach(Event _event in Events){
            if(_event.rarity == "normal"){
                NormalEvents.Add(_event);
            }else if(_event.rarity == "rare"){
                RareEvents.Add(_event);
            }else if(_event.rarity == "super_rare"){
                SuperRareEvents.Add(_event);
            }else if(_event.rarity == "unique"){
                UniqueEvents.Add(_event);
            }
        }

        StringReader readerItem = new StringReader(ItemFile.text);
        readerItem.ReadLine();
        while (readerItem.Peek() != -1)
        {
            string line = readerItem.ReadLine();
            string[] values = line.Split(',');
            Item itemData = new Item();
            itemData.name = values[0];
            itemData.code = values[1];
            itemData.type = values[2];
            Items.Add(itemData);
        }

        Loading = false;
    }


}
[System.Serializable]
public class Event
{
    public string name;
    public string code;
    public string rarity;
    public int[] rootcoordinate;
    public int[] detailcoordinate;
    public  Condition condition_0;
    public  Condition condition_1;
    public Result success_result;
    public Result failure_result;
}

[System.Serializable]
public class Condition
{
    public string key_item;
    public string condition;
    public string condition_number;
}

[System.Serializable]
public class Result
{
    public int[] status_change;
    public string message;
    public bool is_game_over;
    public string item_found;
}

[System.Serializable]
public class Item
{
    public string name;
    public string code;
    public string type;
}

