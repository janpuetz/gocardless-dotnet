using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GoCardless.Resources
{

    /// <summary>
    /// Represents a event resource.
    ///
    /// Events are stored for all webhooks. An event refers to a resource which
    /// has been updated, for example a payment which has been collected, or a
    /// mandate which has been transferred.
    /// </summary>
    public class Event
    {
        /// <summary>
        /// What has happened to the resource.
        /// </summary>
        [JsonProperty("action")]
        public string Action { get; set; }

        /// <summary>
        /// Fixed [timestamp](#api-usage-time-zones--dates), recording when this
        /// resource was created.
        /// </summary>
        [JsonProperty("created_at")]
        public DateTimeOffset? CreatedAt { get; set; }

        /// <summary>
        /// Present only in webhooks when an integrator is authorised to send
        /// their own
        /// notifications. See
        /// [here](/getting-started/api/handling-customer-notifications/)
        /// for further information.
        /// 
        /// </summary>
        [JsonProperty("customer_notifications")]
        public List<EventCustomerNotification> CustomerNotifications { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("details")]
        public EventDetails Details { get; set; }

        /// <summary>
        /// Unique identifier, beginning with "EV".
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Resources linked to this Event.
        /// </summary>
        [JsonProperty("links")]
        public EventLinks Links { get; set; }

        /// <summary>
        /// If the `details[origin]` is `api`, this will contain any metadata
        /// you specified when triggering this event. In other cases it will be
        /// an empty object.
        /// </summary>
        [JsonProperty("metadata")]
        public IDictionary<string, string> Metadata { get; set; }

        /// <summary>
        /// The resource type for this event. One of:
        /// <ul>
        /// <li>`payments`</li>
        /// <li>`mandates`</li>
        /// <li>`payouts`</li>
        /// <li>`refunds`</li>
        /// <li>`subscriptions`</li>
        /// </ul>
        /// </summary>
        [JsonProperty("resource_type")]
        public EventResourceType? ResourceType { get; set; }
    }
    
    /// <summary>
    /// Present only in webhooks when an integrator is authorised to send their
    /// own
    /// notifications. See
    /// [here](/getting-started/api/handling-customer-notifications/)
    /// for further information.
    /// 
    /// </summary>
    public class EventCustomerNotification
    {
        /// <summary>
        /// Time after which GoCardless will send the notification by email.
        /// </summary>
        [JsonProperty("deadline")]
        public string Deadline { get; set; }

        /// <summary>
        /// The id of the notification.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Whether or not the notification must be sent.
        /// </summary>
        [JsonProperty("mandatory")]
        public bool? Mandatory { get; set; }

        /// <summary>
        /// The type of notification the customer shall receive.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
    }
    
    /// <summary>
    /// The type of notification the customer shall receive.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EventCustomerNotificationType {

        /// <summary>`type` with a value of "payment_created"</summary>
        [EnumMember(Value = "payment_created")]
        PaymentCreated,
        /// <summary>`type` with a value of "mandate_created"</summary>
        [EnumMember(Value = "mandate_created")]
        MandateCreated,
        /// <summary>`type` with a value of "subscription_created"</summary>
        [EnumMember(Value = "subscription_created")]
        SubscriptionCreated,
    }

    public class EventDetails
    {
        /// <summary>
        /// What triggered the event. _Note:_ `cause` is our simplified and
        /// predictable key indicating what triggered the event.
        /// </summary>
        [JsonProperty("cause")]
        public string Cause { get; set; }

        /// <summary>
        /// Human readable description of the cause. _Note:_ Changes to event
        /// descriptions are not considered breaking.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Who initiated the event. One of:
        /// <ul>
        /// <li>`bank`: this event was triggered by a report from the banks</li>
        /// <li>`gocardless`: this event was performed by GoCardless
        /// automatically</li>
        /// <li>`api`: this event was triggered by an API endpoint</li>
        /// <li>`customer`: this event was triggered by a Customer</li>
        /// </ul>
        /// </summary>
        [JsonProperty("origin")]
        public EventDetailsOrigin? Origin { get; set; }

        /// <summary>
        /// Set when a `bank` is the origin of the event. This is the reason
        /// code received in the report from the customer's bank. See the
        /// [GoCardless Direct Debit
        /// guide](https://gocardless.com/direct-debit/receiving-messages) for
        /// information on the meanings of different reason codes. _Note:_
        /// `reason_code` is payment scheme-specific and can be inconsistent
        /// between banks.
        /// </summary>
        [JsonProperty("reason_code")]
        public string ReasonCode { get; set; }

        /// <summary>
        /// A Direct Debit scheme. Set when a bank is the origin of the event.
        /// </summary>
        [JsonProperty("scheme")]
        public EventDetailsScheme? Scheme { get; set; }
    }
    
    /// <summary>
    /// Who initiated the event. One of:
    /// <ul>
    /// <li>`bank`: this event was triggered by a report from the banks</li>
    /// <li>`gocardless`: this event was performed by GoCardless automatically</li>
    /// <li>`api`: this event was triggered by an API endpoint</li>
    /// <li>`customer`: this event was triggered by a Customer</li>
    /// </ul>
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EventDetailsOrigin {

        /// <summary>`origin` with a value of "bank"</summary>
        [EnumMember(Value = "bank")]
        Bank,
        /// <summary>`origin` with a value of "api"</summary>
        [EnumMember(Value = "api")]
        Api,
        /// <summary>`origin` with a value of "gocardless"</summary>
        [EnumMember(Value = "gocardless")]
        Gocardless,
        /// <summary>`origin` with a value of "customer"</summary>
        [EnumMember(Value = "customer")]
        Customer,
    }

    /// <summary>
    /// A Direct Debit scheme. Set when a bank is the origin of the event.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EventDetailsScheme {

        /// <summary>`scheme` with a value of "ach"</summary>
        [EnumMember(Value = "ach")]
        Ach,
        /// <summary>`scheme` with a value of "autogiro"</summary>
        [EnumMember(Value = "autogiro")]
        Autogiro,
        /// <summary>`scheme` with a value of "bacs"</summary>
        [EnumMember(Value = "bacs")]
        Bacs,
        /// <summary>`scheme` with a value of "becs"</summary>
        [EnumMember(Value = "becs")]
        Becs,
        /// <summary>`scheme` with a value of "becs_nz"</summary>
        [EnumMember(Value = "becs_nz")]
        BecsNz,
        /// <summary>`scheme` with a value of "betalingsservice"</summary>
        [EnumMember(Value = "betalingsservice")]
        Betalingsservice,
        /// <summary>`scheme` with a value of "pad"</summary>
        [EnumMember(Value = "pad")]
        Pad,
        /// <summary>`scheme` with a value of "sepa_core"</summary>
        [EnumMember(Value = "sepa_core")]
        SepaCore,
        /// <summary>`scheme` with a value of "sepa_cor1"</summary>
        [EnumMember(Value = "sepa_cor1")]
        SepaCor1,
    }

    /// <summary>
    /// Resources linked to this Event
    /// </summary>
    public class EventLinks
    {
        /// <summary>
        /// If `resource_type` is `mandates`, this is the ID of the
        /// [mandate](#core-endpoints-mandates) which has been updated.
        /// </summary>
        [JsonProperty("mandate")]
        public string Mandate { get; set; }

        /// <summary>
        /// This is only included for mandate transfer events, when it is the ID
        /// of the [customer bank
        /// account](#core-endpoints-customer-bank-accounts) which the mandate
        /// is being transferred to.
        /// </summary>
        [JsonProperty("new_customer_bank_account")]
        public string NewCustomerBankAccount { get; set; }

        /// <summary>
        /// This is only included for mandate replaced events, when it is the ID
        /// of the new [mandate](#core-endpoints-mandates) that replaces the
        /// existing mandate.
        /// </summary>
        [JsonProperty("new_mandate")]
        public string NewMandate { get; set; }

        /// <summary>
        /// If the event is included in a [webhook](#webhooks-overview) to an
        /// [OAuth app](#appendix-oauth), this is the ID of the account to which
        /// it belongs.
        /// </summary>
        [JsonProperty("organisation")]
        public string Organisation { get; set; }

        /// <summary>
        /// If this event was caused by another, this is the ID of the cause.
        /// For example, if a mandate is cancelled it automatically cancels all
        /// pending payments associated with it; in this case, the payment
        /// cancellation events would have the ID of the mandate cancellation
        /// event in this field.
        /// </summary>
        [JsonProperty("parent_event")]
        public string ParentEvent { get; set; }

        /// <summary>
        /// If `resource_type` is `payments`, this is the ID of the
        /// [payment](#core-endpoints-payments) which has been updated.
        /// </summary>
        [JsonProperty("payment")]
        public string Payment { get; set; }

        /// <summary>
        /// If `resource_type` is `payouts`, this is the ID of the
        /// [payout](#core-endpoints-payouts) which has been updated.
        /// </summary>
        [JsonProperty("payout")]
        public string Payout { get; set; }

        /// <summary>
        /// This is only included for mandate transfer events, when it is the ID
        /// of the [customer bank
        /// account](#core-endpoints-customer-bank-accounts) which the mandate
        /// is being transferred from.
        /// </summary>
        [JsonProperty("previous_customer_bank_account")]
        public string PreviousCustomerBankAccount { get; set; }

        /// <summary>
        /// If `resource_type` is `refunds`, this is the ID of the
        /// [refund](#core-endpoints-refunds) which has been updated.
        /// </summary>
        [JsonProperty("refund")]
        public string Refund { get; set; }

        /// <summary>
        /// If `resource_type` is `subscription`, this is the ID of the
        /// [subscription](#core-endpoints-subscriptions) which has been
        /// updated.
        /// </summary>
        [JsonProperty("subscription")]
        public string Subscription { get; set; }
    }
    
    /// <summary>
    /// The resource type for this event. One of:
    /// <ul>
    /// <li>`payments`</li>
    /// <li>`mandates`</li>
    /// <li>`payouts`</li>
    /// <li>`refunds`</li>
    /// <li>`subscriptions`</li>
    /// </ul>
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EventResourceType {

        /// <summary>`resource_type` with a value of "payments"</summary>
        [EnumMember(Value = "payments")]
        Payments,
        /// <summary>`resource_type` with a value of "mandates"</summary>
        [EnumMember(Value = "mandates")]
        Mandates,
        /// <summary>`resource_type` with a value of "payouts"</summary>
        [EnumMember(Value = "payouts")]
        Payouts,
        /// <summary>`resource_type` with a value of "refunds"</summary>
        [EnumMember(Value = "refunds")]
        Refunds,
        /// <summary>`resource_type` with a value of "subscriptions"</summary>
        [EnumMember(Value = "subscriptions")]
        Subscriptions,
    }

}
