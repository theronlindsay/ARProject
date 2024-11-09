using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;

public class TrackedImageInfo : MonoBehaviour
{
    
    [SerializeField] ARTrackedImageManager m_TrackedImageManager;
    [SerializeField] InputActionReference m_ToggleAction;

    void Update(){
        if (m_ToggleAction.action.triggered)
        {
            Debug.Log("List all tracked images");
            ListAllTrackedImages();
        }
    }

    void OnEnable()
    {
        m_TrackedImageManager.trackedImagesChanged += OnChanged;

    }

    void OnDisable()
    {
        m_TrackedImageManager.trackedImagesChanged -= OnChanged;
    }

    void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            Debug.Log($"Tracked image detected: {trackedImage.referenceImage.name}");
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            //Debug.Log($"Tracked image updated: {trackedImage.referenceImage.name}");
        }

        foreach (var trackedImage in eventArgs.removed)
        {
            Debug.Log($"Tracked image removed: {trackedImage.referenceImage.name}");
        }
    }

    void ListAllTrackedImages()
    {
        Debug.Log("There are " + m_TrackedImageManager.trackables.count + " tracked images");   

        foreach (var trackedImage in m_TrackedImageManager.trackables)
        {
            Debug.Log($"Tracked image detected: {trackedImage.referenceImage.name}");
        }
    }
}
