using UnityEngine;

[CreateAssetMenu(fileName = "Artifact Info", menuName = "Info Container")]
public class InfoContainer : ScriptableObject
{
    public string title;
    public string description;
    public string hyperlink;
    public string extra1;
    public string extra2;
    public Color extra1Color;
    public Color extra2Color;
    public Color extra1TextColor;
    public Color extra2TextColor;
    public Sprite preview;
}
