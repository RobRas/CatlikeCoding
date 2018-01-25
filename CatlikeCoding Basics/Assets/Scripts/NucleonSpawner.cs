using UnityEngine;

public class NucleonSpawner : MonoBehaviour {
	[SerializeField] float timeBetweenSpawns;
	[SerializeField] float spawnDistance;
	[SerializeField] Nucleon[] nucleonPrefabs;

	float timeSinceLastSpawn;

	void FixedUpdate() {
		timeSinceLastSpawn += Time.fixedDeltaTime;
		if (timeSinceLastSpawn >= timeBetweenSpawns) {
			timeSinceLastSpawn -= timeBetweenSpawns;
			SpawnNucleon();
		}
	}

	void SpawnNucleon() {
		Nucleon prefab = nucleonPrefabs[Random.Range(0, nucleonPrefabs.Length)];
		Nucleon spawn = Instantiate<Nucleon>(prefab);
		spawn.transform.localPosition = Random.onUnitSphere * spawnDistance;
	}
}
