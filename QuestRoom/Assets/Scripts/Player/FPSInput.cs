﻿using UnityEngine;
using System.Collections;
[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{
    //Определяем скорость движения персонажа
    public float speed = 3.0f;
    //Сила гравитации, которая действует на персонажа
    public float gravity = -9.8f;
    //Создаём переменную, через которую можно получить доступ к Character Controller
    private CharacterController _charController;
    //Вводим выключатель (умножаем на него скорость всех перемещений)
    public int movingAbility = 1;
    void Start()
    {
        //Определяем переменную, через которую можно получить доступ к Character Controller
        _charController = GetComponent<CharacterController>();
        //mode = GetComponent<Modes>();
        //Отключаем курсор, чтоб не мелькал перед глазами
        Cursor.visible = false;
    }

    void Update()
    {
        //Определяем движение параллельно осям
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal") * speed * movingAbility, 0, Input.GetAxis("Vertical") * speed * movingAbility);
        //Ограничим движение по диагонали той же скоростью, что и движение параллельно осям.
        movement = Vector3.ClampMagnitude(movement, speed * movingAbility);
        //Включаем гравитацию, чтобы персонаж не летал
        movement.y = gravity;
        //Сглаживаем движение
        movement *= Time.deltaTime;
        //Преобразуем вектор движения от локальных к глобальным координатам.
        movement = transform.TransformDirection(movement);
        //Заставим этот вектор перемещать компонент CharacterController
        _charController.Move(movement * movingAbility);
    }    

    public void SetPosition(Vector3 newPos)
    {
        _charController.enabled = false;
        this.gameObject.transform.position = newPos;
        _charController.enabled = true;
    }
}