/*2014年6月26日09:31:31*/
(function($) {
    $.fn.e_input_tip = function(options) {
        var defaults = "请输入";
        return this.each(function() {
            var el = $(this);
            var text = options ? options : defaults, tip_text = $(this).attr("tip_text");
            text = tip_text ? tip_text : text;
            el.addClass("col_gray");
            el.val(text);
            el.focusin(function(event) {
                if (el.val() == text) {
                    el.val("").removeClass("col_gray");
                }
            }).focusout(function(event) {
                if (el.val() == "") {
                    el.val(text).addClass("col_gray");
                }
            }).keyup(function(event) {
                if (el.val() == text) {
                    el.addClass("col_gray");
                } else {
                    el.removeClass("col_gray");
                }
            });
        });
    };
})(jQuery);

$(".tip_input").e_input_tip();

$(".multiple").change(function(event) {
    var input = $(this).parents(".input_line").prev(".hide"), vals = input.val(), val = $(this).val();
    if (vals.indexOf(val) > -1) {
        vals = vals.replace(RegExp("^" + val + ",|," + val + "|^" + val + "$", "ig"), "");
    } else {
        vals = vals ? vals + "," + val : val;
    }
    input.val(vals);
});

$("#hotel_province").change(function(event) {
    map.centerAndZoom($(this).find(":selected").text());
    $.ajax({
        url: "/help/location.ashx",
        type: "GET",
        dataType: "json",
        data: {
            type: "city",
            value: $(this).val()
        }
    }).done(function(data) {
        $("#h_city").children().slice(1).remove();
        $("#h_administrative_region").children().slice(1).remove();
        $("#h_business_zone").children().slice(1).remove();
        for (var i = 0; i < data.length; i++) {
            var city = data[i];
            var option = '<option value="' + city.id + '">' + city.name + "</option>";
            $("#h_city").append(option);
        }
    }).fail(function() {}).always(function() {});
});

$("#h_city").change(function(event) {
    map.centerAndZoom($(this).find(":selected").text());
    $.ajax({
        url: "/help/location.ashx",
        type: "GET",
        dataType: "json",
        data: {
            type: "region",
            value: $(this).val()
        }
    }).done(function(data) {
        $("#h_administrative_region").children().slice(1).remove();
        for (var i = 0; i < data.length; i++) {
            var city = data[i];
            var option = '<option value="' + city.id + '">' + city.name + "</option>";
            $("#h_administrative_region").append(option);
        }
    }).fail(function() {}).always(function() {});
    $.ajax({
        url: "/help/location.ashx",
        type: "GET",
        dataType: "json",
        data: {
            type: "commercial",
            value: $(this).val()
        }
    }).done(function(data) {
        $("#h_business_zone").children().slice(1).remove();
        for (var i = 0; i < data.length; i++) {
            var city = data[i];
            var option = '<option value="' + city.id + '">' + city.name + "</option>";
            $("#h_business_zone").append(option);
        }
    }).fail(function() {}).always(function() {});
});

$("#h_administrative_region,#h_business_zone").change(function(event) {
    map.centerAndZoom($(this).find(":selected").text());
});
//# sourceMappingURL=main.map