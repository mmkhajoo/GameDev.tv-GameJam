using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDragManager 
{
    IDragable SelectedObject { get; }

    Action<IDragable,ISlot> OnRelease { get; }

    Action OnDrag { get; }

    void RegiterCallBack(Action onDrag, Action<IDragable, ISlot> onRelease);
}
