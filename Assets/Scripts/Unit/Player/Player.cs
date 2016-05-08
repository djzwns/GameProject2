using UnityEngine;
using System.Collections;

public class Player : UnitEntity {
    private static Player instance;
    public int accuracy = 80;    // 명중률
    public int evasion = 20;     // 회피율

    // 인스턴스 받아옴
    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Player();
                instance.LoadData();
            }

            return instance;
        }
    }

    // 데이터 저장
    public void SaveData()
    {
        PlayerPrefs.SetInt("Defence", defence);
        PlayerPrefs.SetInt("Strength", strength);
        PlayerPrefs.SetInt("Power", power);
        PlayerPrefs.SetFloat("HelthPoint", helthPoint);
        PlayerPrefs.SetFloat("Speed", speed);
        PlayerPrefs.SetInt("Accuracy", accuracy);
        PlayerPrefs.SetInt("Evasion", evasion);
    }

    // 데이터 불러오기
    public void LoadData()
    {
        defence = PlayerPrefs.GetInt("Defence", defence);
        strength = PlayerPrefs.GetInt("Strength", strength);
        power = PlayerPrefs.GetInt("Power", power);
        helthPoint = PlayerPrefs.GetFloat("HelthPoint", helthPoint);
        speed = PlayerPrefs.GetFloat("Speed", speed);
        accuracy = PlayerPrefs.GetInt("Accuracy", accuracy);
        evasion = PlayerPrefs.GetInt("Evasion", evasion);
        currentHelthPoint = helthPoint;
        jumpPower = jumpPower + (speed * 0.01f);
    }

    public override void TakeDamage(int amount)
    {
        // 명중률에 따라 데미지를 주거나 못주거나.
        if (accuracy >= Random.Range(1, 100))
            base.TakeDamage(amount);
        else
        {
            //Debug.Log("빗나감");
            base.TakeDamage(0);
        }
    }
}
