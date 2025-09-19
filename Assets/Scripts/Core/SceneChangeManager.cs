using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneName
{
    StartScene = 0,
    FarmScene,
    HouseScene
}

public struct SceneInfo
{
    public string Name;
    public bool IsMap;

    public SceneInfo(string name, bool ismap)
    {
        Name = name;
        IsMap = ismap;
    }
}


public class SceneChangeManager : Singleton<SceneChangeManager>
{
    public SceneName currentScene;

    protected override void Initialize()
    {
        currentScene = (SceneName) SceneManager.GetActiveScene().buildIndex;
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public readonly Dictionary<SceneName, SceneInfo> SCENENAMEDICT = new Dictionary<SceneName, SceneInfo>()
    {
        {SceneName.StartScene, new SceneInfo("Start", false) },
        {SceneName.FarmScene, new SceneInfo( "TestFarm", true) },
        {SceneName.HouseScene, new SceneInfo("TestInHouse", true)}
    };

    public const string SCENENAMETAIL = "Scene";

    //How to use
    /*
    public void OnBtnClick()
    {
        SceneChangeManager.Instance.ChangeScene(SceneName.<Type>);
    }
    */
    public void ChangeScene(SceneName scenename)
    {
        StringBuilder stringBuilder = new StringBuilder();
        if (!SCENENAMEDICT.ContainsKey(scenename))
        {
            Debug.LogAssertion(stringBuilder.Append(scenename).Append("Does not Exists").ToString());
            return;
        }

        //전처리
        if (SCENENAMEDICT[currentScene].IsMap)
            MapSaveManager.Instance.SaveMap(SCENENAMEDICT[currentScene].Name);

        //씬 로드
        stringBuilder.Append(SCENENAMEDICT[scenename].Name).Append(SCENENAMETAIL);
        SceneManager.LoadScene(stringBuilder.ToString());

        currentScene = scenename;
    }

    //후처리
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SCENENAMEDICT[currentScene].IsMap)
            MapSaveManager.Instance.LoadMap(SCENENAMEDICT[currentScene].Name);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
