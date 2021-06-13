using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathZone : MonoBehaviour
{
	[SerializeField] private GameObject deathPanel = default;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			collision.GetComponent<PlayerController2D>().enabled = false;
			deathPanel.SetActive(true);
		}
	}

	public void ReloadScene()
	{
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadSceneAsync(scene.buildIndex);
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawCube(transform.position, transform.localScale);
	}
}
