using System.Collections.Generic;


public class WarshipsContainer{

    private List<Warship> warships;

    public WarshipsContainer(List<Warship> ships)
    {
        this.warships = ships;
    }

    public List<Warship> GetWarships()
    {
        return warships;
    }


}
