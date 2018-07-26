using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class used to make base "Bullets". 
 * Will use its own fire rate separate from other spawner types
 **/
public class BaseBulletSpawner : MonoBehaviour {

	public float fireRate;
	public int movementSpeed;

	private Rigidbody2D bullet;
	private float nextFireTime;

	private Transform playerTransform;
	private BasePlayerVariables playerVars;
	private int playerNumber;

	public void Start() {
		bullet = (Rigidbody2D) Resources.Load("BaseBullet", typeof(Rigidbody2D));
		playerTransform = GetComponentInParent<Transform>();
		playerVars = GetComponentInParent<BasePlayerVariables>();
		playerNumber = playerVars.playerNumberInt;
	}

	public void Spawn() {
		if (Time.time > nextFireTime) {
			nextFireTime = Time.time + fireRate;
			bullet = Instantiate(bullet, playerTransform.position, Quaternion.identity)
				as Rigidbody2D;
			if (playerNumber == 1) {
				bullet.velocity = transform.TransformDirection(Vector3.right * movementSpeed);
			} else if (playerNumber == 2) {
				bullet.velocity = transform.TransformDirection(Vector3.left * movementSpeed);
			}
		}
	}

}
