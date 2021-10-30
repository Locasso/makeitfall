using UnityEngine;

public class FrustumCulling : MonoBehaviour
{
#region
    [SerializeField] private Camera DaCamera;
    [SerializeField] private string ControlTag;

    public  enum  SearchMethod { Normal, Static }

    public int           TotalObjects;
    public int           VisibleObjects;

    private Vector3      LP; // Last Position
    private Quaternion   LR; // Last Rotation
    private Vector3      SP; // Screen Point
    private bool         OS; // On Screen 

    private GameObject[] SFCO;
#endregion

    private void Start()
    {
        ControlTag = string.IsNullOrEmpty(ControlTag) ? "Frustum" : ControlTag;
        DaCamera   = DaCamera == null  ? Camera.main : DaCamera;

        Populate(ControlTag);
    }

    private void Check( SearchMethod Mode = SearchMethod.Static )
    {
        if (LP != DaCamera.transform.position || LR != DaCamera.transform.rotation)
        {
            LP = DaCamera.transform.position; LR = DaCamera.transform.rotation;

            if (Mode == SearchMethod.Normal)
                Populate(ControlTag);

            VisibleObjects = 0;
            foreach (GameObject GO in SFCO)
            {
                Plane[] planes = GeometryUtility.CalculateFrustumPlanes(DaCamera);
				if (GO.GetComponent<Renderer>().IsNull())
				{
					
				}
				else
				{
					OS = GeometryUtility.TestPlanesAABB(planes, GO.GetComponent<Renderer>().bounds);

					VisibleObjects += OS ? 1 : 0;

					if (GO.GetComponent<Renderer>().enabled != OS)
						GO.GetComponent<Renderer>().enabled = OS;
				}
            }
        }
    }

    private void Populate(string Tag = "Frustum")
    {
        Debug.Log("=================");
        SFCO = GameObject.FindGameObjectsWithTag(Tag);
        TotalObjects = SFCO.Length;
    }

    private void LateUpdate()
    { Check(); }
}