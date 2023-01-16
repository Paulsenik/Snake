using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{


    public int startX = 10;
    public int startY = 10;

    private float countDown = 0f;
    public float maxWaitTime = 1f;

    private int speedX = 0, speedY = 1;
    private int lastXSpeed = 0, lastYSpeed = 1;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(startX, startY);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x == GameHandler.currentApple.position.x && transform.position.y == GameHandler.currentApple.position.y) {
            Debug.Log("eat");
            GameHandler.gm.randomizeApple();
            GameHandler.gm.addBodypart();
        }
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && lastYSpeed != -1) {
            speedX = 0;
            speedY = 1;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && lastYSpeed != 1) {
            speedX = 0;
            speedY = -1;
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && lastXSpeed != -1) {
            speedX = 1;
            speedY = 0;
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && lastXSpeed != 1) {
            speedX = -1;
            speedY = 0;
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }

        countDown -= Time.deltaTime;
        if(countDown <= 0) {
            countDown = maxWaitTime;
            move();
            lastXSpeed = speedX;
            lastYSpeed = speedY;

            // out of Bounds
            if (transform.position.x >= GameHandler.gm.width || transform.position.y >= GameHandler.gm.height || transform.position.x < 0 || transform.position.y < 0) {
                Debug.Log("Snake hit Wall");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            // Snake hits itself
            for (int i=0;i<GameHandler.gm.bodyParts.Count; i++) {
                if (GameHandler.gm.bodyParts[i].position.Equals(transform.position)) {
                    Debug.Log("Snake hit itself");
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
        }
    }


    private void move() {
        GameHandler.gm.updateBodyParts();
        transform.position += new Vector3(speedX,speedY);
    }
}
