using UnityEngine;
using System.Collections;

public class GenmodGainStrength : Item {

	// 
	override public Vector3 position { get{ return position; } set{ position = value; }}
	
	// Obvious Name
	override public string obvious_name{ get{ return obvious_name; } set{ obvious_name = value; }}
	
	// Obvious Description
	override public string obvious_description { get{ return obvious_description; } set{ obvious_description = value; }}
	
	// Informed Name
	override public string informed_name { get{ return "Genmodification of gain strength"; } set{ informed_name = value; }}
	
	// Informed Desciption
	override public string informed_description { get{ return informed_description; } set{ informed_description = value; }}
	
	// the target upon which the item is used, e. g. the user, an enemy, another item
	override public Object target { get{ return target; } set{ target = value; }}

	// clear target
	override public void clear_target(){
		target = null;
	}
	
	// apply item 
	override public void apply(){

	}

	public GenmodGainStrength (string colorparam){
		obvious_name = "A " + colorparam + " serum injection set.";
	}

}
