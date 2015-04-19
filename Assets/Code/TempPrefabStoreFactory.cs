using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

class TempPrefabStoreFactory
{
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

	public TempPrefabStoreFactory (){
		_load_prefabs ();
	}

	private void _load_prefabs(){
		cam = (GameObject)Resources.Load("prefabs/camera", typeof (GameObject));

		player = (GameObject)Resources.Load("prefabs/playercharacter_ada", typeof (GameObject));

		roomCorner = (GameObject)Resources.Load("prefabs/room_corner", typeof (GameObject));
		roomDoor = (GameObject)Resources.Load("prefabs/room_door", typeof (GameObject));
		roomInner = (GameObject)Resources.Load("prefabs/room_inner", typeof (GameObject));
		roomStairsDown = (GameObject)Resources.Load("prefabs/room_staircase", typeof (GameObject));
		roomWall = (GameObject)Resources.Load("prefabs/room_wall", typeof (GameObject));

		sewer = (GameObject)Resources.Load("prefabs/sewer", typeof (GameObject));
		pointLight = (GameObject)Resources.Load("prefabs/pointlight", typeof (GameObject));

		tunnelStraight = (GameObject)Resources.Load("prefabs/corridor_straight", typeof (GameObject));
		tunnelCorner = (GameObject)Resources.Load("prefabs/tunnel_corner", typeof (GameObject));
		tunnelDeadend = (GameObject)Resources.Load("prefabs/tunnel_deadend", typeof (GameObject));
		tunnelTCrossing = (GameObject)Resources.Load("prefabs/tunnel_t_crossing", typeof (GameObject));
		tunnelXCrossing = (GameObject)Resources.Load("prefabs/tunnel_x_crossing", typeof (GameObject));	
	}

	public TempPrefabStore create_block_for_env(int[] env, int i, int w){
		int x = 2* (i % w);
		int z = 2* (int) (i/w);
		int[] roomInnerPattern = new int[9]{
			1,1,1,
			1,1,1,
			1,1,1
		};
		if( _equalEnvs( env, roomInnerPattern )){
			return new TempPrefabStore (roomInner, new Vector3(x, 0, z), Quaternion.identity, "inner" );
		}
		int[] upperLeftCorner = new int[9]{
			0,0,-1,
			0,1,1,
			-1,1,1
		};
		if( _equalEnvs( env, upperLeftCorner )){
			return new TempPrefabStore (roomCorner, new Vector3( x, 0, z) , Quaternion.identity, "corner" );
		}
		int[] upperRightCorner = new int[9]{
			-1,0,0,
			1,1,0,
			1,1,-1
		};
		if( _equalEnvs( env, upperRightCorner )){
			return new TempPrefabStore (roomCorner, new Vector3(x, 0, z) , Quaternion.AngleAxis(270, Vector3.up), "corner" );
		}
		
		int[] lowerLeftCorner = new int[9]{
			-1,1,1,
			0,1,1,
			0,0,-1
		};
		if( _equalEnvs( env, lowerLeftCorner )){
			return new TempPrefabStore (roomCorner, new Vector3(x, 0, z) , Quaternion.AngleAxis(90, Vector3.up), "corner" );
		}
		int[] lowerRightCorner = new int[9]{
			1,1,-1,
			1,1,0,
			-1,0,0
		};
		if( _equalEnvs( env, lowerRightCorner )){
			return new TempPrefabStore (roomCorner, new Vector3 (x, 0, z),  Quaternion.AngleAxis(180, Vector3.up), "corner");
		}
		int[] rightWall = new int[9]{
			1,1,-1,
			1,1,0,
			1,1,-1
		};
		if( _equalEnvs( env, rightWall )){
			return new TempPrefabStore (roomWall, new Vector3(x, 0, z), Quaternion.AngleAxis(180, Vector3.up));
		} 
		int[] leftWall = new int[9]{
			-1,1,1,
			0,1,1,
			-1,1,1
		};
		if( _equalEnvs( env, leftWall )){
			return new TempPrefabStore (roomWall, new Vector3(x, 0, z), Quaternion.identity);
		} 
		int[] upperWall = new int[9]{
			-1,0,-1,
			1,1,1,
			1,1,1
		};
		if( _equalEnvs( env, upperWall )){
			return new TempPrefabStore (roomWall, new Vector3(x, 0, z), Quaternion.AngleAxis(270, Vector3.up));
		}
		int[] lowerWall = new int[9]{
			1,1,1,
			1,1,1,
			-1,0,-1
		};
		if( _equalEnvs( env, lowerWall )){
			return new TempPrefabStore (roomWall, new Vector3(x, 0, z), Quaternion.AngleAxis(90, Vector3.up));
		}
		int[] upperDoor = new int[9]{
			0,1,0,
			1,1,1,
			1,1,1
		};
		if( _equalEnvs( env, upperDoor )){
			return new TempPrefabStore (roomDoor, new Vector3(x, 0, z), Quaternion.AngleAxis(270, Vector3.up));
		}
		int[] lowerDoor = new int[9]{
			1,1,1,
			1,1,1,
			0,1,0
		};
		if( _equalEnvs( env, lowerDoor )){
			return new TempPrefabStore (roomDoor, new Vector3(x, 0, z), Quaternion.AngleAxis(90, Vector3.up));
		}
		int[] leftDoor = new int[9]{
			0,1,1,
			1,1,1,
			0,1,1
		};
		if( _equalEnvs( env, leftDoor )){
			return new TempPrefabStore (roomDoor, new Vector3(x, 0, z), Quaternion.AngleAxis(0, Vector3.up));
		}
		int[] rightDoor = new int[9]{
			1,1,0,
			1,1,1,
			1,1,0
		};
		if( _equalEnvs( env, rightDoor )){
			return new TempPrefabStore (roomDoor, new Vector3(x, 0, z), Quaternion.AngleAxis(180, Vector3.up));
		}
		int[] straightTunnel = new int[9]{
			-1,0,-1,
			1,1,1,
			-1,0,-1
		};
		if( _equalEnvs( env, straightTunnel )){
			return new TempPrefabStore (tunnelStraight, new Vector3(x, 0, z), Quaternion.AngleAxis(0, Vector3.up));
		}
		int[] straightTunnelTurned1 = new int[9]{
			-1,1,-1,
			0,1,0,
			-1,1,-1
		};
		if( _equalEnvs( env, straightTunnelTurned1 )  ){
			return new TempPrefabStore (tunnelStraight, new Vector3(x, 0, z), Quaternion.AngleAxis(90, Vector3.up));
		}
		int[] corner1 = new int[9]{
			0,0,0,
			1,1,0,
			0,1,0
		};
		if( _equalEnvs( env, corner1 )     ){
			return new TempPrefabStore (tunnelCorner, new Vector3(x, 0, z), Quaternion.AngleAxis(270, Vector3.up));
		}
		int[] corner2 = new int[9]{
			0,1,0,
			1,1,0,
			0,0,0
		};
		if( _equalEnvs( env, corner2 )     ){
			return new TempPrefabStore (tunnelCorner, new Vector3(x, 0, z), Quaternion.AngleAxis(180, Vector3.up));
		}
		int[] corner3 = new int[9]{
			0,1,0,
			0,1,1,
			0,0,0
		};
		if( _equalEnvs( env, corner3 )     ){
			return new TempPrefabStore (tunnelCorner, new Vector3(x, 0, z), Quaternion.AngleAxis(90, Vector3.up));
		}
		int[] corner4 = new int[9]{
			0,0,0,
			0,1,1,
			0,1,0
		};
		if( _equalEnvs( env, corner4 )     ){
			return new TempPrefabStore (tunnelCorner, new Vector3(x, 0, z), Quaternion.identity);
		}	
		int[] x_crossing = new int[9]{
			0,1,0,
			1,1,1,
			0,1,0
		};
		if( _equalEnvs( env, x_crossing )     ){
			return new TempPrefabStore (tunnelXCrossing, new Vector3(x, 0, z), Quaternion.identity);
		}
		int[] t_crossing1 = new int[9]{
			0,0,0,
			1,1,1,
			0,1,0
		};
		if( _equalEnvs( env, t_crossing1 )     ){
			return new TempPrefabStore (tunnelTCrossing, new Vector3(x, 0, z), Quaternion.AngleAxis(270, Vector3.up));
		}
		int[] t_crossing2 = new int[9]{
			0,1,0,
			0,1,1,
			0,1,0
		};
		if( _equalEnvs( env, t_crossing2 )     ){
			return new TempPrefabStore (tunnelTCrossing, new Vector3(x, 0, z), Quaternion.identity);
		}
		int[] t_crossing3 = new int[9]{
			0,1,0,
			1,1,1,
			0,0,0
		};
		if( _equalEnvs( env, t_crossing3 )     ){
			return new TempPrefabStore (tunnelTCrossing, new Vector3(x, 0, z), Quaternion.AngleAxis(90, Vector3.up));
		}
		int[] t_crossing4 = new int[9]{
			0,1,0,
			1,1,0,
			0,1,0
		};
		if( _equalEnvs( env, t_crossing4 )     ){
			return new TempPrefabStore (tunnelTCrossing, new Vector3(x, 0, z), Quaternion.AngleAxis(180, Vector3.up));
		}
		int[] deadend1 = new int[9]{
			0,0,-1,
			0,1,1,
			0,0,-1
		};
		if( _equalEnvs( env, deadend1 )     ){
			return new TempPrefabStore (tunnelDeadend, new Vector3(x, 0, z), Quaternion.identity);
		}
		int[] deadend2 = new int[9]{
			0,0,0,
			0,1,0,
			-1,1,-1
		};
		if( _equalEnvs( env, deadend2 )     ){
			return new TempPrefabStore (tunnelDeadend, new Vector3(x, 0, z), Quaternion.AngleAxis(270, Vector3.up));
		}
		int[] deadend3 = new int[9]{
			-1,0,0,
			1,1,0,
			-1,0,0
		};
		if( _equalEnvs( env, deadend3 )     ){
			return new TempPrefabStore (tunnelDeadend, new Vector3(x, 0, z), Quaternion.AngleAxis(180, Vector3.up));
		}
		int[] deadend4 = new int[9]{
			-1,1,-1,
			0,1,0,
			0,0,0
		};
		if( _equalEnvs( env, deadend4 )     ){
			return new TempPrefabStore (tunnelDeadend, new Vector3(x, 0, z), Quaternion.AngleAxis(90, Vector3.up));
		}
		return null;
	}

	
	private bool _equalEnvs (int[] pixelEnv, int[] pattern ){
		if (pixelEnv.Length == pattern.Length) {
			for (int i = 0; i<pattern.Length; i++){
				if(pixelEnv[i] != pattern[i] && pattern[i] != -1){
					return false;
				}
			}
			return true;
		}
		return false;
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
}

