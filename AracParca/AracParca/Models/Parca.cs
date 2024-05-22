using System.ComponentModel.DataAnnotations;

namespace AracParca.Models
{
	public class Parca
	{
		[Key]
		public int Id { get; set; }

        [Required(ErrorMessage = "ModelAralik  Yazınız")]
		[Display(Name = "ModelAralik ")]
		public string? ModelAralik { get; set; }

		[Required(ErrorMessage = "ArabaMarka Yazınız")]
		[Display(Name = "ArabaMarka ")]
		public string? ArabaMarka { get; set; }

		[Required(ErrorMessage = "ArabaModel  Yazınız")]
		[Display(Name = "ArabaModel")]
		public string? ArabaModel { get; set; }

		[Required(ErrorMessage = "Bolum  Yazınız")]
		[Display(Name = "Bolum ")]
		public string? Bolum { get; set; }

		[Required(ErrorMessage = "Kurs Fiyatı Yazınız")]
		[Display(Name = "Fiyatı")]
		public float? Fiyatı { get; set; }

		[Required(ErrorMessage = "Ürün resimini seeçiniz.")]
		public string? PhotoPath { get; set; }
	}
}
