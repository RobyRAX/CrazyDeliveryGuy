using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pitcher : MonoBehaviour
{
    [Header("Reference")]
    public Transform spawner;
    public Transform hitPoint;
	public GameManager gm;

    [Header("Variable")]
    [SerializeField] Vector3 target;
    GameObject ballClone;
	[SerializeField] int ballShotRemaining;
	[SerializeField] float ballShotDelayTimer;
	[SerializeField] float _ballShotDelayTimer;
	//[SerializeField] float phaseDelayTimer;
	[SerializeField] float _phaseDelayTimer;
	[SerializeField] bool shootAble;

    [Header("Parameter")]
	[SerializeField] float maxRange;
	[SerializeField] Vector2 minMaxBallShot;
	[SerializeField] Vector2 ballShotDelay;
	[SerializeField] float phaseDelay;

	public float gravity;   

	[SerializeField] private bool debugPath;

	void Start() 
    {
		Physics.gravity = Vector3.up * gravity;

        target = hitPoint.position;
	}

	void Update() 
    {			
		if(gm.currentState == GameManager.States.GamePlay)
		{
			ShotSetter();
		}
		
		if (debugPath) 
        {
			DrawPath(ballClone);
		}	
	}

	void ShotSetter()
	{
		if(!shootAble)
		{
			_phaseDelayTimer -= Time.deltaTime;

			if(_phaseDelayTimer <= 0)
			{
				shootAble = true;
			}
			
		}
		else if(shootAble)
		{
			_ballShotDelayTimer -= Time.deltaTime;

			if(_ballShotDelayTimer <= 0)
			{
				if(ballShotRemaining > 0)
				{
					ballShotRemaining--;

					if(gm.Stocks.Count > 0)
					{
						ballClone = Instantiate(gm.Stocks[0], spawner.position, Quaternion.identity);
						Launch(ballClone);
            			Destroy(ballClone, 3.25f);

						gm.RemoveStock(0);
					}
					
					_ballShotDelayTimer = ballShotDelayTimer;
				}	
				if(ballShotRemaining <= 0)
				{
					ballShotRemaining = Random.Range(Mathf.FloorToInt(minMaxBallShot.x), Mathf.FloorToInt(minMaxBallShot.y));
					ballShotDelayTimer = Random.Range(ballShotDelay.x, ballShotDelay.y);

					_ballShotDelayTimer = ballShotDelayTimer;

					_phaseDelayTimer = phaseDelay;
					shootAble = false;
				}
			}
		}
	}

	Vector3 CalculateRandom()
    {
        float randomX = Random.Range(-maxRange, maxRange);

        return target + new Vector3(randomX, 0, 0);
    }

	public void Launch(GameObject ball)
    {			
        ball.GetComponent<Rigidbody>().drag = 0;	
		ball.GetComponent<Rigidbody>().velocity = CalculateLaunchData(ball.transform, CalculateRandom()).initialVelocity;
		ball.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(-45, 45), Random.Range(-45, 45), Random.Range(-45, 45));
	}

	LaunchData CalculateLaunchData(Transform ball, Vector3 target) 
    {
		float h = target.y + 2f;

		float displacementY = target.y - ball.position.y;
		Vector3 displacementXZ = new Vector3 (target.x - ball.position.x, 0, target.z - ball.position.z);

		float time = Mathf.Sqrt(-2*h/gravity) + Mathf.Sqrt(2*(displacementY - h)/gravity);

		Vector3 velocityY = Vector3.up * Mathf.Sqrt (-2 * gravity * h);
		Vector3 velocityXZ = displacementXZ / time;

		return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
	}

	void DrawPath(GameObject ball) 
    {
		LaunchData launchData = CalculateLaunchData(ball.transform, CalculateRandom());
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
