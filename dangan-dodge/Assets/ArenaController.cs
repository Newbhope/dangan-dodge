using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Overall script for the arena
 * 
 * So far destroys anything that exits the arena for performance
 * */
public class ArenaController : MonoBehaviour {

	// Use this for initialization
	void Start() {
		
	}
	
	// Update is called once per frame
	void Update() {
		
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other != null) {
			Destroy(other.gameObject);
		}
	}
}
