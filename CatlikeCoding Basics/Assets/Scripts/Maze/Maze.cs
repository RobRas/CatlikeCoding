using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maze {
	public class Maze : MonoBehaviour {
		[SerializeField] IntVector2 size;
		[SerializeField] MazeCell cellPrefab;
		[SerializeField] float generationStepDelay;

		[SerializeField] MazePassage passagePrefab;
		[SerializeField] MazeDoor doorPrefab;
		[SerializeField] MazeWall[] wallPrefabs;

		[Range(0f, 1f)]
		[SerializeField] float doorProbability;
		
		private MazeCell[,] cells;

		public IntVector2 RandomCoordinates {
			get {
				return new IntVector2(Random.Range(0, size.x), Random.Range(0, size.z));
			}
		}

		public MazeCell GetCell(IntVector2 coordinates) {
			return cells[coordinates.x, coordinates.z];
		}

		public IEnumerator Generate() {
			WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
			cells = new MazeCell[size.x, size.z];
			List<MazeCell> activeCells = new List<MazeCell>();
			DoFirstGenerationStep(activeCells);
			while (activeCells.Count > 0) {
				yield return delay;
				DoNextGenerationStep(activeCells);
			}
		}

		public bool ContainsCoordinates(IntVector2 coordinate) {
			return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
		}

		private MazeCell CreateCell(IntVector2 coordinates) {
			MazeCell newCell = Instantiate<MazeCell>(cellPrefab);
			cells[coordinates.x, coordinates.z] = newCell;
			newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
			newCell.transform.parent = transform;
			newCell.transform.localPosition = new Vector3(coordinates.x - size.x * 0.5f + 0.5f, 0f, coordinates.z - size.z * 0.5f + 0.5f);
			newCell.coordinates = coordinates;
			return newCell;
		}

		private void DoFirstGenerationStep(List<MazeCell> activeCells) {
			activeCells.Add(CreateCell(RandomCoordinates));
		}

		private void DoNextGenerationStep(List<MazeCell> activeCells) {
			int currentIndex = activeCells.Count - 1;
			MazeCell currentCell = activeCells[currentIndex];
			if (currentCell.IsFullyInitialized) {
				activeCells.RemoveAt(currentIndex);
				return;
			}
			MazeDirection direction = currentCell.RandomUninitializedDirection;
			IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();
			if (ContainsCoordinates(coordinates)) {
				MazeCell neighbor = GetCell(coordinates);
				if (neighbor == null) {
					Debug.Log(coordinates);
					neighbor = CreateCell(coordinates);
					CreatePassage(currentCell, neighbor, direction);
					activeCells.Add(neighbor);
				} else {
					CreateWall(currentCell, neighbor, direction);
				}
			} else {
				CreateWall(currentCell, null, direction);
			}
		}

		private void CreatePassage(MazeCell cell, MazeCell otherCell, MazeDirection direction) {
			MazePassage prefab = Random.value < doorProbability ? doorPrefab : passagePrefab;
			MazePassage passage = Instantiate<MazePassage>(prefab);
			passage.Initialize(cell, otherCell, direction);
			passage = Instantiate<MazePassage>(prefab);
			passage.Initialize(otherCell, cell, direction.GetOpposite());
		}

		private void CreateWall(MazeCell cell, MazeCell otherCell, MazeDirection direction) {
			MazeWall wall = Instantiate<MazeWall>(wallPrefabs[Random.Range(0, wallPrefabs.Length)]);
			wall.Initialize(cell, otherCell, direction);
			if (otherCell != null) {
				wall = Instantiate<MazeWall>(wallPrefabs[Random.Range(0, wallPrefabs.Length)]);
				wall.Initialize(otherCell, cell, direction.GetOpposite());
			}
		}
	}
}