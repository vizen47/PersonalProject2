using System;
using CoreLib;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public bool IsActiveCard { get; private set; }
    
    [field: SerializeField] public GameObject UseCardUI {get; private set;}
    
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (IsActiveCard)
            UseCardUI.SetActive(true);
        else
            UseCardUI.SetActive(false);
    }

    public void CheckIsHoveringCard(bool isHoveringCard)
    {
        IsActiveCard = isHoveringCard;
    }
}
