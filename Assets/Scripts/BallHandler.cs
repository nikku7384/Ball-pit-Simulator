using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHandler : MonoBehaviour
{
    [SerializeField]
    private Transform holdPosition;
    [SerializeField]
    private float throwForce = 10f;
    [SerializeField]
    private float throwAngle = 45f;
    [SerializeField]
    private Camera plyCamera;
    private GameObject pickedUpBall = null;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (pickedUpBall == null)
            {
                PickBall();
            }
            else
                LeaveBall();
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (pickedUpBall != null)
            {
                ThrowStright();
            }
        }
        if(Input.GetMouseButtonDown(0))
        {
            if (pickedUpBall != null)
            {
                ThrowAngle();
            }
        }
    }

    //For picking up ball
    private void PickBall()
    {
        RaycastHit hit;
        Ray ray = plyCamera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit, 50f)) 
        {
            Debug.LogError(hit.collider.gameObject.name + "  " + hit.collider.gameObject.tag);
            if (hit.collider.CompareTag("Ball"))
            {
                pickedUpBall = hit.collider.gameObject;
                pickedUpBall.GetComponent<Rigidbody>().isKinematic = true; 
                pickedUpBall.transform.SetParent(plyCamera.transform); 
                pickedUpBall.transform.localPosition = new Vector3(0, 0, 2f);
            }
        }
    }

    //For throwing the ball stright
    private void ThrowStright()
    {
        pickedUpBall.GetComponent<Rigidbody>().isKinematic = false; 
        pickedUpBall.transform.SetParent(null); 
        Rigidbody rb = pickedUpBall.GetComponent<Rigidbody>();
        rb.AddForce(plyCamera.transform.forward * throwForce, ForceMode.Impulse); 
        pickedUpBall = null; 
    }

    //For throwing the ball and angle 
    private void ThrowAngle()
    {
        pickedUpBall.GetComponent<Rigidbody>().isKinematic = false; 
        pickedUpBall.transform.SetParent(null); 
        Rigidbody rb = pickedUpBall.GetComponent<Rigidbody>();

        Vector3 throwDirection = Quaternion.AngleAxis(-45, plyCamera.transform.right) * plyCamera.transform.forward;
        rb.AddForce(throwDirection * throwForce, ForceMode.Impulse); 
        pickedUpBall = null; 
    }

    //If already picked up then leaving the ball 
    private void LeaveBall()
    {
        pickedUpBall.GetComponent<Rigidbody>().isKinematic = false; 
        pickedUpBall.transform.SetParent(null); 
        pickedUpBall = null; 
    }

    private void OnDrawGizmos()
    {
        if (plyCamera != null)
        {
            Ray ray = plyCamera.ScreenPointToRay(Input.mousePosition);
            Gizmos.color = Color.red; 
            Gizmos.DrawRay(ray.origin, ray.direction * 10f);
        }
    }
}
