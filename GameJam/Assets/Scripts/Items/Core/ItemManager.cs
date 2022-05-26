using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    private List<Item> _items;
    private Item _currentItem;
    private bool _canActiveItem;

    private bool _haveEnabledItem;

    public void Initialize()
    {
        _haveEnabledItem = false;
        _canActiveItem = true;

        foreach (var item in _items)
        {
            item.Initialize();
            item.Disable();
        }
        _items[0].Enable();
        _currentItem = _items[0];
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            _canActiveItem = false;

        if (Input.GetMouseButtonUp(0))
            _canActiveItem = true;

        if (!_canActiveItem)
            return;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            EnableNextItem();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ActiveItem();
        }


    }

    private void ActiveItem()
    {
        if (!_haveEnabledItem)
        {
            _currentItem.Active();
            _haveEnabledItem = true;
        }
        else
        {
            _currentItem.DeActive();
            _haveEnabledItem = false;
        }
    }

    private void EnableNextItem()
    {
        bool nextActive = false;
        for (int i = 0; i < _items.Count; i++)
        {
            if (nextActive)
            {
                _items[i].Enable();
                _currentItem = _items[i];
                return;
            }

            if (_items[i].IsEnable)
            {
                _items[i].Disable();
                _haveEnabledItem = false;
                nextActive = true;
            }
        }

        _items[0].Enable();
        _currentItem = _items[0];

    }
}
