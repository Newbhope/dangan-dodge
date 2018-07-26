using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class used to make base "Bullets". 
 * Will use its own fire rate separate from other spawner types
 **/
public class BaseBulletSpawner : MonoBehaviour {

	public float fireRate;

	private GameObject bullet;
	private float nextFireTime;
	private Transform playerTransform;

	//Used to give the spawned bullet an owner (don't damage the creator, damage others)
	//TODO: implement at some time
	private int playerNumber;

	public void Start() {
		bullet = (GameObject) Resources.Load("BaseBullet", typeof(GameObject));
		playerTransform = GetComponentInParent<Transform>();
	}

	public void Spawn() {
		if (Time.time > nextFireTime) {
			nextFireTime = Time.time + fireRate;
			Instantiate(bullet, new Vector3(0, 0, 0), Quaternion.identity);
		}
	}

}
