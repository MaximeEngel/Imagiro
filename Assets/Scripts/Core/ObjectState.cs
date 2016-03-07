using UnityEngine;
using System.Collections;

public abstract class ObjectState : MonoBehaviour
{
	public abstract void Action (int numState);
	public abstract void InstantAction (int numState);
}