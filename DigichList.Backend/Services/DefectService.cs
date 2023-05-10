using DigichList.Backend.Enums;
using DigichList.Backend.Interfaces;
using DigichList.Backend.ViewModel;
using DigichList.Core.Entities;
using DigichList.Core.Repositories;
using DigichList.TelegramNotifications.BotNotifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigichList.Backend.Services
{
    /// <summary>
    /// The dedicated service for working with defects.
    /// </summary>
    public class DefectService : IDefectService
    {
        readonly IDefectRepository _defectRepository;
        readonly IUserRepository _userRepository;
        readonly IBotNotificationSender _botNotificationSender;

        public DefectService(
            IDefectRepository repo,
            IUserRepository userRepository,
            IBotNotificationSender botNotificationSender)
        {
            _defectRepository = repo;
            _userRepository = userRepository;
            _botNotificationSender = botNotificationSender;
        }

        /// <inheritdoc />
        public List<DefectViewModel> GetAll()
        {
            var result = _defectRepository.GetAll();
            var mapped = new List<DefectViewModel>();

            foreach(var d in result)
            {
                mapped.Add(DefectViewModel.ToViewModel(d));
            }

            return mapped;
        }

        /// <inheritdoc />
        public async Task<DefectViewModel> GetByIdAsync(int id)
        {
            var result = await _defectRepository.GetByIdAsync(id);
            var mapped = DefectViewModel.ToViewModel(result);

            return mapped;
        }

        /// <inheritdoc />
        public async Task UpdateAsync(DefectViewModel model)
        {
            var defect = await _defectRepository.GetByIdAsync(model.Id);

            defect.Description = model.Description;
            defect.RoomNumber = model.RoomNumber;
            defect.Status = (int)Enum.Parse(typeof(DefectStatus), model.Status);

            var worker = await _userRepository.GetByIdAsync(model.AssigneeId.Value);

            if (worker != null)
            {
                defect.AssignedWorker = worker;
            }

            await _defectRepository.UpdateAsync(defect);
        }

        /// <inheritdoc />
        public async Task<(bool, string)> AssignAsync(int userId, int defectId)
        {
            var user = await _userRepository.GetUserWithRoleAsync(userId);
            var defect = await _defectRepository.GetByIdAsync(defectId);

            if (user is null)
            {
                return (false, "User not found");
            }

            if (defect is null)
            {
                return (false, "Defect not found");
            }

            if (defect.AssignedWorker != null)
            {
                return (false, "The defect is already assigned");
            }

            if (user?.Role?.Name != "Technician") // TODO: move this value in higher level.
            {
                return (false, "This user cannot fix defects");
            }

            // Update defect.
            defect.AssignedWorker = user;
            await _defectRepository.UpdateAsync(defect);

            //Notify the user.
            await _botNotificationSender.NotifyUserWasGivenWithDefect(defect.AssignedWorker.ChatId, defect);

            return (true, "");
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(int id)
        {
            var defect = await _defectRepository.GetByIdAsync(id);

            if(defect == null)
            {
                return false;
            }

            await _defectRepository.DeleteAsync(defect);
            return true;
        }

        /// <inheritdoc />
        public async Task DeleteRangeAsync(int[] ids) => await _defectRepository.DeleteRangeAsync(ids);
    }
}
