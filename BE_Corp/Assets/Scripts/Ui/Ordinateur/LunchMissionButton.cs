﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class LunchMissionButton : MonoBehaviour
{
    public Image BackgroundColor ;
    public TextMeshProUGUI TextButton ;


    public void LunchMission()
    {
        StartCoroutine(WaitAndLunchTheMission());
    }


    public void PlayerCanPlayThisMission(bool Possible)
    {
        if(!Possible)
        {
            GetComponent<Button>().interactable = false ;
            if(PlayerPrefs.GetInt("Langue") == 0) TextButton.text = "Mission terminée" ;
            if(PlayerPrefs.GetInt("Langue") == 1) TextButton.text = "Mission finish" ;
        } else {
            GetComponent<Button>().interactable = true ;
            if(PlayerPrefs.GetInt("Langue") == 0) TextButton.text = "Commencer la mission" ;
            if(PlayerPrefs.GetInt("Langue") == 1) TextButton.text = "Start Mission" ;
        }
        
        GetComponent<RectTransform>().sizeDelta = new Vector2(TextButton.preferredWidth + 30f, TextButton.preferredHeight + 20f);        
    }

    IEnumerator WaitAndLunchTheMission()
    {
        GameObject.Find("FADE Lunch").GetComponent<Image>().enabled = true ;
        yield return new WaitForSeconds(0.2f);
        GameObject.Find("FADE Lunch").GetComponent<Image>().DOFade(1f, 0.95f) ;
        yield return new WaitForSeconds(1f);
        PlayerPrefs.DeleteAll();

      //  PlayerPrefs.SetInt("Mission1Finish", 1);

        SceneManager.LoadScene("Gameplay");        
        SceneManager.LoadSceneAsync("EntréeTemp", LoadSceneMode.Additive);    
    }

}
