using System.Collections.Generic;
using challenge.Models;
using Microsoft.AspNetCore.Server.Kestrel.Internal.System.Collections.Sequences;

namespace challenge.Repositories.ReportingStructure
{
    public class ReportingStructureRepository : IReportingStructureRepository
    {
        public Node<Employee> CreateNode(Employee employee)
        {
            Node<Employee> node = new Node<Employee>()
            {
                Value = employee,
                Children = new ArrayList<Node<Employee>>()
            };

            // Recursively add children nodes to the tree.
            if (employee.DirectReports != null)
            {
                // Create a Child node for every employee associated with the current employee's directReports.
                foreach (Employee e in employee.DirectReports)
                {
                    node.Children.Add(CreateNode(e));
                }
            }

            return node;
        }

        public Tree<Employee> BuildTree(Employee rootValue)
        {
            return new Tree<Employee>()
            {
                Root = CreateNode(rootValue)
            };
        }

        public int CalculateNumberOfReports(Tree<Employee> treeRoot)
        {
            int totalNumberOfReports = 0;

            // Queue to hold all of the nodes that we need to visit in order to calculate the 
            // total number of reports.
            Queue<Node<Employee>> nodesToVisit = new Queue<Node<Employee>>();

            // Set to determine which Nodes we have already visited.
            HashSet<Node<Employee>> visited = new HashSet<Node<Employee>>();

            nodesToVisit.Enqueue(treeRoot.Root);
            visited.Add(treeRoot.Root);

            while (nodesToVisit.Count > 0)
            {
                Node<Employee> employee = nodesToVisit.Dequeue();

                if (employee.Children != null && employee.Children.Length > 0)
                {
                    // Add the Children nodes to the queue.
                    foreach (Node<Employee> child in employee.Children)
                    {
                        // If we haven't visited the child Node.
                        if (!visited.Contains(child))
                        {
                            // Add the Node to the queue.
                            nodesToVisit.Enqueue(child);

                            // Mark the Node as "visited".
                            visited.Add(child);

                            // increment the count of total number of reports.
                            totalNumberOfReports++;
                        }
                    }
                }
            }

            return totalNumberOfReports;
        }
    }
}