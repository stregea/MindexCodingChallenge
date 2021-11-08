using System;
using System.Threading.Tasks;
using challenge.Models;

namespace challenge.Repositories
{
    public interface ICompensationRepository
    {
        /// <summary>
        /// Create an employee compensation.
        /// </summary>
        /// <param name="compensation">The componsation to create.</param>
        /// <returns>A Compensation object containing all pertinent compensation information for an employee.</returns>
        Compensation CreateCompensation(Compensation compensation);

        /// <summary>
        /// Get an employee compensation based on their employee id.
        /// </summary>
        /// <param name="id">The id associated with an employee.</param>
        /// <returns>A Compensation object containing all pertinent compensation information for an employee.</returns>
        Compensation ReadCompensation(String id);
        
        /// <summary>
        /// Save the compensations within the persistence layer.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous save operation.
        /// The task result contains the number of state entries written to the database.
        /// </returns>
        Task SaveAsync();
    }
}