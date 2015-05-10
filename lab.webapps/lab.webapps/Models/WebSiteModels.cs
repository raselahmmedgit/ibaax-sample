using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace lab.webapps.Models
{
    public class WebSiteDomain : BaseModel
    {
        [Key]
        public int WebSiteDomainId { get; set; }
        
        [DisplayName("Domain Name")]
        [Required(ErrorMessage = "Domain Name is required")]
        [MaxLength(200)]
        public string Name { get; set; }

        [DisplayName("Domain Url")]
        [Required(ErrorMessage = "Domain Url is required")]
        [MaxLength(200)]
        public string Url { get; set; }
    }

    public class WebSiteInfo : BaseModel
    {
        [Key]
        public int WebSiteInfoId { get; set; }

        [DisplayName("Application Name")]
        [Required(ErrorMessage = "Application Name is required")]
        [MaxLength(200)]
        public string Name { get; set; }

        [DisplayName("Application Title")]
        [Required(ErrorMessage = "Application Title is required")]
        [MaxLength(200)]
        public string Title { get; set; }

        [DisplayName("Application Footer")]
        [Required(ErrorMessage = "Application Footer is required")]
        [MaxLength(200)]
        public string Footer { get; set; }

        public int WebSiteDomainId { get; set; }
    }

    public class WebSitePage : BaseModel
    {
        [Key]
        public int WebSitePageId { get; set; }
        [DisplayName("Page Name")]
        [Required(ErrorMessage = "Page Name is required")]
        [MaxLength(200)]
        public string Name { get; set; }

        [DisplayName("Page Url")]
        [Required(ErrorMessage = "Page Url is required")]
        [MaxLength(200)]
        public string Url { get; set; }

        [DisplayName("Page Url Param")]
        [MaxLength(200)]
        public string UrlParam { get; set; }

        public int WebSiteDomainId { get; set; }
    }

    public class WebSiteLayout : BaseModel
    {
        [Key]
        public Int32 WebSitePageLayoutId { get; set; }

        [Required(ErrorMessage = "Layout title is required")]
        [Display(Name = "Layout Title")]
        [StringLength(256)]
        public string LayoutTitle { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Please enter layout markup as HTML")]
        [Display(Name = "Layout Markup")]
        public string LayoutMarkup { get; set; }

        [Required(ErrorMessage = "Layout column is required")]
        [Display(Name = "No of Column")]
        public Int32 LayoutColumn { get; set; }

        [Required]
        public string LayoutMetaData { get; set; }

        public int WebSiteDomainId { get; set; }
    }

    public class WebSiteWidget : BaseModel
    {
        [Key]
        public Int32 WebSiteWidgetId { get; set; }

        [Required(ErrorMessage = "Widget title is required")]
        [Display(Name = "Title")]
        [StringLength(256)]
        public string WidgetTitle { get; set; }

        [Display(Name = "Style")]
        public string WidgetStyle { get; set; }

        [AllowHtml]
        [Required(ErrorMessage = "Widget template is required")]
        [Display(Name = "Template")]
        public string WidgetTemplate { get; set; }

        [Display(Name = "Script")]
        public string WidgetScript { get; set; }

        [Display(Name = "Thumbnail")]
        public byte[] WidgetScreenshot { get; set; }

        public string WidgetScreenshotName { get; set; }

        [Required]
        [Display(Name = "Is BannerAd")]
        public bool IsBannerAd { get; set; }

        [Required]
        [Display(Name = "Is Premium")]
        public bool IsPremium { get; set; }

        [Required]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        public int WebSiteDomainId { get; set; }
    }

    public class WebSiteTheme : BaseModel
    {
        [Key]
        public Int32 WebSiteThemeId { get; set; }

        [Required(ErrorMessage = "Theme Name is required")]
        [Display(Name = "Theme Title")]
        public string Name { get; set; }

        [Display(Name = "Theme Style")]
        public string ThemeStyle { get; set; }

        [Display(Name = "Theme Script")]
        public string ThemeScript { get; set; }

        [Display(Name = "Theme Template")]
        public string ThemeTemplate { get; set; }

        [Display(Name = "Thumbnail")]
        public byte[] ThemeScreenshot { get; set; }

        public string ThemeScreenshotName { get; set; }

        [Required]
        [Display(Name = "Is Draft")]
        [DefaultValue("true")]
        public bool IsDraft { get; set; }

        [Required]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        public int WebSiteDomainId { get; set; }
    }
}