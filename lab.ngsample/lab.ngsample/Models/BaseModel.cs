using System.ComponentModel.DataAnnotations.Schema;


namespace lab.ngsample.Models
{
    public class BaseModel
    {
        [NotMapped]
        public virtual string SuccessMessage { get; set; }

        [NotMapped]
        public virtual string ErrorMessage { get; set; }

        [NotMapped]
        public virtual int TempId { get; set; }

        [NotMapped]
        public virtual string KendoWindow { get; set; }

        [NotMapped]
        public virtual string ActionLink { get; set; }

        [NotMapped]
        public virtual bool HasCreate { get; set; }

        [NotMapped]
        public virtual bool HasUpdate { get; set; }

        [NotMapped]
        public virtual bool HasDelete { get; set; }
    }

}