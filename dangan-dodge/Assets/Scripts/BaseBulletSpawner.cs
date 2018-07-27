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
	private BasePlayerVariables vars;
	private int playerNumber;
	private Transform playerSpriteTransform;

	public void Start() {
		bullet = (Rigidbody2D) Resources.Load("BaseBullet", typeof(Rigidbody2D));
		playerTransform = GetComponentInParent<Transform>();
		vars = GetComponentInParent<BasePlayerVariables>();
		playerNumber = vars.playerNumberInt;
		playerSpriteTransform = vars.getPlayerSpriteTransform();
	}

	public void Spawn() {
		if (Time.time > nextFireTime) {
			nextFireTime = Time.time + fireRate;
			Vector3 spawnPosition = playerTransform.position;
			Vector3 spawnDirection = Vector3.zero;

			if (playerNumber == 1) {
				spawnPosition = playerTransform.position 
					+ new Vector3(playerSpriteTransform.localScale.x / 2, 0, 0);
				spawnDirection = Vector3.right * movementSpeed;
			} else if (playerNumber == 2) {
				spawnPosition = playerTransform.position 
					- new Vector3(playerSpriteTransform.localScale.x / 2, 0, 0);
				spawnDirection = Vector3.left * movementSpeed;
			}

			Rigidbody2D spawnedBullet = Instantiate(
				bullet, 
				spawnPosition, 
				Quaternion.identity) as Rigidbody2D;
			spawnedBullet.velocity = transform.TransformDirection(spawnDirection);
		}
	}

}
