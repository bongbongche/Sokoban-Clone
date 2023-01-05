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
        // 캐릭터 상하좌우 이동
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
            // 상자와 비교할 때 플레이어의 상대적인 방향을 구함
            //Vector3 charContactPos = gameObject.transform.position - collision.transform.position;
            Vector3 charContactPos = gameObject.transform.position - collision.gameObject.GetComponent<Box>().currPosition;

            // 플레이어가 상자를 밀기 전에, 상자가 밀려야하는 방향을 세팅
            if (Mathf.Round(charContactPos.x) == 1)
            {
                // 특정 상자만 밀려야하므로 충돌된 상자의 Box 스크립트를 가져옴
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
        // 플레이어가 상자에서 손을 뗐을 때 세팅되어있던 방향을 초기화
        if (collision.gameObject.tag == "Wall")
        {
            canUp = canDown = canLeft = canRight = true;
        }
    }

}
