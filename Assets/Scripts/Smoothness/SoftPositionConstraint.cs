using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[ExecuteAlways]
public class SoftPositionConstraint : MonoBehaviour
{
    public Transform target;
    public float elasticity = 8; public float smearScale = 1;
    public float range;
    public float dis;
    bool inRange;
    Vector3 velocity = Vector3.zero;
    bool modscale = false;
	Vector3 originalScale;
    public UnityEvent<bool> OnLeftRange, OnJoinedRange;
	// Start is called before the first frame update
	void Start()
    {
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        var tv = target.position - transform.position;
        velocity = Vector3.Lerp(velocity, tv, Time.deltaTime * elasticity); ;
        transform.position += velocity;
        dis = Vector3.Distance(target.position, transform.position);
        if (dis > range) {
            inRange = false;
            OnLeftRange?.Invoke(false);
        } else if (dis < range && !inRange) {
            inRange=true;
            OnJoinedRange?.Invoke(true);
        } 
        //print(dis);
        if (modscale)
        transform.localScale = originalScale + (new Vector3(Mathf.Clamp01(velocity.x), 0, Mathf.Clamp01(velocity.y))*smearScale);
    }
}
