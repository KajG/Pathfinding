using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour {
	[SerializeField]
	private GameObject target;
	private Vector3 velocity;
	private Vector3 position;
	private Vector3 mousePos;
	[SerializeField]
	private List<Vector3> mousePosList = new List<Vector3>();
	[SerializeField]
	private float maxSpeed;
	public float mass;
	private Vector3 targetPosition;
	[SerializeField]
	private float distanceBetweenObjects = 0.2f;

	void Start () 
	{
		velocity = new Vector3 (0, 1 * maxSpeed / 100, 0);
	}
	
	void Update () 
	{
		this.transform.position = position;
		mousePos = Input.mousePosition;
		mousePos = Camera.main.ScreenToWorldPoint (mousePos);
		mousePos.z = 10;
		if (Input.GetMouseButtonDown (0)) {
			Instantiate (target, mousePos, Quaternion.identity);
			mousePosList.Add (mousePos);
			targetPosition = mousePosList[0];
		}	
		if (Vector3.Distance(targetPosition, position) <= distanceBetweenObjects) {
			mousePosList.RemoveAt (0);
			targetPosition = mousePosList[0];
		}
		Seek ();
		print (velocity);
	}
	void Seek(){
		Vector3 desiredStep = targetPosition - position;
		desiredStep.Normalize ();
		Vector3 desiredVelocity = desiredStep * maxSpeed;
		Vector3 steeringForce = desiredVelocity - velocity;
		velocity = velocity + steeringForce / mass;
		position += velocity;
	}
}
