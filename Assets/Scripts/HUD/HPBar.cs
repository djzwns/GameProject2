using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public GameObject HeadPosition; // 체력 바 위치

    public GameObject pfHPBar;      // 프리팹
    private GameObject hpbar;       // 프리팹 인스턴스 시킨 것
    private Slider hpbarSlider;     // hpbar 슬라이더

    public PlayerController playerInfo;
    Enemy enemyInfo;


    // Use this for initialization
    void Start()
    {
        hpbar = Instantiate(pfHPBar/*, HeadPosition.transform.position, Quaternion.identity*/) as GameObject;
        GameObject HPCanvas = GameObject.Find("HPBarCanvas");

        hpbar.transform.SetParent(HPCanvas.transform);

        hpbarSlider = hpbar.GetComponent<Slider>();

        if (playerInfo == null)
            enemyInfo = (Enemy)gameObject.GetComponent<EnemyController>().Enemy;

    }

    // Update is called once per frame
    void Update()
    {
        HPBarUpdate();
    }

    // 체력바 업뎃
    void HPBarUpdate()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(HeadPosition.transform.position);
        hpbar.transform.position = pos;

        if (playerInfo != null)
        {
            hpbarSlider.value = Player.Instance.CurrentHealthPoint / Player.Instance.healthPoint;
        }
        else
            hpbarSlider.value = enemyInfo.CurrentHealthPoint / enemyInfo.healthPoint;
    }

    void OnDisable()
    {
        Destroy(hpbar);
    }
}
