using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(GameObject))]
public class GameManager : MonoBehaviour {
	public GameObject playerPrefab;
	public GameObject enemyPrefab;
	public GameObject effectPrefab;
	public int numEnemies = 1;

	public static GameObject player;

	private static int navRadius = 50;

	void Start()
	{
		player = (GameObject)Instantiate(playerPrefab, GetRandomNavPoint(), Quaternion.identity);
		player.tag = "Player";

		for(int i = 0; i < numEnemies; i++)
		{
			GameObject enemy = (GameObject)Instantiate(enemyPrefab, GetRandomNavPoint(), Quaternion.identity);
			enemy.transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
		}
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.Escape))
			Application.Quit();

		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			if (!Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition),  out hit, 500))
				return;

			if (!hit.transform)
				return;
		
			player.GetComponent<PlayerController>().Target = hit.point;
			//Instantiate(effectPrefab, hit.point, Quaternion.identity);
			//Debug.Log("Target: " + hit.point.ToString());
		}
	}

	public static Vector3 GetRandomNavPoint ()
	{
		Vector3 randomDirection = Random.insideUnitSphere * navRadius;
		
		NavMeshHit hit;
		if(NavMesh.SamplePosition(randomDirection, out hit, navRadius, 1))
		{
			return hit.position;
		}
		
		return Vector3.zero;
	}

}
