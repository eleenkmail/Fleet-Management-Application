using Microsoft.AspNetCore.Mvc;
using FPro;
using FMS_DB;
using System.Collections.Concurrent;
using Newtonsoft.Json;


namespace FMA_API.Controllers
{

    
    [ApiController]
    [Route("[controller]")]
    
    public class ValuesController : ControllerBase
    {

        GVAR ResponseGvar = new();
        Driver Driver = new();
        Vehicles Vehicle = new();
        Geofences Geofences = new();
        RouteHistory RouteHistory = new();
        PolygonGeofence PolygonGeofence = new();
        CircularGeofence CircularGeofence = new();
        RectangleGeofence RectangleGeofence = new();
        VehiclesInformations VehiclesInformatioms = new();


    /* ---------------------------------------------------------
                               Vehicle
     ---------------------------------------------------------*/
    [HttpPost("/Vehicle/Add")]
        public IActionResult AddVehicle([FromBody] GVAR RequestGvar)
        {
            ResponseGvar = new();
            ResponseGvar.DicOfDic[Tags.Tags.ToString()] = new ConcurrentDictionary<string, string> { };

            try
            {
                Vehicle.AddVehicle(RequestGvar);
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.STS.ToString()] = "1";
                
            }
            catch (Exception e)
            {
                
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.STS.ToString()] = "0";
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.Msg.ToString()] = e.ToString();

            }
            return Ok(JsonConvert.SerializeObject(ResponseGvar));
        }


        [HttpPost("/Vehicle/Update")]
        public IActionResult UpdateVehicle([FromBody] GVAR RequestGvar)
        {
            ResponseGvar = new();
            ResponseGvar.DicOfDic[Tags.Tags.ToString()] = new ConcurrentDictionary<string, string> { };

            try
            {
                Vehicle.UpdateVehicle(RequestGvar);
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.STS.ToString()] = "1";
               
            }
            catch (Exception e)
            {
                
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.STS.ToString()] = "0";
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.Msg.ToString()] = e.ToString();


            }
            return Ok(JsonConvert.SerializeObject(ResponseGvar));

        }


        [HttpPost("/Vehicle/Delete")]
        public IActionResult DeleteVehicle([FromBody] GVAR RequestGvar)
        {
            ResponseGvar = new();
            ResponseGvar.DicOfDic[Tags.Tags.ToString()] = new ConcurrentDictionary<string, string> { };

            try
            {
                Vehicle.DeleteVehicle(RequestGvar);
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.STS.ToString()] = "1";
               
            }
            catch (Exception e)
            {
                
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.STS.ToString()] = "0";
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.Msg.ToString()] = e.ToString();


            }
            return Ok(JsonConvert.SerializeObject(ResponseGvar));
        }

        [HttpGet("/Vehicle/RetriveAll")]
        public IActionResult RetriveAllVehicle()
        {
            ResponseGvar = new();
            ResponseGvar.DicOfDic[Tags.Tags.ToString()] = new ConcurrentDictionary<string, string> { };

            try
            {
                ResponseGvar = Vehicle.RetriveAllVehicle();
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.STS.ToString()] = "1";

            }
            catch (Exception e)
            {

                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.STS.ToString()] = "0";
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.Msg.ToString()] = e.ToString();


            }
            return Ok(JsonConvert.SerializeObject(ResponseGvar));
        }



        /* ---------------------------------------------------------
                                   Route History
         ---------------------------------------------------------*/



        [HttpGet("/RouteHistory/RetrieveAllWithLastEpoch")]
        public IActionResult RetrieveAllWithLastRouteHistory()
        {

            ResponseGvar.DicOfDic[Tags.Tags.ToString()] = new ConcurrentDictionary<string, string> { };

            try
            {
                ResponseGvar = RouteHistory.RetrieveAllWithLastRouteHistory();
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.STS.ToString()] = "1";
              
            }
            catch (Exception e)
            {
                
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.STS.ToString()] = "0";
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.Msg.ToString()] = e.ToString();


            }


            return Ok(JsonConvert.SerializeObject(ResponseGvar));
        }


        [HttpPost("/RouteHistory/WithEpochRange")]
        public IActionResult RrtrieveVehicleRouteHistoryWithEpochRange([FromBody] GVAR RequestGvar)
        {
            ResponseGvar.DicOfDic[Tags.Tags.ToString()] = new ConcurrentDictionary<string, string> { };

            try
            {
                ResponseGvar = RouteHistory.RrtrieveRouteHistoryWithEpochRange(RequestGvar);
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.STS.ToString()] = "1";
                
            }
            catch (Exception e)
            {
               
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.STS.ToString()] = "0";
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.Msg.ToString()] = e.ToString();


            }
            return Ok(JsonConvert.SerializeObject(ResponseGvar));
        }

        [HttpPost("/RouteHistory/AddRouteHistory")]
        public IActionResult AddRouteHistory([FromBody] GVAR RequestGvar)
        {
            ResponseGvar = new();
            ResponseGvar.DicOfDic[Tags.Tags.ToString()] = new ConcurrentDictionary<string, string> { };

            try
            {
                RouteHistory.AddRouteHistory(RequestGvar);
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.STS.ToString()] = "1";
                
            }
            catch (Exception e)
            {
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.STS.ToString()] = "0";
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.Msg.ToString()] = e.ToString();

            }

            return Ok(JsonConvert.SerializeObject(ResponseGvar));
        }




        /* ---------------------------------------------------------
                                   VehiclesInformatioms
         ---------------------------------------------------------*/

        [HttpPost("/VehicleInformation/Add")]
        public ActionResult AddVehicleInformatiom([FromBody] GVAR RequestGvar)
        {
            ResponseGvar = new();
            ResponseGvar.DicOfDic[Tags.Tags.ToString()] = new ConcurrentDictionary<string, string> { };


            try
            {
                VehiclesInformatioms.AddVehicleInformation(RequestGvar);
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.STS.ToString()] = "1";

            }
            catch(Exception e)
            {
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.STS.ToString()] = "0";
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.Msg.ToString()] = e.ToString();
            }

            return Ok(JsonConvert.SerializeObject(ResponseGvar));
            

        }



        [HttpPost("/VehicleInformation/Update")]
        public IActionResult UpdateVehicleInformatiom([FromBody] GVAR RequestGvar)
        {
            ResponseGvar = new();
            ResponseGvar.DicOfDic[Tags.Tags.ToString()] = new ConcurrentDictionary<string, string> { };

            try
            {
                VehiclesInformatioms.UpdateVehicleInformation(RequestGvar);

                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.STS.ToString()] = "1";

            }
            catch (Exception e)
            {
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.STS.ToString()] = "0";
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.Msg.ToString()] = e.ToString();

            }

            return Ok(JsonConvert.SerializeObject(ResponseGvar));
           

        }


        [HttpPost("/VehicleInformation/Delete")]
        public IActionResult DeleteVehicleInformatiom([FromBody] GVAR RequestGvar)
        {
            ResponseGvar = new();
            ResponseGvar.DicOfDic[Tags.Tags.ToString()] = new ConcurrentDictionary<string, string> { };

            try
            {
                VehiclesInformatioms.DeleteVehicleInformation(RequestGvar);
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.STS.ToString()] = "1";

            }
            catch (Exception e)
            {
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.STS.ToString()] = "0";
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.Msg.ToString()] = e.ToString();

            }

            return Ok(JsonConvert.SerializeObject(ResponseGvar));

        }

        [HttpPost("/VehicleInformation/RrtrieveDetailedInfo")]
        public IActionResult RrtrieveDetailedVehicleInformation([FromBody] GVAR RequestGvar)
        {
            ResponseGvar.DicOfDic[Tags.Tags.ToString()] = new ConcurrentDictionary<string, string> { };

            try
            {
                ResponseGvar = VehiclesInformatioms.RrtrieveDetailedVehicleInformation(RequestGvar);
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.STS.ToString()] = "1";

            }
            catch (Exception e)
            {
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.STS.ToString()] = "0";
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.Msg.ToString()] = e.ToString();

            }

            return Ok(JsonConvert.SerializeObject(ResponseGvar));

           

        }


        [HttpGet("/VehicleInformation/RrtrieveVehicleInfo")]
        public IActionResult RrtrieveVehicleInformation()
        {
            ResponseGvar.DicOfDic[Tags.Tags.ToString()] = new ConcurrentDictionary<string, string> { };

            try
            {
                ResponseGvar = VehiclesInformatioms.RrtrieveVehicleInformation();
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.STS.ToString()] = "1";

            }
            catch (Exception e)
            {
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.STS.ToString()] = "0";
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.Msg.ToString()] = e.ToString();

            }

            return Ok(JsonConvert.SerializeObject(ResponseGvar));



        }

        /* ---------------------------------------------------------
                                   Driver
         ---------------------------------------------------------*/


        [HttpPost("/Driver/Add")]
        public ActionResult AddVDriver([FromBody] GVAR RequestGvar)
        {

            ResponseGvar = new();
            ResponseGvar.DicOfDic[Tags.Tags.ToString()] = new ConcurrentDictionary<string, string> { };

            try
            {
                Driver.AddDriver(RequestGvar);
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.STS.ToString()] = "1";

            }
            catch (Exception e)
            {
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.STS.ToString()] = "0";
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.Msg.ToString()] = e.ToString();

            }

            return Ok(JsonConvert.SerializeObject(ResponseGvar));

        }


        [HttpPost("/Driver/Update")]
        public IActionResult UpdateDriver([FromBody] GVAR RequestGvar)
        {

            ResponseGvar = new();
            ResponseGvar.DicOfDic[Tags.Tags.ToString()] = new ConcurrentDictionary<string, string> { };

            try
            {
                Driver.UpdateDriver(RequestGvar);
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.STS.ToString()] = "1";

            }
            catch(Exception e)
            {
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.STS.ToString()] = "0";
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.Msg.ToString()] = e.ToString();

            }

            return Ok(JsonConvert.SerializeObject(ResponseGvar));

        }


        [HttpPost("/Driver/Delete")]
        public IActionResult DeleteDriver([FromBody] GVAR RequestGvar)
        {

            ResponseGvar = new();
            ResponseGvar.DicOfDic[Tags.Tags.ToString()] = new ConcurrentDictionary<string, string> { };

            try
            {
                Driver.DeleteDriver(RequestGvar);
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.STS.ToString()] = "1";

            }
            catch (Exception e)
            {
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.STS.ToString()] = "0";
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.Msg.ToString()] = e.ToString();

            }

            return Ok(JsonConvert.SerializeObject(ResponseGvar));
 
        }


        [HttpGet("/Driver/RetriveAllDrivers")]
        public IActionResult RetriveAllDrivers()
        {
          
            ResponseGvar.DicOfDic[Tags.Tags.ToString()] = new ConcurrentDictionary<string, string> { };

            try
            {
                ResponseGvar = Driver.RetriveAllDrivers();
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.STS.ToString()] = "1";

            }
            catch (Exception e)
            {
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.STS.ToString()] = "0";
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.Msg.ToString()] = e.ToString();

            }

            return Ok(JsonConvert.SerializeObject(ResponseGvar));

          
        }


        /* ---------------------------------------------------------
                                Geofences
        ---------------------------------------------------------*/

        [HttpGet("/Geofences/RetrieveGeofencesInformation")]
        public IActionResult RetrieveGeofencesInformation()
        {

            ResponseGvar.DicOfDic[Tags.Tags.ToString()] = new ConcurrentDictionary<string, string> { };

            try
            {
                ResponseGvar = Geofences.RetrieveGeofencesInformation();
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.STS.ToString()] = "1";

            }
            catch (Exception e)
            {
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.STS.ToString()] = "0";
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.Msg.ToString()] = e.ToString();

            }

            return Ok(JsonConvert.SerializeObject(ResponseGvar));

        }

        [HttpGet("/Geofences/RetrieveCircleGeofencesCoordinates")]
        public IActionResult RetrieveCircleGeofencesInformation()
        {

            ResponseGvar.DicOfDic[Tags.Tags.ToString()] = new ConcurrentDictionary<string, string> { };

            try
            {
                ResponseGvar = CircularGeofence.RetrieveCircularGeofencesCoordinates();
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.STS.ToString()] = "1";

            }
            catch (Exception e)
            {
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.STS.ToString()] = "0";
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.Msg.ToString()] = e.ToString();

            }

            return Ok(JsonConvert.SerializeObject(ResponseGvar));

        }


        [HttpGet("/Geofences/RetrieveRectangleGeofencesCoordinates")]
        public IActionResult RetrieveRectangleGeofencesCoordinates()
        {

            ResponseGvar.DicOfDic[Tags.Tags.ToString()] = new ConcurrentDictionary<string, string> { };

            try
            {
                ResponseGvar = RectangleGeofence.RetrieveRectangleGeofencesCoordinates();
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.STS.ToString()] = "1";

            }
            catch (Exception e)
            {
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.STS.ToString()] = "0";
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.Msg.ToString()] = e.ToString();

            }

            return Ok(JsonConvert.SerializeObject(ResponseGvar));

        }

        [HttpGet("/Geofences/RetrievePolygonGeofencesCoordinates")]
        public IActionResult RetrievePolygonGeofencesCoordinates()
        {

            ResponseGvar.DicOfDic[Tags.Tags.ToString()] = new ConcurrentDictionary<string, string> { };

            try
            {
                ResponseGvar = PolygonGeofence.RetrievePolygonGeofencesCoordinates();
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.STS.ToString()] = "1";

            }
            catch (Exception e)
            {
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.STS.ToString()] = "0";
                ResponseGvar.DicOfDic[Tags.Tags.ToString()][Tags.Msg.ToString()] = e.ToString();

            }

            return Ok(JsonConvert.SerializeObject(ResponseGvar));

        }



    }
}
