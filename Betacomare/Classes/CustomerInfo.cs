using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Betacomare.Classes
{
    public class CustomerInfo
    {
        public string? title { get; set; }
        public string firstName { get; set; }
        public string? middleName { get; set; }
        public string lastName { get; set; }
        public string? companyName { get; set; }
        public string emailAddress { get; set; }
        public string? phone { get; set; }
        public string addressLine1 { get; set; }
        public string city { get; set; }
        public string stateProvince { get; set; }
        public string countryRegion { get; set; }
        public string postalCode { get; set; }
    }
}
