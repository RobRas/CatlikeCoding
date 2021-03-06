﻿using System.Collections;
using UnityEngine;

namespace Maze {
	public class GameManager : MonoBehaviour {
		[SerializeField] Maze mazePrefab;
		[SerializeField] Player playerPrefab;

		private Maze mazeInstance;
		private Player playerInstance;

		// Use this for initialization
		void Start () {
			StartCoroutine(BeginGame());
		}
		
		// Update is called once per frame
		void Update () {
			if (Input.GetKeyDown(KeyCode.Space)) {
				RestartGame();
			}
		}

		private IEnumerator BeginGame() {
			Camera.main.clearFlags = CameraClearFlags.Skybox;
			Camera.main.rect = new Rect(0f, 0f, 1f, 1f);
			mazeInstance = Instantiate<Maze>(mazePrefab);
			yield return StartCoroutine(mazeInstance.Generate());
			playerInstance = Instantiate<Player>(playerPrefab);
			playerInstance.SetLocation(mazeInstance.GetCell(mazeInstance.RandomCoordinates));
			Camera.main.clearFlags = CameraClearFlags.Depth;
			Camera.main.rect = new Rect(0f, 0f, 0.5f, 0.5f);
		}

		private void RestartGame() {
			StopAllCoroutines();
			Destroy(mazeInstance.gameObject);
			if (playerInstance != null) {
				Destroy(playerInstance.gameObject);
			}
			StartCoroutine(BeginGame());
		}
	}
}