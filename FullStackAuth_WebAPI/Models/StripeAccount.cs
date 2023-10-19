namespace FullStackAuth_WebAPI.Models
{
    public class StripeAccount
    {
        public string AccountId { get; set; } // Stripe Account ID
        public string UserId { get; set; } // Associated user ID
        public string AccountType { get; set; } // Standard, custom, express, etc.
        public string AccountStatus { get; set; } // Active, pending, deactivated, etc.
        public LegalEntity LegalEntity { get; set; } // Legal entity details
        public PayoutDetails PayoutDetails { get; set; } // Bank account details for payouts
        public Capabilities Capabilities { get; set; } // Card payments, transfers, etc.
        public long Balance { get; set; } // Current balance of the Stripe account (in cents)
        public List<Transactions> TransactionHistory { get; set; } // List of transactions
        public List<string> PayoutHistory { get; set; } // List of payouts
    }

    public class LegalEntity
    {
        public string BusinessName { get; set; } // Business name
        public string LegalName { get; set; } // Legal name
                                              // Add other legal entity details as needed
    }

    public class PayoutDetails
    {
        public string AccountNumber { get; set; } // Bank account number
        public string RoutingNumber { get; set; } // Routing number
        public string Currency { get; set; } // Currency (e.g., USD)
    }

    public class Capabilities
    {
        public bool CardPayments { get; set; }
        public bool Transfers { get; set; }
    }

}
