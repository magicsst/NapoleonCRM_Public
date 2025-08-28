using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Runtime.Serialization;

namespace NapoleonCRM.Shared.Models;

[DataContract]
public class Order
{
    [Key]
    [DataMember]
    public long? Id { get; set; }

    [DataMember]
    public string? ProductId { get; set; }

    [DataMember]
    public string? ServiceId { get; set; }
  
    [DataMember]
    public DateTime? OrderDate { get; set; }

    [DataMember]
    public long? CustomerId { get; set; }

    [DataMember]
    public long? Quantity { get; set; }

    [DataMember]
    public decimal? TotalAmount { get; set; }

    [DataMember]
    public DateTime? OrderDate { get; set; }

    [DataMember]
    public string? ReceiptPhoto { get; set; }

    [DataMember]
    public string? Notes { get; set; }
    
  public Customer? Customer { get; set; }

    [DataMember]
    public List<OrderDetail>? OrderDetails { get; set; }

    [DataMember]
    public DateTime? CreatedDate { get; set; }

    [DataMember]
    public DateTime ModifiedDate { get; set; }
}
