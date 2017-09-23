using MasterData;

public static class ObserverHelper{
	public static void saveToFile<E>(E progress, string fileName, int id,int worldId) {
        ES2.Delete(MasterDataManagerBase.getLoadPass(id,worldId, fileName));
        ES2Writer writer = ES2Writer.Create(MasterDataManagerBase.getLoadPassExceptTag(fileName));
        writer.Write<E>(progress,worldId + "" + id);
		writer.Save();
	}
}
