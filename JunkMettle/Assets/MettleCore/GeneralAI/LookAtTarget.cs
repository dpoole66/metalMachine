using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour {

    public Transform targetToLookAt;

	void Update () {

        transform.LookAt(targetToLookAt);

	}
}
