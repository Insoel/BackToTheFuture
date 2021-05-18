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
        currentTime = CurrentTime.Past;
    }

    // Update is called once per frame
    void Update()
    {
        timeSwapCooldown -= Time.deltaTime;
        timeSwapCooldown = Mathf.Clamp(timeSwapCooldown, 0f, 5f);

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
