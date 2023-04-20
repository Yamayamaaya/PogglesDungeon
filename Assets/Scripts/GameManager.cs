using System.Data;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerManager playerManager;
    public UIManager uiManager;
    public EventManager eventManager;
    public DataManager dataManager;
    enum sceneStatus
    {
        title,
        reading,
        choosing,
        pause
    }


//TODO:セーブ機能
//TODO:タイトル画面とゲーム画面の切り替え

    private sceneStatus nowSceneStatus = sceneStatus.choosing;
    private int nowDirection;//0：前、1：右、2：後、3：左、4：上、5：下
    private int nowRotate; //0:前進 1:後退 2:左 3:右
    
    private bool nowLoading = true;



    void Update()
    {
        //DataManagerの読み込みを待つ
        if (nowLoading != dataManager.Loading){
            nowLoading = dataManager.Loading  ;
            playerManager.enabled = true;
            uiManager.enabled = true;
            eventManager.enabled = true;
        }


        int _AbsoluteDirectionNum;

        switch (nowSceneStatus)
        {
            case sceneStatus.title:
                break;
            case sceneStatus.reading:
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) {
                    nowSceneStatus = sceneStatus.choosing;
                    uiManager.ResetText();
                }
                break;
            case sceneStatus.choosing:
                    if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) {
                        if(nowRotate == 0){
                            _AbsoluteDirectionNum = uiManager.selectedItemIndex;
                            if( uiManager.selectedItemIndex !=4&&uiManager.selectedItemIndex !=5)
                            {nowRotate =uiManager.selectedItemIndex;}
                        }
                        else
                        {
                            _AbsoluteDirectionNum = (nowRotate + uiManager.selectedItemIndex) % 4;
                            nowRotate = nowRotate + uiManager.selectedItemIndex;
                        }
                        Next(_AbsoluteDirectionNum);
                        eventManager.FetchEvent();
                        uiManager.UpdateUI();

                        nowSceneStatus = sceneStatus.reading;
                    }
                break;
            case sceneStatus.pause:
                break;
        }


    void Next(int _direction){
         //_directionの値でswitch
        switch (_direction)
        {
            case 0:
                playerManager.position[1, 1] += 1;
                break;
            case 1:
                playerManager.position[1, 0] += 1;
                break;
            case   2:
                playerManager.position[1, 1] -= 1;
                break;
            case 3:
                playerManager.position[1, 0] -= 1;
                break;
            case 4:
                playerManager.position[1, 2] += 1;
                break;
            case 5:
                playerManager.position[1, 2] -= 1;
                break;
        }
        playerManager.turnCount++;
    }

    
    }

}