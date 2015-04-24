using UnityEngine;
using System.Collections;

abstract class Item {
	// 2D Representation
	// 3D Representation
	
	// Obvious Name
	public string obvious_name;

	// Obvious Description
	public string obvious_description;

	// Informed Name
	public string informed_name;

	// Informed Desciption
	public string informed_description;
		
	// the target upon which the item is used, e. g. the user, an enemy, another item
	public Object target;
}
