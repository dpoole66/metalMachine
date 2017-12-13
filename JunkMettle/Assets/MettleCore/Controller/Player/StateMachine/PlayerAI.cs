using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : MonoBehaviour {

	Animator anim;
	public GameObject enemy;
	public GameObject GetEnemy(){
		return enemy;
	}

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetFloat ("Distance", Vector3.Distance (transform.position, enemy.transform.position));
	}
}
