using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	[SerializeField] private string sceneName = default;


	[Header("Title Settings")]
	[SerializeField] private GameObject titleSprite = default;
	[SerializeField] private Transform titleTarget = default;
	[SerializeField] private float titleAnimationTime = 4f;
	[SerializeField] private float titleAnimationDelay = 1f;
	[SerializeField] private LeanTweenType titleEaseType;

	[Header("Menu Settings")]
	[SerializeField] private GameObject menuButtons = default;
	[SerializeField] private Transform menuTarget = default;
	[SerializeField] private float menuAnimationTime = 4f;
	[SerializeField] private float menuAnimationDelay = 1f;
	[SerializeField] private LeanTweenType menuEaseType;

	[Header("Light Settings")]
	[SerializeField] private Light2D lightGO = default;
	[SerializeField] private float lightStartIntensity = 0f;
	[SerializeField] private float lightIntensity = 1f;

	private void Start()
	{
		lightGO.intensity = lightStartIntensity;
		LeanTween.value(gameObject, lightStartIntensity, lightIntensity, 1f).setOnUpdate((float value) => { lightGO.intensity = value; });
		LeanTween.value(gameObject, 0f, 180f, 5f).setOnUpdate((float value) => { lightGO.pointLightInnerAngle = value; }).setEaseInOutBack().setOnComplete(IdleLightAnimation);
		LeanTween.value(gameObject, 0f, 10f, 3f).setOnUpdate((float value) => { lightGO.pointLightInnerRadius = value; }).setEaseInBounce();

		LeanTween.move(titleSprite, titleTarget.position, titleAnimationTime).setDelay(titleAnimationDelay).setEase(titleEaseType);
		LeanTween.move(menuButtons, menuTarget.position, menuAnimationTime).setDelay(menuAnimationDelay).setEase(menuEaseType).setOnComplete(EnableButtons);
	}

	public void StartGame()
	{
		SceneManager.LoadSceneAsync(sceneName);
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	private void IdleLightAnimation()
	{
		LeanTween.value(gameObject, 0.8f, 1.2f, 1f).setOnUpdate((float value) => { lightGO.intensity = value; }).setLoopPingPong().setEaseInOutBounce();
		LeanTween.value(gameObject, 179f, 181f, 1f).setOnUpdate((float value) => { lightGO.pointLightInnerAngle = value; }).setEaseInOutBounce().setLoopPingPong();
		LeanTween.value(gameObject, 9.9f, 10.1f, 1f).setOnUpdate((float value) => { lightGO.pointLightInnerRadius = value; }).setEaseInOutBounce().setLoopPingPong();
	}

	private void EnableButtons()
	{
		Button[] buttons = menuButtons.GetComponentsInChildren<Button>();
		foreach (Button button in buttons)
		{
			button.interactable = true;
		}
	}
}
