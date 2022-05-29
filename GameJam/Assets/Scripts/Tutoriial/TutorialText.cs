using DefaultNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialText : MonoBehaviour
{
    [SerializeField]
    private float _showTime;
    [SerializeField]
    private bool _haveDetectiion;
    [SerializeField]
    private Transform _playerDetect;

    private Transform _player;

    #region Events

    [Header("Events")]
    [Space]
    public UnityEvent OnShowUp;
    public UnityEvent OnClose;

    #endregion
    
    public void Close()
    {
        OnClose?.Invoke();
        LeanTween.scale(gameObject, Vector3.zero, _showTime).setOnComplete(() => { gameObject.SetActive(false); } );
    }

    public void Show()
    {
        gameObject.SetActive(true);
        LeanTween.scale(gameObject, Vector3.one, _showTime);
        OnShowUp?.Invoke();
    }
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (!_haveDetectiion)
            return;
        if (Vector2.Distance(_playerDetect.position, _player.position) < 2)
            Show();
    }
}
