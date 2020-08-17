using Rewired;
using UnityEngine;

[RequireComponent(typeof(BasePlayerVariables))]
public class PlayerMiscController : MonoBehaviour
{

    private Player player;
    private GameController arenaController;

    void Awake()
    {
        // Do I want multiple people to be able to control the pause menu?
        BasePlayerVariables vars = GetComponent<BasePlayerVariables>();
        player = ReInput.players.GetPlayer(vars.playerId);
        arenaController = FindObjectOfType<GameController>();
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