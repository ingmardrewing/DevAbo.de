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
		TempPrefabStoreFactory factory = new TempPrefabStoreFactory ();
		this.gameObject.tag = "GameController";
		_create_gui_display (factory);
		_create_labyrinth (factory);
	}

	private void _create_gui_display( TempPrefabStoreFactory factory){
		GameObject canvasContainer  = new GameObject("canvas", typeof(Canvas));
		canvasContainer.AddComponent<Text> ();
		Canvas canvas = canvasContainer.GetComponent<Canvas> ();
		canvas.renderMode = RenderMode.ScreenSpaceOverlay;
		statsText = canvas.GetComponent<Text> ();	
		statsText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
	}
	
	private void _create_labyrinth( TempPrefabStoreFactory factory){
		instantiatedPrefabs = new List<GameObject> ();
		labyrinth = new Labyrinth(this, level, factory);
		labyrinth.create_labyrinth ();
	}

	private void _destory_labyrinth(){
		foreach (GameObject g in instantiatedPrefabs) {
			Destroy( g  );
		}
		labyrinth = null;
	}

	void Update(){ 
		Text t = statsText.GetComponents<Text>() [0];
		t.text = "Hitpoints: " + _get_current_hp() 
			           + "  Strength: " + _get_current_strength() 
				       + "  Armor:" + _get_current_armor();
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
			_create_labyrinth(new TempPrefabStoreFactory ());
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