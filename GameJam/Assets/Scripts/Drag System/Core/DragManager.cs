using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DragManager : MonoBehaviour, IDragManager
{
    #region Fields

    private Action<IDragable, ISlot> _onRelease;
    private Action _onDrag;
    private IDragable _selectedDragble;
    private Rigidbody2D _selectedObjectRb;
    private Vector3 offset;
    private Vector3 mousePosition;

    #endregion

    #region Properties

    public Vector3 DragPosition => mousePosition + offset;
    public Action<IDragable, ISlot> OnRelease => _onRelease;
    public Action OnDrag => _onDrag;
    public IDragable SelectedObject => _selectedDragble;

    #endregion

    #region Public Methods

    public void RegiterCallBack(Action onDrag, Action<IDragable, ISlot> onRelease)
    {
        _onDrag += onDrag;
        _onRelease += onRelease;
    }

    #endregion

    #region Private Methods

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            Collider2D[] targetObject = Physics2D.OverlapPointAll(mousePosition);
            foreach (var item in targetObject)
            {
                if (item.TryGetComponent<IDragable>(out IDragable drag))
                {
                    _selectedDragble = drag;

                    OnDrag?.Invoke();

                    _selectedObjectRb = item.transform.gameObject.GetComponent<Rigidbody2D>();
                    offset = _selectedObjectRb.transform.position - mousePosition;
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && _selectedObjectRb)
        {

            Collider2D[] targetObject = Physics2D.OverlapPointAll(mousePosition);

            foreach (var item in targetObject)
            {
                if (item.TryGetComponent<ISlot>(out ISlot slot))
                {
                    slot.OnRelease((DragItem)SelectedObject, DragPosition);

                    OnRelease?.Invoke(SelectedObject, slot);

                    _selectedObjectRb = null;
                    _selectedDragble = null;

                    return;
                }
            }

            _selectedDragble.OnInvalidePlacement();

            _selectedObjectRb = null;
            _selectedDragble = null;

        }

    }

    void FixedUpdate()
    {
        if (_selectedObjectRb)
        {
            _selectedObjectRb.MovePosition(DragPosition);
        }
    }

    #endregion
}
