using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTarget : MonoBehaviour
{
    [Header("Variable")]
    [SerializeField] Vector3 defPos;
    
    [SerializeField] Vector3 offset;

    [SerializeField] bool onTarget;
    [SerializeField] int random;

    [Header("Parameter")]
    [SerializeField] float offsetOffTarget;
    [SerializeField] float offsetOnTarget;
    [SerializeField] Vector2 randomChance;

    // Start is called before the first frame update
    void Start()
    {
        defPos = transform.position;

        CalculateNextPos();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CalculateOnTarget();   
            CalculateNextPos();      
        }      
          
    }

    void CalculateOnTarget()
    {
        random = Random.Range(0, 100);

        if(random >= randomChance.x && random <= randomChance.y)
        {
            onTarget = true;
        }
        else
        {
            onTarget = false;
        }
    }

    void CalculateNextPos()
    {
        float randomX;
        float randomY;

        if(onTarget)
        {
            randomX = Random.Range(-offsetOnTarget, offsetOnTarget);
            randomY = Random.Range(-offsetOnTarget, offsetOnTarget);

            transform.position = defPos + new Vector3(randomX, randomY, 0);
        }
        else
        {
            randomX = Random.Range(-offsetOffTarget, offsetOffTarget);
            randomY = Random.Range(-offsetOffTarget, offsetOffTarget);

            transform.position = defPos + new Vector3(randomX, randomY, 0);
        }
    }
}
