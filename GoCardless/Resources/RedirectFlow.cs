using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GoCardless.Resources
{

    /// <summary>
    /// Represents a redirect flow resource.
    ///
    /// Redirect flows enable you to use GoCardless' [hosted payment
    /// pages](https://pay-sandbox.gocardless.com/AL000000AKFPFF) to set up
    /// mandates with your customers. These pages are fully compliant and have
    /// been translated into Danish, Dutch, French, German, Italian, Norwegian,
    /// Portuguese, Slovak, Spanish and Swedish.
    /// 
    /// The overall flow is:
    /// 
    /// 1. You [create](#redirect-flows-create-a-redirect-flow) a redirect flow
    /// for your customer, and redirect them to the returned redirect url, e.g.
    /// `https://pay.gocardless.com/flow/RE123`.
    /// 
    /// 2. Your customer supplies their name, email, address, and bank account
    /// details, and submits the form. This securely stores their details, and
    /// redirects them back to your `success_redirect_url` with
    /// `redirect_flow_id=RE123` in the querystring.
    /// 
    /// 3. You [complete](#redirect-flows-complete-a-redirect-flow) the redirect
    /// flow, which creates a [customer](#core-endpoints-customers), [customer
    /// bank account](#core-endpoints-customer-bank-accounts), and
    /// [mandate](#core-endpoints-mandates), and returns the ID of the mandate.
    /// You may wish to create a [subscription](#core-endpoints-subscriptions)
    /// or [payment](#core-endpoints-payments) at this point.
    /// 
    /// Once you have [completed](#redirect-flows-complete-a-redirect-flow) the
    /// redirect flow via the API, you should display a confirmation page to
    /// your customer, confirming that their Direct Debit has been set up. You
    /// can build your own page, or redirect to the one we provide in the
    /// `confirmation_url` attribute of the redirect flow.
    /// 
    /// Redirect flows expire 30 minutes after they are first created. You
    /// cannot complete an expired redirect flow.
    /// </summary>
    public class RedirectFlow
    {
        /// <summary>
        /// The URL of a confirmation page, which you may optionally redirect
        /// the customer to rather than use your own page, that confirms in
        /// their chosen language that their Direct Debit has been set up
        /// successfully. Only returned once the customer has set up their
        /// mandate via the payment pages and the redirect flow has been
        /// [completed](#redirect-flows-complete-a-redirect-flow), and only
        /// available for 15 minutes from when you complete the redirect flow.
        /// The structure of this URL may change at any time, so you should read
        /// it directly from the API response.
        /// </summary>
        [JsonProperty("confirmation_url")]
        public string ConfirmationUrl { get; set; }

        /// <summary>
        /// Fixed [timestamp](#api-usage-time-zones--dates), recording when this
        /// resource was created.
        /// </summary>
        [JsonProperty("created_at")]
        public DateTimeOffset? CreatedAt { get; set; }

        /// <summary>
        /// A description of the item the customer is paying for. This will be
        /// shown on the hosted payment pages.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Unique identifier, beginning with "RE".
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Resources linked to this RedirectFlow.
        /// </summary>
        [JsonProperty("links")]
        public RedirectFlowLinks Links { get; set; }

        /// <summary>
        /// The URL of the hosted payment pages for this redirect flow. This is
        /// the URL you should redirect your customer to.
        /// </summary>
        [JsonProperty("redirect_url")]
        public string RedirectUrl { get; set; }

        /// <summary>
        /// The Direct Debit scheme of the mandate. If specified, the payment
        /// pages will only allow the set-up of a mandate for the specified
        /// scheme. It is recommended that you leave this blank so the most
        /// appropriate scheme is picked based on the customer's bank account.
        /// </summary>
        [JsonProperty("scheme")]
        public RedirectFlowScheme? Scheme { get; set; }

        /// <summary>
        /// The customer's session ID must be provided when the redirect flow is
        /// set up and again when it is completed. This allows integrators to
        /// ensure that the user who was originally sent to the GoCardless
        /// payment pages is the one who has completed them.
        /// </summary>
        [JsonProperty("session_token")]
        public string SessionToken { get; set; }

        /// <summary>
        /// The URL to redirect to upon successful mandate setup. You must use a
        /// URL beginning `https` in the live environment.
        /// </summary>
        [JsonProperty("success_redirect_url")]
        public string SuccessRedirectUrl { get; set; }
    }
    
    /// <summary>
    /// Resources linked to this RedirectFlow
    /// </summary>
    public class RedirectFlowLinks
    {
        /// <summary>
        /// The [creditor](#core-endpoints-creditors) for whom the mandate will
        /// be created. The `name` of the creditor will be displayed on the
        /// payment page.
        /// </summary>
        [JsonProperty("creditor")]
        public string Creditor { get; set; }

        /// <summary>
        /// ID of [customer](#core-endpoints-customers) created by this redirect
        /// flow.<br/>**Note**: this property will not be present until the
        /// redirect flow has been successfully completed.
        /// </summary>
        [JsonProperty("customer")]
        public string Customer { get; set; }

        /// <summary>
        /// ID of [customer bank
        /// account](#core-endpoints-customer-bank-accounts) created by this
        /// redirect flow.<br/>**Note**: this property will not be present until
        /// the redirect flow has been successfully completed.
        /// </summary>
        [JsonProperty("customer_bank_account")]
        public string CustomerBankAccount { get; set; }

        /// <summary>
        /// ID of [mandate](#core-endpoints-mandates) created by this redirect
        /// flow.<br/>**Note**: this property will not be present until the
        /// redirect flow has been successfully completed.
        /// </summary>
        [JsonProperty("mandate")]
        public string Mandate { get; set; }
    }
    
    /// <summary>
    /// The Direct Debit scheme of the mandate. If specified, the payment pages will only allow the
    /// set-up of a mandate for the specified scheme. It is recommended that you leave this blank so
    /// the most appropriate scheme is picked based on the customer's bank account.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RedirectFlowScheme {

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
    }

}
