using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("File_Details")]   // ✅ MATCH SQL TABLE NAME
public class FileDetails
{
    [Key]
    public int FileId { get; set; }

    public string FileName { get; set; }
    public string FilePath { get; set; }
    public string FileType { get; set; }
    public DateTime UploadedOn { get; set; }
}
