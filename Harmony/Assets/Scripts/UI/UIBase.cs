using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIBase : MonoBehaviour
{
    [SerializeField] private GameObject _root;

    private void OnEnable()
    {
        OnInitialize();
    }

    private void OnDisable()
    {
        OnUnitialize();
    }

    #region Initialization

    protected abstract void OnInitialize();

    protected abstract void OnUnitialize();

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
