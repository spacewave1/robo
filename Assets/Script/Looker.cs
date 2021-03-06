﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Looker : MonoBehaviour
{
    public globalVars gV;
    public Transform[] destinations;

    public GameObject lookAt;

    public GameObject responsePanel;
    public Text responseText;

    public Dropdown dropdown;
    public GameObject actionPanel;

    public GameObject feedbackPanel;
    public Text feedbackText;

    public float rangeShow = 40f;
    public bool coroutineRunning = false;

    public RoboBehaviour roboBehaviour;
    
    // Start is called before the first frame update
    void Start()
    {
        responseText = responsePanel.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, lookAt.transform.position) <= rangeShow)
        {
            transform.rotation = Quaternion.LookRotation(transform.position - lookAt.transform.position);
        }
    }
    
    public void SetDestination()
    {
        if (dropdown.value == 0)
            Debug.Log("Dropdown Reset");
        else
        {
            gV.destination = destinations[dropdown.value - 1].position;
            gV.asked = true;
            dropdown.value = 0;
            StartFollowDialog();
        }
    }
    public void SetDestination(int destinationCode)
        {
            gV.destination = destinations[destinationCode].position;
                gV.asked = true;
                StartFollowDialog();
         
        }

    public void StartHelpDialog()
    {
        StartCoroutine(HelpDialog());
    }

    public void StartArrivalDialog()
    {
        StartCoroutine(ArrivalDialogue());
    }

    public void StartFollowDialog()
    {
        StartCoroutine(FollowMeStatement());
    }

    public void StartFeedbackDialog()
    {
        StartCoroutine(FeedbackStatement());
    }

    IEnumerator HelpDialog()
    {
        actionPanel.SetActive(false);
        feedbackPanel.SetActive(false);
        responsePanel.SetActive(true);
        responseText.text = "Need Help?";
        yield return new WaitForSeconds(3f);
        actionPanel.gameObject.SetActive(true);
        responsePanel.gameObject.SetActive(false);
        yield return null;
    }

    IEnumerator ArrivalDialogue()
    {
        responsePanel.gameObject.SetActive(true);
        responseText.text = "There you go!";
        yield return new WaitForSeconds(5f);
        responseText.text = "I go back to my origin now!";
        coroutineRunning = false;
        yield return null;
    }

    IEnumerator FollowMeStatement()
    {
        actionPanel.gameObject.SetActive(false);
        feedbackPanel.SetActive(false);
        responsePanel.gameObject.SetActive(true);
        responseText.text = "Follow me";
        yield return new WaitForSeconds(3f);
        responsePanel.gameObject.SetActive(false);
        yield return null;
    }

    IEnumerator FeedbackStatement()
    {
        actionPanel.SetActive(false);
        responsePanel.SetActive(false);
        feedbackPanel.SetActive(true);
        yield return new WaitForSeconds(3f);
        feedbackPanel.SetActive(false);
    }
}
