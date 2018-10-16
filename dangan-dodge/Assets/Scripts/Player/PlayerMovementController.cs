using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour {
	public float movementSpeed;

	private string horizontalAxisName;
	private string verticalAxisName;

	private Transform playerTransform;

	private float horizontalLowerLimit;
	private float horizontalUpperLimit;
	private float verticalLowerLimit;
	private float verticalUpperLimit;

	void Start() {
		BasePlayerVariables vars = GetComponent<BasePlayerVariables>();
		playerTransform = GetComponent<Transform>();
		horizontalAxisName = vars.playerNumberString + "Horizontal";
		verticalAxisName = vars.playerNumberString + "Vertical";

		Boundary arenaBounds = _GLOBAL_CONSTANTS.getPlayerBoundary(vars.playerNumberString);
        Vector3 spriteBounds = GetComponentInChildren<SpriteRenderer>().bounds.size;

        horizontalLowerLimit = arenaBounds.xMin + (spriteBounds.x / 2);
		horizontalUpperLimit = arenaBounds.xMax - (spriteBounds.x / 2);
		verticalLowerLimit = arenaBounds.yMin + (spriteBounds.y / 2);
		verticalUpperLimit = arenaBounds.yMax - (spriteBounds.y / 2);
	}

	/*
	 * Player movement applies movement translation then clamps the final position to
	 * inside the defined limits 
	 */
	void Update() {
		//Translate ranges from -.33 to .33
		var xTranslate = Input.GetAxis(horizontalAxisName) * Time.deltaTime * movementSpeed;
		var yTranslate = Input.GetAxis(verticalAxisName) * Time.deltaTime * movementSpeed;
		//TODO: look into removing this early translate call
		transform.Translate(xTranslate, yTranslate, 0);
		Vector3 clampedPosition = transform.position;
		clampedPosition.x = Mathf.Clamp(playerTransform.position.x, horizontalLowerLimit, horizontalUpperLimit);
		clampedPosition.y = Mathf.Clamp(playerTransform.position.y, verticalLowerLimit, verticalUpperLimit);
		transform.position = clampedPosition;
	}
}