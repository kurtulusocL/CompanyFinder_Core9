$(function () {
    $.ajaxSetup({
        type: 'POST',
        url: '/CompanyOperation/CompanyCategorySelectSystem/',
        dataType: 'JSON'
    });
    $.extend({
        getCompanyCategories: function () {
            $.ajax({
                data: { "tip": "getCompanyCategories" },
                success: function (result) {
                    if (result.ok) {
                        $.each(result.text, function (CompanyCategorySubcategory, item) {
                            var optionhtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#companyCategory").append(optionhtml);
                        });

                    } else {
                        $.each(result.text, function (CompanyCategorySubcategory, item) {
                            var optionhtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#companyCategory").append(optionhtml);
                        });
                    }
                }
            });
        },
        getCompanySubcategories: function (categoryId) {
            $.ajax({
                data: { "categoryId": categoryId, "tip": "getCompanySubcategories" },
                timeout: 10000,
                success: function (result) {
                    $("#companySubcategory option").remove();
                    if (result.ok) {
                        $("#companySubcategory").prop("disabled", false);
                        $.each(result.text, function (index, item) {
                            var optionhtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#companySubcategory").append(optionhtml);
                        });

                    }
                    else {
                        $.each(result.text, function (index, item) {
                            var optionhtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#companySubcategory").append(optionhtml);
                        });
                    };
                }
            });
        }
    });
    $.getCompanyCategories();
    $("#companyCategory").on("change", function () {
        var categoryId = $(this).val();
        $.getCompanySubcategories(categoryId);
    });
})


$(function () {
    $.ajaxSetup({
        type: 'POST',
        url: '/CompanyOperation/CompanyLocationSelectSystem/',
        dataType: 'JSON'
    });
    $.extend({
        getCompanyCountries: function () {
            $.ajax({
                data: { "tip": "getCompanyCountries" },
                success: function (result) {
                    if (result.ok) {
                        $.each(result.text, function (companyCountry, item) {
                            var optionhtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#companyCountry").append(optionhtml);
                        });

                    } else {
                        $.each(result.text, function (companyCountry, item) {
                            var optionhtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#companyCountry").append(optionhtml);
                        });
                    }
                }
            });
        },
        getCompanyCities: function (countryId) {
            $.ajax({
                data: { "countryId": countryId, "tip": "getCompanyCities" },
                timeout: 10000,
                success: function (result) {
                    $("#companyCity option").remove();
                    if (result.ok) {
                        $("#companyCity").prop("disabled", false);
                        $.each(result.text, function (index, item) {
                            var optionhtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#companyCity").append(optionhtml);
                        });

                    } else {
                        $.each(result.text, function (index, item) {
                            var optionhtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#companyCity").append(optionhtml);
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
})