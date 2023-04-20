using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour
{
    public DataManager dataManager;
    public int turnCount;
    public int[,] position = new int[2,3];
    // TODO:大地区の番地更新 
    // TODO:エリア外に出れないようにする
    
    public List<ItemCount> itemCount= new List<ItemCount>();
    public List<GameRecord> thisGameRecords = new List<GameRecord>();

    void Start(){
        position = new int[2,3]{{5,5,0},{0,0,0}};
        int index = 0;
        foreach(Item _item in dataManager.Items){
            ItemCount _itemCount = new ItemCount();
            _itemCount.item = _item;
            switch (index)
            {
                case 0:
                    _itemCount.count = 10;
                    break;
                case 1:
                    _itemCount.count = 10;
                    break;
                case 2:
                    _itemCount.count = 1;
                    break;
                case 3:
                    _itemCount.count = 10;
                    break;
                case 4:
                    _itemCount.count = 10;
                    break;
                default:
                    _itemCount.count = 0;   
                    break;
            }
            itemCount.Add(_itemCount);
            index++;
        }
    }

    public void Recording(string Message)
    {
        GameRecord gameRecord = new GameRecord();
        gameRecord.turnCount = turnCount;
        gameRecord.rootPosition = new int[3];
        gameRecord.rootPosition[0] = position[0,0];
        gameRecord.rootPosition[1] = position[0,1];
        gameRecord.rootPosition[2] = position[0,2];
        gameRecord.detailPosition = new int[3];
        gameRecord.detailPosition[0] = position[1,0];
        gameRecord.detailPosition[1] = position[1,1];
        gameRecord.detailPosition[2] = position[1,2];
        gameRecord.eventLog = Message;
        thisGameRecords.Add(gameRecord);
    }

    void Update() {
    }
}

[System.Serializable]
public class ItemCount
{
    public Item item;
    public int count;
}

[System.Serializable]
public class GameRecord
{
    public int turnCount;
    public int[] rootPosition;
    public int[] detailPosition;
    public string eventLog;
}