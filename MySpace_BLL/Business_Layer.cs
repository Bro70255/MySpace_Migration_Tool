using MySpace_Common;
using MySpace_DAL;

namespace MySpace_BLL
{
    public class Business_Layer
    {
        private readonly Data_Layer _dal;

        public Business_Layer(Data_Layer dal)
        {
            _dal = dal;
        }

        public async Task<Employee?> Sign_InAsync(int employeeCode, string password)
        {
            return await _dal.Sign_InAsync(employeeCode, password);
        }

        public async Task<bool> Save_Registration_Form(Registration model)
        {
            return await _dal.Save_Registration_Form(model);
        }

        public async Task<List<Registration>> Get_Registration_Report_Details()
        {
            return await _dal.Get_Registration_Report_Details();
        }


    }
}
