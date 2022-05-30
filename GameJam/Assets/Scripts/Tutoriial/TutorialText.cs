using DefaultNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialText : MonoBehaviour
{
    [SerializeField]
    private bool _isOneTimeShow = true;
    [SerializeField]
    private bool _isShow;
    [SerializeField]
    private float _showTime;
    [SerializeField]
    private bool _haveDetectiion;
    [SerializeField]
    private Transform _playerDetect;

    private int _numberShow;
    private Transform _player;


    #region Events

    [Header("Events")]
    [Space]
    public UnityEvent OnShowUp;
    public UnityEvent OnClose;

    #endregion
    
    public void Close()
    {
        if (!_isShow)
            return;
        _isShow = false;
        OnClose?.Invoke();
        LeanTween.scale(gameObject, Vector3.zero, _showTime).setOnComplete(() => { gameObject.SetActive(false); } );
    }

    public void Show()
    {
        if(_isOneTimeShow)
        {
            if (_numberShow > 0)
                return;
        }
        _isShow = true;
        _numberShow++;
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
