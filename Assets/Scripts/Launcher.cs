using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Launcher : MonoBehaviour 
{
    [Header("Reference")]
	public Transform target;

    [Header("Variable")]
	[SerializeField] private float h;
	public GameObject[] allBalls;

    [Header("Parameter")]

	public float gravity = -18;   

	[SerializeField] private bool debugPath;

	void Start() 
    {
		Physics.gravity = Vector3.up * gravity;
	}

	void Update() 
    {		
		allBalls = GameObject.FindGameObjectsWithTag("Ball");

		if (Input.GetKeyDown(KeyCode.Space)) 
        {

		}

		if (debugPath) 
        {
			DrawPath ();
		}	
	}

	void LateUpdate()
	{
		//CalculateH();
	}

	void CalculateH()
	{
		h = target.position.y + 0.5f;
	}

	public void Launch()
    {	
		foreach(GameObject ball in allBalls)
		{
			if(ball.GetComponent<BallController>().Hitable)
			{
				CalculateH();
				// ball.GetComponent<Rigidbody>().useGravity = true;
				ball.GetComponent<Rigidbody>().drag = 0;
				ball.GetComponent<Rigidbody>().velocity = CalculateLaunchData(ball.transform).initialVelocity;
			}
		}		
	}

	LaunchData CalculateLaunchData(Transform ball) 
    {
		float displacementY = target.position.y - ball.position.y;
		Vector3 displacementXZ = new Vector3 (target.position.x - ball.position.x, 0, target.position.z - ball.position.z);

		float time = Mathf.Sqrt(-2*h/gravity) + Mathf.Sqrt(2*(displacementY - h)/gravity);

		Vector3 velocityY = Vector3.up * Mathf.Sqrt (-2 * gravity * h);
		Vector3 velocityXZ = displacementXZ / time;

		return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
	}

	void DrawPath() 
    {
		foreach(GameObject ball in allBalls)
		{
			LaunchData launchData = CalculateLaunchData(ball.transform);
			Vector3 previousDrawPoint = ball.transform.position;

			int resolution = 30;
			for (int i = 1; i <= resolution; i++) 
        	{
				float simulationTime = i / (float)resolution * launchData.timeToTarget;
				Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up *gravity * simulationTime * simulationTime / 2f;
				Vector3 drawPoint = ball.transform.position + displacement;
				Debug.DrawLine (previousDrawPoint, drawPoint, Color.green);
				previousDrawPoint = drawPoint;
			}
		}		
	}

	struct LaunchData 
    {
		public readonly Vector3 initialVelocity;
		public readonly float timeToTarget;

		public LaunchData (Vector3 initialVelocity, float timeToTarget)
		{
			this.initialVelocity = initialVelocity;
			this.timeToTarget = timeToTarget;
		}	
	}
}