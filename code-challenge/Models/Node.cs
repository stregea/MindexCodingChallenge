using Microsoft.AspNetCore.Server.Kestrel.Internal.System.Collections.Sequences;

namespace challenge.Models
{
    /// <summary>
    /// A Node for an n-ary tree, meaning the tree can have n-number of children nodes.
    /// </summary>
    /// <typeparam name="T">The Node's type.</typeparam>
    public class Node<T>
    {
        /// <summary>
        /// The value of this node.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// The list of children node's for this node.
        /// </summary>
        public ArrayList<Node<T>> Children { get; set; }
    }
}