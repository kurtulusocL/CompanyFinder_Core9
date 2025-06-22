using CompanyFinder.Core.DataAccess.EntityFramework;
using CompanyFinder.DataAccess.Abstract;
using CompanyFinder.DataAccess.Concrete.EntityFramework.Context;
using CompanyFinder.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyFinder.DataAccess.Concrete.EntityFramework
{
    public class AppointmentDal : EntityRepositoryBase<Appointment, ApplicationDbContext>, IAppointmentDal
    {
        private readonly ApplicationDbContext _context;
        public AppointmentDal(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public int AppointmentCounter()
        {
            try
            {
                return _context.Appointments.Count();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            try
            {
                var appointment = await _context.Set<Appointment>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (appointment == null)
                {
                    return false;
                }
                appointment.IsActive = true;

                var appointmentAnswers = await _context.Set<AppointmentAnswer>().Where(a => a.AppointmentId == id).ToListAsync();
                foreach (var appointmentAnswer in appointmentAnswers)
                {
                    appointmentAnswer.IsActive = true;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Deleted the entity.", ex);
            }
        }

        public async Task<bool> SetApprovedAsync(int id)
        {
            try
            {
                var approved = await _context.Set<Appointment>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (approved != null)
                {
                    approved.IsApproved = true;
                    approved.ApprovedDate = DateTime.Now.ToLocalTime();
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while set Approved the entity.", ex);
            }
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            try
            {
                var appointment = await _context.Set<Appointment>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (appointment == null)
                {
                    return false;
                }

                appointment.IsActive = false;
                appointment.SuspendedDate = DateTime.Now.ToLocalTime();

                var appointmentAnswers = await _context.Set<AppointmentAnswer>().Where(a => a.AppointmentId == id).ToListAsync();
                foreach (var appointmentAnswer in appointmentAnswers)
                {
                    appointmentAnswer.IsActive = false;
                    appointmentAnswer.SuspendedDate = DateTime.Now.ToLocalTime();
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Deleted the entity.", ex);
            }
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            try
            {
                var appointment = await _context.Set<Appointment>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (appointment == null)
                {
                    return false;
                }

                appointment.IsDeleted = true;
                appointment.DeletedDate = DateTime.Now.ToLocalTime();

                var appointmentAnswers = await _context.Set<AppointmentAnswer>().Where(a => a.AppointmentId == id).ToListAsync();
                foreach (var appointmentAnswer in appointmentAnswers)
                {
                    appointmentAnswer.IsDeleted = true;
                    appointmentAnswer.DeletedDate = DateTime.Now.ToLocalTime();
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Deleted the entity.", ex);
            }
        }

        public async Task<bool> SetNotApprovedAsync(int id)
        {
            try
            {
                var approved = await _context.Set<Appointment>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (approved != null)
                {
                    approved.IsApproved = false;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while set NotApproved the entity.", ex);
            }
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            try
            {
                var appointment = await _context.Set<Appointment>().Where(i => i.Id == id).FirstOrDefaultAsync();
                if (appointment == null)
                {
                    return false;
                }

                appointment.IsDeleted = false;

                var appointmentAnswers = await _context.Set<AppointmentAnswer>().Where(a => a.AppointmentId == id).ToListAsync();
                foreach (var appointmentAnswer in appointmentAnswers)
                {
                    appointmentAnswer.IsDeleted = false;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while setting Deleted the entity.", ex);
            }
        }
    }
}
