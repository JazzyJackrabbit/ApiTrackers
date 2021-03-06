
namespace ApiTrackers.ApiParams
{
    public class TrackerCreateDTO
    {
        public string artist { get; set; }
        public string title { get; set; }
        public string bpm { get; set; }
        public string comments { get; set; }
        public string coprightInformations { get; set; }
        public int idUser { get; set; }

        public Tracker toTracker()
        {
            var trackerToInsert = new Tracker();
            trackerToInsert.trackerContent.BPM = bpm;
            trackerToInsert.trackerMetadata.artist = artist;
            trackerToInsert.trackerMetadata.title = title;
            trackerToInsert.trackerMetadata.comments = comments;
            trackerToInsert.trackerMetadata.copyrightInformation = coprightInformations;
            trackerToInsert.idUser = idUser;
            return trackerToInsert;
        }
    }
}
