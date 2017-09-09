using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Mover : NetworkBehaviour {

    public static Action</*string,*/ Controller> CallMover;

    Controller controller;


	// Use this for initialization
	void Start ()
    {

        if (!isLocalPlayer)
        {
            Destroy(this);
            return;
        }
            

        CallMover += CallMoverHandler;
    }

    private void CallMoverHandler(/*string command,*/ Controller mover)
    {
        controller = mover;
        if (controller.touching)
        {
            StartCoroutine(MoveController());
        }
        if(controller.clicked)
        {
            StartCoroutine(Jump());
        }
        if(controller.pulledTrigger)
        {
            StartCoroutine(ShootGun());

        }
            //switch(command)
        //{
        //    case "Touching":
                
        //        StartCoroutine(MoveController());
        //        break;
        //    case "Not Touching":
        //        break;
        //    default:
        //        return;

        //}
    }


    IEnumerator MoveController()
    {
        yield return new WaitForFixedUpdate();
        controller.device = SteamVR_Controller.Input((int)controller.trackedObject.index);
        controller.touchSpot = new Vector2(controller.device.GetAxis().x, controller.device.GetAxis().y);
        if (controller.touchSpot != new Vector2(0, 0))
        {
            transform.Translate(0, 0, (controller.touchSpot.y * controller.moveSpeed) * Time.deltaTime);
            transform.Rotate(0, (controller.touchSpot.x * controller.rotateSpeed) * Time.deltaTime, 0);
        }
        if (controller.touching)
            StartCoroutine(MoveController());

    }

    IEnumerator ShootGun()
    {
        yield return new WaitForFixedUpdate();

        controller.bullet.transform.Translate(Vector3.forward * controller.bulletSpeed * Time.deltaTime);

        yield return new WaitForSeconds(1);


        controller.bullet.transform.position = controller.nose.GetComponent<Transform>().position;
        controller.bullet.transform.localEulerAngles = controller.nose.localEulerAngles;
    }

    IEnumerator Jump()
    {
        yield return new WaitForFixedUpdate();
        transform.Translate(Vector3.up * controller.jumpSpeed * Time.deltaTime);
        if (controller.clicked == true && controller.frameCount < controller.jumpAmount)
            StartCoroutine(Jump());
    }
}


