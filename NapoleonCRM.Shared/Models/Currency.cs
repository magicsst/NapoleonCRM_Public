using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NapoleonCRM.Shared.Models
{
    [DataContract]
    public class Currency
    {
        [Key]
        [DataMember]
        public int Id { get; set; }
        
        [DataMember]
        public string Description { get; set; }
    }
}
