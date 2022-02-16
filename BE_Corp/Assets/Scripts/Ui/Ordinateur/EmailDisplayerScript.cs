﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class EmailDisplayerScript : MonoBehaviour
{
    public Mail MailDisplay ;
    private string MailBoxAdress ;
    
    [Header ("Display Mail Information")]
    [SerializeField] private Image ColorMailBanner ;
    [SerializeField] private TextMeshProUGUI MailObject ;

    [SerializeField] private Image ContactLogo ;
    [SerializeField] private Image FondContactInitiale ;
    [SerializeField] private TextMeshProUGUI ContactInitiale ;


    [SerializeField] private TextMeshProUGUI ContactAdress ;
    [SerializeField] private TextMeshProUGUI DateReception ;
    [SerializeField] private TextMeshProUGUI Destinataire ;
    

    // Start is called before the first frame update
    void Start()
    {
        MailBoxAdress = GetComponentInParent<ComputerNavigationScript>().MailAdress ;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetMailDisplay(Mail NewMailDisplay)
    {
        if(MailDisplay == null) MailDisplay = NewMailDisplay ;
        else {
            if(MailDisplay == NewMailDisplay) MailDisplay = null ;
            else MailDisplay = NewMailDisplay ;
        }

        SetDisplayer();
    }

    public void SetDisplayer()
    {
        if(MailDisplay != null)
        {
            ColorMailBanner.color = MailDisplay.Account.AccountColor ;
            MailObject.text = MailDisplay.Objet ;

            // Affichage d'un logo ou des initiale 
            if(MailDisplay.Account.LogoContact != null)
            {
                ContactLogo.gameObject.SetActive(true) ;
                ContactLogo.sprite = MailDisplay.Account.LogoContact ;
                FondContactInitiale.gameObject.SetActive(false) ;            
            } else {
                ContactLogo.gameObject.SetActive(false) ;

                FondContactInitiale.gameObject.SetActive(true) ;    
                FondContactInitiale.color = MailDisplay.Account.AccountColor ;     
                ContactInitiale.text = GetInitial(MailDisplay.Account.NameAccount) ;
            }

            ContactAdress.text = MailDisplay.Account.NameAccount + " " + "<"+ MailDisplay.Account.MailAdress +">" ;

            // Affichage Date et Heure : xx/xx/xxxx xx:xx
            DateReception.text = MailDisplay.Date + " " + MailDisplay.Heure ;

            Destinataire.text = "A: " + MailBoxAdress ;

        } else {
            this.gameObject.SetActive(false);
        }
    }

    string GetInitial(string Name)
    {
        string InitialFound = "" ;

        string[] Names = Name.Split(' ') ;

        for (int In = 0; In < Names.Length; In++)
        {
            if(Names[In] != "-") InitialFound = InitialFound + Names[In].Substring(0,1);
            else break ;
        }

        return InitialFound ; 
    }
}
