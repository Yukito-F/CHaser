public class ScriptTsukada : PlayerController
{
    int direction = 0;
    int a = 1;
    int aa = 0;

    public void Action1()
    {
        around = GetReady();
    }

    public void Action2()
    {
        int U = around[1];
        int L = around[3];
        int R = around[5];
        int D = around[7];

        if (U == 1)
        {
            Put(SetDir(0));
            return;
        }
        else if (L == 1)
        {
            Put(SetDir(1));
            return;
        }
        else if (R == 1)
        {
            Put(SetDir(2));
            return;
        }
        else if (D == 1)
        {
            Put(SetDir(3));
            return;
        }
        else
        if (U == 3)
        {
            Walk(SetDir(0));
            return;
        }
        else if (L == 3)
        {
            Walk(SetDir(1));
            return;
        }
        else if (R == 3)
        {
            Walk(SetDir(2));
            return;
        }
        else if (D == 3)
        {
            Walk(SetDir(3));
            return;
        }
        else
        {
            bool succes = false;
            while (!succes)
            {
                switch (direction)
                {
                    case 0:
                        if (U == 0)
                            succes = true;
                        else
                            direction += a;
                        break;

                    case 1:
                        if (R == 0)
                            succes = true;
                        else
                            direction += a;
                        break;

                    case 2:
                        if (D == 0)
                            succes = true;
                        else
                            direction += a;
                        break;

                    case 3:
                        if (L == 0)
                            succes = true;
                        else
                            direction += a;
                        break;

                    case -1:
                        direction = 3;
                        break;

                    case 4:
                        direction = 0;
                        break;
                }
            }
            Walk(SetDir(direction));

            aa += 1;
            if (aa >= 16)
            {
                a *= -1;
                aa = 0;
            }
        }
    }
}