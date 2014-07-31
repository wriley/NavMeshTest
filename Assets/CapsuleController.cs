using UnityEngine;
using System.Collections;

public class CapsuleController : MonoBehaviour {

	private Vector3 _target;
	public Vector3 Target
	{
		get { return _target; }
		set { _target = value; }
	}

	NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		_target = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		agent.SetDestination(_target);
	}
}
