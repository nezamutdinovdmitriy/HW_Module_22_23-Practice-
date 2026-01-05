using System.Collections;
using TMPro;
using UnityEngine;

public class ConfirmPopup : MonoBehaviour
{
    [SerializeField] private TMP_Text _messageText;

    public void Show() => gameObject.SetActive(true);
    public void Hide() => gameObject.SetActive(false);

    public void ShowMessage(string message) => _messageText.text = message;

    public IEnumerator WaitConfirm(KeyCode keyForConfirm)
    {
        yield return new WaitWhile(() => Input.GetKeyDown(keyForConfirm) == false);
    }
}
