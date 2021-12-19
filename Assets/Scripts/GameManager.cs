using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //GameObject[,] walls = new GameObject[17, 15];// 多分不要
    const int WIDTH = 15;
    const int HEIGHT = 17;
    int[,] map = new int[HEIGHT, WIDTH]; //0:empty, 1:enemy, 2:block, 3:item

    GameObject wall;

    PlayerController[] players = new PlayerController[2];
    int playerNum = 0;
    string playerName;

    private void Start()
    {
        mapInit();
        wall = (GameObject)Resources.Load("Wall");
        GameObject player = (GameObject)Resources.Load("Player_1");
        GameObject item = (GameObject)Resources.Load("Item");

        for (int i = 0; i < HEIGHT; i++)
        {
            for (int j = 0; j < WIDTH; j++)
            {
                switch (map[i, j])
                {
                    case 1:
                        GameObject temp = Instantiate(
                            player,
                            new Vector3(j, 0, HEIGHT - 1 - i),
                            Quaternion.identity
                            );
                        temp.name = "Player_" + playerNum;
                        players[playerNum] = temp.GetComponent<PlayerController>();
                        players[playerNum++].Init(j, i);
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
        map[y, x] = 0;
        
        if (CheckArea(x + dir[0], y + dir[1]) || map[y + dir[1], x + dir[0]] == 2)
        {
            // 負け
            FinishGame(playerName + "is lose...");
        }
        else
        {
            if (map[y + dir[1], x + dir[0]] == 3)
            {
                map[y, x] = 2;
                Instantiate(wall, new Vector3(x, 0, HEIGHT - 1 - y), Quaternion.identity);
                if( Judge(x + dir[0], y + dir[1]) )
                {
                    FinishGame(playerName + "is lose...");
                }
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
                //勝ち
                FinishGame(playerName + "is win!!!");
            }

            map[y + dir[0], x + dir[1]] = 2;
            Instantiate(wall, new Vector3(x + dir[1], 0, HEIGHT - 1 - (y + dir[0])), Quaternion.identity);
            // 自滅判定
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

    private bool CheckArea(int x, int y) // 場外判定
    {
        return x < 0 || x > WIDTH - 1 || y < 0 || y > HEIGHT - 1;
    }

    private bool Judge(int x, int y) // 4方の壁判定
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

        // シーン切り替え
        SceneManager.LoadScene("Result");
    }
    private void GameSceneLoaded(Scene next, LoadSceneMode mode)
    {
        // シーン切り替え後のスクリプトを取得
        //var gameManager = GameObject.Find("").GetComponent<>();

        // データを渡す処理
        //gameManager.setMap(map);

        // イベントから削除
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
        for (int j = 0; j < 5; j++)
        {
            for (int i = 0; i < 2; i++)
            {
                playerName = players[i].name;

                players[i].Action1();
                yield return new WaitForSeconds(1);

                players[i].Action2();
                yield return new WaitForSeconds(1);

                //players[i].Action1();
                //yield return new WaitForSeconds(1);
            }
        }
    }
}