namespace CarDealer.Dtos.Export
{
    using System.Xml.Serialization;

    // XmlAttribute will be displayed like this: <suplier id="2" name="VF Corporation" parts-count="3" />

    // XmlElement will be displayed like this:  <Supplier>
    //                                                  <name>3M Company</name>
    //                                                  <isImporter>true</isImporter>
    //                                          </Supplier>

    [XmlType("suplier")]
    public class ExportLocalSuppliersDto
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("parts-count")]
        public int PartsCount { get; set; }
    }
}
