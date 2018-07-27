using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitboxController : MonoBehaviour {
	
	private BasePlayerVariables vars;
	private int playerNumber;

	void Start () {
		vars = GetComponentInParent<BasePlayerVariables>();
		playerNumber = vars.playerNumberInt;
	}
	
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D other) {
		BaseBulletVariables bulletVars = other
			.gameObject
			.GetComponent(typeof(BaseBulletVariables)) as BaseBulletVariables;
		if (bulletVars != null && bulletVars.playerNumberInt != playerNumber) {
			//a bullet that isn't owned by the player
			Destroy(gameObject);
		}
	}
}
