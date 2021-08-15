using UnityEngine;
using UnityEngine.Tilemaps;

public class SortWallByY : MonoBehaviour {

	public void Update() {
		GetComponent<TilemapRenderer>().sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
        Destroy(this);
    }
}