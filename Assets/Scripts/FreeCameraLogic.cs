using System.Collections.Generic;
using UnityEngine;

public class FreeCameraLogic : MonoBehaviour
{

    public Transform currentTarget = null;

    private void LateUpdate()
    {
        Vector3 position = currentTarget.position;


        transform.position = new Vector3(position.x,position.y+4f,position.z-7f);
        transform.rotation = new Quaternion(20f, 0f, 0f, 180f);

    }
}
