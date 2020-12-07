using UnityEngine;
using UnityEngine.Tilemaps;

public class SortWallByY : MonoBehaviour {

	public void Awake() {
		GetComponent<TilemapRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
	}

	public void Update() {
		GetComponent<TilemapRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
	}

}