using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStorage : MonoBehaviour
{
    [Header("Backgrounds")]
    public Sprite nightBackground;
    public Sprite heavenBackground;
    public Sprite hellBackground;
    public Sprite defaultBackground;

    [Header("Tenshis")]
    public Sprite cursedTenshi;
    public Sprite defaultTenshi;

    private void Awake()
    {
        Global.objectStorage = this;
    }
}
