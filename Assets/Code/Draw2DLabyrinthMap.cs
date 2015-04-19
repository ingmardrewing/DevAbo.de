using UnityEngine;
using System.Collections.Generic;

public class Draw2DLabyrinthMap {

	public static Texture2D create_map(int level){
		List<Room> possibleRooms = _generate_rooms(level);
		
		// prepare connections
		possibleRooms = _find_and_set_connectable_rooms ( possibleRooms );
		
		// define starting room
		int randIndex = Random.Range (0, possibleRooms.Count);
		Room startingRoom = possibleRooms [randIndex];
		
		// define exit roomt
		randIndex = Random.Range (0, possibleRooms.Count);
		Room exitRoom = possibleRooms[randIndex];
		
		// connect rooms
		List<RoomConnection> roomConnections = _connect_possible_rooms(possibleRooms, startingRoom, exitRoom);
		
		// render 2D Map and return it
		return _render_2D_map (roomConnections, startingRoom, exitRoom);
	}


	private static Texture2D _get_black_map(int w, int h){
		Texture2D map = new Texture2D (w, h);
		Color[] p = map.GetPixels ();
		for (int k =0; k< p.Length; k++) {
			p[k] = new Color(0F, 0F, 0F);
		}
		map.SetPixels (p);
		return map;
	}
	
	private static List<Room> _generate_rooms(int level){
		List<Room> newrooms = new List<Room> ();
		int offset_x = 0;
		int offset_z = 1;
		int gap = 12;
		for (int i=0; i<3; i++) {
			offset_x = 0;
			for (int j=0; j<3; j++) {
				Room r = new Room (offset_x, offset_z, level);
				newrooms.Add(r);
				offset_x += gap;
			}
			offset_z += gap;
		}
		return newrooms;
	}
	
	private static List<Room> _find_and_set_connectable_rooms(List<Room> possibleRooms ){
		for (int i=0; i<possibleRooms.Count; i++) {
			int[] connectableRoomsIndices = new int[4]{
				i -3,
				i-1,
				i+1,
				i+3
			};
			for( int j = 0; j<connectableRoomsIndices.Length; j++){
				if(_element_is_in_field(i, connectableRoomsIndices[j], 3, possibleRooms.Count)){
					possibleRooms[i].connectableRooms.Add(possibleRooms[connectableRoomsIndices[j]]);
				}
			}
		}
		return possibleRooms;
	}
	
	private static List<RoomConnection> _connect_possible_rooms(List<Room> possibleRooms, Room startingRoom, Room exitRoom ){
		List<RoomConnection> roomConnections = new List<RoomConnection> ();
		List<Room> connectedRooms = new List<Room> ();
		connectedRooms.Add (startingRoom);
		Room currentRoom = startingRoom;
		
		while (! connectedRooms.Contains(exitRoom) ) {
			for(var i =0; i< currentRoom.connectableRooms.Count; i++){
				if( connectedRooms.Contains( currentRoom.connectableRooms[i] ) ){
					currentRoom.connectableRooms.Remove(currentRoom.connectableRooms[i]);
				}
			}
			
			if( currentRoom.connectableRooms.Count == 0){
				int r1 = Random.Range ( 0, connectedRooms.Count);
				currentRoom = connectedRooms[r1];
			}
			else{
				int r = Random.Range (0, currentRoom.connectableRooms.Count);
				Room targetRoom = currentRoom.connectableRooms[r];
				roomConnections.Add(new RoomConnection (currentRoom,  targetRoom) );
				connectedRooms.Add(targetRoom);
			}
		}
		return roomConnections;
	}
	
	private static Texture2D _render_2D_map(List<RoomConnection> roomConnections, Room startingRoom, Room exitRoom ){
		Texture2D map = _get_black_map (64, 64);
		
		foreach (RoomConnection rc in roomConnections) {
			map = _draw_2D_connection( rc.room1, rc.room2, map );
			map = rc.room1.renderTo(map);
			map = rc.room2.renderTo(map);
		}
		
		// set entry pixel
		Point charPoint = startingRoom.get_center ();
		map.SetPixel (charPoint.x+1, charPoint.y, new Color(0F, 1F, 0F));
		
		// set exit pixel
		Point exitPoint = exitRoom.get_center ();
		map.SetPixel (exitPoint.x, exitPoint.y, new Color(1F, 0F, 0F));
		return map;
	}
	

	private static Texture2D _draw_2D_connection (Room r1, Room r2, Texture2D map){
		Point r2_center = r2.get_center ();
		Point r1_center = r1.get_center ();
		
		int delta_x = r2_center.x - r1_center.x;
		int delta_y = r2_center.y - r1_center.y;
		int delta_x_abs = Mathf.Abs (delta_x);
		int delta_y_abs = Mathf.Abs (delta_y);
		
		
		if (delta_x_abs > 0) {
			int direction_x =  r2_center.x > r1_center.x ? -1 : 1;
			int px = r2_center.x;
			int py = r2_center.y;
			for(int i=0; i< delta_x_abs; i++){
				px += direction_x;
				map.SetPixel(px, py, new Color(1F, 1F, 1F));
			}
		}
		else if (delta_y_abs > 0) {
			int direction_y =  r2_center.y > r1_center.y ? -1 : 1;
			int px = r2_center.x;
			int py = r2_center.y;
			for(int i=0; i< delta_y_abs; i++){
				py += direction_y;
				map.SetPixel(px, py, new Color(1F, 1F, 1F));
			}
		}
		return map;
	}

	private static bool _element_is_in_field (int i, int iEnv, int w, int l){
		if (iEnv < 0 || iEnv >= l) {
			return false;
		}
		if ( i % w == 0) {
			return iEnv % w == 0 || iEnv % w == 1;
		}
		if (i % w == w -1 ) {
			return iEnv % w == w - 1 || iEnv % w == w - 2;
		}
		return true;
	}

}
