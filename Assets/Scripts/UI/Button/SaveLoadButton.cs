using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadButton : MonoBehaviour
{
    //Refactor : 변수로 int 삽입해서 세이브 데이터 번호로 로드
    public void OnLoadButtonClicked()
    {
        SceneChangeManager.Instance.ChangeScene(SceneName.FarmScene);
    }
}
