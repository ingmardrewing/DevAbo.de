using UnityEngine;
using System.Collections.Generic;

public class Room {
	public int x;
	public int z;
	public int w;
	public int d;
	public int x_offset;
	public int z_offset;
	public int level;
	public List<Room> connectableRooms = new List<Room>();

	public Room( int x_offset_param, int z_offset_param, int level_param ){
		x_offset = x_offset_param;
		z_offset = z_offset_param;
		level = level_param;
		_init ();
	}

	private void _init (){
		w = 3 + 2 * Random.Range (0, 4);
		x = x_offset + (int)(9 - w) / 2;
		d = 3 + 2 * Random.Range (0, 4);
		z = z_offset + (int)(9 - d) / 2;
	}

	public Point get_right_connection_point (){
		return new Point( x + w, z + (int) d / 2 );
	}

	public Point get_upper_connection_point(){
		return new Point( x + (int) w/2, z-1  );
	}

	public Point get_left_connection_point (){
		return new Point( x -1, z + (int) d / 2 );
	}
	public Point get_lower_connection_point(){
		return new Point( x + (int) w/2, z + d);
	}

	public Point get_center(){
		return new Point (x + w / 2, z + d / 2);
	}

	public string to_string(){
		return "x:" + x + " z:" + z + " w:" + w + " d:" + d
			+ "\n lp:" + get_left_connection_point ().ToString ()
			+ "\n rp:" + get_right_connection_point ().ToString ()
			+ "\n up:" + get_upper_connection_point ().ToString ()
			+ "\n lowp:" + get_lower_connection_point ().ToString ();
	}

	public Texture2D renderTo( Texture2D map){
		for ( int i = x; i< x+w; i++){
			for(int j= z; j<z+d; j++){
				if( level == 1 &&  j % 2 == 0 && i % 2 == 0){
					map.SetPixel(i,j,new Color(1F,0F,1F) );
				}
				else{
					map.SetPixel(i,j,new Color(1F,1F,1F) );
				}
			}
		}
		/*
		Point v1 = get_upper_connection_point ();
		map.SetPixel(v1.x, v1.y, new Color(1F,0F,0F));
	
		Point v2 = get_lower_connection_point ();
		map.SetPixel(v2.x, v2.y, new Color(1F,0F,0F));

		Point v3 = get_left_connection_point ();
		map.SetPixel(v3.x, v3.y, new Color(1F,0F,0F));

		Point v4 = get_right_connection_point ();
		map.SetPixel(v4.x, v4.y, new Color(1F,0F,0F));
*/
		return map;
	}
}
