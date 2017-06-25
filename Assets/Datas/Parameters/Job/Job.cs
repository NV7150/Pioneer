using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Parameter{
	[System.SerializableAttribute]
	public class Job{
		[SerializeField]
		private readonly int
			ID,
			MFT,
			FFT,
			MGP,
			PHY,
			DEX,
			AGI,
			SPC;

		private readonly string
			name;

		public Job(string[] parameters){
			ID = int.Parse (parameters[0]);
			name = parameters [1];
			MFT = int.Parse (parameters[2]);
			FFT = int.Parse (parameters[3]);
			MGP = int.Parse (parameters[4]);
			PHY = int.Parse (parameters[5]);
			AGI = int.Parse (parameters[6]);
			DEX = int.Parse (parameters[7]);
			SPC = int.Parse (parameters[8]);

			string log = "";
			foreach (var p in parameters) {
				log += p + ", ";
			}
			Debug.Log (log);
		}

		public string getName (){
			return name;
		}

		public Dictionary<Ability,int> defaultSetting(){
			Dictionary<Ability,int> parameters = new Dictionary<Ability, int> ();
			parameters [Ability.MFT] = MFT;
			parameters [Ability.FFT] = FFT;
			parameters [Ability.AGI] = AGI;
			parameters [Ability.MGP] = MGP;
			parameters [Ability.PHY] = PHY;
			parameters [Ability.DEX] = DEX;
			parameters [Ability.SPC] = SPC;
			parameters [Ability.HP] = PHY;
			parameters [Ability.MP] = MGP;
			parameters [Ability.LV] = 1;
			return parameters;
		}

		public int getId(){
			return ID;
		}

		public override string ToString () {
			return "Job " + name;
		}
	}
}
