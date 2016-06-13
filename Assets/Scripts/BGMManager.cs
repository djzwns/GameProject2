using UnityEngine;
using System.Collections;

public class BGMManager : MonoBehaviour {

    public AudioSource source;      // 오디오 소스
    public AudioClip[] clip;        // 오디오 클립 배열

    const int ATTRIBUTE_SCREEN = 3;
    const int STAGE_SCREEN = 5;

    const int STAGE1 = 0;
    const int STAGE2 = 1;
    const int STAGE3 = 2;
    const int STAGE4 = 3;
    const int STAGE5 = 4;
    const int BOSS = 5;
    const int ATTRIBUTE = 6;
    const int MAIN1 = 7;
    const int MAIN2 = 8;

    public void ChangeBGM(int _state, int _currentStage)
    {
        switch (_state)
        {
            case ATTRIBUTE_SCREEN:
                source.clip = clip[ATTRIBUTE];
                break;
            case STAGE_SCREEN:
                int stage = (int)(_currentStage * 0.1f);

                if (stage == STAGE1)
                    source.clip = clip[STAGE1];
                else if (stage == STAGE2)
                    source.clip = clip[STAGE2];
                else if (stage  == STAGE3)
                    source.clip = clip[STAGE3];
                else if (stage  == STAGE4)
                    source.clip = clip[STAGE4];
                else if (stage == STAGE4)
                    source.clip = clip[STAGE4];
                else
                    source.clip = clip[STAGE5];

                stage = _currentStage % 10;
                if(stage == 9)
                    source.clip = clip[BOSS];
                break;
            default:
                if( StageManager.Instance.AchieveStage == 49 )
                    source.clip = clip[MAIN2];
                else
                    source.clip = clip[MAIN1];
                break;
        }
        source.Play();
    }
}
