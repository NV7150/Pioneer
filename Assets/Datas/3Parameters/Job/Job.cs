using System.Collections;
using System.Collections.Generic;
namespace parameter{
	public interface Job{
		string getName ();

		Dictionary<Ability,int> defaultSetting();
	}

	public enum Ability{
		MFT,
		FFT,
		MGP,
		AGI,
		PHY,
		SPC,
		DEX
	}
}
