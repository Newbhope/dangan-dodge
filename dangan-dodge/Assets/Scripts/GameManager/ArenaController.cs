using UnityEngine;

public class ArenaController : MonoBehaviour {
    void OnTriggerExit2D(Collider2D other) {
        if (other != null) {
            Destroy(other.gameObject);
        }
	}
}
