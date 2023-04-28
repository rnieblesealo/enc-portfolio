using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Text titleT;
    [SerializeField] private Text descriptionT;
    [SerializeField] private Text extra1Text;
    [SerializeField] private Text extra2Text;
    [SerializeField] private Image extra1BG;
    [SerializeField] private Image extra2BG;
    [SerializeField] private Image preview;
    [SerializeField] private string url;
    [SerializeField] private Graphic[] graphics; // Everything except extra

    private RectTransform rectTransform;

    public float transitionTime;
    public bool active = true;
    public bool showExtra1 = true;
    public bool showExtra2 = true;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        // Set position to player
        transform.position = Input.mousePosition;

        if (active)
        {
            // Make opaque
            foreach (Graphic graphic in graphics)
                graphic.CrossFadeAlpha(1, transitionTime, true);
        }

        else
        {
            // Make transparent
            foreach (Graphic graphic in graphics)
                graphic.CrossFadeAlpha(0, transitionTime, true);
        }

        // Show extra information when active
        float extra1Dest = showExtra1 && active ? 1 : 0;
        float extra2Dest = showExtra2 && active ? 1 : 0;

        extra1BG.CrossFadeAlpha(extra1Dest, transitionTime, true);
        extra2BG.CrossFadeAlpha(extra2Dest, transitionTime, true);

        extra1Text.CrossFadeAlpha(extra1Dest, transitionTime, true);
        extra2Text.CrossFadeAlpha(extra2Dest, transitionTime, true);
    }

    public void Set(InfoContainer info)
    {
        // Update information
        titleT.text = info.title;
        descriptionT.text = info.description;
        preview.sprite = info.preview;
        url = info.hyperlink;

        // Optional information
        showExtra1 = info.extra1 != string.Empty;
        showExtra2 = info.extra2 != string.Empty;

        // Set extra if extra is set (lmao)
        if (showExtra1)
        {
            extra1Text.text = info.extra1;
            extra1Text.color = info.extra1TextColor;
            extra1BG.color = info.extra1Color;
        }

        if (showExtra2)
        {
            extra2Text.text = info.extra2;
            extra2Text.color = info.extra2TextColor;
            extra2BG.color = info.extra2Color;
        }
    }
}
