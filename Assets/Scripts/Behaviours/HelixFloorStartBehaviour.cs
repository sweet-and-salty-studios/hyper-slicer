namespace HyperSlicer.Behaviours
{
    public class HelixFloorStartBehaviour : HelixFloorBehaviour
    {
        protected override void Start()
        {
            RemovePieces(3);
        }
    }
}