namespace CrystalVortex.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Drawing;
    using System.Linq;

    public class ReleaseModel : DbContext
    {
        // Your context has been configured to use a 'ReleaseModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'CrystalVortex.Models.ReleaseModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ReleaseModel' 
        // connection string in the application configuration file.
        public ReleaseModel()
            : base("name=ReleaseModel")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

         public virtual DbSet<Release> Releases { get; set; }
    }

    public class Release
    {
        [Key]
        public int Id { get; set; }
        [Index(IsUnique = true)]
        [MaxLength(6)]
        [Required]
        public string ReleaseCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:d}")]
        public DateTime ReleaseDate { get; set; }
        public byte[] AlbumArt { get; set; }
        private List<Track> _tracks = new List<Track>();
        public List<Track> Tracks { get { return _tracks; } set { _tracks = value; } }
        public string TorrentURL { get; set; }
        public string TorrentMD5 { get; set; }
    }

    public class Track
    {
        public int TrackId { get; set; }
        public int TrackNumber { get; set; }
        public string Name { get; set; }
        public TimeSpan Length { get; set; }
        public string URL { get; set; }

    }
}