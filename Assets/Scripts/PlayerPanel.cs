using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour
{
    public int Number => _number;

    [SerializeField]
    private Text _numberText;

    private Image _backImage;
    private int _number;
   
    private void Awake()
    {
        _backImage = GetComponent<Image>();
    }

    public void UpdateInfo(int newNumber, bool isPalyer)
    {
        _number = newNumber;
        _numberText.text = _number.ToString();
        PlayerUpdate(isPalyer);
    }

    private void PlayerUpdate(bool isPalyer) => _backImage.color = isPalyer ? Color.green : Color.white;

}
