using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BallLauncher : MonoBehaviour 
{
    [Header("Reference")]
	public Rigidbody ball;
	public Transform target;
	public Text textErrorBar;

    [Header("Variable")]
	[SerializeField] private float h;
	[SerializeField] private float missRangeBar;
	[SerializeField] private float missRandomX;
	[SerializeField] private float missRandomZ;
	[SerializeField] float errorPercentage = 0;
	[SerializeField] bool isUp = true;
    public float distance;

    [Header("Parameter")]
    [SerializeField] private float minH;
    [SerializeField] private float maxH;
    [SerializeField] private float maxDistance;
	public float MaxDistance
	{
		get {return maxDistance;}
		set {maxDistance = value;}
	}
	
	[SerializeField] private float missRange;
	[SerializeField] private int powerBarSpeed;
	public bool calculateMiss;

	public float gravity = -18;   

	[SerializeField] private bool debugPath;

	void Start() 
    {
		ball.useGravity = false;
	}

	void Update() 
    {		
		// if (Input.GetKeyDown(KeyCode.Space)) 
        // {
		// 	Launch();
		// }

		if (debugPath) 
        {
			DrawPath ();
		}

        CalculateH();
		
		if(calculateMiss)
		{
			CalcualteMiss();
			missRandomX = Random.Range(0, missRangeBar / 3);
			missRandomZ = Random.Range(0, missRangeBar);
		}		
	}

	void CalcualteMiss()
	{		
		if(isUp)
		{
			errorPercentage += Time.deltaTime * powerBarSpeed;

			if(errorPercentage >= 100)
			{
				isUp = false;
			}
		}
		else
		{
			errorPercentage -= Time.deltaTime * powerBarSpeed;

			if(errorPercentage <= -100)
			{
				isUp = true;
			}
		}
		textErrorBar.text = errorPercentage.ToString("F0");

		missRangeBar = missRange * -errorPercentage / 100;	
	}

    void CalculateDistance()
    {
        Vector3 ballPosWithoutY = new Vector3(ball.position.x, 0, ball.position.z);
        Vector3 targetPosWithoutY = new Vector3(target.position.x, 0, target.position.z);

        distance = Vector3.Distance(ballPosWithoutY, targetPosWithoutY);
    }

    void CalculateH()
    {
        CalculateDistance();

        float displacementY = target.position.y - ball.position.y;

        h = (distance/maxDistance) * maxH + minH + displacementY;
    }

	public void Launch()  // Executed --> by Animation Event
    {

		Physics.gravity = Vector3.up * gravity;
		ball.useGravity = true;
		ball.velocity = CalculateLaunchData().initialVelocity;
	}

	LaunchData CalculateLaunchData() 
    {
		float displacementY = target.position.y - ball.position.y;
		Vector3 displacementXZ = new Vector3 ((target.position.x - ball.position.x) + missRandomX, 0, (target.position.z - ball.position.z) + missRandomZ);

		float time = Mathf.Sqrt(-2*h/gravity) + Mathf.Sqrt(2*(displacementY - h)/gravity);

		Vector3 velocityY = Vector3.up * Mathf.Sqrt (-2 * gravity * h);
		Vector3 velocityXZ = displacementXZ / time;

		return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
	}

	void DrawPath() 
    {
		LaunchData launchData = CalculateLaunchData ();
		Vector3 previousDrawPoint = ball.position;

		int resolution = 30;
		for (int i = 1; i <= resolution; i++) 
        {
			float simulationTime = i / (float)resolution * launchData.timeToTarget;
			Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up *gravity * simulationTime * simulationTime / 2f;
			Vector3 drawPoint = ball.position + displacement;
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