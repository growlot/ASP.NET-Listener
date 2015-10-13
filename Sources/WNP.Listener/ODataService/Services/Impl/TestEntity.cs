namespace AMSLLC.Listener.ODataService.Services.Impl
{
    using System.ComponentModel.DataAnnotations;

    public class TestEntity
    {
        [Key]
        public int key { get; set; }

        public string value { get; set; }
    }
}