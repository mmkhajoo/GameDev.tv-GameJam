using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DragManager : MonoBehaviour, IDragManager
{
    #region Fields

    [SerializeField]
    private List<SlotItem> _slots;
    [SerializeField]
    private List<DragItem> _dragItems;

    private Action<IDragable, ISlot> _onRelease;
    private Action _onDrag;
    private DragItem _selectedDragble;
    private Rigidbody2D _selectedObjectRb;
    private Vector3 offset;
    private Vector3 mousePosition;

    #endregion

    #region Properties

    public Vector3 DragPosition => GetDragPosition();
    public Action<IDragable, ISlot> OnRelease => _onRelease;
    public Action OnDrag => _onDrag;
    public DragItem SelectedObject => _selectedDragble;

    #endregion

    #region Public Methods

    public void Initialize()
    {
        foreach (var slot in _slots)
        {
            if(slot.DirectionType == DirectionType.Up)
            {
                foreach (var drag in _dragItems)
                {
                    slot.OnPlaceDragItem(drag, drag.transform.position);
                    drag.OnInvalidePlacement();
                }
            }
        }
    }

    public void RegiterCallBack(Action onDrag, Action<IDragable, ISlot> onRelease)
    {
        _onDrag += onDrag;
        _onRelease += onRelease;
    }

    #endregion

    #region Private Methods

    private Vector3 GetDragPosition()
    {
        if (_selectedDragble.IsScrollDrag)
        {
            return _selectedDragble.GetScrollPosition(mousePosition);
        }

        return mousePosition + offset;
    }

    private void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    private void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            Collider2D[] targetObject = Physics2D.OverlapPointAll(mousePosition);
            foreach (var item in targetObject)
            {
                if (item.TryGetComponent<IDragable>(out IDragable drag))
                {
                    _selectedDragble = (DragItem)drag;

                    OnDrag?.Invoke();

                    _selectedObjectRb = item.transform.gameObject.GetComponent<Rigidbody2D>();
                    offset = _selectedObjectRb.transform.position - mousePosition;
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && _selectedObjectRb)
        {
            if(_selectedDragble.IsScrollDrag)
            {
                _selectedDragble.CurrentSlot.OnPlaceDragItem((DragItem)SelectedObject, DragPosition);
                _selectedObjectRb = null;
                _selectedDragble = null;
                return;
            }

            Collider2D[] targetObject = Physics2D.OverlapPointAll(mousePosition);

            foreach (var item in targetObject)
            {
                if (item.TryGetComponent<ISlot>(out ISlot slot))
                {
                    slot.OnPlaceDragItem((DragItem)SelectedObject, DragPosition);

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

    private void FixedUpdate()
    {
        if (_selectedObjectRb)
        {
            _selectedObjectRb.MovePosition(DragPosition);
        }
    }

    #endregion
}
