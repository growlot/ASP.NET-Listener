namespace AMSLLC.Listener.ODataService.Controllers
{
    using Services.Impl;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.OData;

    public class TestEntitiesController : ODataController
    {
        private int key;
        private string value;

        public TestEntitiesController()
        {
            key = 1;
            value = "ONE";
        }

        public IHttpActionResult Get()
        {
            TestEntity result = new TestEntity();
            result.key = key;
            result.value = value;

            return Ok(result);
        }

        [HttpPost]
        public Task<IHttpActionResult> Action([FromODataUri] int key, ODataActionParameters parameters)
        {
            if (!ModelState.IsValid)
            {
                return Task.FromResult<IHttpActionResult>(BadRequest());
            }

            string value = null;
            if (parameters != null && parameters.ContainsKey("Value"))
            {
                value = (string)parameters["Value"];
            }

            if (!string.IsNullOrWhiteSpace(value))
            {
                this.value = value;
            }
            else
            {
                value = "ONE";
            }

            return Task.FromResult<IHttpActionResult>(StatusCode(HttpStatusCode.NoContent));
        }
    }
}
