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

	void Start() {
		playerTransform = GetComponent<Transform>();
		horizontalAxis = playerNumber + "Horizontal";
		verticalAxis = playerNumber + "Vertical";
	}
	
	void Update() {
		var xTranslate = Input.GetAxis(horizontalAxis) * Time.deltaTime * movementSpeed;

		var yTranslate = Input.GetAxis(verticalAxis) * Time.deltaTime * movementSpeed;

		if (xTranslate + playerTransform.position.x < xMin 
			|| xTranslate + playerTransform.position.x > xMax) {
			xTranslate = 0;
		}

		if (yTranslate + playerTransform.position.y < yMin 
			|| yTranslate + playerTransform.position.y > yMax) {
			yTranslate = 0;
		}

		transform.Translate(xTranslate, yTranslate, 0);
	}
}
