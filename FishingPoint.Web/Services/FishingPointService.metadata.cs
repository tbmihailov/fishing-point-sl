
namespace FishingPoint.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;


    // The MetadataTypeAttribute identifies FishermanUserMetadata as the class
    // that carries additional metadata for the FishermanUser class.
    [MetadataTypeAttribute(typeof(FishermanUser.FishermanUserMetadata))]
    public partial class FishermanUser
    {

        // This class allows you to attach custom attributes to properties
        // of the FishermanUser class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class FishermanUserMetadata
        {
            // Metadata classes are not meant to be instantiated.
            private FishermanUserMetadata()
            {
            }

            [DisplayName("About"),
             Description("About"),
             StringLength(255,
                          ErrorMessage = "'Fisherman about' must be max 255 charecters long!")]
            public string Description { get; set; }

            [DisplayName("Fisherman"),
             Description("Fisherman user details")]
            public int FishermanUserId { get; set; }

            [DisplayName("Name"),
             Description("Name of fisherman"),
             StringLength(50,
                          ErrorMessage = "'Fisherman name' must be max 50 charecters long!")]
            public string Name { get; set; }

            [DisplayName("Register date"),
             Description("Date of registration")]
            public Nullable<DateTime> Registered { get; set; }

            [DisplayName("Username"),
             Description("Username of the fisherman")]
            public string Username { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies PointMetadata as the class
    // that carries additional metadata for the Point class.
    [MetadataTypeAttribute(typeof(Point.PointMetadata))]
    public partial class Point
    {

        // This class allows you to attach custom attributes to properties
        // of the Point class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class PointMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private PointMetadata()
            {
            }

            [DisplayName("Date created"),
             Description("Date of creation")]
            public Nullable<DateTime> Created { get; set; }

            [DisplayName("Description"),
             Description("Description of the point - why do you chose it."),
             StringLength(50,
                          ErrorMessage = "'Point description' must be max 50 charecters long!")]
            public string Description { get; set; }

            [DisplayName("Latitude"),
             Description("Latitude")]
            public Nullable<double> Latitude { get; set; }

            [DisplayName("Longitude"),
             Description("Longtitude")]
            public Nullable<double> Longitude { get; set; }

            [DisplayName("Fishing point"),
             Description("A point for fishing")]
            public int PointId { get; set; }

            [DisplayName("Tags"),
             Description("Tags associated to the point - may be fish species")]
            public string Tags { get; set; }
        }
    }

    
}
