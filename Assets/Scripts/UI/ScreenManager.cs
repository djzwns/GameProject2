using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenManager : MonoBehaviour {
    private Text gold;
    private Text arcade;

    protected Player player;

    void Start()
    {
        player = Player.Instance;
        gold = GameObject.Find("Gold").GetComponentInChildren<Text>();
        arcade = GameObject.Find("ArcadePoint").GetComponentInChildren<Text>();

        PlayerMoneyUpdate();
    }

    // 돈, 아케이드 포인트 업데이트
    protected void PlayerMoneyUpdate()
    {
        // 골드, 아케이드 포인트 각각 입력
        gold.text = player.Gold.ToString();
        arcade.text = player.ArcadePoint.ToString();
    }
}
