using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;



public class Enemy : UnitEntity
{
    // 몬스터 종류
    public EnemySpawn.EnemyType type;
    
    // 함수 재정의 -----------------------------
    public override void SaveData()
    {
        EnemyXml.EnemySave(this, Application.dataPath + "/Resources/EnemyList.xml");
    }
    public override void LoadData()
    {
        EnemyXml.EnemyLoad(this, Application.dataPath + "/Resources/EnemyList.xml", unitName);
        currentHealthPoint = healthPoint;
    }

    public override float Attack(UnitEntity unit)
    {
        return base.Attack(unit);
    }

    public void AttributeSet(EnemySpawn.EnemyType _type)
    {
        switch (_type)
        {
            case EnemySpawn.EnemyType.STRONG:
                healthPoint = healthPoint * 1.5f;
                strength = strength * 1.2f;
                power = power * 1.2f;
                break;
            case EnemySpawn.EnemyType.INSANE:
                healthPoint = healthPoint * 2.0f;
                strength = strength * 1.5f;
                power = power * 1.5f;
                break;
        }
        currentHealthPoint = healthPoint;
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
        XmlElement EnemyList = EnemyDocument.CreateElement(_info.unitName);
        EnemyElement.AppendChild(EnemyList);
        XmlElement element = EnemyDocument.CreateElement("enemy");
        element.SetAttribute("Strength", _info.strength.ToString());
        element.SetAttribute("Defence", _info.defence.ToString());
        element.SetAttribute("Power", _info.power.ToString());
        element.SetAttribute("HP", _info.healthPoint.ToString());
        element.SetAttribute("Speed", _info.speed.ToString());
        element.SetAttribute("Evasion", _info.evasion.ToString());
        EnemyList.AppendChild(element);

        EnemyDocument.Save(_filePath);
    }

    // filePath 의 xml 파일을 불러와 값을 입력 시킨다.
    public static void EnemyLoad(Enemy _enemy, string _filePath, string _unitName)
    {
        XmlDocument EnemyDocument = new XmlDocument();
        // 안드로이드에서 불러오기
        if (Application.platform == RuntimePlatform.Android)
        {
            TextAsset textAsset = (TextAsset)Resources.Load("EnemyList", typeof(TextAsset));

            EnemyDocument.LoadXml(textAsset.text);
        }
        else// if(Application.platform == RuntimePlatform.WindowsEditor)
        {
            // 경로에 해당하는 파일을 불러옴
            EnemyDocument.Load(_filePath);
        }
        

        // _unitName 과 똑같은 정보를 받아옴
        XmlElement EnemyListElement = EnemyDocument["EnemyList"][_unitName];

        foreach (XmlElement element in EnemyListElement.ChildNodes)
        {
            _enemy.strength = System.Convert.ToInt32(element.GetAttribute("Strength"));
            _enemy.defence = System.Convert.ToInt32(element.GetAttribute("Defence"));
            _enemy.power = System.Convert.ToInt32(element.GetAttribute("Power"));
            _enemy.healthPoint = System.Convert.ToInt32(element.GetAttribute("HP"));
            _enemy.speed = System.Convert.ToInt32(element.GetAttribute("Speed"));
            _enemy.evasion = System.Convert.ToInt32(element.GetAttribute("Evasion"));
        }
    }
}
