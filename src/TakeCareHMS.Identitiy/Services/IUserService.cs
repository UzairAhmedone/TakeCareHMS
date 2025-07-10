namespace TakeCareHMS.Identitiy.Services
{
    public interface IUserService
    {
        Task<OperationResults> RegisterDoctor(DoctorSignUpRequest request);
        Task<OperationResults> RegisterNurse(NurseSignUpRequest request);
        Task<OperationResults> RegisterPharmacist(PharmacistSignUpRequest request);
        Task<OperationResults> RegisterLabTechnician(LabTechnicianSignUpRequest request);
        Task<OperationResults> Signup(SignupRequest request);
        Task<OperationResults> Signin(SigninRequest request);
    }
}