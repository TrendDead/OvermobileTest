using UnityEngine;

public class ContentSpawner : MonoBehaviour
{
    public PlayerPanel Palyer => _player;

    [SerializeField]
    private PlayerPanel _playerPanelPrefab;
    [SerializeField]
    private int _numberPanels = 50;

    private PlayerPanel _player;

    private void Awake()
    {
        int number = Random.Range(0, _numberPanels);

        for (int i = 0; i < _numberPanels; i++)
        {
            var player = Instantiate(_playerPanelPrefab, this.transform);

            player.UpdateInfo(i + 1, number == i);

            if (i == number)
            {
                _player = player;
            }
        }
    }
}
