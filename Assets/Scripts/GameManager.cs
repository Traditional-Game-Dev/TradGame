﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;

public class GameManager : MonoBehaviour
{
    #region GeneralImports

    public GameObject player;
    public VisualEffect electric;

    #endregion

    #region UI

    public GameObject gameplayUI;
    public GameObject planningUI;

    public void SwapToGameplay()
    {
        gameplayUI.SetActive(true);
        planningUI.SetActive(false);

        //electric.gameObject.SetActive(true);
        //electric.Stop();

        player.transform.position = new Vector3(0f, 1f, -50f);
        player.SetActive(true);


        var rock = GameObject.Find("Rock1Blueprint");
        if (rock != null)
        {
            rock.gameObject.SetActive(false);
        }
        var wall = GameObject.Find("WallBlueprint");
        if (wall != null)
        {
            wall.gameObject.SetActive(false);
        }
        var ruinsWall = GameObject.Find("RuinsWallBlueprint");
        if (ruinsWall != null)
        {
            ruinsWall.gameObject.SetActive(false);
        }
        var teleporter = GameObject.Find("TeleporterBlueprint");
        if (teleporter != null)
        {
            teleporter.gameObject.SetActive(false);
        }

        LinkTeleporters();

        GameObject.Find("Boss").GetComponent<BossController>().planningPhase = false;
    }

    public void SwapToPlanning()
    {
        gameplayUI.SetActive(false);
        planningUI.SetActive(true);
        planningUI.GetComponent<spawnObject>().mana = planningUI.GetComponent<spawnObject>().planningMana;

        player.SetActive(false);

        //electric.gameObject.SetActive(false);

        GameObject.Find("Boss").GetComponent<BossController>().planningPhase = true;
    }

    private void LinkTeleporters()
    {
        try
        {
            GameObject[] teleporters = GameObject.FindGameObjectsWithTag("Teleporter") ?? null;

            Transform teleporterOne = teleporters.First().transform;
            Transform teleporterTwo = teleporters.Last().transform;

            teleporterOne.GetComponent<Teleporter>().LinkToOtherTeleporter(teleporterTwo);
            teleporterTwo.GetComponent<Teleporter>().LinkToOtherTeleporter(teleporterOne);
        } 
        catch (InvalidOperationException)
        {
            return;
        }
    }

    #endregion

    #region SlowMotion

    public TimeManager timeManager;

    public void EnableSlowMotion(float length)
    {
        timeManager.DoSlowMotion(length);
    }

    #endregion

    #region Boss

    public GameObject boss;
    public bool justHitBoss { get; set; } = false;

    public void DamageBoss(float damage)
    {
        boss.GetComponent<BossHealth>().ReceiveDamage(damage);
        justHitBoss = false;
    }

    #endregion

    #region Player;

    public bool playerIvin { get; set; } = false;

    public bool playerDisabled { get; set; } = false;

    #endregion

    #region Teleporters;

    private bool justTeleported = false;
    private Color teleCooldownColor = new Color(0.0f, 63.0f, 127.0f, 1.0f);
    private Color teleBaseColor = new Color(255.0f, 255.0f, 255.0f, 1.0f);

    public bool JustTeleported()
    {
        return justTeleported;
    }

    public void Teleported()
    {
        justTeleported = true;

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
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Teleporter"))
        {
            go.GetComponent<Teleporter>().UpdateParticleColor(teleCooldownColor);
        }

        yield return new WaitForSeconds(7.0f);

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Teleporter"))
        {
            go.GetComponent<Teleporter>().UpdateParticleColor(teleBaseColor);
        }

        justTeleported = false;
    }
    #endregion
}
