using Rewired;
using UnityEngine;

// [RequireComponent(typeof(BasePlayerVariables))]
public class PlayerMiscController : MonoBehaviour {

    private Player player;
    private GameManager arenaController;

    void Awake()
    {
        // Do I want multiple people to be able to control the pause menu?
        BasePlayerVariables vars = GetComponentInParent<BasePlayerVariables>();
        player = ReInput.players.GetPlayer(vars.playerId);
        arenaController = FindObjectOfType<GameManager>();
    }

    // FixedUpdate doesn't operate when timescale is 0, but Update is called every frame regardless
    void Update()
    {
        // Translate ranges from -1.0 to 1.0
        if (player.GetButtonLongPressDown("Pause"))
        {
            arenaController.PausePressed();
        }
    }
}