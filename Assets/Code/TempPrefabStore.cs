using UnityEngine;
using System.Collections;

public class TempPrefabStore  {

	public GameObject prefab;
	public Vector3 location;
	public Quaternion rotation;
	public string type = "" ;

	public TempPrefabStore( GameObject prefab_param, Vector3 location_param, Quaternion rotation_param, string type_param ){
		prefab = prefab_param;
		location = location_param;
		rotation = rotation_param;
		type = type_param;
	}

	public TempPrefabStore( GameObject prefab_param, Vector3 location_param, Quaternion rotation_param ){
		prefab = prefab_param;
		location = location_param;
		rotation = rotation_param;
	}

	public void set_prefab(GameObject prefab_param){
		prefab = prefab_param;
	}

	public void set_location( Vector3 location_param){
		location = location_param;
	}
}
