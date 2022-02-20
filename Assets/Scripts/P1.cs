using UnityEngine;

public class P1 : PlayerController
{
    public void Action1()
    {
        around = GetReady();
    }

    int state = 0; // 0 normal; 1 item; 2 neerIem
    int[] look;
    int lookdir;

    public void Action2()
    {
        if (state == 1)
        {
            state = 0;
            if (look[4] != 2)
            {
                Walk(SetDir(lookdir));
                return;
            }
        }

        if (state == 0)
        {
            for (int i = 0; i < 4; i++)
            {
                if (around[2 * i + 1] == 1)
                {
                    Put(SetDir(i));
                    return;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                if (around[2 * i + 1] == 3)
                {
                    if (
                        !(
                            (i == 0 || i == 3) && (around[2 * i] == 2 && around[2 * i + 2] == 2) ||
                            (i == 1 || i == 2) && (around[2 * i - 2] == 2 && around[2 * i + 4] == 2)
                         )
                      )
                    {
                        Walk(SetDir(i));
                        return;
                    }
                }
            }
            for (int i = lookdir; i < 4; i++)
            {
                if (around[2 * i + 1] == 3)
                {
                    if (
                        (i == 0 || i == 3) && (around[2 * i] == 2 && around[2 * i + 2] == 2) ||
                        (i == 1 || i == 2) && (around[2 * i - 2] == 2 && around[2 * i + 4] == 2)
                      )
                    {
                        look = Look(SetDir(i));
                        state = 1;
                        lookdir = i;
                        return;
                    }
                }
            }

            int dir = Random.Range(0, 4);
            while (around[2 * dir + 1] != 0)
            {
                dir = Random.Range(0, 4);
            }
            Walk(SetDir(dir));
            return;

        }
    }

    //int state = 0; // 0 normal; 1 item; 2 neerIem
    //int[] look;
    //int lookdir;

    //public void Action2()
    //{
    //    if (state == 1)
    //    {
    //        state = 0;
    //        if (look[4] != 2)
    //        {
    //            Walk(SetDir(lookdir));
    //            return;
    //        }
    //    }

    //    if (state == 0)
    //    {
    //        for (int i = 0; i < 4; i++)
    //        {
    //            if (around[2 * i + 1] == 1)
    //            {
    //                Put(SetDir(i));
    //                return;
    //            }
    //        }

    //        for (int i = 0; i < 4; i++)
    //        {
    //            if (around[2 * i + 1] == 3)
    //            {
    //                if (
    //                    !(
    //                        (i == 0 || i == 3) && (around[2 * i] == 2 && around[2 * i + 2] == 2) ||
    //                        (i == 1 || i == 2) && (around[2 * i - 2] == 2 && around[2 * i + 4] == 2)
    //                     )
    //                  )
    //                {
    //                    Walk(SetDir(i));
    //                    return;
    //                }
    //            }
    //        }
    //        for (int i = lookdir; i < 4; i++)
    //        {
    //            if (around[2 * i + 1] == 3)
    //            {
    //                if (
    //                    (i == 0 || i == 3) && (around[2 * i] == 2 && around[2 * i + 2] == 2) ||
    //                    (i == 1 || i == 2) && (around[2 * i - 2] == 2 && around[2 * i + 4] == 2)
    //                  )
    //                {
    //                    look = Look(SetDir(i));
    //                    state = 1;
    //                    lookdir = i;
    //                    return;
    //                }
    //            }
    //        }

    //        int dir = Random.Range(0, 4);
    //        while (around[2 * dir + 1] != 0)
    //        {
    //            dir = Random.Range(0, 4);
    //        }
    //        Walk(SetDir(dir));
    //        return;

    //    }
    //}

}
