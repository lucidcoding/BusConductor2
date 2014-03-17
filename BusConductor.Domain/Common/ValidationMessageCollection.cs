using System.Collections.Generic;
using System.Linq;

namespace BusConductor.Domain.Common
{
    /// <summary>
    /// Collection container for validation messages.
    /// </summary>
    /// <remarks>
    /// Author  :   Paul Davies
    /// Date    :   20th December 2010
    /// </remarks>
    public class ValidationMessageCollection : List<ValidationMessage>
    {
        /// <summary>
        /// Adds a ValidationMessage with type 'Error' and the specified text. 
        /// </summary>
        /// <param name="text">The text for the ValidationMessage.</param>
        public void AddError(string text)
        {
            Add(new ValidationMessage(ValidationMessageType.Error, text));
        }

        /// <summary>
        /// Adds a ValidationMessage with type 'Error' and the specified field and text. 
        /// </summary>
        /// <param name="field">The field the ValidationMessage relates to.</param>
        /// <param name="text">The text for the ValidationMessage.</param>
        public void AddError(string field, string text)
        {
            Add(new ValidationMessage(ValidationMessageType.Error, field, text));
        }

        /// <summary>
        /// Adds a ValidationMessage with type 'Warning' and the specified text. 
        /// </summary>
        /// <param name="text">The text for the ValidationMessage.</param>
        public void AddWarning(string text)
        {
            Add(new ValidationMessage(ValidationMessageType.Warning, text));
        }

        /// <summary>
        /// Gets a list of messages with the status 'Info'.
        /// </summary>
        public List<ValidationMessage> Infos
        {
            get
            {
                return (from validationMessage
                        in this
                        where validationMessage.Type == ValidationMessageType.Info
                        select validationMessage).ToList();
            }
        }

        /// <summary>
        /// Gets a list of messages with the status 'Warning'.
        /// </summary>
        public List<ValidationMessage> Errors
        {
            get
            {
                return (from validationMessage
                        in this
                        where validationMessage.Type == ValidationMessageType.Error
                        select validationMessage).ToList();
            }
        }

        /// <summary>
        /// Gets a list of messages with the status 'Error'.
        /// </summary>
        public List<ValidationMessage> Warnings
        {
            get
            {
                return (from validationMessage
                        in this
                        where validationMessage.Type == ValidationMessageType.Warning
                        select validationMessage).ToList();
            }
        }

        /// <summary>
        /// Checks whether the collection contains a ValidationMessage with type 'Error' and
        /// the specified field and text.
        /// </summary>
        /// <param name="field">The field the ValidationMessage relates to.</param>
        /// <param name="text">The text for the ValidationMessage.</param>
        /// <returns>True if a ValidationMessage is found in this collection.</returns>
        public bool ContainsError(string field, string text)
        {
            return this.Contains(new ValidationMessage(ValidationMessageType.Error, field, text));
        }
    }
}
