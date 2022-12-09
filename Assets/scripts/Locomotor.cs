using UnityEngine;

[SelectionBase]
public class Locomotor : MonoBehaviour
{
    public float speed;
    public float minDistance;
    public float smoothTime;
    public bool stopped;
    public bool move;
    public LayerMask whatIsTarget;
    public LayerMask whatIsSelectable;
    
    [HideInInspector] public float distance = 0;
    
    private CharacterController controller;
    private InfoPanel panel;
    private Artifact selectedArtifact;
    private Vector3 moveTarget = Vector3.zero;
    private Vector3 velocity = Vector3.zero;
    private Quaternion rotation = Quaternion.identity;
    
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        panel = FindObjectOfType<InfoPanel>();
    }

    private void Update(){
        // Create ray that points to where mouse is
        Ray mouse = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Take target
        moveTarget = Physics.Raycast(mouse, out RaycastHit hit, 500, whatIsTarget) ? hit.point : Vector3.zero;

        // If target OK make car follow target
        if (moveTarget != Vector3.zero)
        {
            float dz = moveTarget.z - transform.position.z;
            float dx = moveTarget.x - transform.position.x;

            float theta = Mathf.Atan2(dz, dx);

            // If min distance isn't reached vel and rot are default values
            Vector3 new_vel = Vector3.zero;
            Quaternion new_rot = transform.localRotation;
            
            distance = Vector3.Distance(transform.position, moveTarget);
            move = distance >= minDistance && !stopped && Application.isFocused;

            if (move)
            {
                new_vel = speed * Time.deltaTime * new Vector3(Mathf.Cos(theta), 0,  Mathf.Sin(theta));
                new_rot = Quaternion.Euler(0, -(theta * Mathf.Rad2Deg) + 90, 0);  // Rotate car, note we convert theta which is in rads to degs, negatiev it, and add 90 to make model rotation line up
            }
                            
            // Smooth out velocity change
            velocity = Vector3.Lerp(velocity, new_vel, smoothTime * Time.deltaTime);
            rotation = Quaternion.Lerp(rotation, new_rot, smoothTime * Time.deltaTime);

            // Apply movement and rotation
            controller.Move(velocity);
            transform.localRotation = rotation;
        }

        // Generate a second target that only considers important object layers
        if (Physics.Raycast(mouse, out RaycastHit uiHit, 500, whatIsSelectable))
        {
            // Try get artifact component, if successful, activate panel, pass it info, and stop car from moving
            selectedArtifact = uiHit.transform.GetComponent<Artifact>();

            if (selectedArtifact)
            {
                selectedArtifact.isSelected = true;

                if (selectedArtifact.panelInfo)
                {
                    panel.Set(selectedArtifact.panelInfo);
                    panel.active = true;
                    stopped = true;

                    // If click on artifact, open URL of item
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        Application.OpenURL(selectedArtifact.panelInfo.hyperlink);
                    }
                }

                else
                    Debug.LogWarning("No artifact info attached to artifact!");
            }
        }

        // If nothing was hit, make panel inactive. Car can move again; car stops when we are looking at an artifact
        else
        {
            if (selectedArtifact)
            {
                selectedArtifact.isSelected = false;
                panel.active = false;
                stopped = false;
            }
        }
    }
}
