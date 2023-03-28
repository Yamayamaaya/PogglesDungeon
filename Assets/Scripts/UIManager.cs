using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

    public GameObject[] menuItems; // メニュー項目の配列
    public int selectedItemIndex = 0; // 現在選択されているメニュー項目のインデックス

    public Data gameData; // Dataクラスの参照

    public UnityEngine.UI.Text turnText; // 経過ターンを表示するUIテキスト
    public UnityEngine.UI.Text foodText; // 食糧数を表示するUIテキスト
    public UnityEngine.UI.Text knifeText; // ナイフの数を表示するUIテキスト
    public UnityEngine.UI.Text pickaxeText; // ツルハシのレベルを表示するUIテキスト
    public UnityEngine.UI.Text ironText; // 鉄の数を表示するUIテキスト
    public UnityEngine.UI.Text goldText; // 金の数を表示するUIテキスト

    void Start () {
        UpdateMenuItemSelection(); // 初期のメニュー項目の選択状態を更新
    }

    void Update () {
        SelectItem();
    }

    public void SelectItem(){
        //UIのボタンとの関連付け
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            // 上キーが押された場合
            UpButton();
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            // 下キーが押された場合
            DownButton();
        } 
    }

    public void UpButton(){
        selectedItemIndex--;
        if (selectedItemIndex < 0) {
            selectedItemIndex = menuItems.Length - 1;
        }
        UpdateMenuItemSelection();
    }

    public void DownButton(){
        selectedItemIndex++;
        if (selectedItemIndex >= menuItems.Length) {
            selectedItemIndex = 0;
        }
        UpdateMenuItemSelection();
    }

    void UpdateMenuItemSelection() {
        // 現在選択されているメニュー項目を強調表示する
        for (int i = 0; i < menuItems.Length; i++) {
            if (i == selectedItemIndex) {
                menuItems[i].GetComponent<UnityEngine.UI.Text>().color = Color.black;
                menuItems[i].GetComponent<UnityEngine.UI.Text>().text = "▶" + menuItems[i].GetComponent<UnityEngine.UI.Text>().text;
            } else {
                menuItems[i].GetComponent<UnityEngine.UI.Text>().color = Color.gray;
                menuItems[i].GetComponent<UnityEngine.UI.Text>().text = menuItems[i].GetComponent<UnityEngine.UI.Text>().text.Replace("▶", "");
            }
        }

        // Dataクラスから情報を取得し、UIに表示する
        //turnText.text = "Turn: " + gameData.turnCount.ToString();
        /*foodText.text = "Food: " + gameData.items[1].itemCount.ToString();
        knifeText.text = "Knife: " + gameData.items[2].itemCount.ToString();
        pickaxeText.text = "Pickaxe Level: " + gameData.items[7].itemCount.ToString();
        ironText.text = "Iron: " + gameData.items[3].itemCount.ToString();
        goldText.text = "Gold: " + gameData.items[4].itemCount.ToString();*/
    }
}