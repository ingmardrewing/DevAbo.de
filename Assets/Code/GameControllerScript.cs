using UnityEngine;
using UnityEngine.UI;

using System.Collections.Generic;

public class GameControllerScript : MonoBehaviour {


	private Labyrinth labyrinth;
	public int level = 1;
	private List<GameObject> instantiatedPrefabs;
	private bool levelEndReached = false;
	private GameObject stats;
	private Text statsText;


	void Start () {
		_init ();
	}

	private void _init(){
		stats = GameObject.FindGameObjectWithTag ("UITXT");
		statsText = stats.GetComponents<Text> () [0];
		_create_labyrinth ();
	}



	private void _create_labyrinth(){
		instantiatedPrefabs = new List<GameObject> ();
		labyrinth = new Labyrinth(this, level);
		labyrinth.create_labyrinth ();
	}

	private void _destory_labyrinth(){
		foreach (GameObject g in instantiatedPrefabs) {
			Destroy( g  );
		}
		labyrinth = null;
	}

	void Update(){
		string statusTxt = "Hitpoints: ▣▣▣▣▣▢▢▢  Strength:▣▣▣  Armor:▣▣▣";
		statsText.text = statusTxt;
	}

	void LateUpdate () {	
		if (levelEndReached) {
			levelEndReached = false;
			_destory_labyrinth ();

			// creating next level
			level++;
			_create_labyrinth();
		}
	}

	public void onLevelEndReached(){
		levelEndReached = true;
	}



	public void instantiate_block( GameObject block, Vector3 position, Quaternion rotation ){
		GameObject clonedBlock = (GameObject) Instantiate (block, position, rotation); 
		instantiatedPrefabs.Add (clonedBlock);
	}

}