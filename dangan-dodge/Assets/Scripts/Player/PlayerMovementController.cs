using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovementController : NetworkBehaviour {
    public float movementSpeed;

    private string horizontalAxisName;
    private string verticalAxisName;
    private Rigidbody2D body;

    void Start() {
        BasePlayerVariables vars = GetComponent<BasePlayerVariables>();
        horizontalAxisName = vars.playerNumberString + "Horizontal";
        verticalAxisName = vars.playerNumberString + "Vertical";
        body = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        if (hasAuthority) {
            var xTranslate = Input.GetAxis(horizontalAxisName) * Time.deltaTime * movementSpeed;
            var yTranslate = Input.GetAxis(verticalAxisName) * Time.deltaTime * movementSpeed;
            Vector2 movement = new Vector2(xTranslate, yTranslate);
            body.MovePosition(body.position + movement);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        body.velocity = Vector2.zero;
    }
}