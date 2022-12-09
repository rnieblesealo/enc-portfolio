using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Welcome : MonoBehaviour
{
    public string[] messages;
    public float nextSlideDelay;

    [SerializeField] private Text welcomeText;
    [SerializeField] private Image welcomeBackground;
    private float nextSlideTime = 0;
    private int currentSlide = -1;

    private void Awake()
    {
        welcomeText = GetComponent<Text>();
    }

    private void Update()
    {
        if (Time.time >= nextSlideTime && currentSlide < messages.Length)
        {
            // Update timer
            nextSlideTime += nextSlideDelay;

            // Change slide
            currentSlide++;

            if (currentSlide >= messages.Length)
                return;

            welcomeText.text = messages[currentSlide];
        }

        // Empty text when over
        else if (currentSlide == messages.Length)
        {
            welcomeText.text = string.Empty;
            welcomeBackground.CrossFadeAlpha(0, 0, true);
        }
    }
}
