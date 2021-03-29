namespace femsolution.Mesh
{
    /// <summary>
    /// Конечные элементы
    /// </summary>
    public class Elements
    {
        /// <summary>
        /// Номер конечного элемента
        /// </summary>
        public int ElementTag { get; set; }

        /// <summary>
        /// Узлы входящие в конечный элемент
        /// </summary>
        public int[] Nodes { get; set; }
    }
}
