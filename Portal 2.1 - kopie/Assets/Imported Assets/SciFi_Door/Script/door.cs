using UnityEngine;
using System.Collections;

public class door : MonoBehaviour {
	GameObject thedoor;
    public bool isEnabled;

void OnTriggerEnter ( Collider obj  ){
        if(isEnabled)
	thedoor= GameObject.FindWithTag("SF_Door");
	thedoor.GetComponent<Animation>().Play("open");
}

void OnTriggerExit ( Collider obj  ){
        if(isEnabled)
	thedoor= GameObject.FindWithTag("SF_Door");
	thedoor.GetComponent<Animation>().Play("close");
}
}