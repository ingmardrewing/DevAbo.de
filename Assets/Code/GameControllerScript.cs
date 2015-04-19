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
		string statusTxt = "Hitpoints: " + _get_current_hp() 
						 + "  Strength: " + _get_current_strength() 
						 + "  Armor:" + _get_current_armor();
		statsText.text = statusTxt;
	}


	private int max_hp = 8;
	private int current_hp = 5;

	private int max_armor = 3;
	private int current_armor = 3;

	private int max_strength = 3;
	private int current_strength = 3;

	private string _get_current_hp(){
		return _write_amount (current_hp, max_hp);
	}

	private string _get_current_strength(){
		return _write_amount (current_strength, max_strength);
	}

	private string _get_current_armor(){
		return _write_amount (current_armor, max_armor);
	}

	private string _write_amount(int current, int max){
		string amount = "";
		for (int i=0; i<max; i++) {
			amount += current <= i ? "□" : "■";
		}
		return amount;
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