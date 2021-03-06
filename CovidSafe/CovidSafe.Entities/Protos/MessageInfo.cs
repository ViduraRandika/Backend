﻿using CovidSafe.Entities.Validation;

namespace CovidSafe.Entities.Protos
{
    /// <summary>
    /// <see cref="MessageInfo"/> partial from generated Protobuf class
    /// </summary>
    public partial class MessageInfo : IValidatable
    {
        /// <inheritdoc/>
        public RequestValidationResult Validate()
        {
            RequestValidationResult result = new RequestValidationResult();

            // MessageId validation
            result.Combine(Validator.ValidateGuid(this.MessageId, nameof(this.MessageId)));

            // Timestamp validation
            result.Combine(Validator.ValidateTimestamp(this.MessageTimestamp, nameof(this.MessageTimestamp)));

            return result;
        }
    }
}
