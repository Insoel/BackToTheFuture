﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSwitchManager : MonoBehaviour
{
    private enum CurrentTime
	{
        Past,
        Present
	}

    [SerializeField] private KeyCode swapTimeKeybind = KeyCode.T;
    [SerializeField] private GameObject pastObject = default;
    [SerializeField] private GameObject presentObject = default;
    [SerializeField] private float timeSwapCooldown = 5f;

    private CurrentTime currentTime = CurrentTime.Past;
    
    // Start is called before the first frame update
    void Start()
    {
        pastObject.SetActive(true);
        presentObject.SetActive(false);
        timeSwapCooldown = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        timeSwapCooldown -= Time.deltaTime;

        if (Input.GetKeyDown(swapTimeKeybind) &&  timeSwapCooldown <= 0f)
		{
            switch(currentTime)
			{
                case CurrentTime.Past:
                    pastObject.SetActive(false);
                    presentObject.SetActive(true);
                    currentTime = CurrentTime.Present;
                    timeSwapCooldown = 5f;
                    break;
                case CurrentTime.Present:
                    pastObject.SetActive(true);
                    presentObject.SetActive(false);
                    currentTime = CurrentTime.Past;
                    timeSwapCooldown = 5f;
                    break;
                default:
                    break;
			}
		}
    }
}