using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class RadioScript : ClickableObject,IClicked, IAction
{
    public BlockReference etatZero, etatUn, etatDeux, etatTrois, remarqueClient;

    public AudioSource Son;

    private ScreenShake camShake;

    public GameObject CameraActivate, AreaCam ;
    public GameObject TexteMeteo;

    public List<ActionWheelChoiceData> ListInteractPossible = new List<ActionWheelChoiceData>() ;

    public List<ActionWheelChoiceData> ListInteractPossible2 = new List<ActionWheelChoiceData>() ;

    public List<ActionWheelChoiceData> ListInteractPossible3 = new List<ActionWheelChoiceData>() ;




    void Awake()
    {       
        CameraActivate = GameObject.Find("---- CAMERAS ----").GetComponent<CameraContainerScript>().CameraRadio;

         if(PlayerPrefs.GetInt("Antenne Branchée")==0&&PlayerPrefs.GetInt("PileDansRadio")==0);
         {
            TexteMeteo.SetActive(false);
         }
         if(PlayerPrefs.GetInt("Antenne Branchée")==1&&PlayerPrefs.GetInt("PileDansRadio")==1);
         {
            TexteMeteo.SetActive(true);
         }


         //PlayerPrefs.SetInt("PileDansRadio",1);
         //PlayerPrefs.SetInt("Antenne Branchée",1);
         //PlayerPrefs.SetInt("Parapluie",1);
         
    }

    private void Start() 
    {
        TexteMeteo=GameObject.Find("Nord meteo");
    }

    void LookZone()
    {

         if(PlayerPrefs.GetInt("Antenne Branchée")==1&&PlayerPrefs.GetInt("PileDansRadio")==1&&PlayerPrefs.GetInt("Parapluie")==0)
         {
        AreaCam.SetActive(false);
            Debug.Log("Go");
        CameraActivate.SetActive(true);

        GameObject[] IndiceZoneCollider ;
        IndiceZoneCollider = GameObject.FindGameObjectsWithTag("Indice Zone");

        foreach (GameObject GameCol in IndiceZoneCollider)
        {
           GameCol.GetComponent<BoxCollider>().enabled = false ;
        }

        StartCoroutine(coroutineA());
        }
    }

    void Update()
    {
        
    }



    public void OnClickAction()
    {
        /* if(PlayerPrefs.GetFloat("Parapluie")==0)
        {
            CursorController.Instance.ActionWheelScript.ChoicesDisplay = ListInteractPossible ;
        }*/

        if(PlayerPrefs.GetInt("PileDansRadio")==0&&PlayerPrefs.GetInt("Antenne Branchée")==0&&PlayerPrefs.GetInt("Parapluie")==0)
        {
            Debug.Log("RIEN EST MIT");
            CursorController.Instance.ActionWheelScript.ChoicesDisplay = ListInteractPossible ;
        }

        if(PlayerPrefs.GetInt("PileDansRadio")==1&&PlayerPrefs.GetInt("Antenne Branchée")==1&&PlayerPrefs.GetInt("Parapluie")==0)
        {
            Debug.Log("TOUT EST MIT");
            CursorController.Instance.ActionWheelScript.ChoicesDisplay = ListInteractPossible2 ;
            TexteMeteo.GetComponent<Animator>().SetInteger("PileAntenne",1);
        }
         if(PlayerPrefs.GetInt("PileDansRadio")==1&&PlayerPrefs.GetInt("Antenne Branchée")==0&&PlayerPrefs.GetInt("Parapluie")==0)
        {
            Debug.Log("JUSTE PILE");
            CursorController.Instance.ActionWheelScript.ChoicesDisplay = ListInteractPossible ;
        }
        if(PlayerPrefs.GetInt("PileDansRadio")==0&&PlayerPrefs.GetInt("Antenne Branchée")==1&&PlayerPrefs.GetInt("Parapluie")==0)
        {
            Debug.Log("JUSTE ANTENNE");
            CursorController.Instance.ActionWheelScript.ChoicesDisplay = ListInteractPossible ;
        }

        if(PlayerPrefs.GetInt("Parapluie")==1)
        {
            CursorController.Instance.ActionWheelScript.ChoicesDisplay = ListInteractPossible3 ;
        }
        
        
        
         //CursorController.Instance.ActionWheelScript.ChoicesDisplay = ListInteractPossible2 ;
        
       
        CursorController.Instance.ActionWheelScript.TargetAction = this;
        CursorController.Instance.ActionWheelScript.gameObject.SetActive(true);
    }

    void DisplayInspection()
    {
        CursorController.Instance.ActionWheelScript.DialogueDisplayer.SetActive(true);
    }

    void DisplayDialogue()
    {
        CursorController.Instance.ActionWheelScript.DialogueDisplayer.GetComponent<DialogueControllerScript>().TargetAction = this ;
        CursorController.Instance.ActionWheelScript.DialogueDisplayer.GetComponent<DialogueControllerScript>().LunchActionAfterClose = true ;
       // CursorController.Instance.ActionWheelScript.DialogueDisplayer.SetActive(true);
        remarqueClient.Execute();
    }

    public void DialogRadio()
    {
        if(PlayerPrefs.GetInt("Antenne Branchée") == 0 && PlayerPrefs.GetInt("PileDansRadio") == 0)
        {
            etatZero.Execute();
        }

        if (PlayerPrefs.GetInt("Antenne Branchée") == 1 && PlayerPrefs.GetInt("PileDansRadio") == 0)
        {
            etatUn.Execute();
        }

        if (PlayerPrefs.GetInt("Antenne Branchée") == 0 && PlayerPrefs.GetInt("PileDansRadio") == 1)
        {
            etatDeux.Execute();
        }

        if (PlayerPrefs.GetInt("Antenne Branchée") == 1 && PlayerPrefs.GetInt("PileDansRadio") == 1)
        {
            etatTrois.Execute();
        }

    }

    IEnumerator coroutineA()
    {
        
        yield return new WaitForSeconds(0.6f);
        TexteMeteo.SetActive(true);
        TexteMeteo.GetComponent<Animator>().SetBool("Zero", false);
       
    }



    public void OnOpen() {Debug.Log("Open") ;}
    public void OnClose() {Debug.Log("Close") ;}
    public void OnTake() {Debug.Log("Take") ;}
    public void OnUse() 
    {
        LookZone();
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
    }
    public void OnInspect() {DialogRadio(); }
    public void OnQuestion() {DisplayDialogue(); }
    public void OnLook() {}


    public void OnLunchActionAfterCloseDialogue() { }
    

}

