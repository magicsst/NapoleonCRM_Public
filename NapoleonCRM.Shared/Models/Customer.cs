using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace NapoleonCRM.Shared.Models;

[DataContract]
public class Customer
{
    [Key]
    [DataMember]
    public long? Id { get; set; }

    [DataMember]
    public string? Name { get; set; }

    [DataMember]
    public string? Type { get; set; }

    [DataMember]
    public string? Industry { get; set; }

    [DataMember]
    public long? AddressId { get; set; }

    [DataMember]
    public long? ContactId { get; set; }

    
    
    [DataMember]
    public long? CountryId { get; set; }

    [DataMember]
    public Country? Country { get; set; }

    [DataMember]
    public long? CurrencyId { get; set; }

    [DataMember]
    public Currency? Currency { get; set; }
    

    [DataMember]
    public long? CategoryFirstId { get; set; }

    [DataMember]
    public CategoryFirst? CategoryFirst { get; set; }
    

    [DataMember]
    public string? Logo { get; set; }

    [DataMember]
    public string? Notes { get; set; }

    [DataMember]
    public Address? Address { get; set; }

    [DataMember]
    public List<Contact>? Contact { get; set; }

     [DataMember]
    public DateTime? CreatedDate { get; set; }

    [DataMember]
    public DateTime ModifiedDate { get; set; }



}
