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
            this.key = 1;
            this.value = "ONE";
        }

        public IHttpActionResult Get()
        {
            TestEntity result = new TestEntity();
            result.key = this.key;
            result.value = this.value;

            return this.Ok(result);
        }

        [HttpPost]
        public Task<IHttpActionResult> Action([FromODataUri] int key, ODataActionParameters parameters)
        {
            if (!this.ModelState.IsValid)
            {
                return Task.FromResult<IHttpActionResult>(this.BadRequest());
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

            return Task.FromResult<IHttpActionResult>(this.StatusCode(HttpStatusCode.NoContent));
        }
    }
}
