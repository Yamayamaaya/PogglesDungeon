using System.Net.NetworkInformation;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EventManager : MonoBehaviour
{
    public PlayerManager playerManager;
    public DataManager dataManager;
    public UIManager uiManager;
    public bool nowCondition;

    void Start()
    { 

    }

    void Update()
    {
        
    }

    public void FetchEvent(){
        int[,] nowPosition = playerManager.position;
        //現在の座標に対応するイベントを取得
        Event nowEvent = dataManager.Events.Find(_event => _event.rootcoordinate[0] == nowPosition[0,0] && _event.rootcoordinate[1] == nowPosition[0,1] && _event.rootcoordinate[2] == nowPosition[0,2]&& _event.detailcoordinate[0] == nowPosition[1,0] && _event.detailcoordinate[1] == nowPosition[1,1] && _event.detailcoordinate[2] == nowPosition[1,2]);
        //イベントがなければランダムイベントを取得
        if(nowEvent == null){
            int normalEventRate = 70;
            int rareEventRate = 20;
            int superRareEventRate = 10;
            //乱数作成
            int randomValue = new System.Random().Next(90);
            switch (randomValue)
            {
                case var _ when randomValue < normalEventRate:
            　//ノーマルイベント
                    nowEvent = dataManager.NormalEvents[randomValue % dataManager.NormalEvents.Count];
                    break;
                case var _ when randomValue < normalEventRate + rareEventRate:
                //レアイベント
                    nowEvent = dataManager.RareEvents[(randomValue - normalEventRate) % dataManager.RareEvents.Count];
                    break;
                default:
                //スーパーレアイベント
                    nowEvent = dataManager.SuperRareEvents[(randomValue - normalEventRate - rareEventRate) % dataManager.SuperRareEvents.Count];
                    break;
            }
        }
        
        //イベントの条件を満たしているか確認
        if(nowEvent.condition_0.condition == "null" && nowEvent.condition_1.condition == "null"){
            //条件がない場合はtrue
            nowCondition = true;
        }else if(nowEvent.condition_1.condition == "null"){
            //条件が一つの場合
            switch(nowEvent.condition_0.condition)
            {
                case "above":
                //「以上」条件の場合
                    nowCondition =
                    playerManager.itemCount.Find(_itemCount => _itemCount.item.code == nowEvent.condition_0.key_item).count >= int.Parse(nowEvent.condition_0.condition_number);
                    break;
                case "below":
                //「以下」条件の場合
                    nowCondition =
                    playerManager.itemCount.Find(_itemCount => _itemCount.item.code == nowEvent.condition_0.key_item).count <= int.Parse(nowEvent.condition_0.condition_number);
                    break;
            }
        }else{
            //条件が二つの場合
            switch(nowEvent.condition_0.condition)
            {
                case "above":
                //一つ目の条件が「以上」条件の場合
                    switch(nowEvent.condition_1.condition)
                    {
                        case "above":   
                        //二つ目の条件が「以上」条件の場合
                            nowCondition =
                            playerManager.itemCount.Find(_itemCount => _itemCount.item.code == nowEvent.condition_0.key_item).count >= int.Parse(nowEvent.condition_0.condition_number) && playerManager.itemCount.Find(_itemCount => _itemCount.item.code == nowEvent.condition_1.key_item).count >= int.Parse(nowEvent.condition_1.condition_number);
                            break;
                        case "below":
                        //二つ目の条件が「以下」条件の場合
                            nowCondition =
                            playerManager.itemCount.Find(_itemCount => _itemCount.item.code == nowEvent.condition_0.key_item).count >= int.Parse(nowEvent.condition_0.condition_number) && playerManager.itemCount.Find(_itemCount => _itemCount.item.code == nowEvent.condition_1.key_item).count <= int.Parse(nowEvent.condition_1.condition_number);
                            break;
                    }          
                break;
                case "below":
                //一つ目の条件が「以下」条件の場合
                    switch(nowEvent.condition_1.condition)
                    {
                        case "above":
                        //二つ目の条件が「以上」条件の場合
                            nowCondition =
                            playerManager.itemCount.Find(_itemCount => _itemCount.item.code == nowEvent.condition_0.key_item).count <= int.Parse(nowEvent.condition_0.condition_number) && playerManager.itemCount.Find(_itemCount => _itemCount.item.code == nowEvent.condition_1.key_item).count >= int.Parse(nowEvent.condition_1.condition_number);
                            break;
                        case "below":
                        //二つ目の条件が「以下」条件の場合
                            nowCondition =
                            playerManager.itemCount.Find(_itemCount => _itemCount.item.code == nowEvent.condition_0.key_item).count <= int.Parse(nowEvent.condition_0.condition_number) && playerManager.itemCount.Find(_itemCount => _itemCount.item.code == nowEvent.condition_1.key_item).count <= int.Parse(nowEvent.condition_1.condition_number);
                            break;
                    }
                break;
            }
        }
        
        //条件に応じた結果を取得
        Result nowResult = nowCondition ? nowEvent.success_result : nowEvent.failure_result;

        //食料消費処理
        playerManager.itemCount.Find(_itemCount => _itemCount.item.code == "food").count -= 5;

        //資源増減処理
        int index = 0;
        foreach(int _status_change in nowResult.status_change){
            playerManager.itemCount[index].count += _status_change;
            index++;
        }
        uiManager.Display(nowResult.message, nowEvent.code);

        //アイテム入手処理
        if(nowResult.item_found != "null"){
        playerManager.itemCount.Find(_itemCount => _itemCount.item.code == nowResult.item_found).count += 1;
        }

        // //進めなかった場合の処理
        if((nowEvent.name =="硬い岩" ||  nowEvent.name =="かなり硬い岩") && !nowCondition){
            if(playerManager.thisGameRecords.Count  < 1)
            {
                playerManager.position = new int[2,3]{ {0,0,0}, {0,0,0} };
            }else{
                playerManager.position = new int[2,3];
                playerManager.position[0,0] = playerManager.thisGameRecords[playerManager.turnCount - 2].rootPosition[0];
                playerManager.position[0,1] = playerManager.thisGameRecords[playerManager.turnCount - 2].rootPosition[1];
                playerManager.position[0,2] = playerManager.thisGameRecords[playerManager.turnCount - 2].rootPosition[2];
                playerManager.position[1,0] = playerManager.thisGameRecords[playerManager.turnCount - 2].detailPosition[0];
                playerManager.position[1,1] = playerManager.thisGameRecords[playerManager.turnCount - 2].detailPosition[1];
                playerManager.position[1,2] = playerManager.thisGameRecords[playerManager.turnCount - 2].detailPosition[2];

                }
        }


        //記録記入処理
        playerManager.Recording(nowResult.message);

        //TODO:gameOver処理
        if(nowResult.is_game_over || playerManager.itemCount.Find(_itemCount => _itemCount.item.code == "food").count <= 0){
            UnityEngine.Debug.Log("<color=red><size=20>GameOver</size></color>");
            UnityEngine.Debug.Log("<i>---------------↓Result↓---------------</i>");
            string gameOverLog = "";
            foreach(GameRecord _record in playerManager.thisGameRecords){
                    gameOverLog = gameOverLog + "\n"+ _record.turnCount.ToString() + "ターン目に"+ _record.detailPosition[0].ToString() + "," + _record.detailPosition[1].ToString() + "," + _record.detailPosition[2].ToString() + "で" +_record.eventLog;
            }
            UnityEngine.Debug.Log(gameOverLog);
            UnityEngine.Debug.Log("<i>---------------↑Result↑---------------</i>");
        }
            
        GameRecord nowRecord = playerManager.thisGameRecords[playerManager.turnCount - 1];
        UnityEngine.Debug.Log(nowRecord.turnCount.ToString() + "ターン目に"+ nowRecord.detailPosition[0].ToString() + "," + nowRecord.detailPosition[1].ToString() + "," + nowRecord.detailPosition[2].ToString() + "で" + nowRecord.eventLog);

    }


}
