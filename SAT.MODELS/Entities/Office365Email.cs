using System;
using System.Collections.Generic;

namespace SAT.MODELS.Entities {
    public class EmailBody
    {
        public string ContentType { get; set; }
        public string Content { get; set; }
        public string InferenceClassification { get; set; }
        public EmailBody Body { get; set; }
        public Sender Sender { get; set; }
        public From From { get; set; }
        public List<EmailAddress> ToRecipients { get; set; }
        public List<object> CcRecipients { get; set; }
        public List<object> BccRecipients { get; set; }
        public List<ReplyTo> ReplyTo { get; set; }
        public Flag Flag { get; set; }
    }

    public class EmailAddress
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class Flag
    {
        public string FlagStatus { get; set; }
    }

    public class From
    {
        public EmailAddress EmailAddress { get; set; }
    }

    public class ReplyTo
    {
        public EmailAddress EmailAddress { get; set; }
    }

    public class Office365Email
    {
        public List<EmailValue> Value { get; set; }
    }

    public class Sender
    {
        public EmailAddress EmailAddress { get; set; }
    }

    public class ToRecipient
    {
        public EmailAddress EmailAddress { get; set; }
    }

    public class EmailValue
    {
        public string Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime LastModifiedDateTime { get; set; }
        public string ChangeKey { get; set; }
        public List<object> Categories { get; set; }
        public DateTime ReceivedDateTime { get; set; }
        public DateTime SentDateTime { get; set; }
        public bool HasAttachments { get; set; }
        public string InternetMessageId { get; set; }
        public string Subject { get; set; }
        public string BodyPreview { get; set; }
        public string Importance { get; set; }
        public string ParentFolderId { get; set; }
        public string ConversationId { get; set; }
        public string ConversationIndex { get; set; }
        public object IsDeliveryReceiptRequested { get; set; }
        public bool IsReadReceiptRequested { get; set; }
        public bool IsRead { get; set; }
        public bool IsDraft { get; set; }
        public string WebLink { get; set; }
        public string InferenceClassification { get; set; }
        public EmailBody Body { get; set; }
    }
}