﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "MettleAI/Stats")]

public class Stats : ScriptableObject {

	public float moveSpeed = 1.0f;
	public float turnSpeed = 4.0f;
	public float lookRange = 6.0f;
	public float lookSphereCastRadius = 2.0f;
	public float enGuardeRange = 3.0f;

	public bool InRange;

	public float attackRange = 1.0f;
	public float attackRate = 1.0f;
	public float attackForce = 15.0f;
	public float attackDamage = 10.0f;

	public float searchDuration = 4.0f;
	public float searchTurningSpeed = 120.0f;


}
