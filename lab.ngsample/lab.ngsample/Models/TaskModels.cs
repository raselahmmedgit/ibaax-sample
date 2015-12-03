using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace lab.ngsample.Models
{

    [Table("Task", Schema = "TSK")]
    public class Task : EntityBaseModel
    {
        [Required]
        [Display(AutoGenerateField = false)]
        [Key]
        public Int32 TaskId { get; set; }

        [Display(Name = "Display Id")]
        [StringLength(64)]
        public String DisplayId { get; set; }

        [Required(ErrorMessage = "Subject is required")]
        [Display(Name = "Subject")]
        [StringLength(256)]
        public String Subject { get; set; }

        [Display(Name = "Description")]
        [MaxLength]
        [DataType(DataType.Html)]
        [AllowHtml]
        public String Description { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Estimate Start Date")]
        public DateTime? EstimatedStartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Estimate End Date")]
        public DateTime? EstimatedEndDate { get; set; }

        public string Timezone { get; set; }
        public string RecurrenceRule { get; set; }
        public int? RecurrenceId { get; set; }
        public string RecurrenceException { get; set; }
        public bool IsAllDay { get; set; }

        [Display(Name = "Contact's Free Time")]
        public bool IsFreeTime { get; set; }

        [Required(ErrorMessage = "Please select task status")]
        [Display(Name = "Status")]
        [ForeignKey("TaskStatusCategory")]
        public Int32 TaskStatusCategoryId { get; set; }

        [Display(Name = "Priority")]
        [ForeignKey("TaskPriorityCategory")]
        public Int32? TaskPriorityCategoryId { get; set; }
        //By default priority Low, not mandatory

        [Display(Name = "Task Type")]
        [ForeignKey("TaskTypeCategory")]
        public Int32? TaskTypeCategoryId { get; set; }

        [ForeignKey("TaskTagCategory")]
        [Display(Name = "Task Tag Category")]
        public Int32? TaskTagCategoryId { get; set; }

        [Display(Name = "Estimated Work Hour")]
        public Int32 EstimatedWorkHour { get; set; }

        [Display(Name = "Is Fixed Rate or Hourly")]
        public bool IsFixedRateOrHourly { get; set; }

        [Display(Name = "Is Workflow Template")]
        public bool IsWorkflowTemplate { get; set; }

        [Display(Name = "Cost Rate")]
        public Double CostRate { get; set; }
        //different view [Task Cost and Billing Rate]

        [Display(Name = "Total Cost Amount")]
        public Double EstimatedCost { get; set; }
        //use function to calculate the amount, no entry

        [Display(Name = "Billing Rate")]
        public Double ClientBillingRate { get; set; }
        //Billing rate depends on hourly or fixed rate

        [Display(Name = "Total Billing Amount")]
        public Double TotalBillingAmount { get; set; }
        //use function to calculate the amount, no entry
        
        [Display(Name = "Color")]
        public String ColorCode { get; set; }

        [ForeignKey("Contact")]
        public Int32? ContactId { get; set; }

        [ForeignKey("Company")]
        public Int32? CompanyId { get; set; }

        [Display(Name = "Completion Percentage")]
        [DataType("Percent")]
        public Double? CompletionPercent { get; set; }

        public virtual Contact Contact { get; set; }
        public virtual Company Company { get; set; }
        public virtual TaskStatusCategory TaskStatusCategory { get; set; }
        public virtual TaskPriorityCategory TaskPriorityCategory { get; set; }
        public virtual TaskTypeCategory TaskTypeCategory { get; set; }
        public virtual TaskTagCategory TaskTagCategory { get; set; }

        #region NotMapped
        [NotMapped]
        [Display(Name = "Completion")]
        public int CompletionStatus { get; set; }
        [NotMapped]
        public string TaskStatusCategoryName { get; set; }
        [NotMapped]
        public string TaskPriorityCategoryName { get; set; }
        [NotMapped]
        public string TaskTypeCategoryName { get; set; }
        [NotMapped]
        public string TaskTagCategoryName { get; set; }
        [NotMapped]
        public string JobName { get; set; }
        [NotMapped]
        public string ContactName { get; set; }
        [NotMapped]
        public string CompanyName { get; set; }
        [NotMapped]
        public string AssignedByUserPhotoFileName { get; set; }
        [NotMapped]
        public string AssignedToUserPhotoFileName { get; set; }

        [NotMapped]
        public string AssignedByUserName { get; set; }
        [NotMapped]
        public string AssignedToUserName { get; set; }

        [NotMapped]
        public bool? IsAssignedToMe { get; set; }

        [NotMapped]
        public bool? IsCreatedByMe { get; set; }

        [NotMapped]
        public bool? HasAttachments { get; set; }

        [NotMapped]
        public string EventName { get; set; }
        [NotMapped]
        [Display(Name = "Assign To Employee")]
        public int? AssignedToUserId { get; set; }
        [NotMapped]
        public int? AssignedByUserId { get; set; }
        [NotMapped]
        public int? CurrentUserId { get; set; }
        [NotMapped]
        public DateTime? TaskStartDate { get; set; }
        [NotMapped]
        public DateTime? TaskCompletedDate { get; set; }
        [NotMapped]
        public DateTime? TaskAssignedDate { get; set; }
        [NotMapped]
        public DateTime? TaskUnAssignedDate { get; set; }
        [NotMapped]
        [Display(Name = "Logo")]
        public Byte[] Logo { get; set; }

        [NotMapped]
        public int TaskAssignedToUserId { get; set; }
        [NotMapped]
        public int TaskAssignedByUserId { get; set; }

        [NotMapped]
        [Display(Name = "Logo File Name With Extension")]
        [StringLength(256)]
        public virtual String LogoFileName { get; set; }

        [NotMapped]
        [Display(Name = "Logo File Size")]
        public virtual Int64? LogoFileSize { get; set; }

        [NotMapped]
        public String StartDate { get; set; }

        [NotMapped]
        public String TaskCreatedByUserName { get; set; }

        [NotMapped]
        public int CreatedBefore
        {
            get
            {
                return (int)(DateTime.Now - this.CreateDate).TotalDays;
            }
        }

        [NotMapped]
        public Int32 RatingValue { get; set; }
        [NotMapped]
        public Boolean IsManager { get; set; }

        [NotMapped]
        public String EndDate { get; set; }

        [NotMapped]
        public String UserPhotoFileName { get; set; }
        [NotMapped]
        public int TotalOpenTaskCount { get; set; }
        [NotMapped]
        public int TotalInProgressTaskCount { get; set; }
        [NotMapped]
        public int TotalCompletedTaskCount { get; set; }
        [NotMapped]
        public int TotalOverDueTaskCount { get; set; }
        [NotMapped]
        public List<string> ContactDocumentIds { get; set; }
        [NotMapped]
        public List<string> CompanyDocumentIds { get; set; }
        [NotMapped]
        public List<string> PropertyDocumentIds { get; set; }

        [NotMapped]
        public List<int> AssigntoEmployeeIds { get; set; }

        [NotMapped]
        public List<TaskTagCategory> TagCategoryList { get; set; }

        [NotMapped]
        public List<Int32> TagIdList { get; set; }
        [NotMapped]
        public Int32 UserId { get; set; }

        [NotMapped]
        public Int32 ManagerId { get; set; }

        [NotMapped]
        public List<int> ParticipantsId { get; set; }

        [NotMapped]
        public List<int> SharedUsersId { get; set; }

        [NotMapped]
        public string EmailAddress { get; set; }

        [NotMapped]
        public List<Task> OpenTaskList { get; set; }

        [NotMapped]
        public List<Task> InprogressTaskList { get; set; }

        [NotMapped]
        public List<Task> CompletedTaskList { get; set; }

        [NotMapped]
        public List<Task> OverdueTaskList { get; set; }
        #endregion
    }

    [Table("TaskTeam", Schema = "TSK")]
    public class TaskTeam : EntityBaseModel
    {
        [Required]
        [Display(AutoGenerateField = false)]
        [Key]
        public Int32 TaskTeamId { get; set; }

        [Required(ErrorMessage = "Please select a task")]
        [ForeignKey("Task")]
        public Int32 TaskId { get; set; }

        [Required(ErrorMessage = "Please select an employee")]
        [ForeignKey("User")]
        [Display(Name = "Employee")]
        public Int32 UserId { get; set; }

        //newly added model attribute
        [Display(Name = "Comment")]
        [StringLength(512)]
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Un-Assigned On")]
        public DateTime? UnAssignedDate { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Started On")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Completion Percentage")]
        [DataType("Percent")]
        public Double CompletionPercentage { get; set; }
        //need to see the data type

        [Display(Name = "Completion Info")]
        [StringLength(512)]
        [DataType(DataType.MultilineText)]
        public String CompletionInfo { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Completed On")]
        public DateTime? CompletedDate { get; set; }

        [Display(Name = "Is Current")]
        public bool IsCurrent { get; set; }

        [Display(Name = "Actual Work Hour")]
        public Int32 ActualWorkHour { get; set; }

        [Display(Name = "Is Manager")]
        public bool IsManager { get; set; } //can rate the task only

        [Display(Name = "Is Participant")]
        public bool IsParticipant { get; set; }

        [Display(Name = "Is Shared")]
        public bool IsShared { get; set; }

        public virtual Task Task { get; set; }
        public virtual User User { get; set; }

        #region NotMapped
        [NotMapped]
        public string TaskName { get; set; }
        [NotMapped]
        public string UserName { get; set; }
        [NotMapped]
        public string UserPhotoFileName { get; set; }
        [NotMapped]
        public string UserEmailAddress { get; set; }
        [NotMapped]
        public string UserPhone { get; set; }
        [NotMapped]
        public bool IsCompleted { get; set; }
        [NotMapped]
        public Int32 AssignedByUserId { get; set; }
        [NotMapped]
        public string AssignedByUserPhotoFileName { get; set; }
        [NotMapped]
        public Int32 AssignedToUserId { get; set; }
        [NotMapped]
        public string AssignedToUserPhotoFileName { get; set; }
        #endregion
        
    }

    [Table("TaskDocument", Schema = "TSK")]
    public class TaskDocument : DocumentBaseModel
    {
        [Required]
        [Display(AutoGenerateField = false)]
        [Key]
        public Int32 TaskDocumentId { get; set; }

        [Required]
        [ForeignKey("Task")]
        public Int32 TaskId { get; set; }

        [ForeignKey("CompanyDocument")]
        public Int32? CompanyDocumentId { get; set; }

        [ForeignKey("ContactDocument")]
        public Int32? ContactDocumentId { get; set; }

        public virtual Task Task { get; set; }
        public virtual ContactDocument ContactDocument { get; set; }
        public virtual CompanyDocument CompanyDocument { get; set; }

        #region NotMapped
        [NotMapped]
        public string TaskName { get; set; }
        [NotMapped]
        public string ContactDocumentName { get; set; }
        [NotMapped]
        public string CompanyDocumentName { get; set; }
        [NotMapped]
        public string PropertyDocumentName { get; set; }
        #endregion
        
    }

    [Table("TaskNote", Schema = "TSK")]
    public class TaskNote : NoteBaseModel
    {
        [Required]
        [Display(AutoGenerateField = false)]
        [Key]
        public Int32 TaskNoteId { get; set; }

        [Required(ErrorMessage = "Task Id is required")]
        [ForeignKey("Task")]
        public Int32 TaskId { get; set; }

        public virtual Task Task { get; set; }

        #region NotMapped

        [NotMapped]
        public string TaskName { get; set; }

        [NotMapped]
        public string UserName { get; set; }

        [NotMapped]
        public string PhotoFileName { get; set; }

        [NotMapped]
        public Int32 UserId { get; set; }

        [NotMapped]
        public Int32 CurrentUserId { get; set; }

        #endregion Custom Fields
    }

    [Table("TaskStatusCategory", Schema = "TSK")]
    public class TaskStatusCategory : BaseModel
    {
        [Required(ErrorMessage = "Task Status Id is required")]
        [Display(AutoGenerateField = false)]
        [Key]
        public Int32 TaskStatusCategoryId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        [StringLength(128)]
        public String Name { get; set; }

        [Display(Name = "Color")]
        public String ColorCode { get; set; }
    }

    [Table("TaskTypeCategory", Schema = "TSK")]
    public class TaskTypeCategory : BaseModel
    {
        [Required(ErrorMessage = "Task Type Id is required")]
        [Display(AutoGenerateField = false)]
        [Key]
        public Int32 TaskTypeCategoryId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        [StringLength(128)]
        public String Name { get; set; }
    }

    [Table("TaskTagCategory", Schema = "TSK")]
    public class TaskTagCategory : EntityBaseModel
    {
        [Required(ErrorMessage = "Task Tag Category Id is required")]
        [Display(AutoGenerateField = false)]
        [Key]
        public Int32 TaskTagCategoryId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        [StringLength(128)]
        public String Name { get; set; }

        [ForeignKey("User")]
        [Display(Name = "User Id")]
        public Int32? UserId { get; set; }

        [ForeignKey("Company")]
        [Display(Name = "Company Id")]
        public Int32? CompanyId { get; set; }

        public virtual User User { get; set; }
        public virtual Company Company { get; set; }

        #region NotMapped
        [NotMapped]
        public int CountedTask { get; set; }
        #endregion
        
    }

    [Table("TaskTagMap", Schema = "TSK")]
    public class TaskTagMap : EntityBaseModel
    {
        [Required(ErrorMessage = "Task Tag Map Id is required")]
        [Display(AutoGenerateField = false)]
        [Key]
        public Int32 TaskTagMapId { get; set; }

        [ForeignKey("Task")]
        [Display(Name = "Task Id")]
        public Int32 TaskId { get; set; }

        [ForeignKey("TaskTagCategory")]
        [Display(Name = "TaskTag Category Id")]
        public Int32 TaskTagCategoryId { get; set; }

        [ForeignKey("Contact")]
        [Display(Name = "Contact Id")]
        public Int32? ContactId { get; set; }

        [ForeignKey("Company")]
        [Display(Name = "Company Id")]
        public Int32? CompanyId { get; set; }

        public virtual Task Task { get; set; }
        public virtual TaskTagCategory TaskTagCategory { get; set; }
        public virtual Contact Contact { get; set; }
        public virtual Company Company { get; set; }
    }

    [Table("TaskPriorityCategory", Schema = "TSK")]
    public class TaskPriorityCategory : BaseModel
    {
        [Required]
        [Display(AutoGenerateField = false)]
        [Key]
        public Int32 TaskPriorityCategoryId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        [StringLength(128)]
        public String Name { get; set; }

        #region NotMapped
        [NotMapped]
        public int CountedPriorityTask { get; set; }
        #endregion
    }
    
}