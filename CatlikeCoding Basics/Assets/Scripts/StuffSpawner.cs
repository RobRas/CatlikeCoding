using UnityEngine;

public class StuffSpawner : MonoBehaviour {
	[SerializeField] float timeBetweenSpawns = 0.1f;
	[SerializeField] float velocity;
	[SerializeField] Stuff[] stuffPrefabs;

	float timeSinceLastSpawn;

	void FixedUpdate() {
		timeSinceLastSpawn += Time.fixedDeltaTime;
		if (timeSinceLastSpawn >= timeBetweenSpawns) {
			timeSinceLastSpawn -= timeBetweenSpawns;
			SpawnStuff();
		}
	}

	void SpawnStuff() {
		Stuff prefab = stuffPrefabs[Random.Range(0, stuffPrefabs.Length)];
		Stuff spawn = Instantiate<Stuff>(prefab);
		spawn.transform.localPosition = transform.position;
		spawn.Body.velocity = transform.up * velocity;
	}
}