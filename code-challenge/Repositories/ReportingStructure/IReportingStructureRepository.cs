using challenge.Models;

namespace challenge.Repositories.ReportingStructure
{
    public interface IReportingStructureRepository
    {
        /// <summary>
        /// Create a Node containing an employee to be inserted within a tree.
        /// </summary>
        /// <param name="employee">The employee to insert within a Node.</param>
        /// <returns>A node containing an employee and it's children Nodes (if any).</returns>
        Node<Employee> CreateNode(Employee employee);

        /// <summary>
        /// Build an n-ary tree of employee's based on their structure of direct reports. 
        /// </summary>
        /// <param name="rootValue">The employee to serve as the root of the tree.</param>
        /// <returns>A tree containing the specified employee as the root of the tree, and the rest of the employees that are within the
        ///          structure of the direct reports as children nodes.
        /// </returns>
        Tree<Employee> BuildTree(Employee rootValue);
        
        /// <summary>
        /// Perform a breadth-first search to calculate the total number of reports based on
        /// a tree of employees.
        /// </summary>
        /// <param name="treeRoot">The root of the n-ary tree to perform BFS on.</param>
        /// <returns>The total count for the number of reports that are associated with an employee.</returns>
        int CalculateNumberOfReports(Tree<Employee> treeRoot);
    }
}