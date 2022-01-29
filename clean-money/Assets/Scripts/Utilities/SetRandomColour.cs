using UnityEngine;

namespace cm.utilities
{   
    [RequireComponent(typeof(Renderer))]
    public class SetRandomColour : MonoBehaviour
    {
        private new Renderer renderer = null;

        [SerializeField]
        private Color[] colours = null;

        private void Awake() => renderer = this.GetComponent<MeshRenderer>();

        private void Start()
        {
            Color colour = colours[Random.Range(0, colours.Length)];
            foreach (Material mat in renderer.materials)
            {
                mat.SetColor("_BaseColor", colour);
                mat.SetColor("_Color", colour);
            }

            Destroy(this);
        }
    }
}
