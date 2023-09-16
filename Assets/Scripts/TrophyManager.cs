using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class TrophyManager : MonoBehaviour
{
    public List<Trophy> Trophys;
    public Transform basePos;

    // Start is called before the first frame update
    void Start()
    {
        float ypos = 0;
        foreach (Transform t in transform) 
        {
            if (t.TryGetComponent(out Trophy tr))
            {
                t.position = new UnityEngine.Vector3(basePos.position.x, basePos.position.y - ypos, basePos.position.z);
                Trophys.Add(tr);
                ypos += 140f;
            }
        }

        Trophys[0].SetGoal(BigInteger.Parse("100000"));
        Trophys[1].SetGoal(BigInteger.Parse("100000000"));
        Trophys[2].SetGoal(BigInteger.Parse("100000000000"));
        Trophys[3].SetGoal(BigInteger.Parse("100000000000000"));
        Trophys[4].SetGoal(BigInteger.Parse("100000000000000000"));
        Trophys[5].SetGoal(BigInteger.Parse("100000000000000000000"));
    }
}
