namespace ApiTrackers.ApiParams
{
    public class TrackerUpdateDTO
    {
        public int id { get; set; }
        public string artist { get; set; }
        public string title { get; set; }
        public string bpm { get; set; }
        public string comments { get; set; }
        public string coprightInformations { get; set; }

        internal Tracker toTracker()
        {
            var trackerToInsert = new Tracker();
            trackerToInsert.trackerContent.BPM = bpm;
            trackerToInsert.trackerMetadata.artist = artist;
            trackerToInsert.trackerMetadata.title = title;
            trackerToInsert.trackerMetadata.comments = comments;
            trackerToInsert.trackerMetadata.copyrightInformation = coprightInformations;
            return trackerToInsert;
        }
    }
}
