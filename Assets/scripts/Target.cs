using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    Transform player;
    
    public LayerMask whatIsTarget;
    
    private void Awake(){
        player = FindObjectOfType<Locomotor>().GetComponent<Transform>();
    }

    void Update(){
        // Lock mouse
        Cursor.visible = false;

        // Target tracks player
        Ray screenRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(screenRay, out RaycastHit hit, 500, whatIsTarget)){
            Vector3 target = hit.point;
            transform.position = new Vector3(hit.point.x, hit.point.y + 0.5f, hit.point.z);
        }
    }
}
