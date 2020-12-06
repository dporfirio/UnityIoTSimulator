using UnityEngine;

public class SortByY : MonoBehaviour {

	public void Update() {
		GetComponent<SpriteRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
	}

}