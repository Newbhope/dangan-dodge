using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public int movementSpeed;
	public string playerNumber;

	private string horizontalAxis;
	private string verticalAxis;

	// Use this for initialization
	void Start () {
		horizontalAxis = playerNumber + "Horizontal";
		verticalAxis = playerNumber + "Vertical";
	}
	
	// Update is called once per frame
	void Update () {
		var x = Input.GetAxis (horizontalAxis) * Time.deltaTime * movementSpeed;
		var y = Input.GetAxis (verticalAxis) * Time.deltaTime * movementSpeed;

		transform.Translate (x, y, 0);
	}
}
