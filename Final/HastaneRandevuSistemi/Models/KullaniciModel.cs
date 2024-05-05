using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace HastaneRandevuSistemi.Models
{
    public class KullaniciModel
    {
        [Key]
        public int KullaniciId { get; set; }

        [Required(ErrorMessage ="Durum Boş Olamaz")]
        public string state { get; set; } = "Hasta";

        [Required(ErrorMessage = "Ad Boş Olamaz")]
        [MaxLength(50)]
        public string KullaniciAd { get; set; }
        [Required(ErrorMessage = "Soyad Boş Olamaz")]
        [MaxLength(50)]
        public string KullaniciSoyad { get; set; }
        [Required(ErrorMessage = "Kimlik Numarası Boş Olamaz")]
        [StringLength(11,MinimumLength =11,ErrorMessage ="Kimlik Numarası Hatalı")]
        
        public string KullaniciTcNo { get; set; }
        [Required(ErrorMessage = "Tarih Boş Olamaz")]
        [Display(Name = "Dogum Tarihi")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyy}",ApplyFormatInEditMode =true)]
        public DateTime KullaniciDogumTarihi { get; set; }
        [Required(ErrorMessage = "Telefon Boş Olamaz")]
        [StringLength(10, MinimumLength = 10,ErrorMessage ="Telefon Numarası 10 Haneli olmalıdır")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Telefon numarası 10 haneli olmalıdır.")]
        
        public string KullaniciTelefon { get; set; }
        [Required(ErrorMessage = "Mail Boş Olamaz")]
        [EmailAddress(ErrorMessage = "Mail Adresi Uygun Değil")]
        public string KullaniciMail { get; set; }
        [Required(ErrorMessage = "Cinsiyet Boş Olamaz")]
        public string KullaniciCinsiyet { get; set; }
        
        [Required(ErrorMessage = "Şifre Boş Olamaz")]
        [DataType(DataType.Password)]
        public string KullaniciSifre { get; set; }

        [Required(ErrorMessage = "Şifre Boş Olamaz")]
        [DataType(DataType.Password)]
        [Compare("KullaniciSifre")]
        [NotMapped]
        public string KullaniciSifreTekrar { get; set; }


        
        //public virtual DoktorModel Doktor { get; set; }
    }
}
