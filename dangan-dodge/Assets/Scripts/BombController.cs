using UnityEngine;

public class BombController : MonoBehaviour {


    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        Destroy(gameObject);
    }

    private void OnDestroy() {

    }
}
