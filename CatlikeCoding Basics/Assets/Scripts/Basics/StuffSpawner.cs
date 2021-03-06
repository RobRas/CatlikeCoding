﻿using UnityEngine;

public class StuffSpawner : MonoBehaviour {
	[SerializeField] FloatRange timeBetweenSpawns, scale, randomVelocity, angularVelocity;
	[SerializeField] float velocity;
	[SerializeField] Stuff[] stuffPrefabs;
	[HideInInspector] public Material StuffMaterial;

	float timeSinceLastSpawn;
	float currentSpawnDelay;

	void FixedUpdate() {
		timeSinceLastSpawn += Time.fixedDeltaTime;
		if (timeSinceLastSpawn >= currentSpawnDelay) {
			timeSinceLastSpawn -= currentSpawnDelay;
			currentSpawnDelay = timeBetweenSpawns.RandomInRange;
			SpawnStuff();
		}
	}

	void SpawnStuff() {
		Stuff prefab = stuffPrefabs[Random.Range(0, stuffPrefabs.Length)];
		Stuff spawn = prefab.GetPooledInstance<Stuff>();

		spawn.transform.localPosition = transform.position;
		spawn.transform.localScale = Vector3.one * scale.RandomInRange;
		spawn.transform.localRotation = Random.rotation;

		spawn.Body.velocity = transform.up * velocity + Random.onUnitSphere * randomVelocity.RandomInRange;
		spawn.Body.angularVelocity = Random.onUnitSphere * angularVelocity.RandomInRange;

		spawn.SetMaterial(StuffMaterial);
	}
}