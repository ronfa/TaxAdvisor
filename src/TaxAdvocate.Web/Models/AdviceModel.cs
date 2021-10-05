using System.Collections.Generic;
using TaxAdvocate.Business.Model;

namespace TaxAdvocate.Web.Models
{
    public class AdviceModel
    {
        public string AdviceId { get; set; }
        public List<AdviceIndicator> AdviceIndicators { get; set; }
    }
}
