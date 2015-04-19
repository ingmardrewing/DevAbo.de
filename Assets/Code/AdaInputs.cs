using UnityEngine;
using System.Collections;

public class AdaInputs : MonoBehaviour
{
	
	private Animator _animator;
	
	// Use this for initialization
	void Start ()	{
		_animator = GetComponent<Animator> ();	
	}
	
	// Update is called once per frame
	void Update ()	{
		// Vertical -> Bewegung nach vorn
		float vertical = Input.GetAxis ("Vertical");	

		// Horizontal -> links / rechts
		float horizontal = Input.GetAxis ("Horizontal");	
		
		
		// und hier setzen wir die Werte, die wir eben 
		// aus den Inputs gelesen haben in den AnimationController
		_animator.SetFloat ("speed", vertical);
		_animator.SetFloat ("direction", horizontal);
	}
}
