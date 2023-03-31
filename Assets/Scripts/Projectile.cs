using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float initVelocity;
    [SerializeField] float angleDeg;

    [SerializeField] LineRenderer line;
    [SerializeField] float step;

    private void Update()
    {
        float angle = angleDeg * Mathf.Deg2Rad;
        DrawPath(initVelocity, angle, step);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void DrawPath(float v0, float angle, float step)
    {
        step = Mathf.Max(0.01f, step);

        float totalTime = 10;
        line.positionCount = (int)(totalTime / step) + 2;
        int count = 0;

        for(float i = 0; i < totalTime; i += step)
        {
            float x = v0 * i * Mathf.Cos(angle);
            float y = v0 * i * Mathf.Sin(angle) - 0.5f * -Physics.gravity.y * Mathf.Pow(i, 2);
            line.SetPosition(count, new Vector3(x, y, 0));

            count++;
        }
        float xFinal = v0 * totalTime * Mathf.Cos(angle);
        float yFinal = v0 * totalTime * Mathf.Sin(angle) - 0.5f * -Physics.gravity.y * Mathf.Pow(totalTime, 2);
        line.SetPosition(count, new Vector3(xFinal, yFinal, 0));
    }

    void Shoot()
    {
        float angle = angleDeg * Mathf.Deg2Rad;
        StopAllCoroutines();
        //StartCoroutine(TargetMovement(initVelocity, angle));


    }

    IEnumerator TargetMovement(float v0, float angle)
    {
        float t = 0;
        while(t < 100)
        {
            float x = v0 * t * Mathf.Cos(angle);
            float y = v0 * t * Mathf.Sin(angle) - (1f/2f) * -Physics.gravity.y * Mathf.Pow(t, 2);
            
            transform.position = new Vector3(x, y, 0);

            t += Time.deltaTime;

            yield return null;
        }
    }
}
