using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshHoverInfo : HasHoverInfo
{
    private string refreshText;

    public override string GetInfoText()
    {
        return refreshText;
    }

    public void SetRefreshText(string refreshText)
    {
        this.refreshText = refreshText;
    }

    private void Update()
    {
        if (IsShowing())
        {
            Refresh();
        }
    }
}
