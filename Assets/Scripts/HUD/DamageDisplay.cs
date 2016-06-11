using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageDisplay : MonoBehaviour {
    public GameObject pfText;
    private GameObject text;
    private Text damageText;

    private static DamageDisplay instance;
    public static DamageDisplay Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.Find("DamageCanvas").GetComponent<DamageDisplay>();
            }

            return instance;
        }
    }

    // 데미지 텍스트 생성
    public void CreateDamageText(float _damage, Vector3 _headPos)
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(_headPos);
        text = Instantiate(pfText/*, HeadPosition.transform.position, Quaternion.identity*/) as GameObject;
        GameObject DamageCanvas = GameObject.Find("DamageCanvas");

        text.transform.SetParent(DamageCanvas.transform);

        damageText = text.GetComponent<Text>();
        if (_damage > 0)
        {
            damageText.color = Color.red;
            damageText.text = _damage.ToString("0");
        }
        else if (_damage == 0)
        {
            damageText.color = Color.cyan;
            damageText.text = "miss";
        }
        else
        {
            damageText.color = Color.green;
            damageText.text = "evasion";
        }

        text.transform.position = pos;
    }
}
