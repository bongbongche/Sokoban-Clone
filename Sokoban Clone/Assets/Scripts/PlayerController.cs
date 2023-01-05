using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool canLeft;
    public bool canRight;
    public bool canUp;
    public bool canDown;
    public GameManager gameManager;

    private Box specificBoxScript;
    private RaycastHit hit;
    private float maxDistance = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        canLeft = true;
        canRight = true;
        canUp = true;
        canDown = true;

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // ĳ���� �����¿� �̵�
        if(Input.GetKeyDown(KeyCode.A) && canLeft)
        {
            transform.Translate(Vector3.left);
            gameManager.UpdateMoves();
        }
        if(Input.GetKeyDown(KeyCode.S) && canDown)
        {
            transform.position += new Vector3(0, 0, -1);
            gameManager.UpdateMoves();
        }
        if (Input.GetKeyDown(KeyCode.W) && canUp)
        {
            transform.position += new Vector3(0, 0, 1);
            gameManager.UpdateMoves();
        }
        if (Input.GetKeyDown(KeyCode.D) && canRight)
        {
            transform.position += new Vector3(1, 0, 0);
            gameManager.UpdateMoves();
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Box")
        {
            // ���ڿ� ���� �� �÷��̾��� ������� ������ ����
            //Vector3 charContactPos = gameObject.transform.position - collision.transform.position;
            Vector3 charContactPos = gameObject.transform.position - collision.gameObject.GetComponent<Box>().currPosition;

            // �÷��̾ ���ڸ� �б� ����, ���ڰ� �з����ϴ� ������ ����
            if (Mathf.Round(charContactPos.x) == 1)
            {
                // Ư�� ���ڸ� �з����ϹǷ� �浹�� ������ Box ��ũ��Ʈ�� ������
                specificBoxScript = collision.gameObject.GetComponent<Box>();
                specificBoxScript.setDirection = "left";
                canLeft = specificBoxScript.canMove;
            }
            else if(Mathf.Round(charContactPos.x) == -1)
            {
                specificBoxScript = collision.gameObject.GetComponent<Box>();
                specificBoxScript.setDirection = "right";
                canRight = specificBoxScript.canMove;
            }
            else if(Mathf.Round(charContactPos.z) == 1)
            {
                specificBoxScript = collision.gameObject.GetComponent<Box>();
                specificBoxScript.setDirection = "down";
                canDown = specificBoxScript.canMove;
            }
            else if(Mathf.Round(charContactPos.z) == -1)
            {
                specificBoxScript = collision.gameObject.GetComponent<Box>();
                specificBoxScript.setDirection = "up";
                canUp = specificBoxScript.canMove;
            }
        }

        if (collision.gameObject.tag == "Wall")
        {
            if (Physics.Raycast(transform.position, Vector3.forward, out hit, maxDistance))
            {
                if(hit.transform.gameObject.tag == "Wall")
                {
                    canUp = false;
                }
            }
            if (Physics.Raycast(transform.position, Vector3.back, out hit, maxDistance))
            {
                if (hit.transform.gameObject.tag == "Wall")
                {
                    canDown = false;
                }
            }
            if (Physics.Raycast(transform.position, Vector3.left, out hit, maxDistance))
            {
                if (hit.transform.gameObject.tag == "Wall")
                {
                    canLeft = false;
                }
            }
            if (Physics.Raycast(transform.position, Vector3.right, out hit, maxDistance))
            {
                if (hit.transform.gameObject.tag == "Wall")
                {
                    canRight = false;
                }
            }
        }
        
    }

    private void OnCollisionExit(Collision collision)
    {
        // �÷��̾ ���ڿ��� ���� ���� �� ���õǾ��ִ� ������ �ʱ�ȭ
        if (collision.gameObject.tag == "Wall")
        {
            canUp = canDown = canLeft = canRight = true;
        }
    }

}
