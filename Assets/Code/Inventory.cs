using UnityEngine;
using System.Collections;

public class Inventory  {

	private Item[] items;
	private GameControllerScript gmc;

	public Inventory(GameControllerScript gmc_param){
		gmc = gmc_param;
	}
}
