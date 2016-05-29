using UnityEngine;
using System.Collections;
using System.Collections.Generic;       // List 사용

using System.Xml;

// 스테이지 정보를 담고 있는 클래스
public class StageInfo
{
    // 불러올 스테이지 번호
    public int stageNumber = 0;

    // 배경화면 갯수 많을 수록 맵이 길어짐
    public int bgCount = 1;

    // 게임 모드
    public GAMEMODE.Gamemode gameMode = GAMEMODE.Gamemode.STORY;

    // 적 관련
    public int basicEnemyCount      = 1;    // 일반 몬스터
    public int strongEnemyCount    = 0;    // 방어 몬스터
    public int insaneEnemyCount     = 0;    // 공격 몬스터
}

public class StageXml
{
    // 스테이지 정보를 저장하는 함수인데 
    // 실 게임에서는 필요가 없어지므로 사용하지 않을 것임
    public static void StageSave(StageInfo _info, string _filePath)
    {
        XmlDocument StageDocument = new XmlDocument();
        StageDocument.Load(_filePath);

        XmlElement StageElement;

        // 파일이 없으면 새로 생성
        if (StageDocument["StageList"] == null)
            StageElement = StageDocument.CreateElement("StageList");
        // 있으면 불러오기
        else
            StageElement = StageDocument["StageList"];

        StageDocument.AppendChild(StageElement);

        XmlElement element = StageDocument.CreateElement("Stage");
        element.SetAttribute("StageNumber", _info.stageNumber.ToString());
        element.SetAttribute("BGCount", _info.bgCount.ToString());
        element.SetAttribute("GameMode", System.Convert.ToInt32(_info.gameMode).ToString());
        element.SetAttribute("BasicEnemy", _info.basicEnemyCount.ToString());
        element.SetAttribute("StrongEnemy", _info.strongEnemyCount.ToString());
        element.SetAttribute("InsaneEnemy", _info.insaneEnemyCount.ToString());
        StageElement.AppendChild(element);

        StageDocument.Save(_filePath);
    }

    // filePath 의 xml 파일을 불러와 값을 입력 시킨다.
    public static List<StageInfo> StageLoad(string _filePath)
    {
        XmlDocument StageDocument = new XmlDocument();

        // 안드로이드에서 불러오기
        if (Application.platform == RuntimePlatform.Android)
        {
            //string strFile = "Stage.xml";
            //string strFilePath = Application.persistentDataPath + "/" + strFile;
            //if (!System.IO.File.Exists(strFilePath))
            //{
            //    WWW wwwUrl = new WWW("jar:file://" + Application.dataPath + "!/assets/" + strFile);
            //    System.IO.File.WriteAllBytes(strFilePath, wwwUrl.bytes);
            //}
            
            //StageDocument.Load(strFilePath);
            TextAsset textAsset = (TextAsset)Resources.Load("Stage", typeof(TextAsset));
            //XmlDocument xmldoc = new XmlDocument();
            StageDocument.LoadXml(textAsset.text);
        }
        else// if(Application.platform == RuntimePlatform.WindowsEditor)
        {
            // 경로에 해당하는 파일을 불러옴
            StageDocument.Load(_filePath);
        }

        // StageList 에 해당하는 모든 정보를 받아옴
        XmlElement StageListElement = StageDocument["StageList"];

        // 반환해줄 리스트 생성
        List<StageInfo> StageList = new List<StageInfo>();
        
        foreach(XmlElement StageElement in StageListElement.ChildNodes)
        {
            StageInfo info = new StageInfo();
            info.stageNumber = System.Convert.ToInt32(StageElement.GetAttribute("StageNumber"));
            info.bgCount = System.Convert.ToInt32(StageElement.GetAttribute("BGCount"));
            info.gameMode = (GAMEMODE.Gamemode)System.Convert.ToInt32(StageElement.GetAttribute("GameMode"));
            info.basicEnemyCount = System.Convert.ToInt32(StageElement.GetAttribute("BasicEnemy"));
            info.strongEnemyCount = System.Convert.ToInt32(StageElement.GetAttribute("StrongEnemy"));
            info.insaneEnemyCount = System.Convert.ToInt32(StageElement.GetAttribute("InsaneEnemy"));

            // 리스트에 하나씩 추가
            StageList.Add(info);
        }

        return StageList;
    }
}
