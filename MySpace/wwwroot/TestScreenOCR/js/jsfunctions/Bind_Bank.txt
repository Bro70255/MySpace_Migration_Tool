function Bind_Bank(dropdownId, UnitDropdownId) {
    var selectElement = document.getElementById(UnitDropdownId);
    var unit = selectElement.options[selectElement.selectedIndex].value;

    if (unit) { // Proceed only if a unit is selected
        $.ajax({
            url: "/Home/Get_Bind_Bank",
            type: "GET",
            data: { unit: unit }, // Pass the unit in query string or as data
            dataType: 'json',
            success: function (response) {
                var dropdown = document.getElementById(dropdownId);
                dropdown.length = 0; // Clear the dropdown

                if (response.length > 0) {
                    var opt = document.createElement('option');
                    opt.text = 'Select Bank';
                    opt.value = ''; // Set the default option
                    dropdown.options.add(opt);

                    // Populate the dropdown with bank details
                    response.forEach(function (bank) {
                        opt = document.createElement('option');
                        opt.text = bank.BANK;  // Assuming 'BANK' is the key in the returned data
                        opt.value = bank.BANK;
                        dropdown.options.add(opt);
                    });
                } else {
                    var opt = document.createElement('option');
                    opt.text = 'No Banks Available';
                    opt.value = '';
                    dropdown.options.add(opt);
                }
            },
            error: function () {
                alert('Error while fetching bank details.');
            }
        });
    } else {
        alert('Please select a unit.');
    }
}