using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueObject testDialogue;
    
    private TypewriterEffect _typewriterEffect;

    private InputDevice rControl;
    private InputDevice lControl;
    
    private AsyncOperation _asyncOperation;
    private string _sceneName = "LeScene";
    
    private IEnumerator LoadSceneAsyncProcess(string sceneName)
    {
        _asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        _asyncOperation.allowSceneActivation = false;
        while (!_asyncOperation.isDone)
        {
            yield return null;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        rControl = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        lControl = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        _typewriterEffect = GetComponent<TypewriterEffect>();
        this.StartCoroutine(LoadSceneAsyncProcess(sceneName: _sceneName));
        ShowDialogue(testDialogue);
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        foreach (string dialogue in dialogueObject.Dialogue)
        {
            yield return _typewriterEffect.Run(dialogue, textLabel);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.anyKeyDown);
        }

        _asyncOperation.allowSceneActivation = true;
    }
    
    

}
