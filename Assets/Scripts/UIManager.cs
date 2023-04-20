using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject[] menuItems; // メニュー項目の配列
    public int selectedItemIndex = 0; // 現在選択されているメニュー項目のインデックス

    public PlayerManager playerManager;

    public UnityEngine.UI.Text turnText; // 経過ターンを表示するUIテキスト
    public UnityEngine.UI.Text foodText; // 食糧数を表示するUIテキスト
    public UnityEngine.UI.Text knifeText; // ナイフの数を表示するUIテキスト
    public UnityEngine.UI.Text pickaxeText; // ツルハシのレベルを表示するUIテキスト
    public UnityEngine.UI.Text ironText; // 鉄の数を表示するUIテキスト
    public UnityEngine.UI.Text goldText; // 金の数を表示するUIテキスト
    public UnityEngine.UI.Text displayText; // 金の数を表示するUIテキスト
    public SpriteRenderer displayImage; // 画像を表示するスプライト
    // public Transform cameraTransform;

    private char[] charArray;

    void Start () {
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
        UpdateUI();
    }

    public void DownButton(){
        selectedItemIndex++;
        if (selectedItemIndex >= menuItems.Length) {
            selectedItemIndex = 0;
        }
        UpdateUI();
    }

    public void UpdateUI() {
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

        // PlayerManagerクラスから情報を取得し、UIに表示する
        turnText.text = "Turn: " + playerManager.turnCount.ToString();
        foodText.text = "Food: " + playerManager.itemCount[0].count.ToString();
        knifeText.text = "Knife: " + playerManager.itemCount[1].count.ToString();
        pickaxeText.text = "Pickaxe Level: " + playerManager.itemCount[2].count.ToString();
        ironText.text = "Iron: " +  playerManager.itemCount[3].count.ToString();
        goldText.text = "Gold: " + playerManager.itemCount[4].count.ToString();
    }

    //テキストと画像表示
    public void Display(string displayText, string displayImageName){
            displayImage.sprite = Resources.Load<Sprite>(displayImageName);
            charArray = displayText.ToCharArray();
            StartCoroutine("SetText");
    }

    //TODO : 決定ボタン連打で文字送りスキップ
    IEnumerator SetText()
    {
        foreach (var c in charArray)
        {
            displayText.text = displayText.text + c;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void ResetText(){
        displayText.text = "";
    }



    //TODO : シーン遷移でカメラ移動
    // public void StartScene()
    // {
    //     cameraTransform.position = new Vector3(1388, 480, -830);
    // }
    // public void PlayScene()
    // {
    //     cameraTransform.position = new Vector3(300, 480, -830);
    // }
    // public void CollectionScene()
    // {
    //     cameraTransform.position = new Vector3(1388, -654, -830);
    // }

}