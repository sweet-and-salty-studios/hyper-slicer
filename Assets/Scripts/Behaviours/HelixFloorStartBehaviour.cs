namespace HyperSlicer.Behaviours
{
    public class HelixFloorStartBehaviour : HelixFloorBehaviour
    {
        protected override void Start()
        {
            RandomizePieces();
            RemovePieces(3);
        }
    }
}