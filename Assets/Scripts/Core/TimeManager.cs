using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

public struct GameTime
{
    public int date;

    public int hour;
    public int minute;
}


public class TimeManager : Singleton<TimeManager>
{
    public GameTime currentTime;
    [field:SerializeField]public int resetTime { get; private set; } = 2;
    [SerializeField] TimeUI timeUI;
    public float TimeSclae = 1.0f;
    [SerializeField] float ReloadTimeDelay = 2.0f;
    Coroutine currentCorutine;

    public void Start()
    {
        //만약 저장된 데이터가 있다면 시간정보 추출

        //아니라면 초기날짜로 초기화
        Init();
    }
    //Refactor : 삭제
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
            SetTomorrow();
    }

    public void Init()
    {
        currentTime.date = 1;
        currentTime.hour = 6;
        currentTime.minute = 0;
        StopAllCoroutines();
        currentCorutine = StartCoroutine(TimeLogic());
    }

    public void Init(GameTime savedTime)
    {
        currentTime.date = savedTime.date;
        currentTime.hour = savedTime.hour;
        currentTime.minute = savedTime.minute;
        StopAllCoroutines();
        currentCorutine = StartCoroutine(TimeLogic());
    }

    public bool UpdateTime()
    {
        currentTime.minute += 10;
        if (currentTime.minute == 60)
        {
            currentTime.minute = 0;
            currentTime.hour++;
            if (currentTime.hour == 24)
            {
                currentTime.hour = 0;
                currentTime.date++;
                //플레이어 디버프 부여
            }
            //강제 취침
            else if (currentTime.hour == resetTime)
            {
                return false;
            }
        }
        return true;
        
    }

    //Refacotr : 삭제
    public void TimeLog()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("현재 날짜 : ").Append(currentTime.date).Append(" ").Append(currentTime.hour).Append("시 ").Append(currentTime.minute).Append("분");
        Debug.Log(stringBuilder.ToString());
    }

    public IEnumerator TimeLogic()
    {
        do
        {
            timeUI.UpdateUI(currentTime);
            yield return new WaitForSeconds(10 / TimeSclae);
        } while (UpdateTime());
        timeUI.UpdateUI(currentTime);
        SetTomorrow();
    }

    public void SetTomorrow()
    {
        StopCoroutine(currentCorutine);
        if (currentTime.hour >= 6)
            currentTime.date++;
        currentTime.hour = 6;
        currentTime.minute = 0;
        // 맵 리로딩 로직
        currentCorutine = StartCoroutine(EndDay());
    }

    public IEnumerator EndDay()
    {
        Debug.Log("End Day");
        //Refactor : 맵 저장 로직 -> 씬에따라 변수가 바뀌게 설정
        MapSaveManager.Instance.SaveMap("TestFarm");
        yield return new WaitForSeconds(ReloadTimeDelay);
        //Refactor : 맵 로드 로직 -> 씬에따라 변수가 바뀌게 설정
        MapSaveManager.Instance.LoadMap("TestFarm");
        currentCorutine = StartCoroutine(TimeLogic());
    }

    public int GetActualUpdateDate()
    {
        if (currentTime.hour >= 6)
            return currentTime.date;
        return currentTime.date - 1;
    }

}
