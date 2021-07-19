using UnityEngine;

namespace HyperSlicer.Utilities.MeshSlicing
{
    public enum SideOfPlane
    {
        UP,
        DOWN,
        ON
    }

    public struct Plane
    {
        private Vector3 m_normal;
        private float m_dist;

#if UNITY_EDITOR
        private Transform trans_ref;
#endif

        public Plane(Vector3 pos, Vector3 norm)
        {
            this.m_normal = norm;
            this.m_dist = Vector3.Dot(norm, pos);

            // this is for editor debugging only!
#if UNITY_EDITOR
            trans_ref = null;
#endif
        }

        public Plane(Vector3 norm, float dot)
        {
            this.m_normal = norm;
            this.m_dist = dot;

            // this is for editor debugging only!
#if UNITY_EDITOR
            trans_ref = null;
#endif
        }

        public Plane(Vector3 a, Vector3 b, Vector3 c)
        {
            m_normal = Vector3.Normalize(Vector3.Cross(b - a, c - a));
            m_dist = -Vector3.Dot(m_normal, a);

            // this is for editor debugging only!
#if UNITY_EDITOR
            trans_ref = null;
#endif
        }

        public void Compute(Vector3 pos, Vector3 norm)
        {
            this.m_normal = norm;
            this.m_dist = Vector3.Dot(norm, pos);
        }

        public void Compute(Transform trans)
        {
            Compute(trans.position, trans.up);

            // this is for editor debugging only!
#if UNITY_EDITOR
            trans_ref = trans;
#endif
        }

        public void Compute(GameObject obj)
        {
            Compute(obj.transform);
        }

        public Vector3 Normal
        {
            get { return this.m_normal; }
        }

        public float Distance
        {
            get { return this.m_dist; }
        }

        public SideOfPlane SideOf(Vector3 pt)
        {
            float result = Vector3.Dot(m_normal, pt) - m_dist;

            if(result > Intersector.Epsilon)
            {
                return SideOfPlane.UP;
            }

            if(result < -Intersector.Epsilon)
            {
                return SideOfPlane.DOWN;
            }

            return SideOfPlane.ON;
        }

        public void OnDebugDraw()
        {
            OnDebugDraw(Color.white);
        }

        public void OnDebugDraw(Color drawColor)
        {
            // NOTE -> Gizmos are only supported in the editor. We will keep these function
            // signatures for consistancy however at final build, these will do nothing
            // TO/DO -> Should we throw a runtime exception if this function tried to get executed
            // at runtime?
#if UNITY_EDITOR

            if(trans_ref == null)
            {
                return;
            }

            Color prevColor = Gizmos.color;
            Matrix4x4 prevMatrix = Gizmos.matrix;

            // TO-DO
            Gizmos.matrix = Matrix4x4.TRS(trans_ref.position, trans_ref.rotation, trans_ref.localScale);
            Gizmos.color = drawColor;

            Gizmos.DrawWireCube(Vector3.zero, new Vector3(1.0f, 0.0f, 1.0f));

            Gizmos.color = prevColor;
            Gizmos.matrix = prevMatrix;

#endif
        }
    }
}
