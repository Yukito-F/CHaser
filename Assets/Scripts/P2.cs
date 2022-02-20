using System.Collections.Generic;
using UnityEngine;

public class P2 : PlayerController
{
    public void Action1()
    {
        around = GetReady();
    }

    int state = 0; // 0 search; 1 move; 2 destroy
    int lookdir = -1;
    int[] look = new int[9];

    public void Action2()
    {
        for (int i = 0; i < 4; i++)
        {
            if (around[2 * i + 1] == 1)
            {
                Put(SetDir(i));
                return;
            }
        }

        if (state == 0)
        {
            for (int i = 0; i < 4; i++)
            {
                if (look[i] == 1)
                {
                    state = 1;
                    look = new int[9];
                }
            }
        }

        if (state == 1)
        {
            if (around[2 * lookdir + 1] != 2)
            {
                Walk(SetDir(lookdir));
                return;
            }
            else
            {
                state = 0;
            }
        }

        if (state == 0)
        {
            lookdir++;
            if (lookdir > 3)
            {
                lookdir = 0;
                List<int> dirList = new List<int>();
                for (int i = 0; i < 4; i++) dirList.Add(i);
                for (int i = 0; i < 4; i++)
                {
                    int rand = Random.Range(0, dirList.Count);
                    if (around[2 * dirList[rand] + 1] != 2 && around[2 * dirList[rand] + 1] != 3)
                    {
                        Walk(SetDir(dirList[rand]));
                        return;
                    }
                    dirList.RemoveAt(rand);
                }

                int dir = Random.Range(0, 4);
                while (around[2 * dir + 1] != 0)
                {
                    dir = Random.Range(0, 4);
                }
                Walk(SetDir(dir));
                return;
            }
            look = Look(SetDir(lookdir));
            return;
        }

        Look(SetDir(0));
        return;
    }
}