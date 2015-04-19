using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

class TempPrefabStoreFactory
{
	private GameControllerScript gmc;

	public TempPrefabStoreFactory (GameControllerScript gmc_param){
		gmc = gmc_param;
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
			return new TempPrefabStore (gmc.get_inner_room(), new Vector3(x, 0, z), Quaternion.identity, "inner" );
		}
		int[] upperLeftCorner = new int[9]{
			0,0,-1,
			0,1,1,
			-1,1,1
		};
		if( _equalEnvs( env, upperLeftCorner )){
			return new TempPrefabStore (gmc.get_room_corner(), new Vector3( x, 0, z) , Quaternion.identity, "corner" );
		}
		int[] upperRightCorner = new int[9]{
			-1,0,0,
			1,1,0,
			1,1,-1
		};
		if( _equalEnvs( env, upperRightCorner )){
			return new TempPrefabStore (gmc.get_room_corner(), new Vector3(x, 0, z) , Quaternion.AngleAxis(270, Vector3.up), "corner" );
		}
		
		int[] lowerLeftCorner = new int[9]{
			-1,1,1,
			0,1,1,
			0,0,-1
		};
		if( _equalEnvs( env, lowerLeftCorner )){
			return new TempPrefabStore (gmc.get_room_corner(), new Vector3(x, 0, z) , Quaternion.AngleAxis(90, Vector3.up), "corner" );
		}
		int[] lowerRightCorner = new int[9]{
			1,1,-1,
			1,1,0,
			-1,0,0
		};
		if( _equalEnvs( env, lowerRightCorner )){
			return new TempPrefabStore (gmc.get_room_corner(), new Vector3 (x, 0, z),  Quaternion.AngleAxis(180, Vector3.up), "corner");
		}
		int[] rightWall = new int[9]{
			1,1,-1,
			1,1,0,
			1,1,-1
		};
		if( _equalEnvs( env, rightWall )){
			return new TempPrefabStore (gmc.get_room_wall(), new Vector3(x, 0, z), Quaternion.AngleAxis(180, Vector3.up));
		} 
		int[] leftWall = new int[9]{
			-1,1,1,
			0,1,1,
			-1,1,1
		};
		if( _equalEnvs( env, leftWall )){
			return new TempPrefabStore (gmc.get_room_wall(), new Vector3(x, 0, z), Quaternion.identity);
		} 
		int[] upperWall = new int[9]{
			-1,0,-1,
			1,1,1,
			1,1,1
		};
		if( _equalEnvs( env, upperWall )){
			return new TempPrefabStore (gmc.get_room_wall(), new Vector3(x, 0, z), Quaternion.AngleAxis(270, Vector3.up));
		}
		int[] lowerWall = new int[9]{
			1,1,1,
			1,1,1,
			-1,0,-1
		};
		if( _equalEnvs( env, lowerWall )){
			return new TempPrefabStore (gmc.get_room_wall(), new Vector3(x, 0, z), Quaternion.AngleAxis(90, Vector3.up));
		}
		int[] upperDoor = new int[9]{
			0,1,0,
			1,1,1,
			1,1,1
		};
		if( _equalEnvs( env, upperDoor )){
			return new TempPrefabStore (gmc.get_room_door(), new Vector3(x, 0, z), Quaternion.AngleAxis(270, Vector3.up));
		}
		int[] lowerDoor = new int[9]{
			1,1,1,
			1,1,1,
			0,1,0
		};
		if( _equalEnvs( env, lowerDoor )){
			return new TempPrefabStore (gmc.get_room_door(), new Vector3(x, 0, z), Quaternion.AngleAxis(90, Vector3.up));
		}
		int[] leftDoor = new int[9]{
			0,1,1,
			1,1,1,
			0,1,1
		};
		if( _equalEnvs( env, leftDoor )){
			return new TempPrefabStore (gmc.get_room_door(), new Vector3(x, 0, z), Quaternion.AngleAxis(0, Vector3.up));
		}
		int[] rightDoor = new int[9]{
			1,1,0,
			1,1,1,
			1,1,0
		};
		if( _equalEnvs( env, rightDoor )){
			return new TempPrefabStore (gmc.get_room_door(), new Vector3(x, 0, z), Quaternion.AngleAxis(180, Vector3.up));
		}
		int[] straightTunnel = new int[9]{
			-1,0,-1,
			1,1,1,
			-1,0,-1
		};
		if( _equalEnvs( env, straightTunnel )){
			return new TempPrefabStore (gmc.get_tunnel_straight(), new Vector3(x, 0, z), Quaternion.AngleAxis(0, Vector3.up));
		}
		int[] straightTunnelTurned1 = new int[9]{
			-1,1,-1,
			0,1,0,
			-1,1,-1
		};
		if( _equalEnvs( env, straightTunnelTurned1 )  ){
			return new TempPrefabStore (gmc.get_tunnel_straight(), new Vector3(x, 0, z), Quaternion.AngleAxis(90, Vector3.up));
		}
		int[] corner1 = new int[9]{
			0,0,0,
			1,1,0,
			0,1,0
		};
		if( _equalEnvs( env, corner1 )     ){
			return new TempPrefabStore (gmc.get_tunnel_corner(), new Vector3(x, 0, z), Quaternion.AngleAxis(270, Vector3.up));
		}
		int[] corner2 = new int[9]{
			0,1,0,
			1,1,0,
			0,0,0
		};
		if( _equalEnvs( env, corner2 )     ){
			return new TempPrefabStore (gmc.get_tunnel_corner(), new Vector3(x, 0, z), Quaternion.AngleAxis(180, Vector3.up));
		}
		int[] corner3 = new int[9]{
			0,1,0,
			0,1,1,
			0,0,0
		};
		if( _equalEnvs( env, corner3 )     ){
			return new TempPrefabStore (gmc.get_tunnel_corner(), new Vector3(x, 0, z), Quaternion.AngleAxis(90, Vector3.up));
		}
		int[] corner4 = new int[9]{
			0,0,0,
			0,1,1,
			0,1,0
		};
		if( _equalEnvs( env, corner4 )     ){
			return new TempPrefabStore (gmc.get_tunnel_corner(), new Vector3(x, 0, z), Quaternion.identity);
		}	
		int[] x_crossing = new int[9]{
			0,1,0,
			1,1,1,
			0,1,0
		};
		if( _equalEnvs( env, x_crossing )     ){
			return new TempPrefabStore (gmc.get_tunnel_x_crossing(), new Vector3(x, 0, z), Quaternion.identity);
		}
		int[] t_crossing1 = new int[9]{
			0,0,0,
			1,1,1,
			0,1,0
		};
		if( _equalEnvs( env, t_crossing1 )     ){
			return new TempPrefabStore (gmc.get_tunntel_t_crossing(), new Vector3(x, 0, z), Quaternion.AngleAxis(270, Vector3.up));
		}
		int[] t_crossing2 = new int[9]{
			0,1,0,
			0,1,1,
			0,1,0
		};
		if( _equalEnvs( env, t_crossing2 )     ){
			return new TempPrefabStore (gmc.get_tunntel_t_crossing(), new Vector3(x, 0, z), Quaternion.identity);
		}
		int[] t_crossing3 = new int[9]{
			0,1,0,
			1,1,1,
			0,0,0
		};
		if( _equalEnvs( env, t_crossing3 )     ){
			return new TempPrefabStore (gmc.get_tunntel_t_crossing(), new Vector3(x, 0, z), Quaternion.AngleAxis(90, Vector3.up));
		}
		int[] t_crossing4 = new int[9]{
			0,1,0,
			1,1,0,
			0,1,0
		};
		if( _equalEnvs( env, t_crossing4 )     ){
			return new TempPrefabStore (gmc.get_tunntel_t_crossing(), new Vector3(x, 0, z), Quaternion.AngleAxis(180, Vector3.up));
		}
		int[] deadend1 = new int[9]{
			0,0,-1,
			0,1,1,
			0,0,-1
		};
		if( _equalEnvs( env, deadend1 )     ){
			return new TempPrefabStore (gmc.get_tunnel_deadend(), new Vector3(x, 0, z), Quaternion.identity);
		}
		int[] deadend2 = new int[9]{
			0,0,0,
			0,1,0,
			-1,1,-1
		};
		if( _equalEnvs( env, deadend2 )     ){
			return new TempPrefabStore (gmc.get_tunnel_deadend(), new Vector3(x, 0, z), Quaternion.AngleAxis(270, Vector3.up));
		}
		int[] deadend3 = new int[9]{
			-1,0,0,
			1,1,0,
			-1,0,0
		};
		if( _equalEnvs( env, deadend3 )     ){
			return new TempPrefabStore ( gmc.get_tunnel_deadend(), new Vector3(x, 0, z), Quaternion.AngleAxis(180, Vector3.up));
		}
		int[] deadend4 = new int[9]{
			-1,1,-1,
			0,1,0,
			0,0,0
		};
		if( _equalEnvs( env, deadend4 )     ){
			return new TempPrefabStore (gmc.get_tunnel_deadend(), new Vector3(x, 0, z), Quaternion.AngleAxis(90, Vector3.up));
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
}

