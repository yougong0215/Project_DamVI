using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StageEnum
{
    stage1,
    stage2,
    stage3,
    stage4
}
public class StageButton : MonoBehaviour
{
    public List<Stage> stageInfos = new List<Stage>();

    public void Click(GameObject stageInfo)
    {
        for(int i = 0; i < stageInfos.Count; i++)
        {
            if (stageInfos[i].stage == stageInfo.GetComponent<Stage>().stage)
            {
                stageInfos[i].gameObject.SetActive(true);
            }
            else
            {
                stageInfos[i].gameObject.SetActive(false);
            }
        }
    }
}
