using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : Singleton<SceneChangeManager>
{
    public const string FARMSCENE = "TestFarm";
    public const string HOUSESCENE = "TestInHouse";

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
        //StringBuilder로 Refactor 필요
        SceneManager.LoadScene(scenename + "Scene");


        //후처리

    }

    
}
