using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {

    public static GameHandler gm;

    public int width = 20, height = 20;
    public Transform Snake;
    public Transform Apple;
    public Transform snakeBody;

    public static Transform currentSnake, currentApple;

    public List<Transform> bodyParts = new List<Transform>();
    private bool isBodyToAdd = false;

    void Start() {
        gm = this;
        Debug.Log("start");

        currentApple = Instantiate(Apple);
        randomizeApple();
        currentSnake = Instantiate(Snake);
    }

    public void randomizeApple() {
        currentApple.position = new Vector3(Random.Range(0, width), Random.Range(0, height));
    }

    private void Update() {
        if (isBodyToAdd) {
            isBodyToAdd = false;
            Transform pos = transform;
            if (bodyParts.Count > 0) {
                pos = ((Transform)bodyParts[bodyParts.Count - 1]);
            }
            bodyParts.Add(Instantiate(snakeBody));
        }
    }

    public void updateBodyParts() {
        //Debug.Log("updateBody");
        if(bodyParts.Count > 0) {
            for(int i = bodyParts.Count-1; i >= 0; i--) {
                if (i != 0) {
                    bodyParts[i].position = bodyParts[i - 1].position;
                } else {
                    bodyParts[0].position = currentSnake.position;
                }
            }
        }
    }

    public void addBodypart() {
        isBodyToAdd = true;
    }

}
