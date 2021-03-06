﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Data", fileName = "New Data", order = 1)]
public class ShipData : ScriptableObject
{
    [SerializeField]
    private float m_Acceleration = 2.5f;
    [SerializeField]
    private float m_SlowDown = 2f;
    [SerializeField]
    private float m_BreakSpeed = 20f;
    [SerializeField]
    private float m_VelocityMax = 30f;
    //vitesse des déplacements latéraux
    [SerializeField]
    private float m_MovSpeed = 25f;
    //Vitesse de rotation(changer l'angle du vaisseau)
    [SerializeField]
    private float m_RotationSpeed = 2;
    [SerializeField]
    private float m_Hp = 3;
    [SerializeField]
    private float m_HitBonusTextDuration = 0.75f;

    [SerializeField]
    private Transform m_GoLeft;
    [SerializeField]
    private Transform m_GoRight;

    public Transform GoLeft
    {
        get { return m_GoLeft; }
    }

    public Transform GoRight
    {
        get { return m_GoRight; }
    }

    public float Acceleration
	{
		get{return m_Acceleration; }
	}

    public float SlowDown
	{
		get{return m_SlowDown;}
	}	
	public float BreakSpeed
	{
		get{return m_BreakSpeed;}
	}	
	public float VelocityMax
	{
		get{return m_VelocityMax;}
	}

	public float MovSpeed
	{
		get{return m_MovSpeed;}
	}	

	public float RotationSpeed
	{
		get{return m_RotationSpeed;}
	}	

	public float Hp
	{
		get{return m_Hp;}
	}

    public float HitBonusTextDuration
	{
		get{return m_HitBonusTextDuration;}
	}
}
