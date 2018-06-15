using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, yMin, yMax;
}

public class PlayerController : MonoBehaviour {

	public int movementSpeed;
	public string playerNumber;

	public Boundary bounds;

	private string horizontalAxis;
	private string verticalAxis;

	private Transform playerTransform;
	private Transform spriteTransform;

	private float horizontalLowerLimit;
	private float horizontalUpperLimit;
	private float verticalLowerLimit;
	private float verticalUpperLimit;

	void Start() {
		playerTransform = GetComponent<Transform>();
		horizontalAxis = playerNumber + "Horizontal";
		verticalAxis = playerNumber + "Vertical";

		var childComponents = GetComponentsInChildren<Transform>();

		foreach (Transform transform in childComponents) {
			if (transform.tag.Equals("PlayerSprite")) {
				spriteTransform = transform;
			}
		}

		horizontalLowerLimit = bounds.xMin + (spriteTransform.localScale.x / 2);
		horizontalUpperLimit = bounds.xMax - (spriteTransform.localScale.x / 2);
		verticalLowerLimit = bounds.yMin + (spriteTransform.localScale.y / 2);
		verticalUpperLimit = bounds.yMax - (spriteTransform.localScale.y / 2);
	}

	/*
	 * Player movement applies movement translation then clamps the final position to
	 * inside the defined limits 
	 */
	void Update() {
		//Translate ranges from -.33 to .33
		var xTranslate = Input.GetAxis(horizontalAxis) * Time.deltaTime * movementSpeed;

		var yTranslate = Input.GetAxis(verticalAxis) * Time.deltaTime * movementSpeed;

		transform.Translate(xTranslate, yTranslate, 0);

		//Why isn't there the weird bouncing effect if I'm applying this after the translation
		Vector3 clampedPosition = transform.position;
		clampedPosition.x = Mathf.Clamp(playerTransform.position.x, 
			horizontalLowerLimit, 
			horizontalUpperLimit);
		clampedPosition.y = Mathf.Clamp(playerTransform.position.y, 
			verticalLowerLimit, 
			verticalUpperLimit);
		transform.position = clampedPosition;
	}
}
