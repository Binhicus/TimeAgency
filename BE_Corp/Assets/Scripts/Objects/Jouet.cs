﻿using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jouet : ClickableObject, IHasItemInteraction, IClicked, IAction
{
    public string nomItem = "Tournevis";
    public string inventoryItemID => nomItem;
    public GameObject piles;
    public BlockReference tip, finito, remarque;
    public List<ActionWheelChoiceData> ListInteractPossible1 = new List<ActionWheelChoiceData>();
    public List<ActionWheelChoiceData> ListInteractPossible2 = new List<ActionWheelChoiceData>() ;

    public void DoItemInteraction()
    {
        piles.SetActive(true);
        PlayerPrefs.SetInt("Piles", 1);
    }

    public void ItemDropAnim() //////////////
    {

    }

    // Start is called before the first frame update
    void Awake()
    {
        if(piles == null)    piles = GameObject.Find("Piles");
        if(PlayerPrefs.GetInt("Piles") == 1)
        {
            this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            this.enabled = false;
        }
    }

    void OnEnable()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickAction()
    {
        if(GameObject.Find("BarbaraDialog") == null && GameObject.Find("AgentDialog") == false)
        {
            if(PlayerPrefs.GetInt("Piles") == 0)
            {
                CursorController.Instance.ActionWheelScript.ChoicesDisplay = ListInteractPossible1 ;
            } else {
                CursorController.Instance.ActionWheelScript.ChoicesDisplay = ListInteractPossible2 ;
            }
            
            CursorController.Instance.ActionWheelScript.TargetAction = this;
            CursorController.Instance.ActionWheelScript.gameObject.SetActive(true);            
        }
    }

    public void OnOpen()
    {
        
    }

    public void OnClose()
    {
        
    }

    public void OnTake()
    {
        
    }

    public void OnUse()
    {
        
    }

    public void OnInspect()
    {
        if (PlayerPrefs.GetInt("Piles") < 1)
        {
            tip.Execute();
        }
        else finito.Execute();

        
    }

    public void OnQuestion()
    {
        remarque.Execute();
    }

    public void OnLook()
    {
        
    }

    public void OnLunchActionAfterCloseDialogue()
    {
        
    }
}
