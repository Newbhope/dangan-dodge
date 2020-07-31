using UnityEngine;
using Rewired;

[RequireComponent(typeof(BasePlayerVariables))]
public class PlayerMovementController : MonoBehaviour {
	public float movementSpeed;

    private Rigidbody2D body;

    // The Rewired Player
    private Player player;

    private void Awake() {
        BasePlayerVariables vars = GetComponent<BasePlayerVariables>();
        body = GetComponent<Rigidbody2D>();

        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(vars.playerId);
    }

    void FixedUpdate() {
        // Translate ranges from -1.0 to 1.0
        Vector2 inputVector = new Vector2(player.GetAxis("Move Horizontal"), player.GetAxis("Move Vertical"));
        
        // Diagonal movement is faster than horizontal if the input vector isn't normalized
        Vector2 movementVector = inputVector.normalized * movementSpeed * 0.35f;

        body.MovePosition(body.position + movementVector);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        body.velocity = Vector2.zero;
    }
}