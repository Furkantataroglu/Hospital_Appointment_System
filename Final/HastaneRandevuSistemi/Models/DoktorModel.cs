using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HastaneRandevuSistemi.Models
{
    public class DoktorModel
    {
        [Key]
        public int DoktorId { get; set; }
        [Required]
        public string state { get; set; } = "Doktor";

        [ForeignKey("BolumId")]
        public int BolumId { get; set; }
        public BolumModel bolum { get; set; }

        [ForeignKey("hastanelerId")]
        public HastanelerModel hastaneler { get; set; }
        public int hastanelerId { get; set; }
        [Required(ErrorMessage ="Ad Girilmelidir")]
        [MaxLength(50)]

        public string DoktorAdi { get; set; }
        [Required(ErrorMessage = "Soyad Girilmelidir")]
        [MaxLength(50)]
        public string DoktorSoyad { get; set; }
        [Required(ErrorMessage = "Kimlik Girilmelidir")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Geçerli Kimlik Numarası Girin")]
        public string DoktorTcNo { get; set; }
        [Required(ErrorMessage = "Telefon Girilmelidir")]
        [StringLength(10, MinimumLength = 10 , ErrorMessage ="Geçerli Telefon Numarası Girin")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Telefon numarası 10 haneli olmalıdır.")]
        public string DoktorTelefon { get; set; }
        [Required(ErrorMessage = "Mail Girilmelidir")]
        [EmailAddress(ErrorMessage ="Geçerli Mail Adresi Girin")]
        public string DoktorMail { get; set; }
        [Required(ErrorMessage = "Şifre Girilmelidir")]
        [DataType(DataType.Password)]
        public string DoktorSifre { get; set; }
        [Required(ErrorMessage = "Şifre Tekrar Girilmelidir")]
        [DataType(DataType.Password)]
        [Compare("DoktorSifre",ErrorMessage ="Şifreler Uyuşmuyor")]
        [NotMapped]
        public string DoktorSifreTekrar { get; set; }


       // public ICollection<KullaniciModel> kullanici { get; set; }
    }
}
