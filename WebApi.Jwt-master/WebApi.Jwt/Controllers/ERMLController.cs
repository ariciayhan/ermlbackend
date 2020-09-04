using System;
using System.Web.Http;
using Iaea.SG.EQUIS.Common.BusinessEntities.ViewModel.AssetManagement.Receive;
using Iaea.SG.EQUIS.Frontend.Web.JWT.Filters;

namespace Iaea.SG.EQUIS.Frontend.Web.API.m
{
    public class ERMLController : ApiController
    {
        [JwtAuthentication]
        [Authorize]
        [HttpPost]
        public MeasurementSaveReponseViewModel SaveMeasurement(MeasurementInputModel inputModel)
        {
            try
            {                

                //var validator = new MeasurementInputValidator();
                //var results = validator.Validate(inputModel);

                if (true)//(results.IsValid)
                {
                    //_receiveDataAccess.SaveMeasurement(inputModel);
                    return new MeasurementSaveReponseViewModel() { Success = true, Message = "Measurement saved." };

                }
                else
                {
                    return new MeasurementSaveReponseViewModel() { Success = false, Message = "Please correct the inputs", ValidationMessages = new string[0] };
                        //results.Errors.Select(x => x.ErrorMessage).ToArray() };
                }

            }
            catch (Exception ex)
            {
                //Logger.Log(LogLevel.Error, "ERML Measurement could not be saved", ex);
                return new MeasurementSaveReponseViewModel() { Success = false, Message = "Measurement could not be saved. Due to server error." };
            }
        }
    }
}
