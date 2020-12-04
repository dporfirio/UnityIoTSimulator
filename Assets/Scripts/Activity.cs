using UnityEngine;

public abstract class Activity : Operation
{	

	public Player player;

    public abstract void EndAct();
    public abstract bool CheckActivityConditions();
}
