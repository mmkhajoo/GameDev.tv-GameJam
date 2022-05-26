using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private IItem _activeItem;

    [SerializeField]
    private List<IItem> _items;


    private void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            _activeItem.DeActive();
        }
    }

}
