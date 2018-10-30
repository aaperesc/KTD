using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitBehaviour : ScriptableObject {

	public UnitStats Stats;

	public abstract UnitStats GetRuntimeStats { get; }
	public abstract void Attack(GameUnit target);

	public delegate void OnDead();
	public OnDead onDead;

}
