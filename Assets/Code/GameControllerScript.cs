using UnityEngine;
using System.Collections.Generic;

public class GameControllerScript : MonoBehaviour {
	private GameObject tunnelStraight;
	private GameObject tunnelCorner;
	private GameObject tunnelXCrossing;
	private GameObject tunnelTCrossing;
	private GameObject tunnelDeadend;
	private GameObject roomInner;
	private GameObject roomCorner;
	private GameObject roomWall;
	private GameObject roomDoor;
	private GameObject roomStairsDown;
	private GameObject pointLight;
	private GameObject player;
	private GameObject cam;
	private GameObject sewer;

	private Labyrinth labyrinth;
	public int level = 1;
	private List<GameObject> instantiatedPrefabs;
	private bool levelEndReached = false;



	void Start () {
		_init ();
	}

	private void _init(){
		_load_prefabs ();
		_create_labyrinth ();
	}

	private void _load_prefabs(){
		cam = (GameObject)Resources.Load("prefabs/camera", typeof (GameObject));
		tunnelStraight = (GameObject)Resources.Load("prefabs/corridor_straight", typeof (GameObject));
		pointLight = (GameObject)Resources.Load("prefabs/pointlight", typeof (GameObject));
		player = (GameObject)Resources.Load("prefabs/playercharacter_ada", typeof (GameObject));
		roomCorner = (GameObject)Resources.Load("prefabs/room_corner", typeof (GameObject));
		roomDoor = (GameObject)Resources.Load("prefabs/room_door", typeof (GameObject));
		roomInner = (GameObject)Resources.Load("prefabs/room_inner", typeof (GameObject));
		roomStairsDown = (GameObject)Resources.Load("prefabs/room_staircase", typeof (GameObject));
		roomWall = (GameObject)Resources.Load("prefabs/room_wall", typeof (GameObject));
		sewer = (GameObject)Resources.Load("prefabs/sewer", typeof (GameObject));
		tunnelCorner = (GameObject)Resources.Load("prefabs/tunnel_corner", typeof (GameObject));
		tunnelDeadend = (GameObject)Resources.Load("prefabs/tunnel_deadend", typeof (GameObject));
		tunnelTCrossing = (GameObject)Resources.Load("prefabs/tunnel_t_crossing", typeof (GameObject));
		tunnelXCrossing = (GameObject)Resources.Load("prefabs/tunnel_x_crossing", typeof (GameObject));	
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

	public GameObject get_inner_room(){
		return roomInner;
	}

	public GameObject get_room_corner(){
		return roomCorner;
	}

	public GameObject get_room_wall(){
		return roomWall;
	}

	public GameObject get_room_door(){
		return roomDoor;
	}

	public GameObject get_tunnel_straight(){
		return tunnelStraight;
	}

	public GameObject get_tunnel_corner(){
		return tunnelCorner;
	}

	public GameObject get_tunnel_x_crossing (){
		return tunnelXCrossing;
	}

	public GameObject get_tunntel_t_crossing(){
		return tunnelTCrossing;
	}

	public GameObject get_tunnel_deadend(){
		return tunnelDeadend;
	}

	public GameObject get_player(){
		return player;
	}

	public GameObject get_stairsdown (){
		return roomStairsDown;
	}
	
	public GameObject get_pointlight (){
		return pointLight;
	}

	public GameObject get_camera(){
		return cam;
	}

	public GameObject get_sewer(){
		return sewer;
	}

	public void instantiate_block( GameObject block, Vector3 position, Quaternion rotation ){
		GameObject clonedBlock = (GameObject) Instantiate (block, position, rotation); 
		instantiatedPrefabs.Add (clonedBlock);
	}

}