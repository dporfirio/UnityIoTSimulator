using UnityEngine;

public class SortByY : MonoBehaviour {

	[SerializeField]
	private bool runOnlyOnce = false;

	public void Update() {
		GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
		if (runOnlyOnce)
		{
			Destroy(this);
		}
	}

}