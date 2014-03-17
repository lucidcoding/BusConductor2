using System.Runtime.Serialization;

namespace BusConductor.Domain.Common
{
    /// <summary>
    /// Represents a validation error.
    /// </summary>
    /// <remarks>
    /// Author  :   Paul Davies
    /// Date    :   19th December 2010
    /// </remarks>
    [DataContract(IsReference = true)]
    public class ValidationMessage
    {
        protected ValidationMessageType _type;
        protected string _field;
        protected string _text;

        /// <summary>
        /// Gets/sets the error level.
        /// </summary>
        [DataMember]
        public ValidationMessageType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        /// <summary>
        /// Gets/sets the field that this validation message relates to.
        /// </summary>
        [DataMember]
        public string Field
        {
            get { return _field; }
            set { _field = value; }
        }

        /// <summary>
        /// Gets/sets the error message.
        /// </summary>
        [DataMember]
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
        
        /// <summary>
        /// Constructor that creates a validation error with the specified type and message.
        /// </summary>
        /// <param name="type">The type of the error.</param>
        /// <param name="text">The error message.</param>
        public ValidationMessage(ValidationMessageType type, string text)
        {
            _type = type;
            _text = text;
        }

        /// <summary>
        /// Constructor that creates a validation error with the specified type, field and message.
        /// </summary>
        /// <param name="type">The type of the error.</param>
        /// <param name="field">The field that this message relates to.</param>
        /// <param name="text">The error message.</param>
        public ValidationMessage(ValidationMessageType type, string field, string text)
        {
            _type = type;
            _field = field;
            _text = text;
        }
    }
}
