using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tauntScript : MonoBehaviour
{
    private Transform target;

    public void setTarget(Transform targetTrans)
    {
        target = targetTrans;
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this, 5);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position;
    }
}
