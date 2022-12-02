using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController2 : MonoBehaviour
{
    private Transform followTarget;
    private Camera self;
    private bool gameEnded;
    public float targetZoom;
    public float zoomSpeed;

    public void SetTarget(Transform player)
    {
        followTarget = player;
        ZoomToTarget();
    }

    private void ZoomToTarget()
    {
        gameEnded = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        self = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnded)
        {
            Vector3 targetPos = new Vector3(followTarget.position.x, followTarget.position.y, transform.position.z);
            targetPos = new Vector3(Mathf.Clamp(targetPos.x, -5, 5), Mathf.Clamp(targetPos.y, -3, 5), transform.position.z);
            Vector3 newPosition = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * zoomSpeed);
            transform.position = newPosition;
            float newSize = Mathf.Lerp(self.orthographicSize, targetZoom, Time.deltaTime * zoomSpeed*3);
            self.orthographicSize = newSize;
        }
    }
}
