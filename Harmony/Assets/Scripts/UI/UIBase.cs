using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIBase : MonoBehaviour
{
    [SerializeField] private GameObject root;
    [SerializeField] private List<Button> closeButtons;
    private bool isInitialized = false;

    private void Awake()
    {
        foreach (var button in closeButtons)
        {
            button.onClick.AddListener(Hide);
        }
    }

    #region Initialization

    public void Initialize()
    {
        if (isInitialized) return;

        OnInitialize();

        isInitialized = true;
    }

    protected abstract void OnInitialize();

    #endregion

    #region Show / Hide
    public void Show()
    {
        if (root != null)
            root.SetActive(true);

        OnShow();
    }

    public virtual void OnShow() { }

    public void Hide()
    {
        if (root != null)
            root.SetActive(false);

        OnHide();
    }

    public virtual void OnHide() { }

    #endregion

    public bool IsActive => root.activeSelf;
}
