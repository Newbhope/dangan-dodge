using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBulletController : MonoBehaviour {

	private int playerNumber;

	void Start() {
	}

	void Update() {
		if (playerNumber == 1) {
			transform.Translate(3 * Vector3.right * Time.deltaTime);
		} else if (playerNumber == 2) {
			transform.Translate(3 * Vector3.left * Time.deltaTime);
		}
	}
}
