namespace Cinema.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("Projection")]
    public class ProjectionImportDto
    {
        // What we need in the Projection DTO:
        // <Projection>
        //      <MovieId>38</MovieId>
        //      <HallId>4</HallId>
        //      <DateTime>2019-04-27 13:33:20</DateTime>
        // </Projection>

        [Required]
        [XmlElement("MovieId")]
        public int MovieId { get; set; }

        [Required]
        [XmlElement("HallId")]
        public int HallId { get; set; }

        [Required]
        [XmlElement("DateTime")]
        public string DateTime { get; set; }
    }
}
