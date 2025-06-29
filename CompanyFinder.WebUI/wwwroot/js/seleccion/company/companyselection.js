$(function () {
    $.ajaxSetup({
        type: 'POST',
        url: '/CompanyOperation/CategorySelectSystem/',
        dataType: 'JSON'
    });

    $.extend({
        getCompanyCategories: function () {
            $.ajax({
                data: { "tip": "getCompanyCategories" },
                success: function (result) {
                    if (result.ok) {
                        $.each(result.text, function (CompanyCategorySubcategory, item) {
                            var optionHtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#companyCategory").append(optionHtml);
                        });
                    } else {
                        $.each(result.text, function (CompanyCategorySubcategory, item) {
                            var optionHtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#companyCategory").append(optionHtml);
                        });
                    }
                }
            });
        },
        getCompanySubcategories: function (companyCategoryId) {
            $.ajax({
                data: { "companyCategoryId": companyCategoryId, "tip": "getCompanySubcategories" },
                success: function (result) {
                    $("#companySubcategory option").remove();
                    if (result.ok) {
                        $("#companySubcategory").prop("disabled", false);
                        $.each(result.text, function (index, item) {
                            var optionHtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#companySubcategory").append(optionHtml);
                        });
                    } else {
                        $.each(result.text, function (index, item) {
                            var optionHtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#companySubcategory").append(optionHtml);
                        });
                    }
                }
            });
        }
    });
    $.getCompanyCategories();
    $("#companyCategory").on("change", function () {
        var companyCategoryId = $(this).val();
        $.getCompanySubcategories(companyCategoryId);
    });
});