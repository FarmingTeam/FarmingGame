using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameResource : MonoBehaviour
{
    
    enum SuccessStatus
    {
        Fail,
        Good,
        VeryGood

    }
    //캐릭터한테 일단 월드 캔버스 달기

    [SerializeField] RectTransform barRect;
    [SerializeField] RectTransform goodRect;
    [SerializeField] RectTransform perfectRect;
    [SerializeField] RectTransform movingNeedle;
    [SerializeField] Image gageBar;
    [SerializeField] TextMeshProUGUI successText;
    [SerializeField] GameObject successIcon;
    float speed = 3f;
    float half;
    bool isMiniGameOn = false;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            isMiniGameOn = true;
            StartCoroutine( StartMiniGame());
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            PressStop();
        }
        


    }

    IEnumerator StartMiniGame()
    {
        
        successText.SetText(string.Empty);
        half = barRect.rect.width * 0.5f;
        float time = 0f;
        
        while (isMiniGameOn)
        {
            time += Time.deltaTime * speed;
            float x = Mathf.PingPong(time, barRect.rect.width) - half;
            movingNeedle.localPosition = new Vector3(x, 0, 0);
            yield return null;
        }
    }

    void PressStop()
    {
        isMiniGameOn = false;
        var status= CheckSuccessStatus( CheckNeedlePosition());
        successText.SetText(status.ToString());

        if(gageBar.fillAmount>=1f)
        {
            ReachExpMax();
        }
    }


    float CheckNeedlePosition()
    {

        return movingNeedle.localPosition.x;
    }


    SuccessStatus CheckSuccessStatus(float needleX)
    {
        float perfectRange = perfectRect.sizeDelta.x * 0.5f;
        float goodRange = goodRect.sizeDelta.x * 0.5f;

        float needleAbs= Mathf.Abs(needleX);
        
        //만약 멈춘 범위가 verygood미면 
        if(needleAbs<=perfectRange)
        {
            
            gageBar.fillAmount += 0.3f;
            
            return SuccessStatus.VeryGood;
        }
        else if(needleAbs<=goodRange)
        {
            Debug.Log("굿");
            gageBar.fillAmount += 0.2f;
            return SuccessStatus.Good;
        }
        else
        {
            Debug.Log("실패");
            
            return SuccessStatus.Fail;
        }
    }


    void ReachExpMax()
    {
        gageBar.fillAmount = 0f;
        successText.SetText("Complete!");
        Invoke(nameof(TurnOff), 1.5f);
    }

    
    void TurnOff()
    {
        barRect.gameObject.SetActive(false);
        
    }

}
