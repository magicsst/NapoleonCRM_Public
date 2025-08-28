using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace NapoleonCRM.Shared.Models;

[DataContract]
public class Order
{
    [Key]
    [DataMember]
    public long? Id { get; set; }

    [DataMember]
    public DateTime? OrderDate { get; set; }

    [DataMember]
    public long? CustomerId { get; set; }

    [DataMember]
    public Customer? Customer { get; set; }

    [DataMember]
    public List<OrderDetail>? OrderDetail { get; set; }

    [DataMember]
    public DateTime? CreatedDate { get; set; }

    [DataMember]
    public DateTime ModifiedDate { get; set; }
}
