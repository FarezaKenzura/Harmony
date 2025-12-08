using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIBase : MonoBehaviour
{
    [SerializeField] private GameObject _root;
    [SerializeField] private List<Button> _closeButtons;
    private bool _isInitialized = false;

    private void Awake()
    {
        foreach (var button in _closeButtons)
        {
            button.onClick.AddListener(Hide);
        }
    }

    #region Initialization

    public void Initialize()
    {
        if (_isInitialized) return;

        OnInitialize();

        _isInitialized = true;
    }

    protected abstract void OnInitialize();

    #endregion

    #region Show / Hide
    public void Show()
    {
        if (_root != null)
            _root.SetActive(true);

        OnShow();
    }

    public virtual void OnShow() { }

    public void Hide()
    {
        if (_root != null)
            _root.SetActive(false);

        OnHide();
    }

    public virtual void OnHide() { }

    #endregion

    public bool IsActive => _root.activeSelf;
}
