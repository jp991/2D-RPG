using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private CanvasGroup dialoguePanel;
    [SerializeField] private RectTransform dialogueDataHolder;

    
    private void Awake()
    {
        instance = this;
    }

    public void SetDialoguePanel(bool isActive = false)
    {
        if (isActive)
        {
            dialoguePanel.gameObject.SetActive(true);
            dialoguePanel.DOFade(1f, 1f);
            dialogueDataHolder.DOMoveY(0f, 1f);
        }
        else
        {
            dialogueDataHolder.DOMoveY(-400f, 1f);
            dialoguePanel.DOFade(0f, 1f).OnComplete(() =>
            {
                dialoguePanel.gameObject.SetActive(false);
            });
        }
    }

  
    
}
