using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour {
	public float movementSpeed;

	private string horizontalAxisName;
	private string verticalAxisName;
    private Rigidbody2D body;

    Vector2 movementInput;

    void Start() {
		BasePlayerVariables vars = GetComponent<BasePlayerVariables>();
		horizontalAxisName = vars.playerNumberString + "Horizontal";
		verticalAxisName = vars.playerNumberString + "Vertical";
        body = GetComponent<Rigidbody2D>();
	}

    public void OnMove(InputValue value) {
        //Translate ranges from -1 to 1
        Debug.Log(value.Get<Vector2>());
        movementInput = value.Get<Vector2>();
    }

    void FixedUpdate() {
        //Translate ranges from -1 to 1
        Vector2 movement = movementInput * movementSpeed * 0.35f;
        body.MovePosition(body.position + movement);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        body.velocity = Vector2.zero;
    }
}