using UnityEngine;

public class CrateGridBoxes : MonoBehaviour
{
    [SerializeField] private Box boxPrefab;
    [SerializeField] private int mapWidth = 16;
    [SerializeField] private int mapHeight = 16;
    [SerializeField] private float spacing = 1.1f;

    [SerializeField] private GridBoxesCollector gridBoxesCollector;

    public void GenerateBoxes()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int z = 0; z < mapHeight; z++)
            {
                Vector3 pos = new Vector3(x * spacing, z * spacing);
                var box = Instantiate(boxPrefab, pos, Quaternion.identity, this.transform);
                box.InitBox(x, z);
                gridBoxesCollector.AddGridBox(box);
            }
        }
    }
}

#if UNITY_EDITOR
[UnityEditor.CustomEditor(typeof(CrateGridBoxes))]
public class CrateGridBoxesEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        CrateGridBoxes script = (CrateGridBoxes)target;

        if (GUILayout.Button("Generate Boxes"))
        {
            script.GenerateBoxes();
        }

        DrawDefaultInspector();
    }
}
#endif