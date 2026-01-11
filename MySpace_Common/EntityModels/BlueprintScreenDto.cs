using System;
using System.Collections.Generic;
using System.Text;

namespace MySpace_Common
{
    public class BlueprintScreenDto
    {
        public int ScreenId { get; set; }
        public string ScreenName { get; set; }
        public List<BlueprintJsDto> JsFunctions { get; set; } = new();
    }
}
