using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class UIManager : Singleton<UIManager>
{
    public const string UIPrefabPath = "UI/Prefabs/";

    private bool _isCleaning;
    private Dictionary<string, UIBase> _uiDictionary = new Dictionary<string, UIBase>();

    //이걸통해 팝업ui관리
    public Stack<UIPopup> uiStack=new Stack<UIPopup>();


    private Stack<UIPopup> _tempStack=new Stack<UIPopup>();

    Canvas _canvas;
    private void OnEnable()
    {
        _canvas = FindObjectOfType<Canvas>();
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }


    

    // ================================
    // UI 관리
    // ================================
    public void OpenUI<T>() where T : UIBase
    {
        var ui = GetUI<T>();
        ui?.OpenUI();
       
    }

    //그냥 일반 닫기
    public void CloseUI<T>() where T : UIBase
    {
        if (IsExistUI<T>())
        {
            var ui = GetUI<T>();
            ui?.CloseUI();
        }
    }


    //토글 기능 필요시
    public void ToggleUI<T> () where T : UIBase
    {
        var ui = GetUI<T>();
        if (ui.IsActiveInHierarchy())
        {
            ui.CloseUI();
        }
        else
        {
            ui.OpenUI();
        }
    }




    //맨 위부터 닫기(esc키와 바인딩)
    public void CloseTopPopUpUI()
    {
        while(uiStack.Count > 0)
        {
            var ui = uiStack.Pop();
            if(ui.IsActiveInHierarchy()==true)
            {
                ui.gameObject.SetActive(false);
                return;
            }
            
        }
        
    }




    public T GetUI<T>() where T : UIBase
    {
        if (_isCleaning) return null;

        string uiName = GetUIName<T>();

        UIBase ui;
        if (IsExistUI<T>())
            ui = _uiDictionary[uiName];
        else
            ui = CreateUI<T>();

        return ui as T;
    }

    private T CreateUI<T>() where T : UIBase
    {
        if (_isCleaning) return null;

        string uiName = GetUIName<T>();
        if (_uiDictionary.TryGetValue(uiName, out var prevUi) && prevUi != null)
        {
            Destroy(prevUi.gameObject);
            _uiDictionary.Remove(uiName);
        }

        // 1. 프리팹 로드
        string path = GetPath<T>();
        GameObject prefab = Resources.Load<GameObject>(path);
        if (prefab == null)
        {
            Debug.LogError($"[UIManager] Prefab not found: {path}");
            return null;
        }

        // 2. 인스턴스 생성
        GameObject go = Instantiate(prefab, _canvas.transform, false);

        // 3. 컴포넌트 획득
        T ui = go.GetComponent<T>();
        if (ui == null)
        {
            Debug.LogError($"[UIManager] Prefab has no component : {uiName}");
            Destroy(go);
            return null;
        }

        // 4. Dictionary 등록
        _uiDictionary[uiName] = ui;

        return ui;
    }

    public bool IsExistUI<T>() where T : UIBase
    {
        string uiName = GetUIName<T>();
        return _uiDictionary.TryGetValue(uiName, out var ui) && ui != null;
    }


    // ================================
    // path 헬퍼
    // ================================
    private string GetPath<T>() where T : UIBase
    {
        return UIPrefabPath + GetUIName<T>();
    }

    private string GetUIName<T>() where T : UIBase
    {
        return typeof(T).Name;
    }


    // ================================
    // 리소스 정리
    // ================================
    private void OnSceneUnloaded(Scene scene)
    {
        CleanAllUIs();
        StartCoroutine(CoUnloadUnusedAssets());
    }

    private void CleanAllUIs()
    {
        if (_isCleaning) return;
        _isCleaning = true;

        try
        {
            foreach (var ui in _uiDictionary.Values)
            {
                if (ui == null) continue;
                // Close 프로세스 추가 가능
                Destroy(ui.gameObject);
            }
            _uiDictionary.Clear();
        }
        finally
        {
            _isCleaning = false;
        }
    }

    // UI 뿐만 아니라 전체 오브젝트 관리 시스템측면에서도 있으면 좋음
    private IEnumerator CoUnloadUnusedAssets()
    {
        yield return Resources.UnloadUnusedAssets();
        System.GC.Collect();
    }
}