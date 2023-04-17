using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Launcher : MonoBehaviour 
{
    [Header("Reference")]
	public Transform target;
	public Transform hitZone;

    [Header("Variable")]
	//[SerializeField] private float h;
	[SerializeField] Vector3 missOffset;
	[SerializeField] private float errorPercentage;
	[SerializeField] float errorRange;
	public GameObject[] allBalls;
	

    [Header("Parameter")]
	[SerializeField] float maxErrorRange;

	public float gravity;   

	[SerializeField] private bool debugPath;

	void Start() 
    {
		Physics.gravity = Vector3.up * gravity;
	}

	void Update() 
    {		
		allBalls = GameObject.FindGameObjectsWithTag("Package");

		foreach(GameObject ball in allBalls)
		{
			if(ball.GetComponent<PackageController>().Hitable)
			{
				Launch(ball);

				ball.GetComponent<PackageController>().Hitable = false;
			}
		}		

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

	Vector3 CalculateMiss(GameObject ball)
	{
		errorPercentage = Mathf.Clamp(((ball.transform.position.x - hitZone.transform.position.x) * 100), -100, 100);
		errorRange = maxErrorRange * errorPercentage/100;

		float randomX = Random.Range(-errorRange, errorRange);
		float randomY = Mathf.Clamp(Random.Range(-errorRange, errorRange), -5f, maxErrorRange);

		missOffset = new Vector3(randomX, randomY, 0);

		return target.position + missOffset;
	}

	public void Launch(GameObject ball)
    {				
		CalculateMiss(ball);

		ball.GetComponent<Rigidbody>().drag = 0;
		ball.GetComponent<Rigidbody>().velocity = CalculateLaunchData(ball.transform, CalculateMiss(ball)).initialVelocity;
		ball.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180));
	}

	LaunchData CalculateLaunchData(Transform ball, Vector3 target) 
    {
		float h = target.y + 1f;

		float displacementY = target.y - ball.position.y;
		Vector3 displacementXZ = new Vector3 (target.x - ball.position.x, 0, target.z - ball.position.z);

		float time = Mathf.Sqrt(-2*h/gravity) + Mathf.Sqrt(2*(displacementY - h)/gravity);

		Vector3 velocityY = Vector3.up * Mathf.Sqrt (-2 * gravity * h);
		Vector3 velocityXZ = displacementXZ / time;

		return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
	}

	void DrawPath() 
    {
		foreach(GameObject ball in allBalls)
		{
			LaunchData launchData = CalculateLaunchData(ball.transform, CalculateMiss(ball));
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