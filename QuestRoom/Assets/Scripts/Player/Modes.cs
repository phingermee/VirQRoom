﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Modes : MonoBehaviour
{
    bool isClickModeActive = false;
    bool isPauseActive = false;
    bool isGasActive = false;
    public bool isSeifOpen = false;
    public bool iscodeLockActive = false;
    public GameObject _player;
    public GameObject _camera;
    public GameObject ventilationGrid;
    public GameObject menuUI;

    //Режим сбора предметов (игрок "заморожен", курсор активен)
    public void ClickMode()
    {
        FPSInput moving = _player.GetComponent<FPSInput>();
        MouseLook _rotation = _camera.GetComponent<MouseLook>();

        if (isClickModeActive)
        {
            isClickModeActive = !isClickModeActive;
            moving.movingAbility = 1;
            _rotation.rotationAbility = 1;
            Cursor.visible = false;
        }
        else
        {
            isClickModeActive = !isClickModeActive;
            moving.movingAbility = 0;
            _rotation.rotationAbility = 0;
            Cursor.visible = true;
        }
    }

    public void PauseMenuMode()
    {
        if (isPauseActive)
        {
            isPauseActive = false;
            menuUI.SetActive(false);
            Time.timeScale = 1f;
            ClickMode();
        }
        else
        {
            isPauseActive = true;
            menuUI.SetActive(true);
            Time.timeScale = 0f;
            ClickMode();
        }
    }

    //Режим монтировки (монтировка в руках, можно разрушить окно)
    public void CrowbarMode()
    {
        GameObject crowbar = GameObject.FindGameObjectWithTag("Damage");
        GameObject weaponPoint = GameObject.FindGameObjectWithTag("WeaponPoint");
        crowbar.transform.SetParent(_player.GetComponent<Transform>());
        crowbar.transform.position = weaponPoint.transform.position;
        crowbar.transform.rotation = weaponPoint.transform.rotation;
    }

    //Режим громкой улицы (когда окно разбито)
    public void LoudStreetMode()
    {
        AudioSource audioSrc = GetComponent<AudioSource>();
        float musicVolume = 0.5f;
        audioSrc.volume = musicVolume;
        GasMode();
    }

    //Режим работы с кодовы замком
    public void SeifLockMode()
    {
        if (!iscodeLockActive && !isSeifOpen)
        {
            GameObject codeLock = Instantiate(Resources.Load("Prefabs/CodeLock", typeof(GameObject)), _player.GetComponent<Transform>()) as GameObject;
            iscodeLockActive = true;
        }
    }

    //Режим отравляющего газа
    public void GasMode()
    {
        StopGas gasScript = ventilationGrid.GetComponent<StopGas>();
        if (!isGasActive)
        {
            gasScript.shoulWeStopIt = false;
            gasScript.GasIsOn();
            isGasActive = true;
        }
        else
        {
            gasScript.shoulWeStopIt = true;
            isGasActive = false;
        }
    }
}
