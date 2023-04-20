using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectStockIn : MonoBehaviour
{
    public GameManager gm;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Package")
        {
            int stock = col.gameObject.GetComponent<PackageController>().Capacity;

            transform.parent.GetComponent<Animator>().SetTrigger("In");

            gm.AddCapacity(stock);
        }
    }
}
