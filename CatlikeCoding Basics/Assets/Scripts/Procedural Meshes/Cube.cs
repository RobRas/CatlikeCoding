using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {
	public int xSize, ySize, zSize;

	private Vector3[] vertices;
	private Mesh mesh;

	private void Awake() {
		StartCoroutine(Generate());
	}

	private IEnumerator Generate() {
		GetComponent<MeshFilter>().mesh = mesh = new Mesh();
		mesh.name = "Procedural Grid";
		WaitForSeconds wait = new WaitForSeconds(0.05f);

		int cornerVertices = 8;
		int edgeVectices = (xSize + ySize + zSize - 3) * 4;
		int faceVectices = (
			(xSize - 1) * (ySize - 1) +
			(xSize - 1) * (zSize - 1) +
			(ySize - 1) * (zSize - 1) * 2);

		vertices = new Vector3[cornerVertices + edgeVectices + faceVectices];

		int v = 0;
		for (int x = 0; x <= xSize; x++) {
			vertices[v++] = new Vector3(x, 0, 0);
			yield return wait;
		}

		yield return wait;
	}

	private void OnDrawGizmos() {
		if (vertices == null) return;
		Gizmos.color = Color.black;
		for (int i = 0; i < vertices.Length; i++) {
			Gizmos.DrawSphere(vertices[i], 0.1f);
		}
	}
}
