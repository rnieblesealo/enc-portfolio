using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class InfoPanel : MonoBehaviour
{
    [SerializeField] private Text titleT;
    [SerializeField] private Text descriptionT;
    [SerializeField] private Image preview;
    [SerializeField] private string url;

    private RectTransform rectTransform;

    public float transitionTime;
    public bool active = true;

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
            foreach (Graphic graphic in GetComponentsInChildren<Graphic>())
                graphic.CrossFadeAlpha(1, transitionTime, true);
        }

        else
        {
            // Make transparent
            foreach (Graphic graphic in GetComponentsInChildren<Graphic>())
                graphic.CrossFadeAlpha(0, transitionTime, true);
        }
    }

    public void Set(InfoContainer info)
    {
        // Update information
        titleT.text = info.title;
        descriptionT.text = info.description;
        preview.sprite = info.preview;
        url = info.hyperlink;
    }
}
