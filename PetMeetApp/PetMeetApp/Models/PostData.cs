namespace PetMeetApp.Models
{
    public class PostData
    {
        public long Id { get; set; }
        public long PostId { get; set; }
        public PostModel Post { get; set; }
        public string AWSKey { get; set; }
        public long ContentTypeModelId { get; set; }
        public ContentTypeModel ContentTypeModel { get; set; }



    }
}
