using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DetectStockIn : MonoBehaviour
{
    public GameManager gm;
    public GameObject TextFX;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Package")
        {
            int stock = col.gameObject.GetComponent<PackageController>().Capacity;

            transform.parent.GetComponent<Animator>().SetTrigger("In");
            GameObject FX = Instantiate(TextFX, Camera.main.WorldToScreenPoint(transform.position), Quaternion.identity, GameObject.Find("Canvas").transform);
            FX.GetComponent<RectTransform>().anchoredPosition += new Vector2(Random.Range(-150, 150), Random.Range(-100, 100));
            FX.GetComponentInChildren<TextMeshProUGUI>().text = "+" + stock;
            Destroy(FX, 0.5f);

            gm.AddCapacity(stock);
        }
    }
}
