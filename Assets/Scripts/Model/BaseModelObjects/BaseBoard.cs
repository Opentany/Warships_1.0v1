using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBoard<T> where T : BaseField, new(){

    public static int boardSize = 10;
    protected List<List<T>> board;
    protected int fieldsOccupiedByWarships;
    protected List<Warship> warshipList;


    public BaseBoard()
    {
        board = new List<List<T>>();
        warshipList = new List<Warship>();
        fieldsOccupiedByWarships = 0;
        GenerateBoardModel();
    }

    public virtual void GenerateBoardModel()
    {
        board = new List<List<T>>();
        for (int i = 0; i < boardSize; i++)
        {
            List<T> row = new List<T>();
            for (int j = 0; j < boardSize; j++)
            {
                T field = new T();
                field.SetCoordinates(i, j);
                row.Add(field);
            }
            board.Add(row);
        }
    }

    public List<List<T>> GetBoard()
    {
        return board;
    }

    public T GetField(int column, int row)
    {
        return board[column][row];
    }

    public void SetWarship(Warship warship)
    {
        if (warship.GetOrientation().Equals(WarshipOrientation.HORIZONTAL))
        {
            SetWarshipHorizontal(warship);
        }
        else
        {
            SetWarshipVertical(warship);
        }
        fieldsOccupiedByWarships += warship.GetSize();
        warshipList.Add(warship);
    }

    protected virtual void SetWarshipHorizontal(Warship warship)
    {
        int x = warship.GetXPosition();
        for (int i = x; i < x + warship.GetSize(); i++)
        {
            board[i][warship.GetYPosition()].SetWarship(warship);
        }
    }

    protected virtual void SetWarshipVertical(Warship warship)
    {
        int y = warship.GetYPosition();
        for (int i = y; i < y + warship.GetSize(); i++)
        {
            board[warship.GetXPosition()][i].SetWarship(warship);
        }
    }

    protected bool CheckIfFieldHasWarshipOnCurrentIndexs(int i, int j)
    {
        return board[i][j].warship != null;
    }

    public virtual void RemoveWarship(Warship warship)
    {
        warshipList.Remove(warship);
        if (warship.GetOrientation() == WarshipOrientation.HORIZONTAL)
        {
            RemoveWarshipHorizontal(warship);
        }
        else
        {
            RemoveWarshipVertical(warship);
        }
    }

    private void RemoveWarshipHorizontal(Warship warship)
    {
        int x = warship.GetXPosition();
        for (int i = x; i < x + warship.GetSize(); i++)
        {
            board[i][warship.GetYPosition()].SetWarship(null);
        }
        fieldsOccupiedByWarships -= warship.GetSize();
    }

    private void RemoveWarshipVertical(Warship warship)
    {
        int y = warship.GetYPosition();
        for (int i = y; i < y + warship.GetSize(); i++)
        {
            board[warship.GetXPosition()][i].SetWarship(null);
        }
        fieldsOccupiedByWarships -= warship.GetSize();
    }

    public int GetFieldsOccupiedByWarships()
    {
        return fieldsOccupiedByWarships;
    }

    public void SetFieldsOccupiedByWarships(int fields)
    {
        fieldsOccupiedByWarships = fields;
    }

    public WarshipsContainer GetWarshipList()
    {
        return new WarshipsContainer(warshipList);
    }

}
