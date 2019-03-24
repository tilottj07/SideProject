using System;
using System.ComponentModel.DataAnnotations;
using Scheduler.BL.Shared.Models;
using Scheduler.BL.User.Interface.Models;
using static Scheduler.BL.User.Implementation.UserService;

namespace SchedulerApp.Models.User
{
    public class UserEdit
    {
        public UserEdit() { IsAddNew = true; }

        public UserEdit(IUser user)
        {
            if (user != null)
            {
                UserId = user.UserId;
                UserName = user.UserName;
                FirstName = user.FirstName;
                MiddleInitial = user.MiddleInitial;
                LastName = user.LastName;
                PrimaryPhoneNumber = user.PrimaryPhoneNumber;
                BackupPhoneNumber = user.BackupPhoneNumber;
                PrimaryEmail = user.PrimaryEmail;
                BackupEmail = user.BackupEmail;

                IsAddNew = false;
            }
        }

        public bool IsAddNew { get; set; }

        public Guid UserId { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Initial")]
        public string MiddleInitial { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

  
        [Required]
        [Display(Name = "Primary Phone #")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PrimaryPhoneNumber { get; set; }

        [Display(Name = "Backup Phone #")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string BackupPhoneNumber { get; set; }

        [Required]
        [Display(Name = "Primary Email")]
        [EmailAddress]
        public string PrimaryEmail { get; set; }

        [EmailAddress]
        [Display(Name = "Backup Email")]
        public string BackupEmail { get; set; }


        public string ModalTitle
        {
            get
            {
                if (IsAddNew) return "Add User";
                return $"Edit {UserName}";
            }
        }


        public ChangeResult Result { get; set; }
    }
}
