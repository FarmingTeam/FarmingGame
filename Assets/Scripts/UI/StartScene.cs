using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public GameObject startbt;
    public GameObject Newcontinuebt;
    public GameObject Optionbox;
    public GameObject savebox;


    public void Awake()
    {
        startbt.SetActive(true);
        Newcontinuebt.SetActive(false);
        Optionbox.SetActive(false);
        savebox.SetActive(false);
    }

    public void OnClickstart()
    {
        startbt.SetActive(false);
        Newcontinuebt.SetActive(true);
        savebox.SetActive(false);
    }
    public void OnClicknewstart()
    {
        //SceneManager.LoadScene("PlayerTest");
    }

    public void OnOptionboxbt()
    { 
        Optionbox.SetActive(true);
    }

    public void SaveSelection()
    {
        savebox.SetActive(true);

    }



}
