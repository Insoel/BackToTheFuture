using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatControl : MonoBehaviour
{
    [SerializeField] private KeyCode tutorialLoad = KeyCode.Alpha1;
    [SerializeField] private KeyCode level01Load = KeyCode.Alpha2;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(tutorialLoad))
		{
            SceneManager.LoadScene("Tutorial");
		}
        else if (Input.GetKeyDown(level01Load))
		{
            SceneManager.LoadScene("Level01");
		}
    }
}
