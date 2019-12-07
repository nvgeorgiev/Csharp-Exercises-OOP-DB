namespace Cinema.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("Customer")]
    public class CustomerImportDto
    {
        // What we need in the Customer DTO:
        //  <Customer>
        //    <FirstName>Randi</FirstName>
        //    <LastName>Ferraraccio</LastName>
        //    <Age>20</Age>
        //    <Balance>59.44</Balance>
        //    <Tickets>
        //        <Ticket>
        //            <ProjectionId>1</ProjectionId>
        //            <Price>7</Price>
        //        </Ticket>
        //        <Ticket>
        //            <ProjectionId>1</ProjectionId>
        //            <Price>15</Price>
        //        </Ticket>
        //  </Customer>

        [Required]
        [StringLength(20, MinimumLength = 3)]
        [XmlElement("FirstName")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        [XmlElement("LastName")]
        public string LastName { get; set; }

        [Required]
        [Range(12, 110)]
        [XmlElement("Age")]
        public int Age { get; set; }

        [Required]
        [Range(typeof(decimal), "0.01", "1000000000000000000000")]
        [XmlElement("Balance")]
        public decimal Balance { get; set; }

        [XmlArray("Tickets")]
        public TicketCustomerImportDto[] Tickets { get; set; }
    }
}
