using UnityEngine;

public class Locomotor : MonoBehaviour
{
    public float speed;
    public float minDistance;
    public float smoothTime;
    public bool move;
    
    [HideInInspector] public float distance = 0;
    
    private CharacterController controller;
    
    private Vector3 target = Vector3.zero;
    private Vector3 velocity = Vector3.zero;
    private Quaternion rotation = Quaternion.identity;
    
    private void Awake(){
        controller = GetComponent<CharacterController>();
    }

    private void Update(){
        Ray mouse = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Take target
        target = Physics.Raycast(mouse, out RaycastHit hit, 500, Physics.AllLayers) ? hit.point : Vector3.zero;

        // If target OK make car follow target
        if (target != Vector3.zero){
            float dz = target.z - transform.position.z;
            float dx = target.x - transform.position.x;

            float theta = Mathf.Atan2(dz, dx);

            // If min distance isn't reached vel and rot are default values
            Vector3 new_vel = Vector3.zero;
            Quaternion new_rot = transform.localRotation;
            
            distance = Vector3.Distance(transform.position, target);
            move = distance >= minDistance;
            if (move){
                new_vel = new Vector3(Mathf.Cos(theta), 0,  Mathf.Sin(theta)) * speed * distance * Time.deltaTime;
                new_rot = Quaternion.Euler(0, -(theta * Mathf.Rad2Deg) + 90, 0);  // Rotate car, note we convert theta which is in rads to degs, negatiev it, and add 90 to make model rotation line up
            }
                            
            // Smooth out velocity change
            velocity = Vector3.Lerp(velocity, new_vel, smoothTime * Time.deltaTime);
            rotation = Quaternion.Lerp(rotation, new_rot, smoothTime * Time.deltaTime);

            // Apply movement and rotation
            controller.Move(velocity);
            transform.localRotation = rotation;
        }
    }
}
