using UnityEngine;

namespace Interface
{
    public interface IDraggable
    {
        void BeginDrag(Ray ray);
        void Drag(Ray ray);
        void EndDrag();
    }
}