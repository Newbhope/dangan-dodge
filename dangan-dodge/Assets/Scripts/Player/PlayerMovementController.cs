using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour {
	public float movementSpeed;

	private string horizontalAxisName;
	private string verticalAxisNamee;

	private Transform playerTransform;

	private float horizontalLowerLimit;
	private float horizontalUpperLimit;
	private float verticalLowerLimit;
	private float verticalUpperLimit;

	void Start() {
		BasePlayerVariables vars = GetComponent<BasePlayerVariables>();
		playerTransform = GetComponent<Transform>();
		horizontalAxisName = vars.playerNumberString + "Horizontal";
		verticalAxisNamee = vars.playerNumberString + "Vertical";

		Boundary bounds = _GLOBAL_CONSTANTS.getPlayerBoundary(vars.playerNumberString);
		Transform spriteTransform = vars.getPlayerSpriteTransform();

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
		var xTranslate = Input.GetAxis(horizontalAxisName) * Time.deltaTime * movementSpeed;
		var yTranslate = Input.GetAxis(verticalAxisNamee) * Time.deltaTime * movementSpeed;
		//TODO: look into removing this early translate call
		transform.Translate(xTranslate, yTranslate, 0);
		Vector3 clampedPosition = transform.position;
		clampedPosition.x = Mathf.Clamp(playerTransform.position.x, horizontalLowerLimit, horizontalUpperLimit);
		clampedPosition.y = Mathf.Clamp(playerTransform.position.y, verticalLowerLimit, verticalUpperLimit);
		transform.position = clampedPosition;
	}
}