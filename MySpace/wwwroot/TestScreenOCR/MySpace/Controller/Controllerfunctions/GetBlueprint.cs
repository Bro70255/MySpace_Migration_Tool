        public async Task<IActionResult> GetBlueprint()
        {
            var data = await _dal.GetBlueprintData();
            return Json(data);
        }