using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

    public GameObject[] menuItems; // メニュー項目の配列
    public int selectedItemIndex = 0; // 現在選択されているメニュー項目のインデックス

    void Start () {
        UpdateMenuItemSelection(); // 初期のメニュー項目の選択状態を更新
    }

    void Update () {
        //UIのボタンとの関連付け
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            // 上キーが押された場合
            selectedItemIndex--;
            if (selectedItemIndex < 0) {
                selectedItemIndex = menuItems.Length - 1;
            }
            UpdateMenuItemSelection();
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            // 下キーが押された場合
            selectedItemIndex++;
            if (selectedItemIndex >= menuItems.Length) {
                selectedItemIndex = 0;
            }
            UpdateMenuItemSelection();
        } 
    }

    void UpdateMenuItemSelection() {
        // 現在選択されているメニュー項目を強調表示する
        for (int i = 0; i < menuItems.Length; i++) {
            if (i == selectedItemIndex) {
                menuItems[i].GetComponent<UnityEngine.UI. Text>().color = Color.black;
                menuItems[i].GetComponent<UnityEngine.UI. Text>().text　= "▶" + menuItems[i].GetComponent<UnityEngine.UI. Text>().text;
            } else {
                menuItems[i].GetComponent<UnityEngine.UI. Text>().color = Color.gray;
                menuItems[i].GetComponent<UnityEngine.UI. Text>().text　= menuItems[i].GetComponent<UnityEngine.UI. Text>().text.Replace("▶", "");
            }
        }
    }
}
