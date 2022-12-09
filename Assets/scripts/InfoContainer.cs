using UnityEngine;

[CreateAssetMenu(fileName = "Artifact Info", menuName = "Info Container")]
public class InfoContainer : ScriptableObject
{
    public string title;
    public string description;
    public string hyperlink;
    public Sprite preview;
}
