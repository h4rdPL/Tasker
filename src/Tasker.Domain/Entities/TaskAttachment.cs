namespace Tasker.Domain.Entities
{
    public class TaskAttachment
    {
        public Guid Id { get; private set; }
        public Guid TaskId { get; private set; }

        public string FileName { get; private set; }
        public string FilePath { get; private set; }
        public long FileSize { get; private set; }

        public DateTime UploadedAt { get; private set; }

        protected TaskAttachment() { }

        public TaskAttachment(Guid taskId, string fileName, string filePath, long fileSize)
        {
            Id = Guid.NewGuid();
            TaskId = taskId;
            FileName = fileName;
            FilePath = filePath;
            FileSize = fileSize;
            UploadedAt = DateTime.UtcNow;
        }
    }

}
