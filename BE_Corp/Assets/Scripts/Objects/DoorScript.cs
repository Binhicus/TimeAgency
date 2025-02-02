﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Rendering.Universal;
using EPOOutline;
using Fungus;

public enum Direction { Salon, Chambre}
[RequireComponent (typeof(Animator))]


public class DoorScript : MonoBehaviour, IClicked, IAction
{
    public Direction direction;
    public SplCameraShake cameraShake;
    [SerializeField] private GameObject Panel;
    [SerializeField] private Material glitchEffectOn, glitchEffectOff;
    [SerializeField] ForwardRendererData rendererData;
    //public Material glitchMat;

    private Animator DoorAnimator ;
    [SerializeField] private string PlayerPrefNameState ;
    [SerializeField] private string LeaveStepNameRef ;
    private GameObject LeaveStepRef ;
    private bool MouseOver = false ;
    private bool DoorIsOpen = false ;
    private bool AsCameraShake = false ;
    public AudioSource openingSound;
    public BlockReference porteClose;

    public List<ActionWheelChoiceData> ListInteractPossible = new List<ActionWheelChoiceData>();

    void Awake()
    {
        DoorAnimator = this.GetComponent<Animator>();     
        if(GameObject.Find(LeaveStepNameRef) != null) LeaveStepRef = GameObject.Find(LeaveStepNameRef);   

        if(GameObject.Find("Panel") != null)
        {
            Panel = GameObject.Find("Panel");
            Panel.SetActive(false);            
        }

        if(this.gameObject.GetComponent<SplCameraShake>() != null)
        {
            cameraShake = this.gameObject.GetComponent<SplCameraShake>();   
            AsCameraShake = true ;         
        }

        //glitchEffect = GameObject.Find("EffectManager").GetComponent<GlitchyBreak>().glitchMat;
    }

    private void Start() 
    {
        VerifAnimation();
        if (direction == Direction.Chambre)
        {
            this.gameObject.GetComponent<Outlinable>().enabled = false;
        }
    }
    void VerifAnimation()
    {
        if(PlayerPrefs.GetInt(PlayerPrefNameState) == 0)
        {
            LeaveStepRef.GetComponent<DynamicLoad>().DispStep(false);
        } else {
            LeaveStepRef.GetComponent<DynamicLoad>().DispStep(true);
            DoorIsOpen = true ;
            DoorAnimator.SetTrigger("Open");            
        }
    }

    void OnMouseOver()
    {
        MouseOver = true ;

        if (direction == Direction.Chambre)
        {
            if (PlayerPrefs.GetInt("Porte Ouverte") == 0)
            {
                CursorController.Instance.ChangeCursor(CursorType.Object);
                this.gameObject.GetComponent<Outlinable>().enabled = true;
            }
        }

        if(!DoorIsOpen && AsCameraShake && cameraShake.enabled == true)
        {
            if(cameraShake != null) cameraShake.Shaker();
            GlitchyBreak.Instance.GlitchEffectOn();
        }
    }

    void OnMouseExit()
    {
        MouseOver = false ;
        if (direction == Direction.Chambre)
        {
            if (PlayerPrefs.GetInt("Porte Ouverte") == 0)
            {
                CursorController.Instance.ChangeCursor(CursorType.Default);
                this.gameObject.GetComponent<Outlinable>().enabled = false;
            }
        }


        if(!DoorIsOpen && AsCameraShake)
        {
            GlitchyBreak.Instance.GlitchEffectOff();
        }
    }

    /*void GlitchEffectOn()
    {
        var blitFeature = rendererData.rendererFeatures.OfType<UnityEngine.Experiemntal.Rendering.Universal.Blit>().FirstOrDefault();
        Debug.Log("glitched");
        blitFeature.settings.blitMaterial = glitchEffectOn;
        rendererData.SetDirty();
    }

    void GlitchEffectOff()
    {
        var blitFeature = rendererData.rendererFeatures.OfType<UnityEngine.Experiemntal.Rendering.Universal.Blit>().FirstOrDefault();
        Debug.Log("unglitched");
        blitFeature.settings.blitMaterial = glitchEffectOff;
        rendererData.SetDirty();
    }*/

    public void OpenDoorAnimation()
    {
        cameraShake.enabled = false ;
        cameraShake.triggered = false ;
        DoorIsOpen = true ;
        openingSound.Play();
        StartCoroutine(DoorAnimation());
    }
    

    IEnumerator DoorAnimation()
    {
        yield return new WaitForSeconds(2.0f);
        DoorAnimator.SetTrigger("Door Animation");        
        yield return new WaitForSeconds(1.0f);
        LeaveStepRef.GetComponent<DynamicLoad>().DispStep(true);
        GlitchyBreak.Instance.GlitchEffectOff();
    }

    public void OnClickAction()
    {
        if (direction == Direction.Chambre)
        {
            if (PlayerPrefs.GetInt("Porte Ouverte") == 0)
            {
                CursorController.Instance.ActionWheelScript.ChoicesDisplay = ListInteractPossible;
                CursorController.Instance.ActionWheelScript.TargetAction = this;
                CursorController.Instance.ActionWheelScript.gameObject.SetActive(true);
            }
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
        if (direction == Direction.Chambre)
        {
            if (PlayerPrefs.GetInt("Porte Ouverte") == 0)
            {
                porteClose.Execute();
            }
        }
    }

    public void OnQuestion()
    {
    }

    public void OnLook()
    {
    }

    public void OnLunchActionAfterCloseDialogue()
    {
    }
}
