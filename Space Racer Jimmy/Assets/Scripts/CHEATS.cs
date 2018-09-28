using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHEATS : MonoBehaviour
{

	private void Start ()
    {
#if UNITY_CHEAT
        Debug.Log("That cheat!");
#endif
        Debug.Log("That yesn't cheat!");
    }
	
	private void Update ()
    {
		
	}
}
