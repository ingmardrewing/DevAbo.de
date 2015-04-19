using UnityEngine;
using System.Collections;

public class GoalDetection : MonoBehaviour {
	
	void OnTriggerEnter (Collider collision) {
		if (collision.gameObject.tag == "Player"){
			GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>().onLevelEndReached();
		}
	}
}
