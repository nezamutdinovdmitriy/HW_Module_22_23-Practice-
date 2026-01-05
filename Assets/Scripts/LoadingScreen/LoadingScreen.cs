using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Image _throbber;
    [SerializeField] private TMP_Text _messageText;
    [SerializeField] private float _speedRotateThrobber = 100;

    public void Show() => gameObject.SetActive(true);

    public void Hide() => gameObject.SetActive(false);

    public void ShowMessage(string text) => _messageText.text = text;

    private void Update() => _throbber.transform.Rotate(Vector3.forward * Time.deltaTime * _speedRotateThrobber, Space.World);
}
