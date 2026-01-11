using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("File_Details")]   // ✅ MATCH SQL TABLE NAME
public class FileDetails
{
    [Key]
    public int FileId { get; set; }

    [Required]
    public int ParentFileId { get; set; }

    [Required]
    public string FileName { get; set; }

    public string FilePath { get; set; }
    public string FileType { get; set; }

    [Required]
    [Column("TextContent", TypeName = "nvarchar(max)")]
    public string TextContent { get; set; }

    public DateTime UploadedOn { get; set; }
}
