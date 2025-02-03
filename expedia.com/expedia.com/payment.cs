using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace expedia.com
{
    public class Payment
    {
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string CVV { get; set; }
        public double Amount { get; set; }

       
        public Payment(string cardNumber, string expirationDate, string cvv, double amount)
        {
            CardNumber = cardNumber;
            ExpirationDate = expirationDate;
            CVV = cvv;
            Amount = amount;
        }

        // Method to validate payment details
        public bool ValidatePaymentDetails()
        {
            // Basic validation (can be expanded further)
            if (string.IsNullOrEmpty(CardNumber) || string.IsNullOrEmpty(ExpirationDate) || string.IsNullOrEmpty(CVV))
            {
                Console.WriteLine("Invalid payment details. Please provide all required information.");
                return false;
            }

            // For now, assume card is valid if all fields are filled
            Console.WriteLine("Payment details validated successfully.");
            return true;
        }

        // Method to process the payment
        public bool ProcessPayment()
        {
            Console.WriteLine("\nPayment Details:");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"{"Card Number:",-15} {CardNumber.Substring(0, 4)}XXXXXXXX{CardNumber.Substring(12)}");
            Console.WriteLine($"{"Expiration Date:",-15} {ExpirationDate}");
            Console.WriteLine($"{"CVV:",-15} {CVV}");
            Console.WriteLine($"{"Amount:",-15} ${Amount}");
            Console.WriteLine(new string('-', 50));

            if (ValidatePaymentDetails())
            {
                Console.WriteLine("Processing payment...");
                Console.WriteLine("Payment successful!");
                return true;
            }

            return false;
        }
    }

}
