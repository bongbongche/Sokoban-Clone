using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public string setDirection = "none";
    public Vector3 currPosition;
    public bool canMove;
    public GameManager gameManager;

    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        currPosition = transform.position;

        if (Input.GetKeyDown(KeyCode.A))
        {
            // ���ڰ� ������ �������� �÷��̾ �����̸� ���� ������
            if (setDirection == "left")
            {
                MoveBox("left");
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (setDirection == "down")
            {
                MoveBox("down");
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (setDirection == "up")
            {
                MoveBox("up");
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (setDirection == "right")
            {
                MoveBox("right");
            }
        }

    }

    public void MoveBox(string direction)
    {
        if(canMove == true)
        {
            gameManager.UpdatePushes();
            if (direction == "up")
            {
                transform.position += Vector3.forward;
            }
            else if (direction == "down")
            {
                transform.position += Vector3.back;
            }
            else if (direction == "left")
            {
                transform.position += Vector3.left;
            }
            else if (direction == "right")
            {
                transform.position += Vector3.right;
            }
        }
    }

    public Vector3 PreMovePos(string direction)
    {
        Vector3 preMoveBoxPos = new Vector3(0, 0, 0);
        if (direction == "up")
        {
            preMoveBoxPos = transform.position + Vector3.forward;
            return preMoveBoxPos;
        }
        else if (direction == "down")
        {
            preMoveBoxPos = transform.position + Vector3.back;
            return preMoveBoxPos;
        }
        else if (direction == "left")
        {
            preMoveBoxPos = transform.position + Vector3.left;
            return preMoveBoxPos;
        }
        else if (direction == "right")
        {
            preMoveBoxPos = transform.position + Vector3.right;
            return preMoveBoxPos;
        }

        return preMoveBoxPos;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Ground")
        {
            if(PreMovePos(setDirection) == collision.gameObject.transform.position)
            {
                canMove = false;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // �÷��̾ ���ڿ��� ���� ���� �� ���õǾ��ִ� ������ �ʱ�ȭ
        if (collision.gameObject.tag == "Player")
        {
            setDirection = "none";
            canMove = true;
            playerController.canUp = playerController.canDown = playerController.canLeft = playerController.canRight = true;
        }
    }
}
