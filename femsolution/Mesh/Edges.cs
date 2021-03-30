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
        public Point Point1 { get; set; }

        /// <summary>
        /// Точка вторая
        /// </summary>
        public Point Point2 { get; set; }
    }
}
