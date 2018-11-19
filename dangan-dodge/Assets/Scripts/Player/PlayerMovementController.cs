using UnityEngine;

public class PlayerMovementController : MonoBehaviour {
	public float movementSpeed;

	private string horizontalAxisName;
	private string verticalAxisName;
    private Rigidbody2D body;

	void Start() {
		BasePlayerVariables vars = GetComponent<BasePlayerVariables>();
		horizontalAxisName = vars.playerNumberString + "Horizontal";
		verticalAxisName = vars.playerNumberString + "Vertical";

        Boundary arenaBounds = _GLOBAL_CONSTANTS.getPlayerBoundary(vars.playerNumberString);

        Vector3 spriteBounds = GetComponentInChildren<SpriteRenderer>().bounds.size;

        body = GetComponent<Rigidbody2D>();
	}

	/*
	 * Player movement applies movement translation then clamps the final position to
	 * inside the defined limits 
	 */
	void FixedUpdate() {
		//Translate ranges from -.33 to .33
		var xTranslate = Input.GetAxis(horizontalAxisName) * Time.deltaTime * movementSpeed;
		var yTranslate = Input.GetAxis(verticalAxisName) * Time.deltaTime * movementSpeed;
        Vector2 movement = new Vector2(xTranslate, yTranslate);
        body.MovePosition(body.position + movement);
	}

    private void OnCollisionEnter2D(Collision2D collision) {
        body.velocity = Vector2.zero;
        Debug.Log("wew");
    }
}