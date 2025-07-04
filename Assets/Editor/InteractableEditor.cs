using UnityEditor;

[CustomEditor(typeof(Interactable), true)]
public class InteractableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Interactable interactable = (Interactable)target;

        if(target.GetType() == typeof(EventOnlyInteractable))
        {
            interactable.promptMsg = EditorGUILayout.TextField("Prompt Message", interactable.promptMsg);
            EditorGUILayout.HelpBox("This can only use UnityEvents!", MessageType.Info);

            if(interactable.GetComponent<InteractionEvent>() == null)
            {
                interactable.useEvents = true;
                interactable.gameObject.AddComponent<InteractionEvent>();
            }
        }

        else
        {
            base.OnInspectorGUI();

            if (interactable.useEvents)
            {
                // Using events, add component
                if (interactable.GetComponent<InteractionEvent>() == null)
                    interactable.gameObject.AddComponent<InteractionEvent>();
            }

            else
            {
                if (interactable.GetComponent<InteractionEvent>() != null)
                    DestroyImmediate(interactable.GetComponent<InteractionEvent>());
            }
        }
    }
}
