
using System.Collections.Generic;

public class WarshipCreator {

    List<Warship> warshipList = new List<Warship>();

    public WarshipCreator() {
        GenerateWarshipList();
    }

    private void GenerateWarshipList()
    {
		warshipList.AddRange(GenerateShips(Variables.numberOfShipsOfSizeFour, WarshipSize.FOUR));
		warshipList.AddRange(GenerateShips(Variables.numberOfShipsOfSizeThree, WarshipSize.THREE));
		warshipList.AddRange(GenerateShips(Variables.numberOfShipsOfSizeTwo, WarshipSize.TWO));
		warshipList.AddRange(GenerateShips(Variables.numberOfShipsOfSizeOne, WarshipSize.ONE));
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
