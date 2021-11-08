namespace challenge.Models
{
    /// <summary>
    /// A generic Tree structure.
    /// </summary>
    /// <typeparam name="T">The Tree's type.</typeparam>
    public class Tree<T>
    {
        /// <summary>
        /// The root node for the tree.
        /// </summary>
        public Node<T> Root { get; set; }
    }
}