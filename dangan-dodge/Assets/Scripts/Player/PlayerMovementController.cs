using Photon.Pun;
using Rewired;
using UnityEngine;

// [RequireComponent(typeof(BasePlayerVariables))]
// This is in the parent now which makes the RequireComponent not useful sadly
public class PlayerMovementController : MonoBehaviourPun {
	public float movementSpeed;

    private Rigidbody2D body;

    // The Rewired Player
    private Player player;

    private void Awake() {
        BasePlayerVariables vars = GetComponentInParent<BasePlayerVariables>();
        body = GetComponent<Rigidbody2D>();

        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(vars.playerId);
    }

    void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            // Translate ranges from -1.0 to 1.0
            Vector2 inputVector = new Vector2(player.GetAxis("Move Horizontal"), player.GetAxis("Move Vertical"));

            // Diagonal movement is faster than horizontal if the input vector isn't normalized
            Vector2 movementVector = inputVector.normalized * movementSpeed * 0.35f;

            body.MovePosition(body.position + movementVector);
        }

    }

    // huh what is this for
    private void OnCollisionEnter2D(Collision2D collision)
    {
        body.velocity = Vector2.zero;
    }
}