using UnityEngine;

public class PlayerMovementController : MonoBehaviour {
	public float movementSpeed;

	private string horizontalAxisName;
	private string verticalAxisName;
    private Rigidbody2D body;

    private Controls controls;

    Vector2 movementInput;

    private void Awake() {
        controls = new Controls();
        controls.Default.Move.performed += callback => movementInput = callback.ReadValue<Vector2>();
    }

    void Start() {
		BasePlayerVariables vars = GetComponent<BasePlayerVariables>();
		horizontalAxisName = vars.playerNumberString + "Horizontal";
		verticalAxisName = vars.playerNumberString + "Vertical";
        body = GetComponent<Rigidbody2D>();
	}

    /*
	 * Player movement applies movement translation then clamps the final position to
	 * inside the defined limits 
	 */
    void FixedUpdate() {
        //Translate ranges from -1 to 1
        Vector2 movement = movementInput * movementSpeed * 0.35f;
        body.MovePosition(body.position + movement);
	}

    private void OnCollisionEnter2D(Collision2D collision) {
        body.velocity = Vector2.zero;
    }

    private void OnEnable() {
        controls.Enable();
    }

    private void OnDisable() {
        controls.Disable();
    }
}