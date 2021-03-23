using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool justTeleported = false;
    private bool playerIsIvin = false;

    #region Player;

    public bool PlayerInvin()
    {
        return playerIsIvin;
    }

    public void SetPlayerInvin(bool status)
    {
        playerIsIvin = status;
    }

    #endregion

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
