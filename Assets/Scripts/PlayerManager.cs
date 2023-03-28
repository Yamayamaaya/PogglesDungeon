using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // 現在のターン経過数
    public int turnCount;

    //座標情報
    public int[,] position = {{5,5,0},{0,0,0}};

    // アイテム保持状態 int[]
    public int[] items = {10};
}