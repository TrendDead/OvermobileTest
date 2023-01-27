using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(ScrollRect))]
public class CustomScroll : MonoBehaviour
{
    [SerializeField]
    private float _topSpace = 0;
    [SerializeField]
    private float _bottomSpace = 0;
    [SerializeField]
    private ContentSpawner _contentSpawner;

    private RectTransform _playerPanelTransform;
    private PlayerPanel _playerPanel;
    private ScrollRect _scrollRect;
    private float _maxPanelPosition;
    private float _minPanelPosition;
    private Vector2 _startPos;

    private void Awake()
    {
        _scrollRect = GetComponent<ScrollRect>();
        _scrollRect.onValueChanged.AddListener(PositionPlayerPanel);
    }

    private void OnDestroy()
    {
        _scrollRect.onValueChanged.RemoveListener(PositionPlayerPanel);
    }

    private void Start()
    {
        _playerPanel = _contentSpawner.Palyer;
        _playerPanelTransform = _playerPanel.GetComponent<RectTransform>();

        _maxPanelPosition = gameObject.GetComponent<RectTransform>().rect.height + _scrollRect.viewport.transform.position.y - (_playerPanelTransform.rect.height / 2) - _topSpace;
        _minPanelPosition = _scrollRect.viewport.transform.position.y + (_playerPanelTransform.rect.height / 2) + _bottomSpace;
        _playerPanel.gameObject.AddComponent<Canvas>().overrideSorting = true;

        StartCoroutine(WaitFrame());
    }

    private IEnumerator WaitFrame()
    {
        yield return new WaitForEndOfFrame();

        _startPos = _playerPanelTransform.localPosition;
        PositionPlayerPanel(Vector2.zero);
    }

    public void PositionPlayerPanel(Vector2 vector)
    {
        var globalPos = _contentSpawner.transform.TransformPoint(_startPos);

        if (globalPos.y > _minPanelPosition && globalPos.y < _maxPanelPosition)
        {
            _playerPanelTransform.localPosition = _startPos;
        }
        else if (globalPos.y < _minPanelPosition)
        {
            _playerPanelTransform.position = new Vector2(_playerPanelTransform.position.x, _minPanelPosition);
        }
        else if (globalPos.y > _maxPanelPosition)
        {
            _playerPanelTransform.position = new Vector2(_playerPanelTransform.position.x, _maxPanelPosition);
        }
    }
}