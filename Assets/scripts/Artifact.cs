using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[SelectionBase]
public class Artifact : MonoBehaviour
{
    public InfoContainer panelInfo;
    public float smoothTime;
    public float defaultScale;
    public bool isSelected;

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, isSelected ? (Vector3.one * defaultScale) * 1.5f : (Vector3.one * defaultScale), smoothTime * Time.deltaTime);
    }
}
