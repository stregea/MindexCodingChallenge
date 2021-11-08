using System;
using challenge.Models;

namespace challenge.Services
{
    public interface ICompensationService
    {
        /// <summary>
        /// Create an employee compensation.
        /// </summary>
        /// <param name="compensation">The compensation to create.</param>
        /// <returns>A Compensation object containing all pertinent compensation information for an employee.</returns>
        Compensation CreateCompensation(Compensation compensation);
        
        /// <summary>
        /// Get an employee compensation based on their employee id.
        /// </summary>
        /// <param name="id">The id associated with an employee.</param>
        /// <returns>A Compensation object containing all pertinent compensation information for an employee.</returns>
        Compensation ReadCompensation(String id);
    }
}