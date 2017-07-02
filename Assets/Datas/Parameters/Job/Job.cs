using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Parameter{
	[System.SerializableAttribute]
	public class Job{
		[SerializeField]
		private readonly int
			//このジョブのIDです
			ID,
			//このジョブの基礎mftです
			MFT,
			//このジョブの基礎fftです
			FFT,
			//このジョブの基本mgpです
			MGP,
			//このジョブの基礎phyです
			PHY,
			//このジョブの基礎dexです
			DEX,
			//このジョブの基礎agiです
			AGI,
			//このジョブの基礎spcです
			SPC;

		private readonly string
			//このジョブのなまえです
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
		}

		//ジョブの名前を取得します
		public string getName (){
			return name;
		}

		//能力値テーブルを表すDictionaryを取得します
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

		//ジョブのIDを取得します
		public int getId(){
			return ID;
		}

		public override string ToString () {
			return "Job " + name;
		}
	}
}
