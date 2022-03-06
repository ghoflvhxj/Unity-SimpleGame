using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractionUiPannel : MonoBehaviour
{
    [SerializeField] private Image interactionTextBackground;
    [SerializeField] private TextMeshProUGUI interactionText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string text)
    {
        interactionTextBackground.enabled = true;
        interactionText.SetText(text);
    }

    public void Reset()
    {
        interactionTextBackground.enabled = false;
        interactionText.SetText("");
    }
}
