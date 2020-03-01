using UnityEngine;
using UnityEngine.Networking;

public class BasePlayerVariables : NetworkBehaviour {
    public GameObject explosionParticles;

    //TODO: maybe refactor all player variables into this?

    public string playerNumberString = "One";

    [SyncVar]
    public int playerNumberInt;
    //Vector representing which way the player is facing
    //Needed because only the sprite is flipped
    //Rotating player makes controls weird
    [SyncVar]
    public Vector2 playerVector;
    [SyncVar]
    public bool isFlipped;

    [SyncVar(hook = "OnChangeHealth")]
    public int health = 1;

    private ArenaController arenaController;

    public Sprite Shaq;
    public Sprite Bobe;

    //TODO: don't use this shit
    private Collider2D otherBullet;

    void Start() {
        arenaController = FindObjectOfType<ArenaController>();

        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.flipX = isFlipped;

        if (playerNumberInt == 1) {
            //spriteRenderer.sprite = Shaq;
        }

        if (playerNumberInt == 2) {
            spriteRenderer.color = Color.blue;
            //spriteRenderer.sprite = Bobe;
        }
    }

    internal void CheckDamage(Collider2D other) {
        if (isServer) {
            BaseBulletVariables bulletVars = other
                .gameObject
                .GetComponent(typeof(BaseBulletVariables)) as BaseBulletVariables;
            int shootingPlayerNumber = bulletVars.playerNumberInt;

            //A bullet that isn't owned by the player
            if (bulletVars != null && shootingPlayerNumber != playerNumberInt) {
                otherBullet = other;
                health--;
     
            }
        }
    }

    private void OnChangeHealth(int health) {
        BaseBulletVariables bulletVars = otherBullet
            .gameObject
            .GetComponent<BaseBulletVariables>();
        int shootingPlayerNumber = bulletVars.playerNumberInt;

        int currentScore;
        GameStats.playerScores.TryGetValue(shootingPlayerNumber, out currentScore);
        currentScore += 1;
        GameStats.playerScores[shootingPlayerNumber] = currentScore;

        arenaController.UpdateScoreUi();
        arenaController.CheckGameOver();

        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bullets) {
            Destroy(bullet);
        }

        GameObject particleObject = Instantiate
            (explosionParticles,
            gameObject.transform.position,
            Quaternion.identity);
        particleObject.GetComponent<ParticleSystem>().Play();



        Destroy(otherBullet.gameObject);
        Destroy(gameObject);
    }
}
