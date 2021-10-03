using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LinkClicker : MonoBehaviour
{
    private TextMeshProUGUI textComponent = null;

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    private void LateUpdate()
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(textComponent, Input.mousePosition, null);

        if (linkIndex != -1 && Input.GetMouseButtonDown(0))
        {
            TMP_LinkInfo linkInfo = textComponent.textInfo.linkInfo[linkIndex];

            Application.OpenURL(linkInfo.GetLinkText());
        }
    }
}
