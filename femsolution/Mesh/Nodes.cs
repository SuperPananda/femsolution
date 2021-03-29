namespace femsolution.Mesh
{
    /// <summary>
    /// Узлы
    /// </summary>
    public class Nodes
    {
        /// <summary>
        /// Номер узла
        /// </summary>
        public int NodeTag { get; set; }

        /// <summary>
        /// Координата x
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Координата y
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Координата z
        /// </summary>
        public double Z { get; set; }

        public int EntityDim { get; set; }

        public int EntityTag { get; set; }

        public int Parametric { get; set; }

        public int NumNodesInBlock { get; set; }
    }
}
