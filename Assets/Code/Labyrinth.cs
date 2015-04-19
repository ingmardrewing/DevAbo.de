using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Labyrinth {
	
	private GameControllerScript gmc;
	private TempPrefabStoreFactory factory;

	public Labyrinth(GameControllerScript gmc_param, int level_param){
		gmc = gmc_param;
		factory = new TempPrefabStoreFactory (gmc);
	}

	public void create_labyrinth(){
		Texture2D m = Draw2DLabyrinthMap.create_map (gmc.level);

		// Encode texture into PNG
		var bytes = m.EncodeToPNG();

		// For testing purposes, also write to a file in the project folder
		File.WriteAllBytes(Application.dataPath + "/../SavedScreen.png", bytes);

		//Texture2D mapdata = Resources.Load ("roomsMap" ) as Texture2D;
		_create_3D_rooms (m);
	}


	private void _create_3D_rooms(Texture2D map){
		Color[] pixels = map.GetPixels ();
		int w = map.width;
		for (int i=0; i< pixels.Length; i++) {
			int [] pixelEnv = _get_pixel_env( pixels, i, w);
			TempPrefabStore temp = factory.create_block_for_env(pixelEnv, i, w);

			if( temp != null ){
				int x = 2 * ( i % w );
				int z = 2 * ( (int) i / w );

				if(pixels[i].r == 1F && pixels[i].g == 0F && pixels[i].b == 0F ){
					temp.set_prefab(gmc.get_stairsdown());
					temp.set_location(new Vector3(x, 0, z ) );
				}
				
				if(pixels[i].r == 0F && pixels[i].g == 1F && pixels[i].b == 0F ){
					gmc.instantiate_block(gmc.get_player(), new Vector3 (x, 0f, z),  Quaternion.identity);
					gmc.instantiate_block(gmc.get_camera(), new Vector3 (x, 1.87f, z),  Quaternion.identity);
				}

				if(pixels[i].r == 1F && pixels[i].g == 0F && pixels[i].b == 1F && temp.type.Equals("inner")){
					gmc.instantiate_block(gmc.get_pointlight(), new Vector3 (x, 2.87f, z),  Quaternion.identity);
					temp.set_prefab(gmc.get_sewer());
				}

				gmc.instantiate_block(temp.prefab, temp.location, temp.rotation );
			}

		}
	}

	
	private int[] _get_pixel_env(Color[] pixels, int i, int width){
		int[] pixelEnv = new int[9]{
			0,0,0,
			0,0,0,
			0,0,0
		};
		int [] indices = new int[9]{
			i - width -1, i -width, i -width +1, 
			i-1, i, i+1, 
			i + width -1, i+width, i+width +1 
		};
		for (int j = 0; j<9; j++) {
			if (_element_is_in_field (i, indices[j], width, pixels.Length)) {
				pixelEnv[j] = pixels[indices[j]].grayscale > 0 ? 1 : 0 ;
			} 
		}
		return pixelEnv;
	}
	
	private bool _element_is_in_field (int i, int iEnv, int w, int l){
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
