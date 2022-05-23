﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntreeManager : MonoBehaviour
{

    public GameObject fog, epouvantail, porteManteau, Porte, Pas, Tournevis, antenne;


    private void OnEnable()
    {
        epouvantail = GameObject.Find("Scarecrow");
        porteManteau = GameObject.Find("Coat Rack");
        fog = GameObject.Find("Gray Volume Fog");
        Tournevis = GameObject.Find("Tournevis");
        antenne = GameObject.Find("Antenne");
        EntreeLoader();
    }
    public void EpouvantailState()
    {
        if (PlayerPrefs.GetInt("Scarecrow") == 1)
        {
            epouvantail.SetActive(false);
            porteManteau.SetActive(true);
            porteManteau.GetComponent<Animator>().enabled = false;
        }
    }

    public void TournevisState()
    {
        if (PlayerPrefs.GetInt("Tournevis") == 1)
        {
            if(Tournevis != null)
            {
                Tournevis.SetActive(false);
            }
        }
    }

    public void AntenneState()
    {
        if (PlayerPrefs.GetInt("Antenne") == 1)
        {
            if(antenne != null)
            {
                antenne.SetActive(false);
            }

        }
    }

    public void FogState()
    {
        fog = GameObject.Find("Gray Volume Fog");
        if (PlayerPrefs.GetInt("Brume") == 1)
        {
            if(fog != null)
            {
                fog.SetActive(false);
            }
        }
        //ajouter seconde condition pour débloquer la porte
    }

    /*public void PorteChambre()
    {

    }*/

    public void EntreeLoader()
    {
        TournevisState();
        AntenneState();
        FogState();
        EpouvantailState();
    }
}
