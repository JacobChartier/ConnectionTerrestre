using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public static Tooltip Instance;

    [SerializeField] RectTransform canvasRectTransform;

    [SerializeField] private Image background;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text content;

    private const int MAX_TOOLTIP_WIDTH = 450;

    private void Awake()
    {
        Instance = this;

        Hide();
    }

    private void Update()
    {
        Vector2 anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;

        if (anchoredPosition.x + background.rectTransform.rect.width > canvasRectTransform.rect.width)
        {
            anchoredPosition.x = canvasRectTransform.rect.width - background.rectTransform.rect.width;
        }

        if (anchoredPosition.y + background.rectTransform.rect.height > canvasRectTransform.rect.height)
        {
            anchoredPosition.y = canvasRectTransform.rect.height - background.rectTransform.rect.height;
        }

        this.gameObject.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
    }

    public void Show(string text)
    {
        gameObject.SetActive(true);

        this.title.text = text;

        this.title.ForceMeshUpdate();

        float sizeX = this.title.GetRenderedValues(false).x;
        if (sizeX > MAX_TOOLTIP_WIDTH) {
            sizeX = MAX_TOOLTIP_WIDTH;
        }

        Vector2 backgroundSize = new Vector2(sizeX, this.title.GetRenderedValues(false).y);
        Vector2 padding = new Vector2(10, 10);

        this.background.rectTransform.sizeDelta = backgroundSize + padding;
    }

    public void Show(string title, string content)
    {
        gameObject.SetActive(true);

        this.title.text = $"<b>{title}</b>\n{content}";

        this.title.ForceMeshUpdate();

        float sizeX = this.title.GetRenderedValues(false).x;
        if (sizeX > MAX_TOOLTIP_WIDTH)
        {
            sizeX = MAX_TOOLTIP_WIDTH;
        }

        Vector2 backgroundSize = new Vector2(sizeX, this.title.GetRenderedValues(false).y);
        Vector2 padding = new Vector2(10, 10);

        this.background.rectTransform.sizeDelta = backgroundSize + padding;

    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetColors(Color bg = default, Color outlines = default)
    {
        background.color = bg;
        background.GetComponent<Outline>().effectColor = outlines;
    }
}
