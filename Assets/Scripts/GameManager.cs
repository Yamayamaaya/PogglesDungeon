using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerManager playerManager;
    public UIManager uiManager;
    enum sceneStatus
    {
        title,
        reading,
        choosing,
        pause
    }

//TO-DO:セーブ機能
//TO-DO:タイトル画面とゲーム画面の切り替え

    private sceneStatus nowSceneStatus;
    private int nowDirection;//0：前　1：右　2：後　3：左　4：上　5：下
    private int nowRotate; //0:前進 1:後退 2:左 3:右


    
void Next(int _direction)
    {
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


    void Update()
    {
        int _AbsoluteDirectionNum;


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
            UnityEngine.Debug.Log(_AbsoluteDirectionNum);
            Next(_AbsoluteDirectionNum);
            UnityEngine.Debug.Log(playerManager.position[1,0].ToString()+"/"+playerManager.position[1,1].ToString()+"/"+playerManager.position[1,2].ToString());

        }
        // UnityEngine.Debug.Log(playerManager.position[0,0].ToString()+"/"+playerManager.position[0,1].ToString()+"/"+playerManager.position[0,2].ToString());

    }

}
