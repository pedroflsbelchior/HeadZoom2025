using UnityEngine;
using UnityEngine.Events;

public class AnchorDataEvents : MonoBehaviour
{
    public UnityEvent<FilterData> OnFilterDataChanged;

    public void SetAnchorData(AnchorObject anchorData)
    {
        if (anchorData == null)
            return;
        OnFilterDataChanged.Invoke(anchorData.filterData);
    }
}
