using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //GameObject[,] walls = new GameObject[17, 15];// �����s�v
    const int WIDTH = 15;
    const int HEIGHT = 17;
    int[,] map = new int[HEIGHT, WIDTH]; //0:empty, 1:enemy, 2:block, 3:item

    GameObject wall;

    P1 player1;
    P2 player2;

    int playerNum = 1;
    string playerName;

    private void Start()
    {
        mapInit();
        wall = (GameObject)Resources.Load("Wall");
        GameObject item = (GameObject)Resources.Load("Item");

        for (int i = 0; i < HEIGHT; i++)
        {
            for (int j = 0; j < WIDTH; j++)
            {
                switch (map[i, j])
                {
                    case 1:
                        Debug.Log(playerNum);
                        GameObject temp = Instantiate(
                            (GameObject)Resources.Load("Player_" + playerNum),
                            new Vector3(j, 0, HEIGHT - 1 - i),
                            Quaternion.identity
                            );
                        temp.name = "Player_" + playerNum;

                        if (playerNum == 1){
                            
                            player1 = temp.GetComponent<P1>();
                            player1.Init(j, i);

                            playerNum++;
                        }
                        else{
                            player2 = temp.GetComponent<P2>();
                            player2.Init(j, i);
                        }
                        break;

                    case 2:
                        //walls[i, j] =
                        Instantiate(
                            wall,
                            new Vector3(j, 0, HEIGHT - 1 - i),
                            Quaternion.identity
                            );
                        break;
                    case 3:
                        Instantiate(
                            item,
                            new Vector3(j, 0, HEIGHT - 1 - i),
                            Quaternion.identity
                            );
                        break;
                }
            }
        }
    }

    void mapInit()
    {
        //for (int i = 0; i < HEIGHT; i++)
        //{
        //    for (int j = 0; j < WIDTH; j++)
        //    {
        //        if (i == 0 || i == HEIGHT - 1 || j == 0 || j == WIDTH - 1)
        //        {
        //            map[i, j] = 2;
        //        }
        //        else
        //        {
        //            map[i, j] = 0;
        //        }

        //        if (i == 8 && (j == 0 || j == 14))
        //        {
        //            map[i, j] = 1;
        //        }
        //    }
        //}

        map = new int[,]{
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,3,0,3,0,3,0,3,0,3,0,3,0,3,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,3,0,0,3,0,0,0,0,3,3,3,0,3,0},
            {0,2,0,3,3,3,0,2,0,0,3,0,0,2,0},
            {0,3,0,0,0,0,0,3,0,0,0,0,0,3,0},
            {0,0,0,3,0,0,3,3,0,2,0,0,0,0,0},
            {0,3,0,3,0,0,0,0,0,3,3,0,0,3,0},
            {0,0,1,0,0,3,0,3,0,3,0,0,1,0,0},
            {0,3,0,0,3,3,0,0,0,0,0,3,0,3,0},
            {0,0,0,0,0,2,0,3,3,0,0,3,0,0,0},
            {0,3,0,0,0,0,0,3,0,0,0,0,0,3,0},
            {0,2,0,0,3,0,0,2,0,3,3,3,0,2,0},
            {0,3,0,3,3,3,0,0,0,0,3,0,0,3,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,3,0,3,0,3,0,3,0,3,0,3,0,3,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
        };
    }

    public int[] GetReady(int x, int y)
    {
        if (Judge(x, y))
        {
            FinishGame(playerName + "is lose...");
        }

        int[] result = new int[9];

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (CheckArea(x - 1 + j, y - 1 + i))
                {
                    result[j + 3 * i] = 2;
                }
                else if (i == 1 && j == 1)
                {
                    result[j + 3 * i] = 0;
                }
                else
                {
                    result[j + 3 * i] = map[y - 1 + i, x - 1 + j];
                }
            }
        }

        return result;
    }

    public void Walk(int x, int y, int[] dir) //dir = up: , left: , right: , down;
    {       
        if (CheckArea(x + dir[0], y + dir[1]) || map[y + dir[1], x + dir[0]] == 2)
        {
            // ����
            FinishGame(playerName + "is lose...");
        }
        else
        {
            if (map[y + dir[1], x + dir[0]] == 3)
            {
                map[y, x] = 2;
                Instantiate(wall, new Vector3(x, 0, HEIGHT - 1 - y), Quaternion.identity);
                if (Judge(x + dir[0], y + dir[1]))
                {
                    FinishGame(playerName + "is lose...");
                }
            }
            else
            {
                map[y, x] = 0;
            }
            map[y + dir[1], x + dir[0]] = 1;
        }
    }

    public void Put(int x, int y, int[] dir)
    {
        if (!CheckArea(x + dir[1], y + dir[0]))
        {
            if (map[y + dir[0], x + dir[1]] == 1)
            {
                //����
                FinishGame(playerName + "is win!!!");
            }

            map[y + dir[0], x + dir[1]] = 2;
            Instantiate(wall, new Vector3(x + dir[1], 0, HEIGHT - 1 - (y + dir[0])), Quaternion.identity);
            // ���Ŕ���
            if (Judge(x, y))
            {
                FinishGame(playerName + "is lose...");
            }
        }
    }

    public int[] Look(int x, int y, int[] dir)
    {
        int[] result = new int[9];
        int targetX = x + 2 * dir[1], targetY = y + 2 * dir[0];

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (CheckArea(targetX - 1 + j, targetY - 1 + i))
                {
                    result[j + 3 * i] = 2;
                }
                else
                {
                    result[j + 3 * i] = map[targetY - 1 + i, targetX - 1 + j];
                }
            }
        }

        return result;
    }

    public int[] Search(int x, int y, int[] dir)
    {
        int[] result = new int[9];

        for (int i = 1; i <= 9; i++)
        {
            if (CheckArea(x + dir[1] * i, y + dir[0] * i))
            {
                result[i - 1] = 2;
            }
            else
            {
                result[i - 1] = map[y + dir[0] * i, x + dir[1] * i];
            }
        }

        return result;
    }

    private bool CheckArea(int x, int y) // ��O����
    {
        return x < 0 || x > WIDTH - 1 || y < 0 || y > HEIGHT - 1;
    }

    private bool Judge(int x, int y) // 4���̕ǔ���
    {
        // ans 1

        //bool result = true;
        //for (int i = 0; i < 4; i++)
        //{
        //    result = result
        //        && CheckArea(x + (int)Mathf.Cos(Mathf.PI / 4 * i), y + (int)Mathf.Sin(Mathf.PI / 4 * i))
        //        || map[y + (int)Mathf.Sin(Mathf.PI / 4 * i), x + (int)Mathf.Cos(Mathf.PI / 4 * i)] == 2;
        //}
        //return result;

        // ans 2

        //bool result = (CheckArea(x + 0, y + 1) || map[y + 1, x + 0] == 2)
        //    && (CheckArea(x - 1, y + 0) || map[y + 0, x - 1] == 2)
        //    && (CheckArea(x + 1, y + 0) || map[y + 0, x + 1] == 2)
        //    && (CheckArea(x + 0, y - 1) || map[y - 1, x + 0] == 2);

        // ans 3

        bool result = true;
        result = result && (CheckArea(x + 1, y + 0) || map[y + 0, x + 1] == 2);
        result = result && (CheckArea(x - 1, y + 0) || map[y + 0, x - 1] == 2);
        result = result && (CheckArea(x + 0, y + 1) || map[y + 1, x + 0] == 2);
        result = result && (CheckArea(x + 0, y - 1) || map[y - 1, x + 0] == 2);

        return result;
    }

    void FinishGame(string message)
    {
        Debug.Log(message);
        SceneManager.sceneLoaded += GameSceneLoaded;

        // �V�[���؂�ւ�
        SceneManager.LoadScene("Result");
    }
    private void GameSceneLoaded(Scene next, LoadSceneMode mode)
    {
        // �V�[���؂�ւ���̃X�N���v�g���擾
        //var gameManager = GameObject.Find("").GetComponent<>();

        // �f�[�^��n������
        //gameManager.setMap(map);

        // �C�x���g����폜
        SceneManager.sceneLoaded -= GameSceneLoaded;
    }

    bool flag = true;
    private void Update()
    {
        if (flag)
        {
            StartCoroutine("Game");
            flag = false;
        }
    }

    IEnumerator Game()
    {
        for (int j = 0; j < 100; j++)
        {
            playerName = player1.name;

            player1.Action1();
            yield return new WaitForSeconds(0.1f);

            player1.Action2();
            yield return new WaitForSeconds(0.1f);

            //players[i].Action1();
            //yield return new WaitForSeconds(1);

            playerName = player2.name;

            player2.Action1();
            yield return new WaitForSeconds(0.1f);

            player2.Action2();
            yield return new WaitForSeconds(0.1f);

            //players[i].Action1();
            //yield return new WaitForSeconds(1);

        }
    }
}