
using System.Collections.Generic;

public class WarshipCreator {

    List<Warship> warshipList = new List<Warship>();

    public WarshipCreator() {
        GenerateWarshipList();
    }

    private void GenerateWarshipList()
    {
        warshipList.AddRange(GenerateShips(1, WarshipSize.FOUR));
        warshipList.AddRange(GenerateShips(2, WarshipSize.THREE));
        warshipList.AddRange(GenerateShips(3, WarshipSize.TWO));
        warshipList.AddRange(GenerateShips(4, WarshipSize.ONE));
    }


    public List<Warship> GetWarshipList() {
        return warshipList;
    }

    public List<Warship> GetWarships(WarshipSize bySize)
    {
        if (warshipList == null) return null;
        var statki = warshipList.FindAll(x => x.warshipSize == bySize);
        return statki;
    }
    private List<Warship> GenerateShips(int howmany, WarshipSize whatsize)
    {
        List<Warship> miniList = new List<Warship>();
        for (int i = 0; i < howmany; i++)
        {
            miniList.Add(new Warship(whatsize));
        }
        return miniList;
    }
	
}
