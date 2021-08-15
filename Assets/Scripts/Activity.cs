using UnityEngine;

public abstract class Activity : Operation
{	

	public Player player;
    public bool canMove = GameObject.Find("Player").GetComponent<PlayerMovement>().canMove;

    public abstract void EndAct();
    public abstract bool CheckActivityConditions();
}
