using UnityEngine;
using System.Collections;

[RequireComponent (typeof(GameObject))]
[RequireComponent (typeof(GameObject))]
public class ClickManager : MonoBehaviour {
	public GameObject capsule;
	public GameObject effectPrefab;

	void Update()
	{
		if (Input.GetKey(KeyCode.Escape))
			Application.Quit();

		if (Input.GetMouseButtonDown(1))
		{
			RaycastHit hit;
			if (!Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition),  out hit, 500))
				return;

			if (!hit.transform)
				return;
		
			capsule.GetComponent<CapsuleController>().Target = hit.point;
			Instantiate(effectPrefab, hit.point, Quaternion.identity);
			//Debug.Log("Target: " + hit.point.ToString());
		}
	}
}
