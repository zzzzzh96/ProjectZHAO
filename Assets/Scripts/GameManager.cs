using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<string> resultString = new List<string>();
    public List<string> resultString2 = new List<string>();
    public List<string> currentString = new List<string>();
    public List<GameObject> objs = new List<GameObject>();
    public GameObject winText;
    public GameObject finishText;
    public GameObject resetErrorText;
    public GameObject resetErrorTextWon;
    public TextMeshProUGUI finishedText;
    public TextMeshProUGUI finishedText2;
    public TextMeshProUGUI currentText;
    public string finishedString;
    public string curString;
    public int point;
    public int leftPoint;

    float currCountdownValue;
    void Start()
    {
        point = 0;
        leftPoint = 4;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool checkString(List<string> resStrings) {
        if (resStrings.Count != currentString.Count) {
            return false;
        }
        //Debug.Log("same size");
        for (int i = 0; i < resStrings.Count; i++) {
            //Debug.Log("checking i:" + i);
            Debug.Log("resString[" + i + "]: " + resStrings[i]);
            Debug.Log("currentString[" + i + "]: " + currentString[i]);
            if (resStrings[i] != currentString[i]) {
                return false;
            }
        }
        return true;
    }

    private void killBalls(string str) {
        //selectText.text += str;
        for (int i = 0; i < objs.Count; i++) {
            if (str == objs[i].GetComponent<BallManager>().str) {
                objs[i].SetActive(false);
                leftPoint--;
            }
        }
    }

    private void currentStringUpdate() {
        curString = "";
        for (int i = 0; i < currentString.Count; i++) {
            curString = curString + currentString[i];
        }
        currentText.SetText(curString);

    }

    private void win(List<string> winString) {
        for (int i = 0; i < winString.Count; i++) {
            
            killBalls(winString[i]);
            currentString.Remove(winString[i]);
        }
        currentStringUpdate();
        point++; ;
        winText.SetActive(true);
        StartCoroutine(StartCountdown(1.0f,winText));
        if(leftPoint==0)    finishText.SetActive(true);
    }

    public void addString(string str) {
        //添加元素
        currentString.Add(str);
        currentStringUpdate();
        //stringUpdate();
        checkUpdate();
        
    }
    public void checkUpdate() {
        if (checkString(resultString))
        {
            win(resultString);
            finishedString = "";
            for (int i = 0; i < resultString.Count; i++)
            {
                finishedString = finishedString + resultString[i];
            }
            finishedText.SetText(finishedString);
            Debug.Log("check result string 1");
        }
        Debug.Log("check result string 1.5");
        if (checkString(resultString2))
        {
            win(resultString2);
            finishedString = "";
            for (int i = 0; i < resultString2.Count; i++)
            {
                finishedString = finishedString + resultString2[i];
            }
            finishedText2.SetText(finishedString);
            Debug.Log("check result string 2");
        }
    }

    public void delString(string str) {
        //删除元素
        for (int i = 0; i < currentString.Count; i++) {
            if (str == currentString[i]) {
                Debug.Log("删除第i个: " + i);
                Debug.Log("删除元素为: " + currentString[i]);
                currentString.Remove(currentString[i]);

            }
        }
        currentStringUpdate();
        checkUpdate();
    }

    public void cleanString() {
        //删除全部元素
        for (int i = 0; i < objs.Count; i++)
            for (int j = 0; j < currentString.Count; j++) {
            {
                if (objs[i].GetComponent<BallManager>().str == currentString[j]) {
                    objs[i].GetComponent<BallManager>().OffChose();
                }
            }
        }
        //currentString.Clear();
        Debug.Log("删除全部元素");
    }

    public void restartScene() {
        SceneManager.LoadScene("Game");
        cleanString();
        resetBalls();
    }

    public void resetBalls() {
        if (point!=0){
            resetErrorTextWon.SetActive(true);
            StartCoroutine(StartCountdown(1.0f, resetErrorTextWon));
            return;
        }
        //当前list不为空，无法重置
        if (currentString.Count != 0) {
            resetErrorText.SetActive(true);
            StartCoroutine(StartCountdown(1.0f, resetErrorText));
            return;
        }
        for (int i = 0; i < objs.Count; i++) {
            Vector3 randomPosition = objs[i].transform.position;
            randomPosition.x = Random.Range(-1.5f, 1.5f);
            randomPosition.y = Random.Range(2.5f, 6.5f);
            objs[i].transform.position = randomPosition;
            objs[i].GetComponent<ParticleSystem>().Play();
        }
        StartCoroutine(stopParticle());
    }

    IEnumerator stopParticle() {
        yield return new WaitForSeconds(2.0f);
        for (int i = 0; i < objs.Count; i++) {
            objs[i].GetComponent<ParticleSystem>().Stop();
        }
    }

    public IEnumerator StartCountdown(float countdownValue,GameObject obj)
    {
        currCountdownValue = countdownValue;
        while (currCountdownValue >= 0)
        {
            //Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
            if (currCountdownValue <= 0) {
                obj.SetActive(false);
            }
        }
    }

    public void SaveBalls() {

        SaveSystem.SaveBall(this);
    }

    public void LoadBalls() {

        BallData data = SaveSystem.LoadBall();

        for (int i = 0; i < 4; i++) {
            if (data.isActive[i] == 1){
                objs[i].SetActive(true);
            }
            else {
                objs[i].SetActive(false);
            }
        }

        for (int i = 0; i < 4; i++) {
            if (data.isSeclected[i] == 1) {
                objs[i].GetComponent<BallManager>().isChosen = true;
            } else {
                objs[i].GetComponent<BallManager>().isChosen = false;
            }  
        }

        currentString.Clear();
        foreach (string str in data.currentString)
        {
            Debug.Log("str: " + str);
            currentString.Add(str);
        }

        

        Vector3 position01;
        position01.x = data.position01[0];
        position01.y = data.position01[1];
        position01.z = data.position01[2];
        objs[0].transform.position = position01;

        Vector3 position02;
        position02.x = data.position02[0];
        position02.y = data.position02[1];
        position02.z = data.position02[2];
        objs[1].transform.position = position02;

        Vector3 position03;
        position03.x = data.position03[0];
        position03.y = data.position03[1];
        position03.z = data.position03[2];
        objs[2].transform.position = position03;

        Vector3 position04;
        position04.x = data.position04[0];
        position04.y = data.position04[1];
        position04.z = data.position04[2];
        objs[3].transform.position = position04;

        Vector3 scale01;
        scale01.x = data.scale01[0];
        scale01.y = data.scale01[1];
        scale01.z = data.scale01[2];
        objs[0].transform.localScale = scale01;

        Vector3 scale02;
        scale02.x = data.scale02[0];
        scale02.y = data.scale02[1];
        scale02.z = data.scale02[2];
        objs[1].transform.localScale = scale02;

        Vector3 scale03;
        scale03.x = data.scale03[0];
        scale03.y = data.scale03[1];
        scale03.z = data.scale03[2];
        objs[2].transform.localScale = scale03;

        Vector3 scale04;
        scale04.x = data.scale04[0];
        scale04.y = data.scale04[1];
        scale04.z = data.scale04[2];
        objs[3].transform.localScale = scale04;

        point = data.point;

        leftPoint = data.leftPoint;

        
    }
}
