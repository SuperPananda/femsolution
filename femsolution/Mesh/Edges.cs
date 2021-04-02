namespace femsolution.Mesh
{
    /// <summary>
    /// Ребро
    /// </summary>
    public class Edges
    {
        /// <summary>
        /// Номер ребра
        /// </summary>
        public int EdgeTag { get; set; }

        /// <summary>
        /// Точка первая
        /// </summary>
        public Nodes Node1 { get; set; }

        /// <summary>
        /// Точка вторая
        /// </summary>
        public Nodes Node2 { get; set; }
    }
}
