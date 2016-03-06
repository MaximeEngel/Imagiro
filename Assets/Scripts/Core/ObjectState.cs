using UnityEngine;
using System.Collections;

public abstract class ObjectState : MonoBehaviour
{
	public abstract void Action ();
	public abstract void InstantAction ();
}