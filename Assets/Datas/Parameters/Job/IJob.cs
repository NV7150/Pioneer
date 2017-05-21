using System.Collections;
using System.Collections.Generic;

namespace parameter{
	public interface IJob{
		string getName ();
		Dictionary<Ability,int> defaultSetting();
	}
}
