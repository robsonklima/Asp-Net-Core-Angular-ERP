using System;
using System.Collections.Generic;

namespace SAT.MODELS.Entities {
    public class Office365Email
    {
        public string Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime LastModifiedDateTime { get; set; }
        public string ChangeKey { get; set; }
        public DateTime ReceivedDateTime { get; set; }
        public DateTime SentDateTime { get; set; }
        public bool HasAttachments { get; set; }
        public string InternetMessageIdyProperty { get; set; }
        public string Subject { get; set; }
        public string BodyPreview { get; set; }
        public string Importance { get; set; }
        public string ParentFolderId { get; set; }
        public string ConversationId { get; set; }
        public string ConversationIndex { get; set; }
        public string IsDeliveryReceiptRequested { get; set; }
        public bool IsReadReceiptRequested { get; set; }
        public bool IsRead { get; set; }
        public bool IsDraft { get; set; }
        public string WebLink { get; set; }
        public string InferenceClassification { get; set; }
        public Office365EmailBody Body { get; set; }
        public Office365EmailAddress Sender { get; set; }
        public Office365EmailAddress From { get; set; }
        public List<Office365EmailAddress> ToRecipients { get; set; }
        public List<Office365EmailAddress> CcRecipients { get; set; }
        public List<Office365EmailAddress> BccRecipients { get; set; }
        public List<Office365EmailAddress> ReplyTo { get; set; }
        public Office365Flag Flag { get; set; }
    }
}