using System.Collections;
using UnityEngine;

namespace Maze {
	public class GameManager : MonoBehaviour {
		[SerializeField] Maze mazePrefab;

		private Maze mazeInstance;

		// Use this for initialization
		void Start () {
			BeginGame();
		}
		
		// Update is called once per frame
		void Update () {
			if (Input.GetKeyDown(KeyCode.Space)) {
				RestartGame();
			}
		}

		private void BeginGame() {
			mazeInstance = Instantiate<Maze>(mazePrefab);
			StartCoroutine(mazeInstance.Generate());
		}

		private void RestartGame() {
			StopAllCoroutines();
			Destroy(mazeInstance.gameObject);
			BeginGame();
		}
	}
}