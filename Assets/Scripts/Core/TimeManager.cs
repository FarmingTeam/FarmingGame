using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GameTime
{
    public int date;

    public int hour;
    public int minute;
}


public class TimeManager : Singleton<TimeManager>
{
    public GameTime currentTime;
    public int resetTime { get; private set; } = 2;

    public void Start()
    {
        //만약 저장된 데이터가 있다면 시간정보 추출

        //아니라면 초기날짜로 초기화
        Init();
    }

    public void Init()
    {
        currentTime.date = 1;
        currentTime.hour = 6;
        currentTime.minute = 0;
    }

    public void Init(GameTime savedTime)
    {
        currentTime.date = savedTime.date;
        currentTime.hour = savedTime.hour;
        currentTime.minute = savedTime.minute;
    }

    public void UpdateTime()
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
            else if (currentTime.hour == resetTime)
            {
                StopCoroutine(TimeLogic());
                //Force Day pass
            }
        }
    }

    public IEnumerator TimeLogic()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            UpdateTime();
        }
    }

    public void MoveTomorrow()
    {
        StopCoroutine(TimeLogic());
        if (currentTime.hour >= 6)
            currentTime.date++;
        currentTime.hour = 6;
        currentTime.minute = 0;
        StartCoroutine(TimeLogic());
    }

}
