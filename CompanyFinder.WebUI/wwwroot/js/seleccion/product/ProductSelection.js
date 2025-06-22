$(function () {
    $.ajaxSetup({
        type: 'POST',
        url: '/CompanyOperation/ProductCategorySelectSystem/',
        dataType: 'JSON'
    });
    $.extend({
        getProductCategories: function () {
            $.ajax({
                data: { "tip": "getProductCategories" },
                success: function (result) {
                    if (result.ok) {
                        $.each(result.text, function (ProductCategorySubcategory, item) {
                            var optionhtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#productCategory").append(optionhtml);
                        });

                    } else {
                        $.each(result.text, function (ProductCategorySubcategory, item) {
                            var optionhtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#productCategory").append(optionhtml);
                        });
                    }
                }
            });
        },
        getProductSubcategories: function (productCategoryId) {
            $.ajax({
                data: { "productCategoryId": productCategoryId, "tip": "getProductSubcategories" },
                success: function (result) {
                    $("#productSubcategory option").remove();
                    if (result.ok) {
                        $("#productSubcategory").prop("disabled", false);
                        $.each(result.text, function (index, item) {
                            var optionhtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#productSubcategory").append(optionhtml);
                        });

                    } else {
                        $.each(result.text, function (index, item) {
                            var optionhtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#productSubcategory").append(optionhtml);
                        });
                    }
                }
            });
        }
    });
    $.getProductCategories();
    $("#productCategory").on("change", function () {
        var productCategoryId = $(this).val();
        $.getProductSubcategories(productCategoryId);
    });
})