using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ZTarget : MonoBehaviour {


    public int speed = 10;
    public Transform focus;
    public Quaternion newRotate;
    bool touching;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            if (Input.GetButton("Fire1"))
            {
                touching = true;
                StartCoroutine(FirstLook());
                //transform.Rotate(focus.position, Vector3., Input.GetAxis("Horizontal") * speed * Time.deltaTime)
            }
            else
            {
                touching = false;
            }
            transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, Input.GetAxis("Vertical") * speed * Time.deltaTime);
        }
        
		
	}

    IEnumerator FirstLook()
    {
        newRotate = Quaternion.LookRotation(focus.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotate, )
        yield return new WaitForSeconds(1.5f);

    }

    IEnumerator ZTargeting()
    {
        transform.LookAt(new Vector3(focus.position.x, transform.position.y, focus.position.z));
        yield return new WaitForFixedUpdate();
        if (touching)
            StartCoroutine(ZTargeting());
    }
}
