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

	// Use this for initialization
	void Start () {
		horizontalAxis = playerNumber + "Horizontal";
		verticalAxis = playerNumber + "Vertical";
	}
	
	// Update is called once per frame
	void Update () {
		var xTranslate = Input.GetAxis (horizontalAxis) * Time.deltaTime * movementSpeed;
		var yTranslate = Input.GetAxis (verticalAxis) * Time.deltaTime * movementSpeed;

		if (xTranslate + GetComponent<Transform> ().position.x < xMin ||
		    xTranslate + GetComponent<Transform> ().position.x > xMax) {
			xTranslate = 0;
			print ("test");

		}

		if (yTranslate + GetComponent<Transform> ().position.y < yMin ||
			yTranslate + GetComponent<Transform> ().position.y > yMax) {
			yTranslate = 0;
			print ("test");
		}

		transform.Translate (xTranslate, yTranslate, 0);
	}
}
