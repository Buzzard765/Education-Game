using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraMovement : MonoBehaviour
{
    public Transform target;
    public float smoothing;

    public Vector2 maxpos, minpos;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        clampMovement();
    }

    void clampMovement() {
        if (transform.position != target.position) {
            Vector3 targetpos = new Vector3(target.position.x, target.position.y, transform.position.z);
            targetpos.x = Mathf.Clamp(targetpos.x, minpos.x, maxpos.x);
            targetpos.y = Mathf.Clamp(targetpos.y, minpos.y, maxpos.y);

            transform.position = Vector3.Lerp(transform.position, targetpos, smoothing);
        }
    }
}
