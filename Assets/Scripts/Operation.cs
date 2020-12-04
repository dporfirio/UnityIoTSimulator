using UnityEngine;

public abstract class Operation
{
	public string command;
	public string description;
	public GameObject actingObject;

	public abstract void Act();
}