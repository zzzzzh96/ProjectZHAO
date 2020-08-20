using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public string str;

    public AudioClip OnClickSound;
    private AudioSource source;

    public Vector3 initSize;
    public Vector3 maxSize;

    public bool isChosen = false;

    public AnimationCurve curveBig;
    public AnimationCurve curveSmall;

    public GameObject _gameManager;

    [Range(0.1f,1.0f)]
    public float durationTime;
    float x;


    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        initSize = this.transform.localScale;
        maxSize = initSize * 1.5f;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        source.PlayOneShot(OnClickSound, 1F);

        if (!isChosen) {
            OnChose();
        }else{
            OffChose();
        }
    }
    public void OnChose() {
        StartCoroutine(biggerAnimation());
        _gameManager.GetComponent<GameManager>().addString(str);
        isChosen = true;
    }

    public void OffChose() {
        StartCoroutine(smallerAnimation());
        _gameManager.GetComponent<GameManager>().delString(str);
        isChosen = false;
    }

    IEnumerator biggerAnimation()
    {
        x = 0;
        while (true)
        {
            x += Time.deltaTime / durationTime;
            this.transform.localScale = Vector3.Lerp(this.transform.localScale, maxSize, curveBig.Evaluate(x));
            yield return null;
            if (this.transform.localScale.x >= maxSize.x) {
                break;
            }
           
        }
    }

    IEnumerator smallerAnimation()
    {
        x = 0;
        while (true)
        {
            x += Time.deltaTime  / durationTime;
            this.transform.localScale = Vector3.Lerp(this.transform.localScale, initSize, curveBig.Evaluate(x));
            yield return null;
            if (this.transform.localScale.x <= initSize.x)
            {
                break;
            }

        }

    }

}
