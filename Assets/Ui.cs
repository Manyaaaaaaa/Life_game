using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ui : MonoBehaviour {

    [SerializeField] public CreateGame createGame;
    [SerializeField] public GameObject cameraGO;

    [Header("Variables")]
    [SerializeField] public int sizeArena = 5;
    [SerializeField] public int countEvolution = 5;

    [Header("Ui")]
    [SerializeField] public TMP_InputField inputSize;
    [SerializeField] public TextMeshProUGUI textCountEvolv; 

    void Start() {
        createGame.sizeX = sizeArena;
        createGame.sizeY = sizeArena;

        createGame.SetSetting();
        NewPositionCamera();
    }

    public void SetArenaSize() {
        int num = 5;

        if (int.TryParse(inputSize.text, out int numIf)) {
            if(numIf < 1 || numIf > 50) {
                numIf = 5;
            }

            num = numIf;
        }

        inputSize.text = num.ToString();

        createGame.sizeX = num;
        createGame.sizeY = num;
        sizeArena = num;

        createGame.SetSetting();
        NewPositionCamera();
    }

    public void PlusEvolution() {
        countEvolution++;
        textCountEvolv.text = countEvolution.ToString();
    }

    public void MinusEvolution() {
        countEvolution--;

        if(countEvolution < 1) { countEvolution = 1; }

        textCountEvolv.text = countEvolution.ToString();
    }

    public void StartEvolution() {
        StopAllCoroutines();
        StartCoroutine(LifeVisual());
    }

    private IEnumerator LifeVisual() {
        for(int i = 0; i < countEvolution; i++) {
            createGame.StartLife();

            yield return new WaitForSeconds(0.4f);
        }
    }

    public void Clear() {
        StopAllCoroutines();
        createGame.SetSetting();
    }

    public void ExitApp() {
        Application.Quit();
    }


    private void NewPositionCamera() {
        Vector3 newVec = new Vector3(sizeArena / 2.5f + sizeArena * 0.1f, -sizeArena / 2.5f, -sizeArena + (-sizeArena * 0.2f));

        cameraGO.transform.position = newVec;
    }
}
