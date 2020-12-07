﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IColliderListener
{
    void Awake();

    void OnTriggerEnter(Collider other);

    void OnTriggerExit(Collider other);
}