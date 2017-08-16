using MasterData;

public static class ObserverHelper{
	public static void saveToFile<E>(E progress, string fileName, int id) {
        ES2.Delete(MasterDataManagerBase.getLoadPass(id, fileName));
        ES2Writer writer = ES2Writer.Create(MasterDataManagerBase.getLoadPassExceptTag(fileName));
        writer.Write<E>(progress,"" + id);
		writer.Save();
	}
}
