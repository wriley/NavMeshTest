using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	public float scanRange = 30;
	public float turnSpeed = 0.01f;

	NavMeshAgent _agent;
	Vector3 _target;
	GameObject _player;

	bool arrived = false;
	bool chasing = false;
	Quaternion startingAngle = Quaternion.AngleAxis(-60, Vector3.up);
	Quaternion stepAngle = Quaternion.AngleAxis(5, Vector3.up);

	float stopDistance = 2.5f;

	void Start () {
		_agent = GetComponent<NavMeshAgent>();
		_player = GameObject.FindGameObjectWithTag("Player");
		_target = GameManager.GetRandomNavPoint();
	}

	void Update()
	{
		chasing = LookForPlayer();

		if(chasing)
		{
			_target = _player.transform.position;

			if(DistanceToTarget(_target) > scanRange)
				chasing = false;
		}
		else
		{
			if(arrived && DistanceToTarget(_player.transform.position) > 10)
			{
				_target = GameManager.GetRandomNavPoint();
				arrived = false;
			}
		}

		if(DistanceToTarget(_target) > stopDistance)
		{
			arrived = false;
			_agent.SetDestination(_target);
		}
		else
		{
			arrived = true;
			_agent.Stop();
		}

		TurnTowardsTarget();
	}

	bool LookForPlayer()
	{
		RaycastHit hit;
		var angle = transform.rotation * startingAngle;
		var direction = angle * Vector3.forward;
		var pos = transform.position;
		for(var i = 0; i < 24; i++)
		{
			if(Physics.Raycast(pos, direction, out hit, scanRange))
			{
				if(hit.collider.tag == "Player")
				{
					_target = hit.collider.transform.position;
					//Debug.Log("FOUND PLAYER");
					renderer.material.color = Color.red;
					return true;
				}
			}
			direction = stepAngle * direction;
		}
		renderer.material.color = Color.yellow;
		return false;
	}

	float DistanceToTarget (Vector3 t)
	{
		float d = Vector3.Distance(transform.position, t);
		//Debug.Log("DistanceToTarget: " + d);
		return d;
	}

	void TurnTowardsTarget()
	{
		//transform.LookAt(new Vector3(_target.x, transform.position.y, _target.z));
		Quaternion oldRot = transform.rotation;
		transform.LookAt(_target);
		Quaternion newRot = transform.rotation;
		transform.rotation = Quaternion.Lerp(oldRot, newRot, turnSpeed);
	}
}
