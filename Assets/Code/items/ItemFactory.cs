using UnityEngine;
using System.Collections.Generic;

public class ItemFactory {
	private List<string> genmod_colors = new List<string>{"green", "blue", "red"};

	private string genmod_gain_strength_color;


	public ItemFactory(){
		_init ();
	}

	private void _init(){
		genmod_gain_strength_color = _get_random_genmod_color ();
	}

	public GenmodGainStrength get_genmod_gain_strength(){
		return new GenmodGainStrength (_get_random_genmod_color());
	}

	private string _get_random_genmod_color(){
		int i = Random.Range(0, genmod_colors.Count);
		string c = genmod_colors[i];
		genmod_colors.RemoveAt (i);
		return c;
	}
}
