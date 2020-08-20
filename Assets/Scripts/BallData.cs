using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BallData
{
    public int[] isSeclected;
    public int[] isActive;
    //public bool isExist;
    public float[] position01;
    public float[] position02;
    public float[] position03;
    public float[] position04;

    public float[] scale01;
    public float[] scale02;
    public float[] scale03;
    public float[] scale04;

    public int point;

    public int leftPoint;

    public string[] currentString;

    public BallData(GameManager balls) {

        isSeclected = new int[4];
        for (int i = 0; i < 4; i++) {
            //Debug.Log("str is: " + balls.objs[i].GetComponent<BallManager>().str);
            if (balls.objs[i].GetComponent<BallManager>().isChosen)
            {
                isSeclected[i] = 1;
            }
            else
            {
                isSeclected[i] = 0;
            }
            //isSeclected[i] = balls.objs[i].GetComponent<BallManager>().isChosen;
        }


        isActive = new int[4];
        for (int i = 0; i < 4; i++) {
            if (balls.objs[i].activeSelf)
            {
                isActive[i] = 1;
            }
            else {
                isActive[i] = 0;
            }
        }

        //int index = 0;
        currentString = new string[balls.currentString.Count];
        for (int i = 0; i < balls.currentString.Count; i++) {
            currentString[i] = balls.currentString[i];
            //index++;
        }

        point = balls.point;
        leftPoint = balls.leftPoint;

        position01 = new float[3];
        position01[0] = balls.objs[0].transform.position.x;
        position01[1] = balls.objs[0].transform.position.y;
        position01[2] = balls.objs[0].transform.position.z;

        position02 = new float[3];
        position02[0] = balls.objs[1].transform.position.x;
        position02[1] = balls.objs[1].transform.position.y;
        position02[2] = balls.objs[1].transform.position.z;

        position03 = new float[3];
        position03[0] = balls.objs[2].transform.position.x;
        position03[1] = balls.objs[2].transform.position.y;
        position03[2] = balls.objs[2].transform.position.z;

        position04 = new float[3];
        position04[0] = balls.objs[3].transform.position.x;
        position04[1] = balls.objs[3].transform.position.y;
        position04[2] = balls.objs[3].transform.position.z;

        scale01 = new float[3];
        scale01[0] = balls.objs[0].transform.localScale.x;
        scale01[1] = balls.objs[0].transform.localScale.y;
        scale01[2] = balls.objs[0].transform.localScale.z;

        scale02 = new float[3];
        scale02[0] = balls.objs[1].transform.localScale.x;
        scale02[1] = balls.objs[1].transform.localScale.y;
        scale02[2] = balls.objs[1].transform.localScale.z;

        scale03 = new float[3];
        scale03[0] = balls.objs[2].transform.localScale.x;
        scale03[1] = balls.objs[2].transform.localScale.y;
        scale03[2] = balls.objs[2].transform.localScale.z;

        scale04 = new float[3];
        scale04[0] = balls.objs[3].transform.localScale.x;
        scale04[1] = balls.objs[3].transform.localScale.y;
        scale04[2] = balls.objs[3].transform.localScale.z;

    }

}
