namespace Cinema.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("Ticket")]
    public class TicketCustomerImportDto
    {
        // <Ticket>
        //     <ProjectionId>1</ProjectionId>
        //     <Price>7</Price>
        // </Ticket>

        [Required]
        [Range(typeof(decimal), "0.01", "1000000000000000000000")]
        [XmlElement("Price")]
        public decimal Price { get; set; }

        [Required]
        [XmlElement("ProjectionId")]
        public int ProjectionId { get; set; }
    }
}
