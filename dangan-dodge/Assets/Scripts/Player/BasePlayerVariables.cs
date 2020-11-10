using UnityEngine;

public class BasePlayerVariables : MonoBehaviour
{
    public int playerId;
    public int bombsLeft;

    // Sadly property fields can't be exposed by the default Unity editor
    public int Energy
    {
        get
        {
            return energy;
        }

        set
        {
            energy = value;
            gameController.UpdateEnergyUi();
        }
    }
    private int energy;

    private GameManager gameController;

    void Awake()
    {
        gameController = FindObjectOfType<GameManager>();
    }
}
