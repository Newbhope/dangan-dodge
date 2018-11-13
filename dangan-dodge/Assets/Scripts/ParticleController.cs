using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour {

    private ParticleSystem pSystem;

	// Use this for initialization
	void Start () {
        pSystem = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!pSystem.isPlaying) {
            Destroy(gameObject);
        }
	}
}
