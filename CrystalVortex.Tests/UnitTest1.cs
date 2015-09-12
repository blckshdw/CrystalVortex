using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Diagnostics;
using System.Data.Entity.Migrations;

namespace CrystalVortex.Tests
{
    [TestClass]
    public class ReleasesUnitTest
    {
        [TestMethod]
        public void CreateDatabase()
        {
            using (var db = new CrystalVortex.Models.ReleaseModel())
            {
                db.Database.CreateIfNotExists();
            }

        }
        [TestMethod]
        public void InsertReleases()
        {
            using (var db = new CrystalVortex.Models.ReleaseModel())
            {
                var release = new Models.Release { Name = "Test", Description = "Unit Test", ReleaseDate = DateTime.Now.Date };
                release.Tracks.Add(new Models.Track { Name = "Unit Test Track", TrackNumber = 0 });

                db.Releases.Add(release);
                db.SaveChanges();
            }
        }
        [TestMethod]
        public void QueryDatabase()
        {
            using (var db = new Models.ReleaseModel())
            {
                var query = from r in db.Releases
                            orderby r.Id
                            select r;

                foreach (var item in query)
                {
                    Debug.WriteLine(item.Name);
                }

                Assert.IsTrue(query.Count() > 0);
            }
        }
        [TestMethod]
        public void UpsertReleases()
        {
            using (var db = new CrystalVortex.Models.ReleaseModel())
            {
                var release = new Models.Release { Id=2, Name = "Test "+DateTime.Now.Ticks, Description = "Unit Test", ReleaseDate = DateTime.Now.Date };
                for (int i = 1; i < 10; i++)
                {

                    release.Tracks.Add(new Models.Track { Name = "Unit Test Track #"+i, TrackNumber = i });
                }

                db.Releases.AddOrUpdate(release);
                db.SaveChanges();
            }
        }

        [TestMethod]
        public void LoadReleases()
        {
            using (var db = new CrystalVortex.Models.ReleaseModel())
            {
                db.Database.ExecuteSqlCommand("TRUNCATE TABLE Tracks");
                db.Database.ExecuteSqlCommand("DELETE FROM Releases");
                db.Database.ExecuteSqlCommand("DBCC CHECKIDENT (Releases, RESEED, 0)");

                db.Releases.Add(new Models.Release
                {
                    Id = 1,
                    ReleaseCode = "CVR001",
                    Description = "Corporate Government",
                    ReleaseDate = new DateTime(2013, 2, 12),
                    Name = "Intandem ",
                    TorrentMD5 = "81d087f81547c63975b12cb30c85a622",
                    TorrentURL = "http://ia601703.us.archive.org/6/items/CVR001/CVR001_archive.torrent",
                    Tracks = new System.Collections.Generic.List<Models.Track>
                    {
                        new Models.Track { TrackNumber = 1, Name = "Corporate Government (Original Mix)", Length = new TimeSpan(0,7,46), URL = "http://ia601703.us.archive.org/6/items/CVR001/Intandem%20-%20Corporate%20Government%20%28Original%20Mix%29.mp3" },
                        new Models.Track { TrackNumber = 2, Name = "Counter Subliminal (Original Mix)", Length = new TimeSpan(0,6,41), URL = "http://ia601703.us.archive.org/6/items/CVR001/Intandem%20-%20Counter%20Subliminal%20%28Original%20Mix%29.mp3" },
                    }
                });

                db.Releases.Add(new Models.Release
                {
                    Id = 2,
                    ReleaseCode = "CVR002",
                    Description = "locus somnorum ep",
                    ReleaseDate = new DateTime(2013, 5, 26),
                    Name = "camcussion",
                    TorrentMD5 = "0e3c84f70f5af1ed4327f85533e51dd4",
                    TorrentURL = "http://ia801709.us.archive.org/35/items/CVR002/CVR002_archive.torrent",
                    Tracks = new System.Collections.Generic.List<Models.Track>
                    {
                        new Models.Track { TrackNumber = 1, Name = "SynapticalShrapnel", Length = new TimeSpan(0,7,31), URL = "http://ia801709.us.archive.org/35/items/CVR002/camcussion-SynapticalShrapnel.mp3" },
                        new Models.Track { TrackNumber = 2, Name = "TangentCamcussions", Length = new TimeSpan(0,5,57), URL = "http://ia801709.us.archive.org/35/items/CVR002/camcussion-TangentCamcussions.mp3" },
                    }
                });

                db.Releases.Add(new Models.Release
                {
                    Id = 3,
                    ReleaseCode = "CVR003",
                    Description = "uh oh",
                    ReleaseDate = new DateTime(2013, 9, 3),
                    Name = "Antonio Bandpass",
                    TorrentMD5 = "dec946155714d4804ce1f074d0ac110b",
                    TorrentURL = "http://ia801007.us.archive.org/11/items/CVR003/CVR003_archive.torrent",
                    Tracks = new System.Collections.Generic.List<Models.Track>
                    {
                        new Models.Track { TrackNumber = 1, Name = "phixed phloat", Length = new TimeSpan(0,9,04), URL = "http://ia801007.us.archive.org/11/items/CVR003/Antonio%20Bandpass%20-%20phixed%20phloat.mp3" },
                        new Models.Track { TrackNumber = 2, Name = "uh oh", Length = new TimeSpan(0,6,24), URL = "http://ia801007.us.archive.org/11/items/CVR003/CVR003_archive.torrent" },
                    }
                });

                db.Releases.Add(new Models.Release
                {
                    Id = 4,
                    ReleaseCode = "CVR003",
                    Description = "Transit",
                    ReleaseDate = new DateTime(2014, 3, 1),
                    Name = "H445",
                    TorrentMD5 = "a43c7a6b1eb8a24b6f835781d2d38d73",
                    TorrentURL = "http://ia800306.us.archive.org/11/items/CVR004/CVR004_archive.torrent",
                    Tracks = new System.Collections.Generic.List<Models.Track>
                    {
                        new Models.Track { TrackNumber = 1, Name = "Transit", Length = new TimeSpan(0,9,43), URL = "http://ia800306.us.archive.org/11/items/CVR004/H445-Transit.mp3" },
                        new Models.Track { TrackNumber = 2, Name = "n0153", Length = new TimeSpan(0,6,26), URL = "http://ia800306.us.archive.org/11/items/CVR004/H445-n0153.mp3" },
                    }
                });

                db.Releases.Add(new Models.Release
                {
                    Id = 5,
                    ReleaseCode = "CVR005",
                    Description = "camcussion & ace prime selector",
                    ReleaseDate = new DateTime(2015, 7, 4),
                    Name = "Come Fly With Us",
                    TorrentMD5 = "6950d9cb87b62c28c965e660ca955871",
                    TorrentURL = "http://ia801502.us.archive.org/26/items/CVR005/CVR005_archive.torrent",
                    Tracks = new System.Collections.Generic.List<Models.Track>
                    {
                        new Models.Track { TrackNumber = 1, Name = "MountainTea", Length = new TimeSpan(0,5,4), URL = "http://ia801502.us.archive.org/26/items/CVR005/CamcussionAcePrimeSelector-MountainTea.mp3" },
                        new Models.Track { TrackNumber = 2, Name = "HoneyJourney", Length = new TimeSpan(0,7,0), URL = "http://ia801502.us.archive.org/26/items/CVR005/CamcussionAcePrimeSelector-HoneyJourney.mp3" },
                    }
                });


                db.SaveChanges();
            }
        }

    }
}
