using UnityEngine;
using System.Collections;

// 싱글톤으로 만듬.
public class GAMEMODE : ScriptableObject
{
    public enum Gamemode
    {
        STORY = 0,
        ARCADE = 1
    }
    private static GAMEMODE instance;

    [SerializeField]
    private Gamemode GameMode;

    void Awake()
    {
        GameMode = Gamemode.STORY;
    }

    public static GAMEMODE Instance
    {
        get
        {
            if( instance == null )
                instance = ScriptableObject.CreateInstance<GAMEMODE>() as GAMEMODE;

            return instance;
        }
    }

    // 게임 모드 get set 설정
    public Gamemode gamemode { get { return GameMode; } set { GameMode = value; } }
       
}
