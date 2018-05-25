using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public int movementSpeed;
	public string playerNumber;

	public float xMin;
	public float xMax;

	public float yMin;
	public float yMax;

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

		//weird magic number, maybe look into later
		//works better for .6 scale, approx x = -17.5
		horizontalLowerLimit = xMin + spriteTransform.localScale.x / 2;
		horizontalUpperLimit = xMax - spriteTransform.localScale.x / 2;

		verticalLowerLimit = yMin + spriteTransform.localScale.y / 2;
		verticalUpperLimit = yMax - spriteTransform.localScale.y / 2;
	}
	
	void Update() {
		var xTranslate = Input.GetAxis(horizontalAxis) * Time.deltaTime * movementSpeed;

		var yTranslate = Input.GetAxis(verticalAxis) * Time.deltaTime * movementSpeed;

		if (xTranslate + playerTransform.position.x < horizontalLowerLimit 
			|| xTranslate + playerTransform.position.x > horizontalUpperLimit) {
			xTranslate = 0;
		}

		if (yTranslate + playerTransform.position.y < verticalLowerLimit 
			|| yTranslate + playerTransform.position.y > verticalUpperLimit) {
			yTranslate = 0;
		}

		transform.Translate(xTranslate, yTranslate, 0);
	}
}
