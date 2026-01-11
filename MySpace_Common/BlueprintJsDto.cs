using System;
using System.Collections.Generic;
using System.Text;

namespace MySpace_Common
{
    public class BlueprintJsDto
    {
        public int JsFunctionId { get; set; }
        public string JsFunctionName { get; set; }
        public List<BlueprintControllerDto> Controllers { get; set; } = new();
    }
}
