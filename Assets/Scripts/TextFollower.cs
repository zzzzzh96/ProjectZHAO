using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFollower : MonoBehaviour
{
    //text预制体
    public GameObject TextType;
    //生成text的引用对象
    private GameObject go;
    // Start is called before the first frame update
    private void Start()
    {
        go = Instantiate(TextType, GameObject.FindGameObjectWithTag("canvas").transform.GetChild(0).transform);
        //BallManager.str = go.GetComponent<Text>();
    }
    private void Update()
    {

        go.transform.position = TextType.transform.position = Camera.main.WorldToScreenPoint(transform.position);
    }
}
