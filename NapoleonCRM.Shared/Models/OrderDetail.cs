using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace NapoleonCRM.Shared.Models;

[DataContract]
public class OrderDetail
{
    [Key]
    [DataMember]
    public long? Id { get; set; }

    [DataMember]
    public long? OrderId { get; set; }

    [DataMember]
    public Order? Order { get; set; }

    [DataMember]
    public long? ProductId { get; set; }

    [DataMember]
    public Product? Product { get; set; }

    [DataMember]
    public long? Quantity { get; set; }

    [DataMember]
    public decimal? UnitPrice { get; set; }

    [DataMember]
    public decimal? TotalPrice { get; set; }

    public DateTime? CreatedDate { get; set; }

    [DataMember]
    public DateTime ModifiedDate { get; set; }
}
