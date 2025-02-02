﻿using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineGlitch : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public BlockReference cinematique;
    // Start is called before the first frame update
    void OnEnable()
    {
        StartGlitch();
    }

    public void StartGlitch()
    {
        if (PlayerPrefs.GetInt("Salon Reveal") == 1 && PlayerPrefs.GetInt("Cinematique Salon") == 0)
        {
            //cinematique.Execute();
            CursorController.Instance.BoolFalseSetter();
            playableDirector.enabled = true;
            GlitchyBreak.Instance.GlitchEffectOn();
            Invoke("EndGlitchSalon", 2f);
        }

        if (PlayerPrefs.GetInt("Cuisine Reveal") == 1 && PlayerPrefs.GetInt("Cinematique Cuisine") == 0)
        {
            //cinematique.Execute();
            CursorController.Instance.BoolFalseSetter();
            playableDirector.enabled = true;
            GlitchyBreak.Instance.GlitchEffectOn();
            Invoke("EndGlitchCuisine", 2f);
        }

    }

    public void EndGlitchSalon()
    {
        CursorController.Instance.BoolTrueSetter();
        GlitchyBreak.Instance.GlitchEffectOff();
        MisAJourEffect.Instance.MiseAJour();
        PlayerPrefs.SetInt("Cinematique Salon", 1);
    }
    public void EndGlitchCuisine()
    {
        CursorController.Instance.BoolTrueSetter();
        GlitchyBreak.Instance.GlitchEffectOff();
        MisAJourEffect.Instance.MiseAJour();
        PlayerPrefs.SetInt("Cinematique Cuisine", 1);
    }
}
