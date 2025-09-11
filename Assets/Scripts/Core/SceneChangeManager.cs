using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : Singleton<SceneChangeManager>
{
    public const string FARMSCENE = "TestFarmScene";
    public const string HOUSESCENE = "TestInHouseScene";

    //How to use
    /*
    public void OnBtnClick()
    {
        SceneChangeManager.Instance.ChangeScene(SceneChangeManager.FARMSCENE);
    }
    */
    public void ChangeScene(string scenename)
    {
        //전처리
        SceneManager.LoadScene(scenename);
        //후처리
    }

    
}
