$(function () {
    $.ajaxSetup({
        type: 'POST',
        url: '/CompanyOperation/CompanySelectSystem/',
        dataType: 'JSON'
    });
    $.extend({
        GetCountry: function () {
            $.ajax({
                data: { "tip": "GetCountry" },
                success: function (sonuc) {
                    if (sonuc.ok) {
                        $.each(sonuc.text, function (CountryCity, item) {
                            var optionhtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#country").append(optionhtml);
                        });

                    } else {
                        $.each(sonuc.text, function (CountryCity, item) {
                            var optionhtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#country").append(optionhtml);

                        });
                    }
                }
            });
        },
        GetCity: function (countryId) {
            $.ajax({
                data: { "countryId": countryId, "tip": "GetCity" },
                success: function (sonuc) {
                    $("#city option").remove();
                    if (sonuc.ok) {
                        $("#city").prop("disabled", false);
                        $.each(sonuc.text, function (index, item) {
                            var optionhtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#city").append(optionhtml);
                        });

                    } else {
                        $.each(sonuc.text, function (index, item) {
                            var optionhtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#city").append(optionhtml);
                        });
                    }
                }
            });
        }
    });
    $.GetCountry();
    $("#country").on("change", function () {
        var countryId = $(this).val();
        $.GetCity(countryId);
    });
});

$(function () {
    $.ajaxSetup({
        type: 'POST',
        url: '/CompanyOperation/CompanySelectSystem/',
        dataType: 'JSON'
    });
    $.extend({
        GetCategory: function () {
            $.ajax({
                data: { "tip": "GetCategory" },
                success: function (result) {
                    if (result.ok) {
                        $.each(result.text, function (CategorySubcategory, item) {
                            var optionhtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#category").append(optionhtml);
                        });

                    } else {
                        $.each(result.text, function (CategorySubcategory, item) {
                            var optionhtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#category").append(optionhtml);
                        });
                    }
                }
            });
        },
        GetSubcategory: function (categoryId) {
            $.ajax({
                data: { "categoryId": categoryId, "tip": "GetSubcategory" },
                success: function (result) {
                    $("#subcategory option").remove();
                    if (result.ok) {
                        $("#subcategory").prop("disabled", false);
                        $.each(result.text, function (index, item) {
                            var optionhtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#subcategory").append(optionhtml);
                        });

                    } else {
                        $.each(result.text, function (index, item) {
                            var optionhtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#subcategory").append(optionhtml);
                        });
                    }
                }
            });
        }
    });
    $.GetCategory();
    $("#category").on("change", function () {
        var categoryId = $(this).val();
        $.GetSubcategory(categoryId);
    });
});