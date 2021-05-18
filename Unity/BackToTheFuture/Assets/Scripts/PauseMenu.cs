using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private enum PauseMenuState
	{
        Open,
        Closed
	}

    [SerializeField] private GameObject pausePanel = default;
    [SerializeField] private KeyCode pauseKey = KeyCode.Escape;

    private PauseMenuState pauseState = PauseMenuState.Closed;

	private void Start()
	{
        ClosePauseMenu();
    }

	// Update is called once per frame
	void Update()
    {
        if (Input.GetKeyDown(pauseKey))
		{
            switch(pauseState)
			{
                case PauseMenuState.Open:
                    ClosePauseMenu();
                    break;
                case PauseMenuState.Closed:
                    OpenPauseMenu();
                    break;
                default:
                    break;
			}
		}
    }

    public void OpenPauseMenu()
	{
        pausePanel.SetActive(true);
        pauseState = PauseMenuState.Open;
        Time.timeScale = 0f;
	}

    public void ClosePauseMenu()
	{
        pausePanel.SetActive(false);
        pauseState = PauseMenuState.Closed;
        Time.timeScale = 1f;
    }

    public void QuitGame()
	{
        Application.Quit();
	}
}
