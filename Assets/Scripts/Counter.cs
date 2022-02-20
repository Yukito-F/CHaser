using UnityEngine;

public class Counter : MonoBehaviour
{
    int pt = 0;
    TextMesh tx;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        tx = GetComponent<TextMesh>();
    }

    public void Point()
    {
        pt++;
        tx.text = pt.ToString();
    }

    public int GetPt()
    {
        return pt;
    }
}
