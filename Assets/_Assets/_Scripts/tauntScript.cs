using UnityEngine;
using TMPro;

public class tauntScript : MonoBehaviour
{
    private Transform target;
    public TMP_Text insult;

    public void setTarget(Transform targetTrans)
    {
        target = targetTrans;
    }

    public void setInsult(string inputInsult)
    {
        insult.text = inputInsult;
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
