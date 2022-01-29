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

            int i = 0;

            foreach (Material mat in renderer.materials)
            {
                if (i == 0)
                {
                    Vector4 v4Colour = colour;

                    v4Colour -= (0.2f * Vector4.one);

                    Color newColour = v4Colour;

                    mat.SetColor("_BaseColor", newColour);
                    mat.SetColor("_Color", newColour);
                }
                else
                {
                    mat.SetColor("_BaseColor", colour);
                    mat.SetColor("_Color", colour);
                }

                
                i++;
            }

            Destroy(this);
        }
    }
}
