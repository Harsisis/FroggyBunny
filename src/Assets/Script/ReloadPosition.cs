using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class ReloadPosition : MonoBehaviour
{
    private List<GameObject> gameObjectsChildList = new List<GameObject>();
    private List<float> gameObjectsPositionsX = new List<float>();
    private List<float> gameObjectsPositionsY = new List<float>();
    private GameObject[] gameObjectsParentList;
    private DataTable NamesAndPositions = new DataTable();
    public DataTable getNamesAndPositions { get => NamesAndPositions;}

    private void Start()
    {
        gameObjectsParentList = GameObject.FindGameObjectsWithTag("ReloadPositionParent");
        NamesAndPositions.Columns.Add("Name", typeof(string));
        NamesAndPositions.Columns.Add("PositionX", typeof(float));
        NamesAndPositions.Columns.Add("PositionY", typeof(float));

        foreach (GameObject GameObjectChilds in gameObjectsParentList)
        {
            for (int i = 0; i < GameObjectChilds.transform.childCount; i++)
            {
                gameObjectsChildList.Add(GameObjectChilds.transform.GetChild(i).gameObject);
                gameObjectsPositionsX.Add(GameObjectChilds.transform.GetChild(i).transform.position.x);
                gameObjectsPositionsY.Add(GameObjectChilds.transform.GetChild(i).transform.position.y);
            }
        }

        for (var i = 0; i < gameObjectsChildList.Count; i++)
        {
            NamesAndPositions.Rows.Add((new object[] { gameObjectsChildList[i].name, gameObjectsPositionsX[i], gameObjectsPositionsY[i] }));
        }
    }
}
