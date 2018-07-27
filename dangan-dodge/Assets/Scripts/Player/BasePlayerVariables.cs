using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlayerVariables : MonoBehaviour {

	//TODO: maybe refactor all player variables into this?

	public string playerNumberString;
	public int playerNumberInt;

	public Transform spriteTransform;

	void Awake() {
		var childComponents = GetComponentsInChildren<Transform>();

		foreach (Transform transform in childComponents) {
			if (transform.tag.Equals("PlayerSprite")) {
				spriteTransform = transform;
			}
		}
	}
}
