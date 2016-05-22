using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class Enemy : UnitEntity
{
    void Awake()
    {
        unitName = "alligator";
        LoadData();
    }
    // 함수 재정의 -----------------------------
    public override void SaveData()
    {
        base.SaveData();
    }
    public override void LoadData()
    {
        base.LoadData();
    }

    public override void Attack(UnitEntity unit)
    {
        base.Attack(unit);
    }
}

public class EnemyXml
{
    // 스테이지 정보를 저장하는 함수인데 
    // 실 게임에서는 필요가 없어지므로 사용하지 않을 것임
    public static void EnemySave(Enemy _info, string _filePath)
    {
        XmlDocument EnemyDocument = new XmlDocument();
        EnemyDocument.Load(_filePath);

        XmlElement EnemyElement;

        // 파일이 없으면 새로 생성
        if (EnemyDocument["EnemyList"] == null)
            EnemyElement = EnemyDocument.CreateElement("EnemyList");
        // 있으면 불러오기
        else
            EnemyElement = EnemyDocument["EnemyList"];

        EnemyDocument.AppendChild(EnemyElement);

        // 몬스터 이름으로 엘리멘트 생성
        XmlElement element = EnemyDocument.CreateElement(_info.unitName);
        EnemyElement.AppendChild(element);
        element.SetAttribute("Strength", _info.strength.ToString());
        element.SetAttribute("Defence", _info.defence.ToString());
        element.SetAttribute("Power", _info.power.ToString());
        element.SetAttribute("HP", _info.healthPoint.ToString());
        element.SetAttribute("Speed", _info.speed.ToString());
        element.SetAttribute("Evasion", _info.evasion.ToString());
        EnemyElement.AppendChild(element);

        EnemyDocument.Save(_filePath);
    }

    // filePath 의 xml 파일을 불러와 값을 입력 시킨다.
    public static List<Enemy> StageLoad(string _filePath, string _unitName)
    {
        XmlDocument EnemyDocument = new XmlDocument();
        // 경로에 해당하는 파일을 불러옴
        EnemyDocument.Load(_filePath);

        // StageList 에 해당하는 모든 정보를 받아옴
        XmlElement EnemyListElement = EnemyDocument[_unitName];

        // 반환해줄 리스트 생성
        List<Enemy> EnemyList = new List<Enemy>();

        foreach (XmlElement StageElement in EnemyListElement.ChildNodes)
        {
            Enemy info = new Enemy();
            info.strength = System.Convert.ToInt32(StageElement.GetAttribute("Strength"));
            info.defence = System.Convert.ToInt32(StageElement.GetAttribute("Defence"));
            info.power = System.Convert.ToInt32(StageElement.GetAttribute("Power"));
            info.healthPoint = System.Convert.ToInt32(StageElement.GetAttribute("HP"));
            info.speed = System.Convert.ToInt32(StageElement.GetAttribute("Speed"));
            info.evasion = System.Convert.ToInt32(StageElement.GetAttribute("Evasion"));

            // 리스트에 하나씩 추가
            EnemyList.Add(info);
        }

        return EnemyList;
    }
}
