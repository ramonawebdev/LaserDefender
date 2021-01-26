using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        FindObjectOfType<Level>().LoadNextScene();
	}
}
