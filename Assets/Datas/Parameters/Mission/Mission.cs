
namespace parameter{
	public interface Mission{
		//Mission名を返します
		string getName ();

		//渡されたFlugListを元にMissionが達成されているかを返します
		bool cheak(FlugList flugs);
	}
}
