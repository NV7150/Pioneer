﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Item{
	//武器の種別です
	public enum WeponType{
		SWORD,
		BOW,
		SPEAR,
		BLUNT,
		AX,
		GUN 
	}
		
	public class WeponTypeHelper{
		private WeponTypeHelper (){}
		//idから武器タイプを取得します
		public static WeponType getTypeFromId(int id){
			switch(id){
				case 0:
					return WeponType.SWORD;
				case 1:
					return WeponType.BOW;
				case 2:
					return WeponType.SPEAR;
				case 3:
					return WeponType.BLUNT;
				case 4:
					return WeponType.AX;
				case 5:
					return WeponType.GUN;
				default:
					throw new ArgumentException ("invalid weponTypeID");
			}
		}
	}
}
