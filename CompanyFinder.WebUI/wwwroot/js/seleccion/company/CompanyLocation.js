$(function () {
    $.ajaxSetup({
        type: 'POST',
        url: '/CompanyOperation/LocationSelectSystem/',
        dataType: 'JSON'
    });

    $.extend({
        getCompanyCountries: function () {
            $.ajax({
                data: { "tip": "getCompanyCountries" },
                success: function (result) {
                    if (result.ok) {
                        $.each(result.text, function (CompanyCountryCity, item) {
                            var optionHtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#companyCountry").append(optionHtml);
                        });
                    } else {
                        $.each(result.text, function (CompanyCountryCity, item) {
                            var optionHtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#companyCountry").append(optionHtml);
                        });
                    }
                }
            });
        },
        getCompanyCities: function (countryId) {
            $.ajax({
                data: { "countryId": countryId, "tip": "getCompanyCities" },
                success: function (result) {
                    $("#companyCity option").remove();
                    if (result.ok) {
                        $("#companyCity").prop("disabled", false);
                        $.each(result.text, function (index, item) {
                            var optionHtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#companyCity").append(optionHtml);
                        });
                    } else {
                        $.each(result.text, function (index, item) {
                            var optionHtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#companyCity").append(optionHtml);
                        });
                    }
                }
            });
        }
    });

    $.getCompanyCountries();
    $("#companyCountry").on("change", function () {
        var countryId = $(this).val();
        $.getCompanyCities(countryId);
    });
});