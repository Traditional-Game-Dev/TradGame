using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool justTeleported = false;


    #region Teleporter;
    public bool GetTeleportStatus()
    {
        return justTeleported;
    }

    public void SetTeleportStatus(bool status)
    {
        justTeleported = status;

        StartCoroutine(TeleporterCooldown());
    }

    public void ResetTeleporters()
    {
        justTeleported = false;

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Teleporter"))
        {
            go.GetComponent<Teleporter>().ResetState();
        }
    }

    private IEnumerator TeleporterCooldown()
    {
        yield return new WaitForSeconds(5.0f);

        justTeleported = false;
    }
    #endregion
}
