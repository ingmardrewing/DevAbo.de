using UnityEngine;
using System.Collections;

public abstract class Item {
	// 2D Representation
	// 3D Representation
	public abstract Vector3 position { get; set; }

	// Obvious Name
	public abstract string obvious_name { get; set; }

	// Obvious Description
	public abstract string obvious_description { get; set; }

	// Informed Name
	public abstract string informed_name { get; set; }

	// Informed Desciption
	public abstract string informed_description { get; set; }
		
	// the target upon which the item is used, e. g. the user, an enemy, another item
	public abstract Object target { get; set; }

	// clear target
	public abstract void clear_target();

	// apply item 
	public abstract void apply();
}
