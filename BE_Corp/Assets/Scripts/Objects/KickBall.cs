﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickBall : ClickableObject
{
    public float hauteur = 1.0f;
    public float allant = 3.0f;
    public Rigidbody rb;
    public Collider ball;
    public VaseSwitch vaseSwitch;
    public SplCameraShake shaker;
    public AudioSource ballTap;

    void OnEnable()
    {
        ball = gameObject.GetComponent<SphereCollider>();
        shaker = GameObject.Find("CameraShaker").GetComponent<SplCameraShake>();
        rb = GetComponent<Rigidbody>();

        if (PlayerPrefs.GetInt("VaseAndKey") <= 1)
        {
            if (GameObject.Find("Switch") != null)
            {
                vaseSwitch = GameObject.Find("Switch").GetComponent<VaseSwitch>();   
            }
         
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Kicked()
    {
        ballTap.Play();
        rb.AddForce(0, hauteur, allant, ForceMode.Impulse);
    }

    public void Consequence()
    {      
       shaker.Shaker();
       vaseSwitch.KicksCount();
       //Debug.Log("ouille ouille je suis le mur trigger et j'ai mal" + gameObject);
    }
}
